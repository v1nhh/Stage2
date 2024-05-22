using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CabinetModule.Services.FullSyncQueue;

namespace CloudAPI.ApplicationCore.Interfaces
{
    public interface IFullSyncQueue
    {
        bool IsWaitingToDequeue { get; set; }
        void QueueCabinetNumbers(List<string> cabinetNumbers, string tenantID);
        Task<TenantCabinetPair> TryDequeueAsync(CancellationToken cancellationToken);
    }
}