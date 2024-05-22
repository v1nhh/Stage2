using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetCellTypeProfile : Profile
    {
        public CabinetCellTypeProfile()
        {
            CreateMap<CabinetCellType, CabinetCellTypeDTO>().ReverseMap();
        }
    }
}
