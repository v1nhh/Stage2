using CTAM.Core;
using CTAM.Core.Exceptions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using CTAM.Core.Interfaces;

namespace CloudAPI.Services
{
    public class CleanLogs
    {
        private ILogger<CleanLogs> _logger;
        private ITenantService _tenantService;

        public CleanLogs(ILogger<CleanLogs> cleanLogs, ITenantService tenantService)
        {
            _logger = cleanLogs;
            _tenantService = tenantService;
        }

        public async Task CleanLogsForTenantConnections(LogType logType)
        {
            var tenantConnections = _tenantService.GetTenantConnections();
            _logger.LogInformation($"Count: {tenantConnections.Count}");
            foreach (var tenantConnection in tenantConnections)
            {
                try
                {
                    await CleanLogsForTenant(logType, tenantConnection);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed to clean logs for {tenantConnection.Key}");
                }
            }
        }

        private async Task CleanLogsForTenant(LogType logType, KeyValuePair<string, string> tenantConnection)
        {
            _logger.LogInformation($"{tenantConnection.Key}: {tenantConnection.Value}");
            var context = _tenantService.GetDbContext(tenantConnection.Value);
            var dateAtInterval = await GetDateAtIntervalByLogType(logType, context);
            if (dateAtInterval != null)
            {
                if(logType == LogType.Operational)
                {
                    await DeleteLogsBeforeDate(
                        logs: context.CabinetAction().Where(log => log.ActionDT.CompareTo(dateAtInterval.Value) < 0),
                        logName: logType.GetTranslation(),
                        context: context);
                }
                else if (logType == LogType.Technical)
                {
                    await DeleteLogsBeforeDate(
                        logs: context.CabinetLog().Where(log => log.LogDT.CompareTo(dateAtInterval.Value) < 0),
                        logName: logType.GetTranslation(),
                        context: context);
                }
                else if (logType == LogType.Management)
                {
                    await DeleteLogsBeforeDate(
                        logs: context.ManagementLog().Where(log => log.LogDT.CompareTo(dateAtInterval.Value) < 0),
                        logName: logType.GetTranslation(),
                        context: context);
                }
            }
        }

        private async Task<DateTime?> GetDateAtIntervalByLogType(LogType logType, MainDbContext context)
        {
            string parName = GetCleanLogIntervalNameByLogType(logType);
            var setting = await context.CTAMSetting()
                .FirstOrDefaultAsync(setting => setting.ParName.Equals(parName));
            if (setting == null)
            {
                _logger.LogWarning($"No {parName} setting found");
                return null;
            }
            _logger.LogInformation($"Interval from CTAMSettings {parName}: {setting.ParName}={setting.ParValue}");
            var interval = JsonConvert.DeserializeObject<CleanLogsIntervalDTO>(setting.ParValue);
            return GetDateAtInterval(interval);
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

        private async Task DeleteLogsBeforeDate<T>(IQueryable<T> logs, string logName, MainDbContext context)
        {
            var table = typeof(T).FullName.Split('.').LastOrDefault();
            var logsToDelete = await logs.BatchDeleteAsync();
            if (logsToDelete > 0)
            {
                _logger.LogInformation($"Deleting {logsToDelete} entries from the {table} table");
                await context.SaveChangesAsync();
                await LogToManagementTable($"{logsToDelete} {logName} verwijderd", context);
            }
            else
            {
                _logger.LogInformation($"No logs found to delete in {table}");
            }
        }

        private DateTime GetDateAtInterval(CleanLogsIntervalDTO interval)
        {
            _logger.LogInformation($"Log interval: {interval.Amount} {interval.IntervalType}");
            var date = DateTime.UtcNow;
            var newDate = interval.IntervalType switch
            {
                IntervalType.Days => date.AddDays(-interval.Amount),
                IntervalType.Weeks => date.AddDays(-7 * (double)interval.Amount),
                IntervalType.Months => date.AddMonths(-interval.Amount),
                _ => throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.logs_apiExceptions_invalidIntervalType,
                                               new Dictionary<string, string> { { "intervalType", interval.IntervalType.ToString() } })
            };
            _logger.LogInformation($"Current date - interval: {newDate.ToString("o")}");
            return newDate;
        }

        private async Task LogToManagementTable(string logResourcePath, MainDbContext context)
        {
            try
            {
                await context.ManagementLog().AddAsync(new ManagementLog
                {
                    Source = LogSource.Other,
                    LogDT = DateTime.UtcNow,
                    Level = UserRoleModule.ApplicationCore.Enums.LogLevel.Info,
                    LogResourcePath = logResourcePath,
                });
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to log to management table");
            }
        }
    }
}