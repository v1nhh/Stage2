using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class RoleLiveSyncEnvelope
    {
        public CTAMRoleBaseDTO Role { get; set; }

        public List<CTAMRole_PermissionBaseDTO> RolePermissions { get; set; }

        public List<CTAMRole_ItemTypeBaseDTO> RoleItemTypes { get; set; }

        public List<ItemTypeBaseDTO> ItemTypes { get; set; }

        public List<CabinetStockBaseDTO> CabinetStocks { get; set; }

        public List<ItemBaseDTO> Items { get; set; }

        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; }

        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; }

        public List<CabinetAccessIntervalsBaseDTO> CabinetAccessIntervals { get; set; }

        public List<CTAMUser_RoleBaseDTO> UserRoles { get; set; }

        public List<ErrorCodeBaseDTO> ErrorCodes { get; set; }

        public List<ItemType_ErrorCodeBaseDTO> ItemTypeErrorCodes { get; set; }

        public List<CTAMUserBaseDTO> Users { get; set; }
    }
}
