using AutoMapper;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemTypeProfile : Profile
    {
        public ItemTypeProfile()
        {
            CreateMap<CTAMRole_ItemType, ItemTypeWebDTO>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(src => src.ItemType.Description))
                .ForMember(dto => dto.ID, opt => opt.MapFrom(src => src.ItemTypeID));
        }
    }
}
