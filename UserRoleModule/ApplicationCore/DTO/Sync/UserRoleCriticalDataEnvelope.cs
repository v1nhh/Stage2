using System.Collections.Generic;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace UserRoleModule.ApplicationCore.DTO.Sync
{
    public class UserRoleCriticalDataEnvelope
    {
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
    }
}
