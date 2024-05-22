using AutoMapper;
using CloudAPI.ApplicationCore.DTO.Import;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CloudAPI.ApplicationCore.Commands.Items
{
    public class ImportItemsCommand : IRequest<List<ItemImportReturnDTO>>, IImportCommand<ItemImportDTO>
    {
        public List<ItemImportDTO> InputList { get; set; }
        public string Filename { get; set; }
    }

    public class ImportItemsHandler : IRequestHandler<ImportItemsCommand, List<ItemImportReturnDTO>>
    {
        private readonly ILogger<ImportItemsHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public ImportItemsHandler(MainDbContext context, ILogger<ImportItemsHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;

            if (!_context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                _context.Database.SetCommandTimeout(180);
            }
        }
        delegate List<TEntity> getDbDelegate<TEntity>(List<string> ids);

        public async Task<List<ItemImportReturnDTO>> Handle(ImportItemsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportItemsHandler called");
            var impItems = _mapper.Map<List<ItemImportReturnDTO>>(request.InputList); // Create room for ErrorMessage
            var invalids = impItems.Where(r => (string.IsNullOrWhiteSpace(r.Description) || r.Description.Length > 250) ||
                                                (string.IsNullOrWhiteSpace(r.ItemTypeDescription) || r.ItemTypeDescription.Length > 250) ||
                                                (string.IsNullOrWhiteSpace(r.Tagnumber) || r.Tagnumber.Length > 40) || 
                                                r.ExternalReferenceID.Length > 40)
                                        .Select(r => { r.ErrorMessage = "Field restrictions"; return r; })
                                        .ToList();
            var validImports = impItems.Except(invalids).ToList();

            // Check double Descriptions in import list
            var doubleDescs = validImports.GroupBy(r => r.Description).Where(g => g.Count() > 1);
            foreach (var dbl in doubleDescs)
            {
                var extraImports = dbl.Take(dbl.Count() - 1).Select(r => { r.ErrorMessage = "Description not unique in import"; return r; }).ToList();
                invalids.AddRange(extraImports);
                validImports = validImports.Except(extraImports).ToList();
            }

            await MapStringsToDatabaseUsersAndItemType(invalids, validImports);

            var (dbExisting, totalChanged, totalUnchanged) = await HandleExistingItems(invalids, validImports);
            var newItemsRetDTO = await HandleNewRows(validImports, dbExisting);

            await _context.SaveChangesAsync();

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_importedItems),
                ("fileName", request.Filename), 
                ("newItems", newItemsRetDTO.Count().ToString()), 
                ("changedItems", totalChanged.ToString()), 
                ("unchangedItems", totalUnchanged.ToString()),
                ("invalidItems", invalids.Count().ToString())); 
            return invalids.Distinct().ToList();
        }

        private async Task MapStringsToDatabaseUsersAndItemType(List<ItemImportReturnDTO> invalids, List<ItemImportReturnDTO> validImports)
        {
            // Load all users and item types at once
            var allUsers = await _context.CTAMUser().ToListAsync();
            var allItemTypes = await _context.ItemType().ToListAsync();

            // Use dictionaries or HashSets for fast lookup
            var userDictionary = allUsers.ToDictionary(u => u.UID, StringComparer.OrdinalIgnoreCase);
            var itemTypeDictionary = allItemTypes.ToDictionary(it => it.Description, StringComparer.OrdinalIgnoreCase);

            var toAddInvalids = new List<ItemImportReturnDTO>();
            var newValidImports = new List<ItemImportReturnDTO>();

            foreach (var imp in validImports)
            {
                var isInvalid = false;

                // Look up users and item types in-memory
                if (!string.IsNullOrWhiteSpace(imp.CTAMUserPersonalItemString))
                {
                    userDictionary.TryGetValue(imp.CTAMUserPersonalItemString, out var personalUser);
                    imp.CTAMUserForCreatingPersonalItem = personalUser;
                    if (personalUser == null)
                    {
                        imp.ErrorMessage += $"CTAMUserPersonalItem user niet gevonden: {imp.CTAMUserPersonalItemString}\n";
                        invalids.Add(imp);
                    }
                }

                if (!string.IsNullOrWhiteSpace(imp.CTAMUserInPossessionString))
                {
                    userDictionary.TryGetValue(imp.CTAMUserInPossessionString, out var possessionUser);
                    imp.CTAMUserForCreatingPossession = possessionUser;
                    if (possessionUser == null)
                    {
                        imp.ErrorMessage += $"CTAMUserInPosession user niet gevonden: {imp.CTAMUserInPossessionString}\n";
                        invalids.Add(imp);
                    }
                }

                itemTypeDictionary.TryGetValue(imp.ItemTypeDescription, out var itemType);
                imp.ItemType = itemType;
                if (itemType == null)
                {
                    imp.ErrorMessage += $"ItemTypeDescription niet gevonden: {imp.ItemTypeDescription} \n";
                    toAddInvalids.Add(imp);
                    isInvalid = true;
                }

                if (!isInvalid)
                {
                    newValidImports.Add(imp);
                }
            }

            invalids.AddRange(toAddInvalids);
            validImports.Clear();
            validImports.AddRange(newValidImports); // Replace the original list with the new one

        }

        private class Result
        {
            public Item I { get; set; }
            public CTAMUserInPossession Uip { get; set; }
            public CTAMUserPersonalItem Pers { get; set; }
            public CTAMUserPersonalItem Repl { get; set; }
            public bool IsInCabinet { get; set; }
        }

        private async Task<(List<Item>, int, int)> HandleExistingItems(List<ItemImportReturnDTO> invalids, List<ItemImportReturnDTO> validImports)
        {
            // Find already existing Items in database
            var uniqueDescs = validImports.Select(r => r.Description).ToList();

            var dbExisting = new List<Result>();
            int totalChanged = 0, totalUnchanged = 0;

            foreach (var chunk in uniqueDescs.Partition(15000))
            {
                var itemsInChunk = await (from i in _context.Item().Where(i => chunk.Contains(i.Description))
                                        join uip in _context.CTAMUserInPossession().Where(uip => uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified)
                                        on i.ID equals uip.ItemID into guip
                                        from subuip in guip.DefaultIfEmpty()
                                        join pers in _context.CTAMUserPersonalItem()
                                        on i.ID equals pers.ItemID into gpers
                                        from subpers in gpers.DefaultIfEmpty()
                                        join persrepl in _context.CTAMUserPersonalItem()
                                        on i.ID equals persrepl.ReplacementItemID into gpersrepl
                                        from subpersrepl in gpersrepl.DefaultIfEmpty()
                                        join cabcontent in _context.CabinetPositionContent()
                                        on i.ID equals cabcontent.ItemID into gcabcontent
                                        from subcabcontent in gcabcontent.DefaultIfEmpty()
                                        select new Result { I = i, Uip = subuip, Pers = subpers, Repl = subpersrepl, IsInCabinet = (subcabcontent != null) })
                                    .ToListAsync();
                dbExisting.AddRange(itemsInChunk);
            }

            // For instance CTAMPersonalItemX points to itemA with (after swap) or without replacement itemB
            // Import:
            // ItemA has pers = CTAMPersonalItemX, repl = null (ID is not mentioned in any ReplacementItemID column)
            // ItemB before swap has pers = null, repl = null (ID is not mentioned in any ReplacementItemID column)
            // ItemB after swap has pers = null, repl = CTAMPersonalItemX
            foreach (var dbi in dbExisting)
            {
                if (dbi.IsInCabinet) // Import is outdated, skip
                {
                    totalUnchanged++;
                    continue;
                }

                bool changed = false;
                var imp = validImports.Where(i => i.Description.Equals(dbi.I.Description)).FirstOrDefault();
                changed = changed || dbi.I.Tagnumber != imp.Tagnumber;
                dbi.I.Tagnumber = imp.Tagnumber;
                changed = changed || dbi.I.ExternalReferenceID != imp.ExternalReferenceID;
                dbi.I.ExternalReferenceID = imp.ExternalReferenceID;

                if (dbi.I.ItemTypeID != imp.ItemType.ID)
                {
                    imp.ErrorMessage += $"ItemType is anders dan in database, alleen ItemType wordt niet gewijzigd";
                    invalids.Add(imp); // but continue with rest of import
                }

                // If it is in possession of someone and the owner changed or will be set to null, end the existing ownership
                // If this is a personal item it should not have a replacement item, CMDB reports it in possession of agent, but we know TLB'r might have it unless it will be set to null
                // If this item is a replacement of a personal item, don't touch it at all, CMDB says its personal, but it isn't
                if (dbi.Uip != null && 
                    ((imp.CTAMUserForCreatingPossession != null && dbi.Uip.CTAMUserUIDOut != imp.CTAMUserForCreatingPossession.UID) || (imp.CTAMUserForCreatingPossession == null)) &&
                    ((dbi.Pers == null || dbi.Pers.ReplacementItemID == null) && dbi.Repl == null))
                {
                    changed = true;
                    // Close current possession
                    dbi.Uip.CabinetNameIn = "import";
                    dbi.Uip.InDT = DateTime.UtcNow;
                    dbi.Uip.Status = UserInPossessionStatus.Returned;
                }

                if (imp.CTAMUserForCreatingPossession != null && 
                    (dbi.Uip == null || (dbi.Uip != null && dbi.Uip.CTAMUserUIDOut != imp.CTAMUserForCreatingPossession.UID)) &&
                    ((dbi.Pers == null || dbi.Pers.ReplacementItemID == null) && dbi.Repl == null)) // CMDB reports it in possession of agent, but we know TLB'r has it, don't change ownership
                {
                    changed = true;
                    var now = DateTime.UtcNow;
                    var newUip = new CTAMUserInPossession
                    {
                        CabinetNameOut = "import",
                        ItemID = dbi.I.ID,
                        Status = UserInPossessionStatus.Picked,
                        CTAMUserUIDOut = imp.CTAMUserForCreatingPossession.UID,
                        CTAMUserNameOut = imp.CTAMUserForCreatingPossession.Name,
                        CTAMUserEmailOut = imp.CTAMUserForCreatingPossession.Email,
                        OutDT = now,
                        CreatedDT = now
                    };
                    await _context.CTAMUserInPossession().AddAsync(newUip);
                }

                // If this item is referenced as personal item and the personal item record has a replacement we don't want to remove the personal item record (it is probably now in
                // possession of TLB'r)
                if (dbi.Pers != null && dbi.Pers.ReplacementItemID == null && (dbi.Pers.CTAMUserUID != imp.CTAMUserForCreatingPersonalItem?.UID || imp.CTAMUserForCreatingPersonalItem == null))
                {
                    changed = true;
                    _context.CTAMUserPersonalItem().Remove(dbi.Pers);
                    if (imp.CTAMUserForCreatingPersonalItem == null)
                    {
                        dbi.I.Status = ItemStatus.NOT_ACTIVE;
                    }
                }

                if ((dbi.Pers == null || dbi.Pers.CTAMUserUID != imp.CTAMUserForCreatingPersonalItem?.UID) && 
                    imp.CTAMUserForCreatingPersonalItem != null &&
                    dbi.Repl == null) // CMDB reports this as a personal item, but it is the replacement, don't add
                {
                    changed = true;
                    var newPers = new CTAMUserPersonalItem
                    {
                        CTAMUserUID = imp.CTAMUserForCreatingPersonalItem.UID,
                        ItemID = dbi.I.ID,
                        Status = UserPersonalItemStatus.OK,
                        CreateDT = DateTime.UtcNow
                    };
                    await _context.CTAMUserPersonalItem().AddAsync(newPers);
                    if (dbi.I.Status == ItemStatus.NOT_ACTIVE || dbi.I.Status == ItemStatus.INITIAL)
                    {
                        dbi.I.Status = ItemStatus.ACTIVE;
                    }
                }

                if (changed)
                {
                    totalChanged++;
                }
                else
                {
                    totalUnchanged++;
                }
            }

            return (dbExisting.Select(x => x.I).ToList(), totalChanged, totalUnchanged);
        }

    private async Task<IEnumerable<ItemImportReturnDTO>> HandleNewRows(List<ItemImportReturnDTO> validImports, List<Item> dbExisting)
        {
            var dbExistingDescs = dbExisting.Select(i => i.Description).ToList();
            var newItemsRetDTO = validImports.Where(imp => !dbExistingDescs.Contains(imp.Description));
            var newItems = _mapper.Map<List<Item>>(newItemsRetDTO);
            newItems.ForEach(i => i.Status = ItemStatus.INITIAL);
            await _context.Item().AddRangeAsync(newItems);

            foreach (var dto in newItemsRetDTO)
            {
                var db = newItems.Where(i => i.Description.Equals(dto.Description)).Single();

                if (dto.CTAMUserForCreatingPossession != null)
                {
                    var now = DateTime.UtcNow;
                    var newUip = new CTAMUserInPossession
                    {
                        CabinetNameOut = "import",
                        Item = db,
                        Status = UserInPossessionStatus.Picked,
                        CTAMUserUIDOut = dto.CTAMUserForCreatingPossession.UID,
                        CTAMUserNameOut = dto.CTAMUserForCreatingPossession.Name,
                        CTAMUserEmailOut = dto.CTAMUserForCreatingPossession.Email,
                        OutDT = now,
                        CreatedDT = now
                    };
                    db.Status = ItemStatus.ACTIVE;
                    await _context.CTAMUserInPossession().AddAsync(newUip);
                }

                if (dto.CTAMUserForCreatingPersonalItem != null)
                {
                    var newPers = new CTAMUserPersonalItem
                    {
                        CTAMUserUID = dto.CTAMUserForCreatingPersonalItem.UID,
                        Item = db,
                        Status = UserPersonalItemStatus.OK,
                        CreateDT = DateTime.UtcNow
                    };
                    db.Status = ItemStatus.ACTIVE;
                    await _context.CTAMUserPersonalItem().AddAsync(newPers);
                }
            }

            return newItemsRetDTO;
        }

    }
}
