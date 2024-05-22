using System.Collections.Generic;
using System.Threading.Tasks;
using CabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;

namespace CTAMSeeder
{
    public interface IBulkDataSeeder
    {
        // UserRoleModule
        Task<List<CTAMUser>> AddUsers(int amount);
        Task<List<CTAMRole>> AddRoles(int amount);
        Task<List<CTAMRole_Permission>> AddRolePermissions(List<CTAMRole> roles, List<CTAMPermission> permissions);
        Task<List<CTAMUser_Role>> AddUserRoles(List<CTAMUser> users, List<CTAMRole> roles, int maxUsersPerRole);

        // CabinetModule
        Task<List<Cabinet>> AddCabinets(int amount);
        Task<List<CTAMRole_Cabinet>> AddRoleCabinets(List<CTAMRole> roles, List<Cabinet> cabinets, int maxCabinetsPerRole);

        // ItemModule
        Task<List<ItemType>> AddItemTypes(int amount);
        Task<List<CTAMRole_ItemType>> AddRoleItemTypes(List<CTAMRole> roles, List<ItemType> itemTypes, int maxItemTypesPerRole);
        Task<List<Item>> AddItems(int amount, List<ItemType> itemTypes);
        Task<List<CabinetLog>> AddCabinetLogs(int amount, List<Cabinet> cabinets);

        // ItemCabinetModule
        Task LinkUserPersonalItems();
    }
}
