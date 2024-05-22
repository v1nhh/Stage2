using System.Collections.Generic;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class UserDTO
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string LoginCode { get; set; }

        public string PinCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public bool IsPasswordTemporary { get; set; }

        public string CardCode { get; set; }

        public string LanguageCode { get; set; }

        public ICollection<RoleDTO> Roles { get; set; }
    }
}
