using AutoMapper;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Entities;
using System;

namespace ItemCabinetModule.ApplicationCore.Profiles
{
    public class UserInPossessionProfile : Profile
    {
        public UserInPossessionProfile()
        {
            CreateMap<CTAMUserInPossession, UserInPossessionDTO>()
                .ForMember(dto => dto.OutDT, opt => opt.MapFrom(src => src.OutDT.HasValue ? DateTime.SpecifyKind((DateTime)src.OutDT, DateTimeKind.Utc) : (DateTime?)null))
                .ForMember(dto => dto.InDT, opt => opt.MapFrom(src => src.InDT.HasValue ? DateTime.SpecifyKind((DateTime)src.InDT, DateTimeKind.Utc) : (DateTime?)null))
                .ForMember(dto => dto.CreatedDT, opt => opt.MapFrom(src => DateTime.SpecifyKind((DateTime)src.CreatedDT, DateTimeKind.Utc)));
            CreateMap<UserInPossessionDTO, CTAMUserInPossession>()
                .ForMember(uip => uip.CreatedDT, opt => opt.MapFrom(src => DateTime.SpecifyKind((DateTime)src.CreatedDT, DateTimeKind.Utc)));
            CreateMap<CTAMUserInPossession, CTAMUserInPossession>();
        }
    }
}
