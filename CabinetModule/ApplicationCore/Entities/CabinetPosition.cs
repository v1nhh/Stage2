using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetPosition
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CabinetNumber { get; set; }

        public int PositionNumber { get; set; }

        public string PositionAlias { get; set; }

        public PositionType PositionType { get; set; }

        public int CabinetCellTypeID { get; set; }

        public int BladeNo { get; set; }

        public int BladePosNo { get; set; }

        public int? CabinetDoorID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int MaxNrOfItems { get; set; }

        public bool IsAllocated { get; set; }

        public CabinetPositionStatus Status { get; set; }

        [JsonIgnore]
        public Cabinet Cabinet { get; set; }

        [JsonIgnore]
        public CabinetDoor CabinetDoor { get; set; }

        [JsonIgnore]
        public CabinetCellType CabinetCellType { get; set; }
    }
}
