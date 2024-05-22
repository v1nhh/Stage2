using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemWithItemTypeProfile : Profile
    {
        public ItemWithItemTypeProfile()
        {
            CreateMap<Item, ItemWithItemTypeDTO>()
                .ReverseMap();
        }
    }
}
