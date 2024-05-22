using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetColumn
    {
        [Key]
        public int ID { get; set; }

        public string CabinetNumber { get; set; }

        public int ColumnNumber { get; set; }

        public string TemplateName { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public bool IsTemplate { get; set; }

        [JsonIgnore]
        public Cabinet Cabinet { get; set; }
    }
}
