using AutoMapper;
using CloudAPI.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.Profiles
{
    public class ItemImportProfile : Profile
    {
        public ItemImportProfile()
        {
            CreateMap<ItemImportReturnDTO, Item>()
                  .ForMember(entity => entity.ExternalReferenceID, opt => opt.MapFrom(src => src.ExternalReferenceID)).ReverseMap(); 
            CreateMap<ItemImportDTO, ItemImportReturnDTO>();
        }
    }
}
