using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemModule.ApplicationCore.Entities
{
    public class ErrorCode
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public ICollection<ItemType_ErrorCode> ItemType_ErrorCode { get; set; }

        [JsonIgnore]
        public ICollection<Item> Items { get; set; }
    }
}
