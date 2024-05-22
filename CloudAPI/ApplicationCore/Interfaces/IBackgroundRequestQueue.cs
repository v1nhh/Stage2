using CommunicationModule.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CloudAPI.Services.BackgroundRequestQueue;

namespace CloudAPI.ApplicationCore.Interfaces
{
    public interface IBackgroundRequestQueue
    {
        void QueueRequests(List<Request> request, string tenantID);

        Task<TenantRequestPair> TryDequeueAsync(CancellationToken cancellationToken);
    }
}
