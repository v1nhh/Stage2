using System;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.DTO.Sync.Base
{
    public class ItemTypeBaseDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public TagType TagType { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public bool IsStoredInLocker { get; set; }

        public bool RequiresMileageRegistration { get; set; }
    }
}
