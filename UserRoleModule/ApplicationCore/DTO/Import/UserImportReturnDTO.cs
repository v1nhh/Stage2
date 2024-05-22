using CTAM.Core.Interfaces;

namespace UserRoleModule.ApplicationCore.DTO.Import
{
    public class UserImportReturnDTO : UserImportDTO, IImportError
    {
        public string ErrorMessage { get; set; }
    }
}
