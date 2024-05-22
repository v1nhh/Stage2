using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAMSharedLibrary.Resources;
using CTAMSharedLibrary.Utilities;
using System;

namespace CabinetModule.ApplicationCore.Profiles
{
    public class CabinetActionProfile : Profile
    {
        private string GetLogString(CabinetAction cabinetAction)
        {
            var resource = LocalTranslations.ResourceManager.GetString(cabinetAction.LogResourcePath);
            if (string.IsNullOrEmpty(resource))
            {
                return cabinetAction.LogResourcePath;
            }
            var log = TranslationUtils.GetTranslation(resource,
                                                            ("cabinetNumber", cabinetAction.CabinetNumber),
                                                            ("cabinetName", cabinetAction.CabinetName),
                                                            ("positionAlias", cabinetAction.PositionAlias),
                                                            ("ctamUserUID", cabinetAction.CTAMUserUID),
                                                            ("ctamUserName", cabinetAction.CTAMUserName),
                                                            ("ctamUserEmail", cabinetAction.CTAMUserEmail),
                                                            ("takeItemDescription", cabinetAction.TakeItemDescription),
                                                            ("putItemDescription", cabinetAction.PutItemDescription),
                                                            ("errorCodeDescription", !string.IsNullOrEmpty(cabinetAction.ErrorCodeDescription) ? cabinetAction.ErrorCodeDescription : CloudTranslations.general_none)
            );
            return log;
        }

        public CabinetActionProfile()
        {
            CreateMap<CabinetAction, CabinetActionDTO>()
                .ForMember(dto => dto.Log, opt => opt.MapFrom(src => GetLogString(src)))
                .ForMember(dto => dto.ActionDT, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ActionDT, DateTimeKind.Utc)))
                .ForMember(dto => dto.UpdateDT, opt => opt.MapFrom(src => src.UpdateDT.HasValue ? DateTime.SpecifyKind((DateTime)src.UpdateDT, DateTimeKind.Utc) : (DateTime?)null));
            CreateMap<CabinetActionDTO, CabinetAction>();
        }
    }
}
