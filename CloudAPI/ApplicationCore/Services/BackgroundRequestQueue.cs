using CloudAPI.ApplicationCore.Interfaces;
using CommunicationModule.ApplicationCore.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.Services
{
    public class BackgroundRequestQueue : IBackgroundRequestQueue
    {
        private ConcurrentQueue<TenantRequestPair> _queue;
        private int _isWaitingToDequeue = 0;
        public SemaphoreSlim _signal = new SemaphoreSlim(0);

        public class TenantRequestPair
        {
            public string TenantID { get; set; }
            public Request Request { get; set; }
        }

        public BackgroundRequestQueue()
        {
            _queue = new ConcurrentQueue<TenantRequestPair>();
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

        public void QueueRequests(List<Request> requests, string tenantID)
        {
            foreach (var r in requests)
            {
                var pair = new TenantRequestPair { TenantID = tenantID, Request = r };
                if (!_queue.Where(p => p.TenantID == pair.TenantID && p.Request.ID == pair.Request.ID).Any())
                {
                    _queue.Enqueue(pair);
                    _signal.Release();
                }
            }
        }

        public async Task<TenantRequestPair> TryDequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var tenantRequestPair);
            return tenantRequestPair;
        }
    }
}
