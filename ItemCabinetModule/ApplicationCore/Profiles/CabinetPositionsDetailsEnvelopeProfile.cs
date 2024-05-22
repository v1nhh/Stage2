using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.DTO.Web;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class CabinetPositionsDetailsEnvelopeProfile : Profile
    {
        public CabinetPositionsDetailsEnvelopeProfile()
        {
            CreateMap<CabinetPosition, CabinetPositionsDetailsEnvelope>()
                .ForMember(env => env.CabinetPosition, opt => opt.MapFrom(src => src));
        }
    }
}
