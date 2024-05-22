using System.Collections.Generic;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.DTO
{
    public class ItemTypeDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public TagType TagType { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public bool IsStoredInLocker { get; set; }

        public bool RequiresMileageRegistration { get; set; }
        
        public ICollection<ErrorCodeDTO> ErrorCodes { get; set; }
    }
}
