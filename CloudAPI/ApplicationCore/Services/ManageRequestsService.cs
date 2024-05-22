using CloudAPI.ApplicationCore.Interfaces;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.ApplicationCore.Services
{
    /// <summary>
    /// Responsible for fetching Requests out of the database and enqueueing them into the singleton BackgroundRequestQueue.
    /// </summary>
    public class ManageRequestsService : IHostedService, IDisposable
    {
        private readonly Logger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        private readonly IBackgroundRequestQueue _queue;
        private readonly IConfiguration _config;
        private readonly ITenantService _tenantService;
        private Timer _timer1 = null;
        private Timer _timer2 = null;
        private readonly int _enqueueRequestsIntervalInSeconds;
        private readonly int _enqueueFailedRequestsIntervalInSeconds;
        private readonly int _maxRetryCountFailedRequests;


        public ManageRequestsService(IServiceProvider serviceProvider, IBackgroundRequestQueue queue, IConfiguration config, ITenantService tenantService)
        {
            _queue = queue;
            _config = config;
            _tenantService = tenantService;
            _enqueueRequestsIntervalInSeconds = _config.GetValue<int>("EnqueueRequestsIntervalInSeconds", 15);
            _enqueueFailedRequestsIntervalInSeconds = _config.GetValue<int>("EnqueueFailedRequestsIntervalInSeconds", 600);
            _maxRetryCountFailedRequests = _config.GetValue<int>("MaxRetryCountFailedRequests", 10);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer1 = new Timer(EnqueueRequests, null, TimeSpan.Zero, TimeSpan.FromSeconds(_enqueueRequestsIntervalInSeconds));
            _timer2 = new Timer(EnqueueFailedRequests, null, TimeSpan.Zero, TimeSpan.FromSeconds(_enqueueFailedRequestsIntervalInSeconds));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        /// <summary>
        /// Enqueue all requests with created status.
        /// </summary>
        /// <param name="state"></param>
        private void EnqueueRequests(object state)
        {
            try
            {
                var tenantConnections = _tenantService.GetTenantConnections();
                foreach (var tenantConnection in tenantConnections)
                {
                    EnqueueRequestsForTenant(tenantConnection);
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, $"Stopped {nameof(EnqueueRequests)} because of exception.");
                throw;
            }
        }

        /// <summary>
        /// Enqueue all requests with failed status and retry count between 3(initial retry count) and _maxRetryCountFailedRequests
        /// </summary>
        /// <param name="state"></param>
        private void EnqueueFailedRequests(object state)
        {
            try
            {
                var tenantConnections = _tenantService.GetTenantConnections();
                foreach (var tenantConnection in tenantConnections)
                {
                    EnqueueFailedRequestsForTenant(tenantConnection);
                }

            }
            catch (Exception e)
            {
                _logger.Error(e, $"Stopped {nameof(EnqueueFailedRequests)} because of exception.");
                throw;
            }
        }

        private void EnqueueRequestsForTenant(KeyValuePair<string, string> tenantConnection)
        {
            try
            {
                using (var dbContext = _tenantService.GetDbContext(tenantConnection.Value))
                {
                    var requestsToEnqueue = dbContext.Request().Include(r => r.APISetting)
                        .Where(r => r.Status == RequestStatus.Created).OrderBy(r => r.CreateDT).ToList();
                    _queue.QueueRequests(requestsToEnqueue, tenantConnection.Key);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error enqueueing requests for tenant {tenantConnection.Key}");
            }
        }

        private void EnqueueFailedRequestsForTenant(KeyValuePair<string, string> tenantConnection)
        {
            try
            {
                using (var dbContext = _tenantService.GetDbContext(tenantConnection.Value))
                {
                    var failedRequestsToEnqueue = dbContext.Request().Include(r => r.APISetting)
                        .Where(r => r.Status == RequestStatus.Failed && r.RetryCount < _maxRetryCountFailedRequests).OrderBy(r => r.CreateDT).ToList();
                   _queue.QueueRequests(failedRequestsToEnqueue, tenantConnection.Key);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error enqueueing failed requests for tenant {tenantConnection.Key}");
            }
        }

        public void Dispose()
        {
            _timer1?.Dispose();
            _timer2?.Dispose();
        }
    }
}
