using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetColumnProfile : Profile
    {
        public CabinetColumnProfile()
        {
            CreateMap<CabinetColumn, CabinetColumnDTO>().ReverseMap();
        }
    }
}
