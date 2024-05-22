using AutoMapper;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class UserRoleCriticalDataProfile : Profile
    {
        public UserRoleCriticalDataProfile()
        {
            CreateMap<CTAMUser_Role, CTAMUser_RoleBaseDTO>();
            CreateMap<CTAMPermission, CTAMPermissionBaseDTO>();
            CreateMap<CTAMRole, CTAMRoleBaseDTO>();
            CreateMap<CTAMSetting, CTAMSettingBaseDTO>();
            CreateMap<CTAMUser, CTAMUserBaseDTO>();
            CreateMap<CTAMRole_Permission, CTAMRole_PermissionBaseDTO>();
        }
    }
}
