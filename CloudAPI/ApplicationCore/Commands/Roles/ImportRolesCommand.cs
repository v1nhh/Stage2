using AutoMapper;
using CloudAPI.ApplicationCore.DTO.Import;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;


namespace CloudAPI.ApplicationCore.Commands.Roles
{
    public class ImportRolesCommand : IRequest<List<RoleImportReturnDTO>>, IImportCommand<RoleImportDTO>
    {
        public List<RoleImportDTO> InputList { get; set; }
        public string Filename { get; set; }
    }

    public class ImportRolesHandler : IRequestHandler<ImportRolesCommand, List<RoleImportReturnDTO>>
    {
        private readonly ILogger<ImportRolesHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public ImportRolesHandler(MainDbContext context, ILogger<ImportRolesHandler> logger, IMapper mapper, IManagementLogger managementLogger)
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

        public async Task<List<RoleImportReturnDTO>> Handle(ImportRolesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportRolesHandler called");
            var impRoles = _mapper.Map<List<RoleImportReturnDTO>>(request.InputList); // Create room for ErrorMessage
            var invalids = impRoles.Where(r => (string.IsNullOrWhiteSpace(r.Description) || r.Description.Length > 250))
                                        .Select(r => { r.ErrorMessage = "Field restrictions"; return r; })
                                        .ToList();
            var validImports = impRoles.Except(invalids).ToList();

            validImports = MapStringsToDates(invalids, validImports);

            // Check double Descriptions in import list
            var doubleDescs = validImports.GroupBy(r => r.Description).Where(g => g.Count() > 1);
            foreach (var dbl in doubleDescs)
            {
                var extraImports = dbl.Take(dbl.Count() - 1).Select(r => { r.ErrorMessage = "Description not unique in import"; return r; }).ToList();
                invalids.AddRange(extraImports);
                validImports = validImports.Except(extraImports).ToList();
            }

            await MapCsvStringsToDatabaseUsersItemTypesAndPermissions(invalids, validImports);
            var (dbExisting, totalChanged, totalUnchanged) = await HandleExistingRoles(validImports);
            var newRolesRetDTO = await HandleNewRows(validImports, dbExisting);

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_importedRoles),
                ("fileName", request.Filename), 
                ("newRoles", newRolesRetDTO.Count().ToString()),
                ("changedRoles", totalChanged.ToString()),
                ("unchangedRoles", totalUnchanged.ToString()),
                ("invalidRoles", invalids.Count().ToString())); 
            return invalids;
        }

        private static List<RoleImportReturnDTO> MapStringsToDates(List<RoleImportReturnDTO> invalids, List<RoleImportReturnDTO> validImports)
        {
            var invalidDates = new List<RoleImportReturnDTO>();
            foreach (var imp in validImports)
            {
                if (!string.IsNullOrWhiteSpace(imp.ValidFromDTString))
                {
                    if (DateTime.TryParse(imp.ValidFromDTString, out DateTime validFrom))
                    {
                        DateTime.SpecifyKind(validFrom, DateTimeKind.Utc);
                        imp.ValidFromDT = validFrom;
                    }
                    else
                    {
                        imp.ErrorMessage = $"Not a valid date: '{imp.ValidFromDTString}'";
                        invalidDates.Add(imp);
                        continue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(imp.ValidUntilDTString))
                {
                    if (DateTime.TryParse(imp.ValidUntilDTString, out DateTime validUntil))
                    {
                        DateTime.SpecifyKind(validUntil, DateTimeKind.Utc);
                        imp.ValidUntilDT = validUntil;
                    }
                    else
                    {
                        imp.ErrorMessage = $"Not a valid date: '{imp.ValidUntilDTString}'";
                        invalidDates.Add(imp);
                    }
                }

                if (imp.ValidFromDT != null && imp.ValidUntilDT != null && imp.ValidFromDT > imp.ValidUntilDT)
                {
                    imp.ErrorMessage = "Invalid date range";
                    invalidDates.Add(imp);
                }
            }
            invalids.AddRange(invalidDates);
            validImports = validImports.Except(invalidDates).ToList();
            return validImports;
        }

        private async Task MapCsvStringsToDatabaseUsersItemTypesAndPermissions(List<RoleImportReturnDTO> invalids, List<RoleImportReturnDTO> validImports)
        {
            foreach (var imp in validImports)
            {
                await FindRelatedElements<CTAMUser>(invalids, imp, imp.CTAMUsersString,
                                                    (uids) => _context.CTAMUser().Where(u => uids.Contains(u.UID.ToUpper())).Distinct().ToListAsync(),
                                                    (list) => imp.CTAMUsers = list,
                                                    (dto) => dto.UID,
                                                    "User(s)");

                await FindRelatedElements<ItemType>(invalids, imp, imp.ItemTypeDescriptions,
                                                    (ids) => _context.ItemType().Where(it => ids.Contains(it.Description.ToUpper())).Distinct().ToListAsync(),
                                                    (list) => imp.ItemTypes = list,
                                                    (dto) => dto.Description,
                                                    "ItemType(s)");
                await FindRelatedElements<CTAMPermission>(invalids, imp, imp.PermissionsString,
                                                          (ids) => _context.CTAMPermission().Where(p => ids.Contains(p.Description.ToUpper())).Distinct().ToListAsync(),
                                                          (list) => imp.Permissions = list,
                                                          (dto) => dto.Description,
                                                          "Permission(s)");
            }
        }

        // Split the csvString, find the ids in the database and set the result in imp. If an id is not found, add it to the invalid list
        private async Task FindRelatedElements<T>(List<RoleImportReturnDTO> invalids, RoleImportReturnDTO imp, string csvString,
                                            Func<List<string>, Task<List<T>>> dbSearcher,
                                            Action<ICollection<T>> impListSetter,
                                            Func<T, string> idGetter,
                                            string errorName)
        {
            if (!string.IsNullOrEmpty(csvString))
            {
                var impIds = csvString.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim().ToUpper()).Distinct().ToList();
                var allDbList = new List<T>();

                foreach (var chunk in impIds.Partition(15000))
                {
                    var dbListChunk = await dbSearcher(chunk);
                    allDbList.AddRange(dbListChunk);
                }

                impListSetter(allDbList);
                if (impIds.Count() != allDbList.Count())
                {
                    var dbs = allDbList.Select(u => idGetter(u).ToUpper()).ToList();
                    var notfound = impIds.Where(uid => !dbs.Contains(uid)).ToList();
                    imp.ErrorMessage = $"{errorName} niet gevonden: {string.Join(',', notfound)}";
                    if (!invalids.Any(i => i.Description.Equals(imp.Description)))
                    {
                        invalids.Add(imp); // But still use it for import, so don't remove it from validImports
                    }
                }
            }
        }

        private async Task<IEnumerable<RoleImportReturnDTO>> HandleNewRows(List<RoleImportReturnDTO> validImports, List<CTAMRole> dbExisting)
        {
            var dbExistingDescs = dbExisting.Select(it => it.Description).ToList();
            var newRolesRetDTO = validImports.Where(imp => !dbExistingDescs.Contains(imp.Description));
            var newRoles = _mapper.Map<List<CTAMRole>>(newRolesRetDTO);
            await _context.CTAMRole().AddRangeAsync(newRoles);

            // Users
            var newurs = newRoles.SelectMany(r => newRolesRetDTO.Where(rr => rr.Description.Equals(r.Description)).First().CTAMUsers
                                                                .Select(u => new CTAMUser_Role
                                                                {
                                                                    CTAMUserUID = u.UID,
                                                                    CTAMRole = r
                                                                }
                                                   ));
            await _context.CTAMUser_Role().AddRangeAsync(newurs);
            // ItemTypes
            var newitrs = newRoles.SelectMany(r => newRolesRetDTO.Where(rr => rr.Description.Equals(r.Description)).First().ItemTypes
                                                                .Select(it => new CTAMRole_ItemType
                                                                {
                                                                    ItemTypeID = it.ID,
                                                                    CTAMRole = r
                                                                }
                                                   ));
            await _context.CTAMRole_ItemType().AddRangeAsync(newitrs);
            // Permissions
            var newrps = newRoles.SelectMany(r => newRolesRetDTO.Where(rr => rr.Description.Equals(r.Description)).First().Permissions
                                                                .Select(p => new CTAMRole_Permission
                                                                {
                                                                    CTAMPermissionID = p.ID,
                                                                    CTAMRole = r
                                                                }
                                                   ));
            await _context.CTAMRole_Permission().AddRangeAsync(newrps);
            return newRolesRetDTO;
        }

        private async Task<(List<CTAMRole>, int, int)> HandleExistingRoles(List<RoleImportReturnDTO> validImports)
        {
            // Find already existing Roles in database
            var uniqueDescs = validImports.Select(r => r.Description).ToList();
            var dbExisting = await _context.CTAMRole()
                                           .Include(r => r.CTAMUser_Roles)
                                           .Include(r => r.CTAMRole_Permission)
                                           .Where(u => uniqueDescs.Contains(u.Description))
                                           .ToListAsync();
            int totalChanged = 0, totalUnchanged = 0;

            foreach (var dbr in dbExisting)
            {
                bool changed = false;
                var imp = validImports.Where(it => it.Description.Equals(dbr.Description)).FirstOrDefault();
                
                if (dbr.ValidFromDT != imp.ValidFromDT)
                {
                    dbr.ValidFromDT = imp.ValidFromDT;
                    changed = true;
                }
                if (dbr.ValidUntilDT != imp.ValidUntilDT)
                {
                    dbr.ValidUntilDT = imp.ValidUntilDT;
                    changed = true;
                }

                var newURs = imp.CTAMUsers
                                .Where(u => !dbr.CTAMUser_Roles.Any(ur => ur.CTAMUserUID == u.UID))
                                .Select(u => new CTAMUser_Role() { CTAMRoleID = dbr.ID, CTAMUserUID = u.UID });
                var removeURs = dbr.CTAMUser_Roles.Where(ur => !imp.CTAMUsers.Any(u => u.UID == ur.CTAMUserUID));

                _context.CTAMUser_Role().RemoveRange(removeURs);
                await _context.CTAMUser_Role().AddRangeAsync(newURs);

                var curRITs = await _context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == dbr.ID).ToListAsync();
                var newRITs = imp.ItemTypes
                                 .Where(it => !curRITs.Any(rit => rit.ItemTypeID == it.ID))
                                 .Select(it => new CTAMRole_ItemType() { CTAMRoleID = dbr.ID, ItemTypeID = it.ID });
                var removeRITs = curRITs.Where(rit => !imp.ItemTypes.Any(it => rit.ItemTypeID == it.ID));
                _context.CTAMRole_ItemType().RemoveRange(removeRITs);
                await _context.CTAMRole_ItemType().AddRangeAsync(newRITs);

                var newRPs = imp.Permissions
                                .Where(p => !dbr.CTAMRole_Permission.Any(rp => rp.CTAMPermissionID == p.ID))
                                .Select(p => new CTAMRole_Permission() { CTAMRoleID = dbr.ID, CTAMPermissionID = p.ID });
                var removeRPs = dbr.CTAMRole_Permission.Where(rp => !imp.Permissions.Any(p => rp.CTAMPermissionID == p.ID));
                _context.CTAMRole_Permission().RemoveRange(removeRPs);
                await _context.CTAMRole_Permission().AddRangeAsync(newRPs);
                
                if (newURs.Any() || removeURs.Any() || newRITs.Any() || removeRITs.Any() || newRPs.Any() || removeRPs.Any())
                {
                    changed = true;
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

            return (dbExisting, totalChanged, totalUnchanged);
        }
    }
}
