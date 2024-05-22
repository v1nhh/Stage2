using AutoMapper;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class RoleItemTypeMaxQtyProfile : Profile
    {
        public RoleItemTypeMaxQtyProfile()
        {
            CreateMap<CTAMRole_ItemType, RoleItemTypeMaxQtyDTO>()
                .ForMember(dto => dto.RoleID, opt => opt.MapFrom(src => src.CTAMRoleID));
        }
    }
}
