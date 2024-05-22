using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetCellProfile : Profile
    {
        public CabinetCellProfile()
        {
            CreateMap<CabinetCell, CabinetCellDTO>().ReverseMap();
        }
    }
}
