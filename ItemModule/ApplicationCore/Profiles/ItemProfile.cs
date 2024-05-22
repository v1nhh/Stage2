using AutoMapper;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemWebDTO>()
                .ReverseMap();

            CreateMap<Item, ItemDTO>()
                .ReverseMap();
        }
    }
}
