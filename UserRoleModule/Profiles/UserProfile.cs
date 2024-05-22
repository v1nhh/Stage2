using System.Linq;
using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Import;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CTAMUser, UserDTO>()
                .ForMember(
                    dto => dto.Roles,
                    opt => opt.MapFrom(src => src.CTAMUser_Roles
                                                .Select(ur => ur.CTAMRole))
                );

            CreateMap<CTAMUser, UserWebDTO>()
                .ForMember(
                    dto => dto.Roles,
                    opt => opt.MapFrom(src => src.CTAMUser_Roles
                                                .Select(ur => ur.CTAMRole))
                );

            CreateMap<CTAMUser, UserWebShortDTO>();

            CreateMap<UserDTO, UserWebDTO>()
                .ForMember(
                    dto => dto.Roles,
                    opt => opt.MapFrom(src => src.Roles)
                );

            CreateMap<UserImportReturnDTO, CTAMUser>();
            CreateMap<UserImportDTO, UserImportReturnDTO>();
        }
    }
}
