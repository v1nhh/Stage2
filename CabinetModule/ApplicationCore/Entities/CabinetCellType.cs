using System;
using System.ComponentModel.DataAnnotations;
using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetCellType
    {
        [Key]
        public int ID { get; set; }

        public string SpecCode { get; set; }

        public string ShortDescr { get; set; }

        public string LongDescr { get; set; }

        public string Picture { get; set; }

        public SpecType SpecType { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Material { get; set; }

        public string Color { get; set; }

        public string LockType { get; set; }

        public string Reference { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
