namespace CloudAPI.ApplicationCore.DTO
{
    public class CTAMSettingResult
    {
        public CTAMSettingResult()
        {
            Status = CTAMSettingResultStatus.OK;
        }

        public string SettingValue { get; set; }
        public CTAMSettingResultStatus Status { get; set; }
    }

    public enum CTAMSettingResultStatus
    {
        OK = 0,
        NotFound = 1
    }
}
