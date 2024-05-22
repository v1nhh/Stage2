using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<CTAMPermission, PermissionDTO>();

            CreateMap<CTAMPermission, PermissionWebDTO>();

            CreateMap<PermissionDTO, PermissionWebDTO>();
        }
    }
}
