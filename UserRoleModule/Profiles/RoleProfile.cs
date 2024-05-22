using System.Linq;
using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<CTAMRole, RoleDTO>()
                .ForMember(dto => dto.Permissions, opt => opt.MapFrom(src => src.CTAMRole_Permission.Select(rp => rp.CTAMPermission)));

            CreateMap<CTAMRole, RoleWebDTO>()
                .ForMember(dto => dto.Permissions, opt => opt.MapFrom(src => src.CTAMRole_Permission.Select(rp => rp.CTAMPermission)));

            CreateMap<CTAMRole, UserRoleWebDTO>();

            CreateMap<RoleDTO, RoleWebDTO>()
                .ForMember(dto => dto.Permissions, opt => opt.MapFrom(src => src.Permissions));

            CreateMap<RoleDTO, UserRoleWebDTO>();
        }
    }
}
