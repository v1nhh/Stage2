using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class CabinetPositionContentProfile: Profile
    {
        public CabinetPositionContentProfile()
        {
            CreateMap<CabinetPositionContent, CabinetPositionContentDTO>()
                .ReverseMap();
        }
    }
}
