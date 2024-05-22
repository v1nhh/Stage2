using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAMSharedLibrary.Resources;
using CTAMSharedLibrary.Utilities;
using System;
using System.Linq;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetLogProfile : Profile
    {
        private string GetLogString(string logResourcePath, string parameters)
        {
            // In case of a rollback. Backwards compatibility for Migration: 20231018141902_CabinetLogLogColumnAndParameters
            if (parameters == null)
            {
                parameters = "[]";
            }
            var resource = LocalTranslations.ResourceManager.GetString(logResourcePath);
            if (string.IsNullOrEmpty(resource))
            {
                return logResourcePath;
            }

            var translation = string.Empty;

            var keyValues = TranslationUtils.Deserialize(parameters).ToList();
            var indexEmergencyUser = keyValues.FindIndex(item => item.value != null && item.value.Equals("Emergency user"));
            // Handle logs with admin panel operational logs
            if (indexEmergencyUser != -1)
            {
                keyValues[indexEmergencyUser] = (keyValues[indexEmergencyUser].key, LocalTranslations.cabinetLog_emergencyUser);
                translation = TranslationUtils.GetTranslation(resource, keyValues.ToArray());
                return translation;
            }

            translation = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
            return translation;
        }

        public CabinetLogProfile()
        {
            CreateMap<CabinetLog, CabinetLogDTO>()
                .ForMember(dto => dto.Log, opt => opt.MapFrom(src => GetLogString(src.LogResourcePath, src.Parameters)))
                .ForMember(dto => dto.LogDT, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LogDT, DateTimeKind.Utc)))
                .ForMember(dto => dto.UpdateDT, opt => opt.MapFrom(src => src.UpdateDT.HasValue ? DateTime.SpecifyKind((DateTime)src.UpdateDT, DateTimeKind.Utc) : (DateTime?)null));
            CreateMap<CabinetLogDTO, CabinetLog>();
        }
    }
}
