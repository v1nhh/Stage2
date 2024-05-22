using CabinetModule.ApplicationCore.DTO;

namespace ReservationModule.ApplicationCore.DTO
{
    public class ReservationCabinetPositionDTO
    {
        public int ReservationID { get; set; }
        public ReservationDTO Reservation { get; set; }

        public int CabinetPositionID { get; set; }
        public CabinetPositionDTO CabinetPosition { get; set; }
    }
}
