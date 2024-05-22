using CsvHelper.Configuration;
using System.Text.Json.Serialization;

namespace UserRoleModule.ApplicationCore.DTO.Import
{
    public class UserImportDTO
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string PhoneNumber { get; set; }

        public string CardCode { get; set; }

        [JsonIgnore]
        public string LanguageCode { get; set; }
    }

    public sealed class UserImportDTOMap : ClassMap<UserImportDTO>
    {
        public UserImportDTOMap()
        {
            Map(m => m.UID);
            Map(m => m.Name);
            Map(m => m.Email);
            Map(m => m.PhoneNumber).Optional();
            Map(m => m.CardCode);
            Map(m => m.LanguageCode).Optional().Default("en-US");
        }
    }
}
