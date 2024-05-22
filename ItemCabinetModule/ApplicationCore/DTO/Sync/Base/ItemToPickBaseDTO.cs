using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class ItemToPickBaseDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public int ItemID { get; set; }

        public int CabinetPositionID { get; set; }

        public ItemToPickStatus Status { get; set; }
    }
}
