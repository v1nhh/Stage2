using CTAM.Core.Interfaces;

namespace CloudAPI.ApplicationCore.DTO.Import
{
    public class ItemImportReturnDTO : ItemImportDTO, IImportError
    {
        public string ErrorMessage { get; set; } = "";
    }
}