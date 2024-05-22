using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetPropertiesProfile : Profile
    {
        public CabinetPropertiesProfile()
        {
            CreateMap<CabinetProperties, CabinetPropertiesDTO>()
                .ReverseMap();
        }
    }
}
