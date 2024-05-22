using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetDoorProfile : Profile
    {
        public CabinetDoorProfile()
        {
            CreateMap<CabinetDoor, CabinetDoorDTO>().ReverseMap();
        }
    }
}
