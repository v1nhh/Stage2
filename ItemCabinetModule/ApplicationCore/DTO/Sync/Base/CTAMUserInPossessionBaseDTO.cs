using ItemCabinetModule.ApplicationCore.Enums;
using System;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMUserInPossessionBaseDTO
    {
        public Guid ID { get; set; }

        public int ItemID { get; set; }

        public string CTAMUserUIDOut { get; set; }

        public string CTAMUserNameOut { get; set; }

        public string CTAMUserEmailOut { get; set; }

        public DateTime? OutDT { get; set; }

        public int? CabinetPositionIDOut { get; set; }

        public string CabinetNumberOut { get; set; }

        public string CabinetNameOut { get; set; }

        public string CTAMUserUIDIn { get; set; }

        public string CTAMUserNameIn { get; set; }

        public string CTAMUserEmailIn { get; set; }

        public DateTime? InDT { get; set; }

        public int? CabinetPositionIDIn { get; set; }

        public string CabinetNumberIn { get; set; }

        public string CabinetNameIn { get; set; }

        public DateTime? ReturnBeforeDT { get; set; }

        public UserInPossessionStatus Status { get; set; }

        public DateTime CreatedDT { get; set; }
    }
}
