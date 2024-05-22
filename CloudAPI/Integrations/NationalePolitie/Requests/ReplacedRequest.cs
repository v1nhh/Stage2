using System.Threading.Tasks;
using CloudAPI.ApplicationCore.DTO.Integration;

namespace CloudAPI.Integrations.NationalePolitie.Requests
{
    public class ReplacedRequest : GenericRequest
    {
        public override Task CollectDataAsync()
        {
            return Task.CompletedTask;
        }

        public override string GetJsonBody()
        {
            throw new System.NotImplementedException();
        }
    }
}