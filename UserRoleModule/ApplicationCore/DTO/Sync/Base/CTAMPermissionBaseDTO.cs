using System;
using CTAM.Core.Enums;

namespace UserRoleModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMPermissionBaseDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public CTAMModule CTAMModule { get; set; }
    }
}
