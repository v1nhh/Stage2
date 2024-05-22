using CloudAPI.ApplicationCore.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.Services
{
    public class FullSyncQueue : IFullSyncQueue
    {
        private ConcurrentQueue<TenantCabinetPair> _queue;
        private int _isWaitingToDequeue = 0;
        public SemaphoreSlim _signal = new SemaphoreSlim(0);

        public FullSyncQueue()
        {
            _queue = new ConcurrentQueue<TenantCabinetPair>();
        }

        public bool IsWaitingToDequeue
        {
            get => Interlocked.CompareExchange(ref _isWaitingToDequeue, 1, 1) == 1;
            set
            {
                if (value)
                    Interlocked.Exchange(ref _isWaitingToDequeue, 1);
                else
                    Interlocked.Exchange(ref _isWaitingToDequeue, 0);
            }
        }

        public class TenantCabinetPair
        {
            public string TenantID { get; set; }
            public string CabinetNumber { get; set; }
        }

        public void QueueCabinetNumbers(List<string> cabinetNumbers, string tenantID)
        {
            foreach (var c in cabinetNumbers)
            {
                var pair = new TenantCabinetPair { TenantID = tenantID, CabinetNumber = c };
                if (!_queue.Where(p => p.TenantID == pair.TenantID && p.CabinetNumber == pair.CabinetNumber).Any())
                {
                    _queue.Enqueue(pair);
                    _signal.Release();
                }
            }
        }

        public async Task<TenantCabinetPair> TryDequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var workItem);
            return workItem;
        }
    }
}