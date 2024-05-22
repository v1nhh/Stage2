using System;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetColumnBaseDTO
    {
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
    }
}
