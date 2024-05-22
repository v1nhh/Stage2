using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Web;
using CabinetModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class CabinetPositionWithSpecCodeProfile : Profile
    {
        public CabinetPositionWithSpecCodeProfile()
        {
            CreateMap<CabinetPosition, CabinetPositionWithSpecCodeDTO>()
                .ForMember(dto => dto.SpecCode, opt => opt.MapFrom(src => src.CabinetCellType.SpecCode))
                .ReverseMap();
        }
    }
}
