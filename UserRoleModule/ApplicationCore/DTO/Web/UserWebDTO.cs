using System.Collections.Generic;

namespace UserRoleModule.ApplicationCore.DTO.Web
{
    public class UserWebDTO
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string LoginCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsPasswordTemporary { get; set; }

        public string CardCode { get; set; }

        public string LanguageCode { get; set; }

        public bool IsChecked { get; set; }

        public ICollection<UserRoleWebDTO> Roles { get; set; }

        public int BadLoginAttempts { get; set; }
    }
}
