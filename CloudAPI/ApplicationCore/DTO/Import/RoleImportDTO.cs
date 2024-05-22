using CsvHelper.Configuration;
using ItemModule.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.DTO.Import
{
    public class RoleImportDTO
    {
        public string Description { get; set; }

        [JsonPropertyName("validFromDT")]
        public string ValidFromDTString { get; set; }
        [JsonIgnore]
        public DateTime? ValidFromDT { get; set; }

        [JsonPropertyName("validUntilDT")]
        public string ValidUntilDTString { get; set; }
        [JsonIgnore]
        public DateTime? ValidUntilDT { get; set; }

        [JsonPropertyName("cTAMUsers")]
        public string CTAMUsersString { get; set; }
        [JsonIgnore]
        public ICollection<CTAMUser> CTAMUsers { get; set; }

        [JsonPropertyName("itemTypeDescriptions")]
        public string ItemTypeDescriptions { get; set; }
        [JsonIgnore]
        public ICollection<ItemType> ItemTypes { get; set; }

        [JsonPropertyName("permissions")]
        public string PermissionsString { get; set; }
        [JsonIgnore]
        public ICollection<CTAMPermission> Permissions { get; set; }

    }

    public sealed class RoleImportDTOMap : ClassMap<RoleImportDTO>
    {
        public RoleImportDTOMap()
        {
            Map(m => m.Description);
            Map(m => m.ValidFromDTString).Name("ValidFromDT");
            Map(m => m.ValidFromDT).Ignore();
            Map(m => m.ValidUntilDTString).Name("ValidUntilDT");
            Map(m => m.ValidUntilDT).Ignore();
            Map(m => m.CTAMUsers).Ignore();
            Map(m => m.CTAMUsersString).Name("CTAMUsers");
            Map(m => m.ItemTypes).Ignore();
            Map(m => m.ItemTypeDescriptions);
            Map(m => m.Permissions).Ignore();
            Map(m => m.PermissionsString).Name("Permissions");
        }
    }
}
