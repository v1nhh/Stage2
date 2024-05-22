using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class AllowedCabinetPositionProfile: Profile
    {
        public AllowedCabinetPositionProfile()
        {
            CreateMap<AllowedCabinetPosition, AllowedCabinetPositionDTO>()
                .ReverseMap();
        }
    }
}
