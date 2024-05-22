using AutoMapper;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Profiles
{
    public class ErrorCodeProfile : Profile
    {
        public ErrorCodeProfile()
        {
            CreateMap<ErrorCode, ErrorCodeDTO>()
                .ReverseMap();

            CreateMap<ErrorCode, ErrorCodeWebDTO>()
                .ReverseMap();

            CreateMap<ErrorCodeImportReturnDTO, ErrorCode>();
            CreateMap<ErrorCodeImportDTO, ErrorCodeImportReturnDTO>();
        }
    }
}
