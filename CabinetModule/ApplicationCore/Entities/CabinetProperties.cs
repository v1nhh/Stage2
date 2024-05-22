using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetProperties
    {
        [Key]
        public string CabinetNumber { get; set; }

        public string LocalApiVersion { get; set; }

        public string LocalUiVersion { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [JsonIgnore]
        public Cabinet Cabinet { get; set; }
    }
}
