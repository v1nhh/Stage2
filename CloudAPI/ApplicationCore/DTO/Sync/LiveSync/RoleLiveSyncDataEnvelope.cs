using System;
using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CloudAPI.ApplicationCore.DTO.Sync.LiveSync
{
    [Obsolete]
    public class RoleLiveSyncDataEnvelope
    {
        
        #region UserRole (2 tables)
        //CTAMRole
        public CTAMRoleBaseDTO CTAMRoleData { get; set; }
        //CTAMRole_Permission
        public List<CTAMRole_PermissionBaseDTO> CTAMRole_PermissionData { get; set; }
        #endregion
        
        #region Cabinet (2 tables)

        //CTAMCabinetAccessIntervals
        public List<CabinetAccessIntervalsBaseDTO>  CabinetAccessIntervalsData {get; set; }

        //CTAMRole_Cabinet
        public List<CTAMRole_CabinetBaseDTO>  CTAMRole_CabinetData {get; set; }
        #endregion


        #region Item (2 tables)
        //CTAMRole_ItemType
        public List<CTAMRole_ItemTypeBaseDTO>  CTAMRole_ItemTypeData {get; set; }

        //CTAMItemType
        public List<ItemTypeBaseDTO>  ItemTypeData {get; set; }
        #endregion

        #region ItemCabinet (1 table)
        //CabinetStock
        public List<CabinetStockBaseDTO>  CabinetStockData {get; set; }
        #endregion

        #region CloudAPI (1 table)
        //UserLiveSyncEnvelope
        public List<UserLiveSyncDataEnvelope> UserDataEnvelopes {get; set; }
        #endregion
    }
}