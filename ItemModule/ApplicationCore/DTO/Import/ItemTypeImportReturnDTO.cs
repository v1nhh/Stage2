using CTAM.Core.Interfaces;

namespace ItemModule.ApplicationCore.DTO.Import
{
    public class ItemTypeImportReturnDTO : ItemTypeImportDTO, IImportError
    {
        public string ErrorMessage { get; set; }
    }
}
