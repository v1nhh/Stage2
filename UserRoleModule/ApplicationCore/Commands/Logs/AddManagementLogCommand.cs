using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;
using LogLevel = UserRoleModule.ApplicationCore.Enums.LogLevel;

namespace UserRoleModule.ApplicationCore.Commands.Logs
{
    public class AddManagementLogCommand: IRequest
    {
        public LogLevel LogLevel { get; set; }

        public LogSource LogSource { get; set; }

        public string LogResourcePath { get; set; }

        public (string key, string value)[] Parameters = Array.Empty<(string, string)>();

        public string UserEmail { get; set; }

        public AddManagementLogCommand(LogLevel logLevel, LogSource logSource, string logResourcePath, (string key, string value)[] parameters, string subValue)
        {
            LogLevel = logLevel;
            LogSource = logSource;
            LogResourcePath = logResourcePath;
            Parameters = parameters;
            if (!string.IsNullOrEmpty(subValue) && subValue.Contains("@")) {
                UserEmail = subValue;
            }
        }
    }

    public class AddManagementLogHandler : IRequestHandler<AddManagementLogCommand>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<AddManagementLogHandler> _logger;

        public AddManagementLogHandler(MainDbContext context, ILogger<AddManagementLogHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddManagementLogCommand request, CancellationToken cancellationToken)
        {
            // Should use received email directly or get the GUID first?
           CTAMUser user = null;
            // Separate try-catch for getting user to ensure that log is saved even if getting user fails
            try
            {
                user = (await _context.CTAMUser().AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Email.Equals(request.UserEmail)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"User with email '{request.UserEmail}' not found!\n  Management log message: '{request.LogResourcePath}'\n  Exception: {ex.Message}");
            }

            try
            {
                var logItem = ManagementLogUtilities.CreateManagementLog(request.LogLevel, request.LogSource, request.LogResourcePath, request.Parameters, user);
                _context.ManagementLog().Add(logItem);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception writing {request.LogResourcePath} to ManagementLog: {ex.Message}");
            }

            return new Unit();
        }
    }

}
