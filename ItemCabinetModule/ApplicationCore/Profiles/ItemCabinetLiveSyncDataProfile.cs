using AutoMapper;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class ItemCabinetLiveSyncDataProfile : Profile
    {
        public ItemCabinetLiveSyncDataProfile()
        {
            CreateMap<CTAMUserPersonalItem, CTAMUserPersonalItemBaseDTO>();
        }
    }
}
