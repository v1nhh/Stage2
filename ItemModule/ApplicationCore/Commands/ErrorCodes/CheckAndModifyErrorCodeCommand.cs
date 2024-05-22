using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.ErrorCodes
{
    public class CheckAndModifyErrorCodeCommand : IRequest<ErrorCodeDTO>
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }
    }

    public class CheckAndModifyErrorCodeHandler : IRequestHandler<CheckAndModifyErrorCodeCommand, ErrorCodeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckAndModifyErrorCodeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndModifyErrorCodeHandler(MainDbContext context, ILogger<CheckAndModifyErrorCodeHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ErrorCodeDTO> Handle(CheckAndModifyErrorCodeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyErrorCodeHandler called");

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var errorCode = await ModifyErrorCode(request);
                scope.Complete();
                return _mapper.Map<ErrorCodeDTO>(errorCode);
            }
        }

        public async Task<ErrorCode> ModifyErrorCode(CheckAndModifyErrorCodeCommand request)
        {
            if (request.Description != null && string.IsNullOrWhiteSpace(request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.errorCodes_apiExceptions_emptyDescription);
            }
            if (request.Code != null && string.IsNullOrWhiteSpace(request.Code))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.errorCodes_apiExceptions_emptyCode);
            }

            var errorCode = await _context.ErrorCode().Where(i => i.ID == request.ID).FirstOrDefaultAsync();

            if (errorCode == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.errorCodes_apiExceptions_notFound,
                                                                     new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            if (await _context.ErrorCode().AnyAsync(ec => ec.ID != request.ID && ec.Code == request.Code))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.errorCodes_apiExceptions_duplicateCode,
                                                                     new Dictionary<string, string> { { "code", request.Code } });
            }

            if (await _context.ErrorCode().AnyAsync(ec => ec.ID != request.ID && ec.Description == request.Description))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.errorCodes_apiExceptions_duplicateDescription,
                                                                     new Dictionary<string, string> { { "description", request.Description } });
            }

            var parameters = ModifySingleFields(errorCode, request);

            if (parameters.Count > 0)
            {
                parameters.Add(("description", errorCode.Description));
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedErrorCode), parameters.ToArray());
            }

            await _context.SaveChangesAsync();

            return errorCode;
        }

        public List<(string key, string value)> ModifySingleFields(ErrorCode errorCode, CheckAndModifyErrorCodeCommand request)
        {
            var changes = new List<(string key, string value)>();

            if (request.Description != null && !request.Description.Equals(errorCode.Description))
            {
                _logger.LogInformation($"ErrorCodeID='{request.ID}' > Changing Description from '{errorCode.Description}' to '{request.Description}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldDescription", errorCode.Description),
                    ("newDescription", request.Description)
                });
                errorCode.Description = request.Description;
            }
            if (request.Code != null && !request.Code.Equals(errorCode.Code))
            {
                _logger.LogInformation($"ErrorCodeID='{request.ID}' > Changing Description from '{errorCode.Code}' to '{request.Description}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldCode", errorCode.Code),
                    ("newCode", request.Code)
                });
                errorCode.Code = request.Code;
            }
            return changes;
        }
    }

}
