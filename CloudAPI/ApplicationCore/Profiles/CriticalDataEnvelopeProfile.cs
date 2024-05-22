using AutoMapper;
using CloudAPI.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.DTO.Sync;
using ItemModule.ApplicationCore.DTO.Sync;
using ItemCabinetModule.ApplicationCore.DTO.Sync;
using UserRoleModule.ApplicationCore.DTO.Sync;

namespace CloudAPI.ApplicationCore.Profiles
{
    public class CriticalDataEnvelopeProfile : Profile
    {
        public CriticalDataEnvelopeProfile()
        {
            CreateMap<CabinetCriticalDataEnvelope, CriticalDataEnvelope>();
            CreateMap<ItemCriticalDataEnvelope, CriticalDataEnvelope>();
            CreateMap<ItemCabinetCriticalDataEnvelope, CriticalDataEnvelope>();
            CreateMap<UserRoleCriticalDataEnvelope, CriticalDataEnvelope>();
        }
    }
}
