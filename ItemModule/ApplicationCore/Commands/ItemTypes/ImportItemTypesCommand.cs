using AutoMapper;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.ItemTypes
{
    public class ImportItemTypesCommand : IRequest<List<ItemTypeImportReturnDTO>>, IImportCommand<ItemTypeImportDTO>
    {
        public List<ItemTypeImportDTO> InputList { get; set; }
        public string Filename { get; set; }
    }

    public class ImportItemTypesHandler : IRequestHandler<ImportItemTypesCommand, List<ItemTypeImportReturnDTO>>
    {
        private readonly ILogger<ImportItemTypesHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public ImportItemTypesHandler(MainDbContext context, ILogger<ImportItemTypesHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<List<ItemTypeImportReturnDTO>> Handle(ImportItemTypesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportItemTypesHandler called");
            var impItemTypes = _mapper.Map<List<ItemTypeImportReturnDTO>>(request.InputList); // Create room for ErrorMessage
            var invalids = impItemTypes.Where(u => (string.IsNullOrWhiteSpace(u.Description) || u.Description.Length > 250) ||
                                                    string.IsNullOrWhiteSpace(u.TagTypeString) || TagTypeAsInt(u.TagTypeString) < 0 ||
                                                    string.IsNullOrEmpty(u.DepthString) || StringAsDouble(u.DepthString) < 0 ||
                                                    string.IsNullOrEmpty(u.WidthString) || StringAsDouble(u.WidthString) < 0 ||
                                                    string.IsNullOrEmpty(u.HeightString) || StringAsDouble(u.HeightString) < 0)
                                        .Select(ec => { ec.ErrorMessage = "Field restrictions"; return ec; })
                                        .ToList();
            var validImports = impItemTypes.Except(invalids).ToList();

            // Check double Descriptions in import list
            var doubleDescs = validImports.GroupBy(it => it.Description).Where(g => g.Count() > 1);
            foreach (var dbl in doubleDescs)
            {
                var extraImports = dbl.Take(dbl.Count() - 1).Select(u => { u.ErrorMessage = "ItemTypeID not unique in import"; return u; }).ToList();
                invalids.AddRange(extraImports);
                validImports = validImports.Except(extraImports).ToList();
            }

            // Find ErrorCodes, map strings to TagType and doubles
            foreach (var imp in validImports)
            {
                imp.TagType = (TagType)TagTypeAsInt(imp.TagTypeString);
                imp.Depth = StringAsDouble(imp.DepthString);
                imp.Width = StringAsDouble(imp.WidthString);
                imp.Height = StringAsDouble(imp.HeightString);

                var impcodes = imp.ErrorCodesString.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ec => ec.Trim()).ToList();
                var ecsdb = _context.ErrorCode().AsNoTracking().Where(ec => impcodes.Contains(ec.Code)).ToList();
                imp.ErrorCodes = ecsdb;
                if (impcodes.Count() != ecsdb.Count())
                {
                    var dbcodes = ecsdb.Select(ec => ec.Code).ToList();
                    var notfound = impcodes.Where(c => !dbcodes.Contains(c)).ToList();
                    imp.ErrorMessage = $"ErrorCode(s) niet gevonden: {string.Join(", ", notfound)}";
                    invalids.Add(imp); // But still use it for import, so don't remove it from validImports
                }
            }

            var uniqueDescs = validImports.Select(it => it.Description).ToList();

            // Find already existing ItemTypes in database
            var existingItemTypes = await _context.ItemType().Include(it => it.ItemType_ErrorCode).Where(it => uniqueDescs.Contains(it.Description)).ToListAsync();
            var totalChanged = 0;
            var totalUnchanged = 0;

            foreach (var existingItemType in existingItemTypes)
            {
                var changed = false;
                var imp = validImports.Where(it => it.Description.Equals(existingItemType.Description)).FirstOrDefault();
                if (existingItemType.TagType != (TagType)TagTypeAsInt(imp.TagTypeString))
                {
                    existingItemType.TagType = (TagType)TagTypeAsInt(imp.TagTypeString);
                    changed = true;
                }
                if (existingItemType.Depth != StringAsDouble(imp.DepthString))
                {
                    existingItemType.Depth = StringAsDouble(imp.DepthString);
                    changed = true;
                }
                if (existingItemType.Width != StringAsDouble(imp.WidthString))
                {
                    existingItemType.Width = StringAsDouble(imp.WidthString);
                    changed = true;
                }
                if (existingItemType.Height != StringAsDouble(imp.HeightString))
                {
                    existingItemType.Height = StringAsDouble(imp.HeightString);
                    changed = true;
                }

                var errorCodesInUse = await _context.Item().AsNoTracking()
                                            .Where(i => i.ErrorCodeID != null && i.ItemTypeID == existingItemType.ID)
                                            .Select(i => i.ErrorCode)
                                            .ToListAsync();

                var errorCodesInUseNotInImport = errorCodesInUse.Where(eciu => !imp.ErrorCodes.Select(ec => ec.Code).Contains(eciu.Code)).ToList();

                if (errorCodesInUseNotInImport.Count > 0)
                {
                    imp.ErrorMessage += $"Error code(s) '{string.Join(", ", errorCodesInUseNotInImport.Select(ec => ec.Code))}' in gebruik bij defect item.";
                    invalids.Add(imp); // But still use it for import, so don't remove it from validImports
                }

                var removeRange = existingItemType.ItemType_ErrorCode.Where(itec => !errorCodesInUse.Select(eciu => eciu.ID).Contains(itec.ErrorCodeID) && !imp.ErrorCodes.Select(ec => ec.ID).Contains(itec.ErrorCodeID));
                if (removeRange.Count() > 0)
                {
                    changed = true;
                }
                _context.ItemType_ErrorCode().RemoveRange(removeRange);

                var currentErrorCodeIds = await _context.ItemType_ErrorCode().Where(itec => itec.ItemTypeID == existingItemType.ID).Select(itec => itec.ErrorCodeID).ToListAsync();
                var itecs = imp.ErrorCodes.Where(e => !currentErrorCodeIds.Contains(e.ID)).Select(ec => new ItemType_ErrorCode() { ItemTypeID = existingItemType.ID, ErrorCodeID = ec.ID });
                if (itecs.Count() > 0)
                {
                    changed = true;
                }

                await _context.ItemType_ErrorCode().AddRangeAsync(itecs);

                if (changed)
                {
                    totalChanged++;
                }
                else
                {
                    totalUnchanged++;
                }
            }

            var dbExistingDescs = existingItemTypes.Select(it => it.Description).ToList();
            var newItemTypesRetDTO = validImports.Where(imp => !dbExistingDescs.Contains(imp.Description));
            var newItemTypes = _mapper.Map<List<ItemType>>(newItemTypesRetDTO);
            foreach (var newItemType in newItemTypes)
            {
                newItemType.IsStoredInLocker = true;
            }
            _context.ItemType().AddRange(newItemTypes);
            var newitecs = newItemTypes.SelectMany(it => newItemTypesRetDTO.Where(itr => itr.Description.Equals(it.Description)).First().ErrorCodes
                                                           .Select(ec => new ItemType_ErrorCode
                                                           {
                                                               ErrorCodeID = ec.ID,
                                                               ItemType = it
                                                           }
                                                   ));
            _context.ItemType_ErrorCode().AddRange(newitecs);

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_importedItemTypes),
                ("fileName", request.Filename),
                ("newItemTypes", newItemTypesRetDTO.Count().ToString()),
                ("changedItemTypes", totalChanged.ToString()),
                ("unchangedItemTypes", totalUnchanged.ToString()),
                ("invalidItemTypes", invalids.Count().ToString()));
            return invalids;
        }

        private int TagTypeAsInt(string s)
        {
            int res = -1;

            if (Enum.TryParse<TagType>(s, true, out TagType tt) && Enum.IsDefined(typeof(TagType), tt))
            {
                res = ((int)tt);
            }

            return res;
        }

        private double StringAsDouble(string s)
        {
            double res = -1;

            if (double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double dbl))
            {
                res = dbl;
            }

            return res;
        }
    }
}
