using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Commands.Logs;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Queries.Logs;

namespace UserRoleModule.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LogsController> _logger;


        public LogsController(ILogger<LogsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint for updating the clean logs interval by logType. During the clean logs process, logs older than (now - clean_log_interval) will be removed
        /// </summary>
        /// <param name="updateCleanLogIntervalCommand"></param>
        /// <returns></returns>
        [Authorize(Roles="MANAGEMENT_WRITE")]
        [HttpPut("config/clean-interval")]
        public async Task<ActionResult> SetCleanLogInterval([FromBody] UpdateCleanLogsIntervalByLogTypeCommand updateCleanLogIntervalCommand)
        {
            try
            {
                await _mediator.Send(updateCleanLogIntervalCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        /// <summary>
        /// Endpoint for getting the clean logs interval by logType.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "MANAGEMENT_READ,MANAGEMENT_WRITE")]
        [HttpGet("config/clean-interval/{logType}")]
        public async Task<ActionResult<CleanLogsIntervalDTO>> GetCleanLogIntervalByLogType(LogType logType)
        {
            try
            {
                var interval = await _mediator.Send(new GetCleanLogsIntervalByLogTypeCommand(logType));
                return Ok(interval);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Logs/management
        [HttpGet("management")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<ManagementLogDTO>> GetAllManagementLogs([FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] DateTime? fromDT = null,
                                                                               [FromQuery] DateTime? untilDT = null, [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                               [FromQuery] string filterQuery = "")
        {
            try
            {
                ManagementLogColumn sortCol = Enum.Parse<ManagementLogColumn>(sortedBy);
                var query = new GetAllManagementLogsQuery(page_limit, page, fromDT, untilDT, sortCol, sortDescending, filterQuery, Request.HttpContext);
                var managementLog = await _mediator.Send(query);

                return Ok(managementLog);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Logs/management/download
        [HttpGet("management/download")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<FileResult> GetAllManagementLogsDownload([FromQuery] DateTime? fromDT = null, [FromQuery] DateTime? untilDT = null, 
                                                                   [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                   [FromQuery] string filterQuery = "", [FromQuery] string columns = "", [FromQuery] string displayNames = "",
                                                                   [FromQuery] string seperator = ";")
        {
            try
            {
                ManagementLogColumn sortCol = Enum.Parse<ManagementLogColumn>(sortedBy);
                var query = new GetAllManagementLogsQuery(-1, 0, fromDT, untilDT, sortCol, sortDescending, filterQuery, Request.HttpContext);
                var managementLog = await _mediator.Send(query);
                byte[] totalBytes;
                using (var memoryStream = new MemoryStream())
                {
                    var columnArr = columns.Split(",").Select(c => Enum.Parse<ManagementLogColumn>(c));
                    byte[] bytes = Encoding.UTF8.GetBytes(displayNames.Replace(",", seperator) + "\r\n");
                    memoryStream.Write(bytes);
                    foreach (var log in managementLog.Objects)
                    {
                        var row = log.ToColumnsString(columnArr, seperator);
                        bytes = Encoding.UTF8.GetBytes($"{row}\r\n");
                        memoryStream.Write(bytes);
                    }
                    totalBytes = memoryStream.ToArray();
                }

                return File(totalBytes, "text/csv", $"MgmLogs_{DateTime.UtcNow.ToString("yyyyMMdd")}.csv");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
