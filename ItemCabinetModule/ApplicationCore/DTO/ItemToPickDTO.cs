using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO
{
    public class ItemToPickDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public int ItemID { get; set; }

        public int CabinetPositionID { get; set; }

        public ItemToPickStatus Status { get; set; }
    }
}
