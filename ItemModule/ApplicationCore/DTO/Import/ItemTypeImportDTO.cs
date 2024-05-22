using CsvHelper.Configuration;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ItemModule.ApplicationCore.DTO
{
    public class ItemTypeImportDTO
    {
        [JsonPropertyName("itemTypeDescription")]
        public string Description { get; set; }

        [JsonPropertyName("tagType")]
        public string TagTypeString { get; set; }
        [JsonIgnore]
        public TagType TagType { get; set; }

        [JsonPropertyName("depth")]
        public string DepthString { get; set; }
        [JsonIgnore]
        public double Depth { get; set; }

        [JsonPropertyName("width")]
        public string WidthString { get; set; }
        [JsonIgnore]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public string HeightString { get; set; }
        [JsonIgnore]
        public double Height { get; set; }

        [JsonPropertyName("errorCodes")]
        public string ErrorCodesString { get; set; }

        [JsonIgnore]
        public ICollection<ErrorCode> ErrorCodes { get; set; }
    }

    public sealed class ItemTypeImportDTOMap : ClassMap<ItemTypeImportDTO>
    {
        public ItemTypeImportDTOMap()
        {
            Map(m => m.Description).Name("ItemTypeDescription");
            Map(m => m.TagTypeString).Name("TagType");
            Map(m => m.TagType).Ignore();
            Map(m => m.DepthString).Name("Depth");
            Map(m => m.Depth).Ignore();
            Map(m => m.WidthString).Name("Width");
            Map(m => m.Width).Ignore();
            Map(m => m.HeightString).Name("Height");
            Map(m => m.Height).Ignore();
            Map(m => m.ErrorCodesString).Name("ErrorCodes");
            Map(m => m.ErrorCodes).Ignore();
        }
    }
}
