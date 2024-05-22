using System;
using System.Collections.Generic;

namespace UserRoleModule.ApplicationCore.DTO.Web
{
    // This DTO is used for fetching roles specifically(i.e. when not part of the user object)
    public class RoleWebDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFromDT { get; set; }

        public DateTime? ValidUntilDT { get; set; }

        public bool IsChecked { get; set; }

        public ICollection<PermissionWebDTO> Permissions { get; set; }
    }
}
