using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CloudAPI.ApplicationCore.DTO
{
    public class CriticalDataEnvelope
    {
        #region Cabinet (9 tables)
        //Cabinet
        public CabinetBaseDTO CabinetData { get; set; }

        //CabinetCell				
        public List<CabinetCellBaseDTO> CabinetCellData { get; set; }

        //CabinetCellType
        public List<CabinetCellTypeBaseDTO> CabinetCellTypeData { get; set; }

        //CabinetDoor				
        public List<CabinetDoorBaseDTO> CabinetDoorData { get; set; }

        //CabinetUI
        public List<CabinetUIBaseDTO> CabinetUIData { get; set; }

        //CabinetColumn           
        public List<CabinetColumnBaseDTO> CabinetColumnData { get; set; }

        //CabinetPosition         
        public List<CabinetPositionBaseDTO> CabinetPositionData { get; set; }

        //CabinetAccessIntervals  
        public List<CabinetAccessIntervalsBaseDTO> CabinetAccessIntervalsData { get; set; }

        //CTAMRole_Cabinet
        public List<CTAMRole_CabinetBaseDTO> CTAMRole_CabinetData { get; set; }
        #endregion

        #region Item (5 tables)
        //Item
        public List<ItemBaseDTO> ItemData { get; set; }

        //ItemSet        	
        public List<ItemSetBaseDTO> ItemSetData { get; set; }

        //ItemType
        public List<ItemTypeBaseDTO> ItemTypeData { get; set; }

        //ErrorCode
        public List<ErrorCodeBaseDTO> ErrorCodeData { get; set; }

        //CTAMRole_ItemType
        public List<CTAMRole_ItemTypeBaseDTO> CTAMRole_ItemTypeData { get; set; }

        //ItemType_ErrorCode
        public List<ItemType_ErrorCodeBaseDTO> ItemType_ErrorCodeData { get; set; }
        #endregion

        #region ItemCabinet (6 tables)
        //CabinetStock   
        public List<CabinetStockBaseDTO> CabinetStockData { get; set; }

        //AllowedCabinetPosition
        public List<AllowedCabinetPositionBaseDTO> AllowedCabinetPositionData { get; set; }

        //CabinetPositionContent
        public List<CabinetPositionContentBaseDTO> CabinetPositionContentData { get; set; }

        //CTAMUserInPossession  
        public List<CTAMUserInPossessionBaseDTO> CTAMUserInPossessionData { get; set; }

        //CTAMUserPersonalItem  
        public List<CTAMUserPersonalItemBaseDTO> CTAMUserPersonalItemData { get; set; }

        //ItemToPick
        public List<ItemToPickBaseDTO> ItemToPickData { get; set; }
        #endregion

        #region UserRole (6 tables)
        //CTAMUser_Role
        public List<CTAMUser_RoleBaseDTO> CTAMUser_RoleData { get; set; }

        //CTAMPermission
        public List<CTAMPermissionBaseDTO> CTAMPermissionData { get; set; }

        //CTAMRole
        public List<CTAMRoleBaseDTO> CTAMRoleData { get; set; }

        //CTAMSetting
        public List<CTAMSettingBaseDTO> CTAMSettingData { get; set; }

        //CTAMUser
        public List<CTAMUserBaseDTO> CTAMUserData { get; set; }

        //CTAMRole_Permission
        public List<CTAMRole_PermissionBaseDTO> CTAMRole_PermissionData { get; set; }
        #endregion
    }
}
