using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.DTO.Web
{
    public class UserPersonalItemWebDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int? ReplacementItemID { get; set; }
        public Item ReplacementItem { get; set; }

        public string CabinetNumber { get; set; }
        public string CabinetName { get; set; }

        public UserPersonalItemStatus Status { get; set; }

        public bool IsChecked { get; set; }
    }
}
