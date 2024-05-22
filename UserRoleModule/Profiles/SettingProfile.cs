using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class SettingProfile : Profile
    {
        public SettingProfile()
        {
            CreateMap<CTAMSetting, SettingDTO>();
        }
    }
}
