using AutoMapper;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemCriticalDataProfile : Profile
    {
        public ItemCriticalDataProfile()
        {
            CreateMap<Item, ItemBaseDTO>();
            CreateMap<ItemSet, ItemSetBaseDTO>();
            CreateMap<ItemType, ItemTypeBaseDTO>();
            CreateMap<ErrorCode, ErrorCodeBaseDTO>();
            CreateMap<CTAMRole_ItemType, CTAMRole_ItemTypeBaseDTO>();
            CreateMap<ItemType_ErrorCode, ItemType_ErrorCodeBaseDTO>();
        }
    }
}
