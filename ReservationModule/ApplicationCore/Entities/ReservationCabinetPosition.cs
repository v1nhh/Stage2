using CabinetModule.ApplicationCore.Entities;

namespace ReservationModule.ApplicationCore.Entities
{
    public class ReservationCabinetPosition
    {
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

        public int CabinetPositionID { get; set; }
        public CabinetPosition CabinetPosition { get; set; }
    }
}
