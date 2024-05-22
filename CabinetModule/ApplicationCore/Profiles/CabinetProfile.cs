using System.Linq;
using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.DTO.Web;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetProfile : Profile
    {
        public CabinetProfile()
        {
            CreateMap<Cabinet, CabinetDTO>()
                .ForMember(dto => dto.Roles, opt => opt.MapFrom(src => src.CTAMRole_Cabinets.Select(cr => cr.CTAMRole).AsQueryable()))
                .ReverseMap();

            CreateMap<Cabinet, CabinetWebDTO>();

            CreateMap<CabinetWebDTO, Cabinet>();
        }
    }
}
