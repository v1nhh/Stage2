using AutoMapper;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemSetProfile: Profile
    {
        public ItemSetProfile()
        {
            CreateMap<ItemSet, ItemSetDTO>()
                .ReverseMap();
        }
    }
}
