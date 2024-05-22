using CTAM.Core.Interfaces;

namespace ItemModule.ApplicationCore.DTO.Import
{
    public class ErrorCodeImportReturnDTO : ErrorCodeImportDTO, IImportError
    {
        public string ErrorMessage { get; set; }
    }
}
