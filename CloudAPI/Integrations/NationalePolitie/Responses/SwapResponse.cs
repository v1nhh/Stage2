using CloudAPI.ApplicationCore.DTO.Integration;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.Integrations.NationalePolitie.Responses
{
    public class SwapResponse : GenericResponse, IRequest
    {
    }

    public class SwapResponseHandler : IRequestHandler<SwapResponse>
    {
        private readonly ILogger<SwapResponseHandler> _logger;
        private readonly ITenantService _tenantService;

        public SwapResponseHandler(ILogger<SwapResponseHandler> logger, ITenantService tenantService)
        {
            _logger = logger;
            _tenantService = tenantService;
        }

        public async Task<Unit> Handle(SwapResponse response, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().FullName} called");

            var dbConnection = _tenantService.GetConnectionStringForTenant(response.TenantID);
            if (!string.IsNullOrEmpty(dbConnection))
            {
                using (var dbContext = _tenantService.GetDbContext(dbConnection))
                {
                    var requestFromDb = await dbContext.Request().Where(r => r.ID == response.RequestOrigin.ID).AsTracking().FirstAsync();
                    requestFromDb.Status = RequestStatus.Closed;
                    await dbContext.SaveChangesAsync();
                }
            }
            return new Unit();
        }
    }
}
