using System;
using System.Collections.Generic;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace UserRoleModule.ApplicationCore.DTO.Sync
{
    [Obsolete]
    public class UserRoleLiveSyncEnvelope
    {

        // CTAMUser
        public CTAMUserBaseDTO CTAMUserData {get; set; }

        // CTAMUser_Role
        public List<CTAMUser_RoleBaseDTO> CTAMUser_RoleData { get; set; }
        
        // CTAMRole_Permission
        public List<CTAMRole_PermissionBaseDTO> CTAMRole_PermissionData { get; set; }
        //CTAMRole
        public CTAMRoleBaseDTO CTAMRoleData { get; set; }

    }
}
