using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Interfaces;
using CloudAPI.ApplicationCore.Services;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.Services
{
    public class BackgroundFullSyncQueueService : BackgroundService
    {
        private readonly IFullSyncQueue _queue;
        private readonly IMediator _mediator;
        private readonly ILogger<BackgroundFullSyncQueueService> _logger;
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public BackgroundFullSyncQueueService(
            IFullSyncQueue queue,
            IMediator mediator,
            ILogger<BackgroundFullSyncQueueService> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _queue = queue;
            _mediator = mediator;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackgroundFullSyncQueueService is running.");
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            var firstDelay = 15000;
            await Task.Delay(firstDelay, stoppingToken); // Give Api time to start up

            while (!stoppingToken.IsCancellationRequested)
            {
                var pair = await _queue.TryDequeueAsync(stoppingToken);
                // Wait for the configured delay until synchronizing the next cabinetNumber from the queue
                // Also wait for the first item in the queue amount of delay seconds
                if (pair != null)
                {
                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var liveSyncService = scope.ServiceProvider.GetRequiredService<LiveSyncService>();
                            _queue.IsWaitingToDequeue = true;
                            await liveSyncService.SendCabinetAction(
                                pair.TenantID,
                                pair.CabinetNumber,
                                LiveSyncAction.SynchronizeCabinet,
                                new SingleLiveSyncMessage { CabinetNumbers = new List<string> { pair.CabinetNumber } });
                        }
                        var delay = _configuration.GetValue("CabinetsFullSyncDelayInSeconds", 45) * 1000;
                        while(delay > 0)
                        {
                            await Task.Delay(1000, stoppingToken);
                            if(_queue.IsWaitingToDequeue)
                            {
                                delay = delay - 1000;
                            }
                            else
                            {
                                delay = 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error during synchronization: " + ex.GetMostInnerException().Message);
                    }
                }
            }
        }
    }
}