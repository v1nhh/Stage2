using CTAM.Core.Interfaces;

namespace CloudAPI.ApplicationCore.DTO.Import
{
    public class RoleImportReturnDTO : RoleImportDTO, IImportError
    {
        public string ErrorMessage { get; set; }
    }
}