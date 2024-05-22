using System;
using System.ComponentModel.DataAnnotations;
using CabinetModule.ApplicationCore.Enums;
using LocalAPI.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetAction
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string CabinetNumber { get; set; }

        public string CabinetName { get; set; }

        public string PositionAlias { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }

        public DateTime ActionDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public CabinetActionStatus Action { get; set; }

        public string TakeItemDescription { get; set; }
        public int? TakeItemID { get; set; }

        public string PutItemDescription { get; set; }
        public int? PutItemID { get; set; }

        public string ErrorCodeDescription { get; set; }

        [Required]
        public string LogResourcePath { get; set;  }

        public CorrectionStatus? CorrectionStatus { get; set; }
    }
}
