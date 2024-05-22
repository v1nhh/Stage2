using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Sync.Base;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetLiveSyncDataProfile : Profile
    {
        public CabinetLiveSyncDataProfile()
        {
            CreateMap<CTAMRole_Cabinet, CTAMRole_CabinetBaseDTO>();
            CreateMap<CabinetAccessInterval, CabinetAccessIntervalsBaseDTO>();
        }
    }
}
