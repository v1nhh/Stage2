using AutoMapper;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemCabinetCriticalDataProfile : Profile
    {
        public ItemCabinetCriticalDataProfile()
        {
            CreateMap<CabinetStock, CabinetStockBaseDTO>();
            CreateMap<AllowedCabinetPosition, AllowedCabinetPositionBaseDTO>();
            CreateMap<CabinetPositionContent, CabinetPositionContentBaseDTO>();
            CreateMap<CTAMUserInPossession, CTAMUserInPossessionBaseDTO>();
            CreateMap<CTAMUserPersonalItem, CTAMUserPersonalItemBaseDTO>();
            CreateMap<ItemToPick, ItemToPickBaseDTO>();
        }
    }
}
