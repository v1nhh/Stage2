using System;

namespace UserRoleModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMUserBaseDTO
    {
        public string UID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public string Name { get; set; }

        public string LoginCode { get; set; }

        public string PinCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public bool IsPasswordTemporary { get; set; }

        public string CardCode { get; set; }

        public string LanguageCode { get; set; }
    }
}
