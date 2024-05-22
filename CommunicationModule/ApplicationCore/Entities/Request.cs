using CommunicationModule.ApplicationCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommunicationModule.ApplicationCore.Entities
{
    public class Request
    {
        [Key]
        public int ID { get; set; }

        public string ExternalRequestID { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        [Required]
        public RequestStatus Status { get; set; }

        public string EntityType { get; set; }

        public int EntityID { get; set; }

        [Required]
        public int APISettingID { get; set; }

        public APISetting APISetting { get; set; }

        public int? ReferredRequestID { get; set; }

        public Request ReferredRequest { get; set; }

        public int RetryCount { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

    }
}
