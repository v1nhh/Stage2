using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.DTO.Web;
using ItemCabinetModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.Profiles
{
    public class UserPersonalItemProfile : Profile
    {
        public UserPersonalItemProfile()
        {
            CreateMap<CTAMUserPersonalItem, UserPersonalItemWebDTO>()
            .ReverseMap();

            CreateMap<CTAMUserPersonalItem, UserPersonalItemDTO>()
                .ReverseMap();
        }
    }
}
