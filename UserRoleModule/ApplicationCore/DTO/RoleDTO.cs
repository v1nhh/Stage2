using System;
using System.Collections.Generic;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class RoleDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFromDT { get; set; }

        public DateTime? ValidUntilDT { get; set; }

        public IEnumerable<PermissionDTO> Permissions { get; set; }

    }
}
