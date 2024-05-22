using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemToPickProfile: Profile
    {
        public ItemToPickProfile()
        {
            CreateMap<ItemToPick, ItemToPickDTO>()
                .ReverseMap();
        }
    }
}
