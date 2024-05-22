using System;

namespace ItemModule.ApplicationCore.Entities
{
    public class ItemType_ErrorCode
    {
        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }
        public int ErrorCodeID { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public DateTime CreateDT { get; set; }
    }
}
