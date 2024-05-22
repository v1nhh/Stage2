using CsvHelper.Configuration;
using ItemModule.ApplicationCore.Entities;
using System.Text.Json.Serialization;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.DTO.Import
{
    public class ItemImportDTO
    {
        [JsonIgnore]
        public int ID { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public ItemType ItemType { get; set; }

        [JsonIgnore]
        public Item DbItem { get; set; }

        public string ItemTypeDescription { get; set; }

        [JsonPropertyName("tagNumber")]
        public string Tagnumber { get; set; }

        [JsonPropertyName("cTAMUserPersonalItem")]
        public string CTAMUserPersonalItemString { get; set; }
        [JsonIgnore]
        public CTAMUser CTAMUserForCreatingPersonalItem { get; set; }

        [JsonPropertyName("cTAMUserInPossession")]
        public string CTAMUserInPossessionString { get; set; }

        [JsonPropertyName("externalReferenceID")]
        public string ExternalReferenceID { get; set; }

        [JsonIgnore]
        public CTAMUser CTAMUserForCreatingPossession { get; set; }
    }


    public sealed class ItemImportDTOMap : ClassMap<ItemImportDTO>
    {
        public ItemImportDTOMap()
        {
            Map(m => m.Description);
            Map(m => m.ItemTypeDescription);
            Map(m => m.ItemType).Ignore();
            Map(m => m.Tagnumber).Name("TagNumber");
            Map(m => m.ExternalReferenceID).Name("ExternalReferenceID");
            Map(m => m.CTAMUserPersonalItemString).Name("CTAMUserPersonalItem");
            Map(m => m.CTAMUserForCreatingPersonalItem).Ignore();
            Map(m => m.CTAMUserInPossessionString).Name("CTAMUserInPossession");
            Map(m => m.CTAMUserForCreatingPossession).Ignore();
        }
    }
}
