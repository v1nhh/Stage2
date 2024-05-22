using CTAM.Core;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.Commands.Logs
{
    public class UpdateCleanLogsIntervalByLogTypeCommand : IRequest
    {
        public LogType LogType { get; set; }
        public int Amount { get; set; }
        public IntervalType IntervalType { get; set; }
    }

    public class UpdateCleanLogsIntervalByLogTypeHandler : IRequestHandler<UpdateCleanLogsIntervalByLogTypeCommand>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<UpdateCleanLogsIntervalByLogTypeHandler> _logger;

        public UpdateCleanLogsIntervalByLogTypeHandler(MainDbContext context, ILogger<UpdateCleanLogsIntervalByLogTypeHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCleanLogsIntervalByLogTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateCleanLogsIntervalByLogTypeHandler called");
            if (!Enum.IsDefined(typeof(IntervalType), request.IntervalType) || request.IntervalType == IntervalType.None)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidIntervalType,
                                          new Dictionary<string, string> { { "intervalType", request.IntervalType.ToString() } });
            }
            if (!Enum.IsDefined(typeof(LogType), request.LogType))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidLogType,
                                          new Dictionary<string, string> { { "logType", request.LogType.ToString() } });
            }
            if (request.Amount <= 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidAmount,
                                          new Dictionary<string, string> { { "amount", request.Amount.ToString() } });
            }
            string parName = GetCleanLogIntervalNameByLogType(request.LogType);
            var match = await _context.CTAMSetting()
                .FirstOrDefaultAsync(setting => setting.ParName.Equals(parName));
            if (match == null)
            {
                var newSetting = new CTAMSetting
                {
                    ParName = parName,
                    ParValue = JsonConvert.SerializeObject(request),
                    CTAMModule = CTAMModule.Management,
                };
                await _context.CTAMSetting().AddAsync(newSetting);
                await _context.SaveChangesAsync();
            }
            else
            {
                match.ParValue = JsonConvert.SerializeObject(request);
                await _context.SaveChangesAsync();
            }
            return new Unit();
        }

        private static string GetCleanLogIntervalNameByLogType(LogType logType)
        {
            return logType switch
            {
                LogType.Operational => CTAMSettingKeys.CleanOperationalLogsInterval,
                LogType.Technical => CTAMSettingKeys.CleanTechnicalLogsInterval,
                LogType.Management => CTAMSettingKeys.CleanManagementLogsInterval,
                _ => throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidLogType,
                                               new Dictionary<string, string> { { "logType", logType.ToString() } })
            };
        }
    }
}
