using AutoMapper;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CTAM.Core.Commands.ErrorCodes
{
    public class CheckAndCreateErrorCodeCommand : IRequest<ErrorCodeDTO>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class CheckAndCreateErrorCodeHandler : IRequestHandler<CheckAndCreateErrorCodeCommand, ErrorCodeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckAndCreateErrorCodeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndCreateErrorCodeHandler(MainDbContext context, ILogger<CheckAndCreateErrorCodeHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ErrorCodeDTO> Handle(CheckAndCreateErrorCodeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndCreateErrorCodeHandler called");
            {
                if (string.IsNullOrWhiteSpace(request.Description))
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.errorCodes_apiExceptions_emptyDescription);
                }
                if (string.IsNullOrWhiteSpace(request.Code))
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.errorCodes_apiExceptions_emptyCode);
                }

                await CheckDuplicateFields(request);
                var errorCode = CreateErrorCode(request);
                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdErrorCode), 
                    ("description", errorCode.Code));
                return _mapper.Map<ErrorCodeDTO>(errorCode);
            }
        }

        private ErrorCode CreateErrorCode(CheckAndCreateErrorCodeCommand request)
        {
            var newErrorCode = new ErrorCode()
            {
                Description = request.Description,
                Code = request.Code,
            };
            var errorCode = _context.ErrorCode().Add(newErrorCode).Entity;
            return errorCode;
        }

        private async Task CheckDuplicateFields(CheckAndCreateErrorCodeCommand request)
        {

            var errorCodeCodeExists = await _context.ErrorCode().AnyAsync(e => e.Code == request.Code);
            if (errorCodeCodeExists)
            {
                var ex = new CustomException(HttpStatusCode.Conflict, CloudTranslations.errorCodes_apiExceptions_duplicateCode);
                ex.Parameters.Add("code", request.Code);
                throw ex;
            }

            var errorCodeDescriptionExists = await _context.ErrorCode().AnyAsync(e => e.Description == request.Description);
            if (errorCodeDescriptionExists)
            {
                var ex = new CustomException(HttpStatusCode.Conflict, CloudTranslations.errorCodes_apiExceptions_duplicateDescription);
                ex.Parameters.Add("description", request.Description);
                throw ex;
            }
        }
    }

}
