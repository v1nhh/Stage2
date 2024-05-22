using CloudAPI.ApplicationCore.Interfaces;
using CloudAPI.ApplicationCore.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.Cabinet
{
    public class EnqueueForFullSyncCommand : IRequest
    {
        public List<string> CabinetNumbers { get; set; }
        public string TenantID { get; set; }

    }

    public class EnqueueForFullSyncHandler : IRequestHandler<EnqueueForFullSyncCommand>
    {
        private readonly ILogger<EnqueueForFullSyncHandler> _logger;
        private readonly IManagementLogger _managementLogger;
        private readonly LiveSyncService _liveSyncService;
        private readonly IFullSyncQueue _queue;


        public EnqueueForFullSyncHandler(IFullSyncQueue queue, ILogger<EnqueueForFullSyncHandler> logger, IManagementLogger managementLogger, LiveSyncService liveSyncService)
        {
            _logger = logger;
            _managementLogger = managementLogger;
            _liveSyncService = liveSyncService;
            _queue = queue;
        }

        public async Task<Unit> Handle(EnqueueForFullSyncCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(EnqueueForFullSyncHandler)} is called");
            await Task.Run(() => _queue.QueueCabinetNumbers(request.CabinetNumbers, request.TenantID));
            return new Unit();
        }

    }
}