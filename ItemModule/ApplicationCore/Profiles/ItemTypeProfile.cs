using System.Linq;
using AutoMapper;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ItemTypeProfile : Profile
    {
        public ItemTypeProfile()
        {
            CreateMap<ItemType, ItemTypeDTO>()
                .ForMember(dto => dto.ErrorCodes, opt => opt.MapFrom(src => src.ItemType_ErrorCode.Select(cr => cr.ErrorCode)));



            CreateMap<ItemTypeImportReturnDTO, ItemType>();
            CreateMap<ItemTypeImportDTO, ItemTypeImportReturnDTO>();

            CreateMap<ItemType, ItemTypeWebDTO>();
        }
    }
}
