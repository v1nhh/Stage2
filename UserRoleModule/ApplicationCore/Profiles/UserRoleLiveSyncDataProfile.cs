using AutoMapper;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class UserRoleLiveSyncDataProfile : Profile
    {
        public UserRoleLiveSyncDataProfile()
        {
            CreateMap<CTAMUser_Role, CTAMUser_RoleBaseDTO>();
            CreateMap<CTAMRole, CTAMRoleBaseDTO>();
            CreateMap<CTAMUser, CTAMUserBaseDTO>();
            CreateMap<CTAMRole_Permission, CTAMRole_PermissionBaseDTO>();
        }
    }
}
