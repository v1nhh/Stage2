using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetCell
    {
        [Key]
        public int ID { get; set; }

        public int CabinetColumnID { get; set; }

        public int CabinetCellTypeID { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }


        [JsonIgnore]
        public CabinetColumn CabinetColumn { get; set; }

        [JsonIgnore]
        public CabinetCellType CabinetCellType { get; set; }
    }
}
