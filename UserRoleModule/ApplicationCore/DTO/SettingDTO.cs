using CTAM.Core.Enums;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class SettingDTO
    {
        public int ID { get; set; }

        public CTAMModule CTAMModule { get; set; }

        public string ParName { get; set; }

        public string ParValue { get; set; }

    }
}
