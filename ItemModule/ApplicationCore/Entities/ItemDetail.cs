using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemModule.ApplicationCore.Entities
{
    public class ItemDetail
    {
        [Key]
        public int ID { get; set; }

        public int ItemID { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }

        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public string FreeText1 { get; set; }

        public string FreeText2 { get; set; }

        public string FreeText3 { get; set; }

        public string FreeText4 { get; set; }

        public string FreeText5 { get; set; }
    }
}
