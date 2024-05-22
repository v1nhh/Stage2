using System.Linq;
using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemCabinetRoleProfile: Profile
    {
        public ItemCabinetRoleProfile()
        {
            CreateMap<CTAMRole, ItemCabinetRoleDTO>()
                .ForMember(dto => dto.Permissions, opt => opt.MapFrom(src => src.CTAMRole_Permission.Select(rp => rp.CTAMPermission)));
        }
    }
}
