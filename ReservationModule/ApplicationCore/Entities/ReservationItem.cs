using ItemModule.ApplicationCore.Entities;

namespace ReservationModule.ApplicationCore.Entities
{
    public class ReservationItem
    {
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

        public string CabinetNumber { get; set; }
        public string CabinetName { get; set; }
    }
}
