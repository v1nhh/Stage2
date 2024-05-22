using CloudAPI.ApplicationCore.DTO.Integration;
using System.Threading.Tasks;

namespace CloudAPI.Integrations.NationalePolitie.Requests
{
    public class RepairedRequest : GenericRequest
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