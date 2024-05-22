using System;
using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CloudAPI.ApplicationCore.DTO.Sync
{
    [Obsolete]
    public class UserLiveSyncDataEnvelope
    {
        #region UserRole (2 tables)
        // CTAMUser
        public CTAMUserBaseDTO CTAMUserData {get; set; }
      
        // CTAMUser_Role
        public List<CTAMUser_RoleBaseDTO> CTAMUser_RoleData { get; set; }
        #endregion

        #region ItemCabinet (1 table)
        // CTAMUserPersonalItem
        public List<CTAMUserPersonalItemBaseDTO> CTAMUserPersonalItemData {get; set; }
        #endregion
        
    }
}
