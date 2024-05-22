using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.DTO.Sync.Base;


namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetCriticalDataProfile : Profile
    {
        public CabinetCriticalDataProfile()
        {
            CreateMap<Cabinet, CabinetBaseDTO>();
            CreateMap<CabinetCell, CabinetCellBaseDTO>();
            CreateMap<CabinetCellType, CabinetCellTypeBaseDTO>();
            CreateMap<CabinetDoor, CabinetDoorBaseDTO>();
            CreateMap<CabinetUI, CabinetUIBaseDTO>();
            CreateMap<CabinetColumn, CabinetColumnBaseDTO>();
            CreateMap<CabinetPosition, CabinetPositionBaseDTO>();
            CreateMap<CabinetAccessInterval, CabinetAccessIntervalsBaseDTO>();
            CreateMap<CTAMRole_Cabinet, CTAMRole_CabinetBaseDTO>(); 
        }
    }
}
