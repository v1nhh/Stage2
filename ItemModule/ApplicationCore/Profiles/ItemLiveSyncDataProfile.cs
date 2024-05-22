using AutoMapper;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemLiveSyncDataProfile : Profile
    {
        public ItemLiveSyncDataProfile()
        {
            CreateMap<ItemType, ItemTypeBaseDTO>();
            CreateMap<CTAMRole_ItemType, CTAMRole_ItemTypeBaseDTO>();
        }
    }
}
