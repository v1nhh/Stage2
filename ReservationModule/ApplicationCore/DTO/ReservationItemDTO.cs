using ItemModule.ApplicationCore.DTO;

namespace ReservationModule.ApplicationCore.DTO
{
    public class ReservationItemDTO
    {
        public int ReservationID { get; set; }
        public ReservationDTO Reservation { get; set; }

        public int ItemID { get; set; }
        public ItemDTO Item { get; set; }

        public string CabinetNumber { get; set; }
        public string CabinetName { get; set; }
    }
}
