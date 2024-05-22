using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.DTO.SignalR
{
    public class CabinetStatusUpdateDTO
    {
        public string CabinetNumber { get; set; }
        public CabinetStatus Status { get; set; }

        public CabinetStatusUpdateDTO(string cabinetNumber, CabinetStatus cabinetStatus)
        {
            CabinetNumber = cabinetNumber;
            Status = cabinetStatus;
        }
    }
}
