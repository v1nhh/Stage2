using AutoMapper;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using CloudAPI.ApplicationCore.Commands.ItemCabinet;

namespace CloudAPI.ApplicationCore.Profiles
{
    public class CabinetStockProfile : Profile
    {
        public CabinetStockProfile()
        {
            CreateMap<CabinetStock, CabinetStockDTO>()
                .ForMember(dto => dto.ItemTypeDescription, opt => opt.MapFrom(src => src.ItemType.Description));
            CreateMap<CabinetStockDTO, CabinetStock>();
            CreateMap<UpdateMinimalStockCommand, GetCabinetStockQuery>();
        }
    }
}
