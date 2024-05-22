using CTAM.Core;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudAPI.ApplicationCore.DTO.Integration
{
    /// <summary>
    /// Concrete classes are used for forming the REST request JSON body.
    /// MainDbContext and Context object are helpers to form these fields but are not serialized in the JSON request body, hence ignored.
    /// </summary>
    public abstract class GenericRequest
    {
        [JsonIgnore]
        public MainDbContext MainDbContext { get; set; }

        [JsonIgnore]
        public object Context { get; set; }

        [JsonIgnore]
        public Dictionary<string, JsonElement> APISettingBody { get; set; }

        public abstract Task CollectDataAsync();

        public abstract string GetJsonBody();

        protected string MergeJsonBodies(string generatedJsonBody, Dictionary<string, JsonElement> apiSettingJsonBody)
        {
            var body = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(generatedJsonBody);
            foreach (var item in apiSettingJsonBody)
            {
                body[item.Key] = item.Value; // APISetting JSON Body is leading in case of duplicates.
            }
            return JsonSerializer.Serialize(body);
        }
    }
}
