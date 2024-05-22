namespace CabinetModule.Web.Security
{
    public class CabinetAuthenticationRequest
    {
        public string CabinetNumber { get; set; }
        public string ApiKey { get; set; }
        // Deprecated ?
        public string SignalRConnectionId { get; set; }
    }
}
