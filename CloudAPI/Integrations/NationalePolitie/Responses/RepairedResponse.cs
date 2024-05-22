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
    public class RepairedResponse : GenericResponse, IRequest
    {
    }

    public class RepairedResponseHandler : IRequestHandler<RepairedResponse>
    {
        private readonly ILogger<RepairedResponseHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RepairedResponseHandler(ILogger<RepairedResponseHandler> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(RepairedResponse response, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{GetType().FullName} called");
            using (var scope = _serviceProvider.CreateScope())
            {
                var mainDbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
                var repairedRequest = await mainDbContext.Request().Include(r => r.APISetting).Include(r => r.ReferredRequest).
                    Where(r => r.ID == response.RequestOrigin.ID).FirstOrDefaultAsync();
                repairedRequest.Status = RequestStatus.Closed;

                if (repairedRequest.ReferredRequestID != null)
                {
                    repairedRequest.ReferredRequest.Status = RequestStatus.Closed;
                }
                await mainDbContext.SaveChangesAsync();
            }
            return new Unit();
        }
    }
}
