using RestSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CommunicationModule.ApplicationCore.Entities
{
    public class APISetting
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string TriggerName { get; set; }

        public string RequestT { get; set; }

        public string ResponseT { get; set; }

        [Required]
        public string API_URL { get; set; }

        public Dictionary<string, string> API_HEADERS { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, JsonElement> API_BODY { get; set; } = new Dictionary<string, JsonElement>();

        [Required]
        public Method CrudOperation  { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool HasAuthentication { get; set; }

        public string AuthenticationTriggerName { get; set; }

        public string IntegrationSystem { get; set; }
    }
}
