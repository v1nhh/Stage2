using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CabinetModule.ApplicationCore.Commands.Sync
{
    public class SyncCabinetStatusCommand : IRequest
    {
        public string CabinetNumber { get; set; }

        public CabinetStatus Status { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class SyncCabinetStatusHandler : IRequestHandler<SyncCabinetStatusCommand>
    {
        private readonly ILogger<SyncCabinetStatusHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public SyncCabinetStatusHandler(MainDbContext context, ILogger<SyncCabinetStatusHandler> logger, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(SyncCabinetStatusCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.CabinetNumber == null)
            {
                throw new NullReferenceException("SyncCabinetStatusHandler failed: provided data cannot null");
            }
            if (!Enum.IsDefined(typeof(CabinetStatus), request.Status))
            {
                throw new ArgumentOutOfRangeException("SyncCabinetStatusHandler failed: provided CabinetStatus doesn't exist");
            }
            if (request.ErrorMessage != null)
            {
                _logger.LogError(request.ErrorMessage);
            }
            var cabinet = await _context.Cabinet()
                .Where(c => c.CabinetNumber.Equals(request.CabinetNumber))
                .FirstOrDefaultAsync();

            cabinet.Status = request.Status;

            await _context.SaveChangesAsync();
            var msg = $"IBK '{cabinet.CabinetNumber}' '{cabinet.Name}' status naar '{cabinet.Status.ToString()}'";
            _logger.LogInformation(msg);
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_cabinetStatusChanged), ("cabinetNumber", cabinet.CabinetNumber), ("name", cabinet.Name), ("status", cabinet.Status.ToString()));
            return new Unit();
        }
    }
}
