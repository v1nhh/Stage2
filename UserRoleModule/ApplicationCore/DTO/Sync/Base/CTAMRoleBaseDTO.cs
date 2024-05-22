using System;

namespace UserRoleModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMRoleBaseDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFromDT { get; set; }

        public DateTime? ValidUntilDT { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
