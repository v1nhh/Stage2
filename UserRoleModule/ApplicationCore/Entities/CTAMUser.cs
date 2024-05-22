using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMUser
    {
        public string UID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [Required]
        public string Name { get; set; }

        public string LoginCode { get; set; }

        public string PinCode { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public bool IsPasswordTemporary { get; set; }

        public string CardCode { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        public ICollection<CTAMUser_Role> CTAMUser_Roles { get; set; }

        public DateTime? LastSyncTimeStamp { get; set; }

        public int BadLoginAttempts { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }
}
