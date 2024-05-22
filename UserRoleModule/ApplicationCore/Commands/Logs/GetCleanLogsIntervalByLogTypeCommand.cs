using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Enums;
using CTAMSharedLibrary.Resources;

namespace UserRoleModule.ApplicationCore.Commands.Logs
{
    public class GetCleanLogsIntervalByLogTypeCommand : IRequest<CleanLogsIntervalDTO>
    {
        public LogType LogType { get; set; }
        public GetCleanLogsIntervalByLogTypeCommand(LogType logType)
        {
            LogType = logType;
        }
    }

    public class GetCleanLogsIntervalByLogTypeHandler : IRequestHandler<GetCleanLogsIntervalByLogTypeCommand, CleanLogsIntervalDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCleanLogsIntervalByLogTypeHandler> _logger;

        public GetCleanLogsIntervalByLogTypeHandler(MainDbContext context, ILogger<GetCleanLogsIntervalByLogTypeHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CleanLogsIntervalDTO> Handle(GetCleanLogsIntervalByLogTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCleanLogsIntervalByLogTypeHandler called");
            if (!Enum.IsDefined(typeof(LogType), request.LogType))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidLogType,
                                          new Dictionary<string, string> { { "logType", request.LogType.ToString() } });
            }
            CleanLogsIntervalDTO interval = null;
            string parName = GetCleanLogIntervalNameByLogType(request.LogType);
            var match = await _context.CTAMSetting().AsNoTracking()
                .FirstOrDefaultAsync(setting => setting.ParName.Equals(parName));

            if (match == null)
            {
                interval = new CleanLogsIntervalDTO() { Amount = 0, IntervalType = IntervalType.None };
            }
            else
            {
                interval = JsonConvert.DeserializeObject<CleanLogsIntervalDTO>(match.ParValue);
            }

            return interval;
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
