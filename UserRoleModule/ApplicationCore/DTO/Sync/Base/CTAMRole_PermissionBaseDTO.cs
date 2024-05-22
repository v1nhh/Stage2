using System;

namespace UserRoleModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMRole_PermissionBaseDTO
    {
        public int CTAMRoleID { get; set; }

        public int CTAMPermissionID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
