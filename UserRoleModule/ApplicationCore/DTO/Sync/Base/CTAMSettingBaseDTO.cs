using System;
using CTAM.Core.Enums;

namespace UserRoleModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMSettingBaseDTO
    {
        public int ID { get; set; }

        public CTAMModule CTAMModule { get; set; }

        public string ParName { get; set; }

        public string ParValue { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
