using CsvHelper.Configuration;

namespace ItemModule.ApplicationCore.DTO.Import
{
    public class ErrorCodeImportDTO
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }

    public sealed class ErrorCodeImportDTOMap : ClassMap<ErrorCodeImportDTO>
    {
        public ErrorCodeImportDTOMap()
        {
            Map(m => m.Code);
            Map(m => m.Description);
        }
    }
}
