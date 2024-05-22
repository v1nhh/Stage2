using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class CheckRoleItemTypeDependencyQuery: IRequest<List<string>>
    {
        public string CTAMUserUID { get; set; }
        public List<int> RolesIDs { get; set; }

        /// <summary>
        /// Query returns RoleID's which are dependencies of Items in possession of the provided user
        /// </summary>
        /// <param name="userUID"></param>
        /// <param name="rolesIDs"></param>
        public CheckRoleItemTypeDependencyQuery(string userUID, List<int> rolesIDs)
        {
            CTAMUserUID = userUID;
            RolesIDs = rolesIDs;
        }
    }

    public class CheckRoleItemTypeDependencyHandler: IRequestHandler<CheckRoleItemTypeDependencyQuery, List<string>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckRoleItemTypeDependencyHandler> _logger;

        public CheckRoleItemTypeDependencyHandler(MainDbContext context, ILogger<CheckRoleItemTypeDependencyHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<string>> Handle(CheckRoleItemTypeDependencyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckRoleItemTypeDependencyHandler called");
            // RoleIDs of roles which are dependencies for ItemTypes
            var dependencyRolesIds = new List<int>();
            if (request.RolesIDs == null || request.RolesIDs.Count == 0)
            {
                return new List<string>();
            }
            // 1. get all current user Roles
            var userRoleIDs = await _context.CTAMUser_Role().AsNoTracking()
                .Where(ur => ur.CTAMUserUID.Equals(request.CTAMUserUID))
                .Select(ur => ur.CTAMRoleID)
                .ToListAsync();

            var usersRoleItemTypes = await _context.CTAMUserInPossession().AsNoTracking()
                // 2. get all picked CTAMUserInPossession items
                .Where(uip => (uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue)
                            && uip.CTAMUserUIDOut.Equals(request.CTAMUserUID))
                // 3. map all picked CTAMUserInPossession items to ItemTypeIDs via Item table
                .Include(uip => uip.Item)
                    .ThenInclude(item => item.ItemType)
                    // 4. get all roles per ItemTypeID from CTAMRole_ItemType
                    .ThenInclude(itemType => itemType.CTAMRole_ItemType)
                .Select(uip => uip.Item.ItemType)
                .SelectMany(itemType => itemType.CTAMRole_ItemType)
                .ToListAsync();

            // 5. Filter to get only the intersection of RoleIDs from ItemTypes and current CTAMUser
            var userRolesPerItemType = usersRoleItemTypes
                .Where(roleItemtype => userRoleIDs.Contains(roleItemtype.CTAMRoleID))
                // 6. Group the RoleIDs by ItemTypeIDs
                .ToLookup(roleItemtype => roleItemtype.ItemTypeID, roleItemtype => roleItemtype.CTAMRoleID);

            foreach (var rolesOfItemType in userRolesPerItemType)
            {
                bool requestedToRemoveAllRolesOfItemType = true;
                // 7. check per ItemTypeID if all roles are requested to be removed
                foreach (var roleOfItemType in rolesOfItemType)
                {
                    if (!request.RolesIDs.Contains(roleOfItemType))
                    {
                        requestedToRemoveAllRolesOfItemType = false;
                    }
                }
                //   7.a. if all - add current users all RoleIDs of that ItemType to return list of roles that are dependencies of ItemTypeIDs
                if (requestedToRemoveAllRolesOfItemType)
                {
                    dependencyRolesIds.AddRange(rolesOfItemType);
                }
                //   7.b. else if at least one role per ItemTypeID remains - continue
            }
            dependencyRolesIds = dependencyRolesIds.Distinct().ToList();
            var dependencyRolesDescriptions = await _context.CTAMRole().AsNoTracking()
                                                        .Where(r => dependencyRolesIds.Contains(r.ID))
                                                        .Select(r => r.Description)
                                                        .ToListAsync();
            string dependenciesFound = (dependencyRolesIds.Count == 0 ? "No" : dependencyRolesIds.Count.ToString());
            _logger.LogInformation($"{dependenciesFound} dependency roles found");
            return dependencyRolesDescriptions;
        }
    }

}
