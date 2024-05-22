using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetUIProfile : Profile
    {
        public CabinetUIProfile()
        {
            CreateMap<CabinetUI, CabinetUIDTO>();
        }
    }
}
