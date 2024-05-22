using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.ErrorCodes
{
    public class CheckAndDeleteErrorCodeCommand : IRequest
    {
        public int ID { get; set; }

        public CheckAndDeleteErrorCodeCommand(int id)
        {
            ID = id;
        }
    }

    public class CheckAndDeleteErrorCodeHandler : IRequestHandler<CheckAndDeleteErrorCodeCommand>
    {
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public CheckAndDeleteErrorCodeHandler(MainDbContext context, IManagementLogger managementLogger)
        {
            _context = context;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(CheckAndDeleteErrorCodeCommand request, CancellationToken cancellationToken)
        {
            var errorcode = await _context.ErrorCode().FirstOrDefaultAsync(ec => ec.ID == request.ID);
            if (errorcode == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.errorCodes_apiExceptions_notFound,
                                                                     new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            var item = await _context.Item().FirstOrDefaultAsync(item => item.ErrorCodeID == request.ID);
            if (item != null)
            {
                throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.errorCodes_apiExceptions_usedByItem,
                                                                     new Dictionary<string, string> { { "errorCode", errorcode.Code }, { "itemDescription", item.Description } });
            }

            _context.ErrorCode().Remove(errorcode);
            await _context.SaveChangesAsync();

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdErrorCode), 
                    ("description", errorcode.Description));
            return new Unit();
        }
    }

}
