using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetAccessIntervalsProfile : Profile
    {
        public CabinetAccessIntervalsProfile()
        {
            CreateMap<CabinetAccessInterval, CabinetAccessIntervalDTO>()
                .ForMember(dto => dto.RoleID, opt => opt.MapFrom(src => src.CTAMRoleID))
                .ReverseMap();
        }
    }
}
