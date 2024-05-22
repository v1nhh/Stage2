using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CloudAPI.ApplicationCore.Commands.Cabinet
{
    public class SynchronizeCabinetCommand : IRequest
    {
        public string CabinetNumber { get; set; }
    }

    public class SynchronizeCabinetHandler : IRequestHandler<SynchronizeCabinetCommand>
    {
        private readonly ILogger<SynchronizeCabinetHandler> _logger;
        private readonly IManagementLogger _managementLogger;
        private readonly LiveSyncService _liveSyncService;

        public SynchronizeCabinetHandler(ILogger<SynchronizeCabinetHandler> logger, IManagementLogger managementLogger, LiveSyncService liveSyncService)
        {
            _logger = logger;
            _managementLogger = managementLogger;
            _liveSyncService = liveSyncService;
        }

        public async Task<Unit> Handle(SynchronizeCabinetCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(SynchronizeCabinetHandler)} is called");
            var message = new MessageWrapper(LiveSyncAction.SynchronizeCabinet, new SingleLiveSyncMessage() { CabinetNumbers = new List<string>() { request.CabinetNumber } });
            await _liveSyncService.SendSingleLiveSyncMessage(message);
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_syncCabinetStarted), ("cabinetNumber", request.CabinetNumber));
            return new Unit();
        }

    }
}