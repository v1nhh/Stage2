using CabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using System;
using System.Collections.Generic;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CloudAPI.ApplicationCore.DTO.Sync.LiveSync
{
    public class CollectedLiveSyncRequest
    {
        public List<string> CabinetUI { get; set; } = new List<string>(); // for now only "DEFAULT" in column CabinetNumber
        public List<string> CabinetNumbers { get; set; } = new List<string>();
        public List<int> CabinetAccessIntervalIDs { get; set; } = new List<int>(); // RoleID, CabinetAccessIntervalID
        public List<int> CabinetStockItemTypeIDs { get; set; } = new List<int>();
        public List<int> CabinetPositionIDs { get; set; } = new List<int>();
        public List<int> CabinetDoorIDs { get; set; } = new List<int>();
        public List<int> ItemIDs { get; set; } = new List<int>();
        public List<int> ItemTypeIDs { get; set; } = new List<int>();
        public List<int> ItemTypeIDsForEnvelope { get; set; } = new List<int>();
        public List<string> UserIDs { get; set; } = new List<string>();
        public List<string> UserIDsForEnvelope { get; set; } = new List<string>();
        public List<int> RoleIDs { get; set; } = new List<int>();
        public List<int> RoleIDsForEnvelope { get; set; } = new List<int>();
        public List<int> ErrorCodeIDs { get; set; } = new List<int>();
        public List<int> SettingIDs { get; set; } = new List<int>();
        public List<int> UserPersonalItemIDs { get; set; } = new List<int>();
        public List<Guid> UserInPossessionIDs { get; set; } = new List<Guid>();
    }

    public class CollectedLiveSyncEnvelope
    {
        public List<CabinetUIBaseDTO> CabinetUIs { get; set; } = new List<CabinetUIBaseDTO>();
        public List<CabinetBaseDTO> Cabinets { get; set; } = new List<CabinetBaseDTO>();
        public List<CabinetAccessIntervalsBaseDTO> CabinetAccessIntervals { get; set; } = new List<CabinetAccessIntervalsBaseDTO>();
        public List<CabinetStockBaseDTO> CabinetStocks { get; set; } = new List<CabinetStockBaseDTO>();
        public List<CabinetPositionBaseDTO> CabinetPositions { get; set; } = new List<CabinetPositionBaseDTO>();
        public List<CabinetDoorBaseDTO> CabinetDoors { get; set; } = new List<CabinetDoorBaseDTO>();
        public List<ItemBaseDTO> Items { get; set; } = new List<ItemBaseDTO>();
        public List<ItemTypeBaseDTO> ItemTypes { get; set; } = new List<ItemTypeBaseDTO>();
        public List<ItemType_ErrorCodeBaseDTO> ItemTypeErrorCodes { get; set; } = new List<ItemType_ErrorCodeBaseDTO>();
        public List<CTAMUserBaseDTO> Users { get; set; } = new List<CTAMUserBaseDTO>();
        public List<CTAMRoleBaseDTO> Roles { get; set; } = new List<CTAMRoleBaseDTO>();
        public List<CTAMRole_PermissionBaseDTO> RolePermissions { get; set; } = new List<CTAMRole_PermissionBaseDTO>();
        public List<CTAMRole_ItemTypeBaseDTO> RoleItemTypes { get; set; } = new List<CTAMRole_ItemTypeBaseDTO>();
        public List<CTAMUser_RoleBaseDTO> UserRoles { get; set; } = new List<CTAMUser_RoleBaseDTO>();
        public List<ErrorCodeBaseDTO> ErrorCodes { get; set; } = new List<ErrorCodeBaseDTO>();
        public List<CTAMSettingBaseDTO> Settings { get; set; } = new List<CTAMSettingBaseDTO>();
        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; } = new List<CTAMUserPersonalItemBaseDTO>();
        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; } = new List<CTAMUserInPossessionBaseDTO>();
    }
}
