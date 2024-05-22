using CloudAPI.ApplicationCore.DTO.Integration;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.Integrations.NationalePolitie.Responses
{
    public class ReplacedResponse : GenericResponse, IRequest
    {
    }

    public class ReplacedResponseHandler : IRequestHandler<ReplacedResponse>
    {
        private readonly ILogger<ReplacedResponseHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ReplacedResponseHandler(ILogger<ReplacedResponseHandler> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(ReplacedResponse response, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().FullName} called");
            using (var scope = _serviceProvider.CreateScope())
            {
                var mainDbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
                var replacedRequest = await mainDbContext.Request().Include(r => r.APISetting).Include(r => r.ReferredRequest).
                    Where(r => r.ID == response.RequestOrigin.ID).FirstOrDefaultAsync();
                replacedRequest.Status = RequestStatus.Closed;

                if (replacedRequest.ReferredRequestID != null)
                {
                    replacedRequest.ReferredRequest.Status = RequestStatus.Closed;
                }
                await mainDbContext.SaveChangesAsync();
            }
            return new Unit();
        }
    }
}
