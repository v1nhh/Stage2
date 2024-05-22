using System;
using System.ComponentModel.DataAnnotations;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Entities
{
    public class CTAMUserInPossession
    {
        [Key]
        public Guid ID { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }

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

        public DateTime? LastSyncTimeStamp { get; set; }

        public DateTime CreatedDT { get; set; }

    }
}
