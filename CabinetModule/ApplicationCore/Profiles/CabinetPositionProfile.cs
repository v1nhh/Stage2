using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetPositionProfile : Profile
    {
        public CabinetPositionProfile()
        {
            CreateMap<CabinetPosition, CabinetPositionDTO>()
                .ReverseMap();
        }
    }
}
