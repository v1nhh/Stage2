using AutoMapper;
using CloudAPI.ApplicationCore.DTO.Import;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.Profiles
{
    public class RoleImportProfile : Profile
    {
        public RoleImportProfile()
        {
            CreateMap<RoleImportReturnDTO, CTAMRole>();
            CreateMap<RoleImportDTO, RoleImportReturnDTO>();
        }
    }
}
