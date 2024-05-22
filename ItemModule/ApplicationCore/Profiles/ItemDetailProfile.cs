using AutoMapper;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemDetailProfile: Profile
    {
        public ItemDetailProfile()
        {
            CreateMap<ItemDetail, ItemDetailDTO>()
                .ReverseMap();
        }
    }
}
