using CabinetModule.ApplicationCore.Commands.Logs;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Queries.Logs;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CTAMSharedLibrary.Resources;

namespace CabinetModule.Web.Controllers
{
    [ApiController]
    [Route("api/Logs")]
    public class CabinetLogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CabinetLogsController> _logger;


        public CabinetLogsController(ILogger<CabinetLogsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("operational")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<CabinetLogDTO>> GetAllCabinetActionLogs([FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] DateTime? fromDT = null,
                                                                         [FromQuery] DateTime? untilDT = null, [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                         [FromQuery] string filterQuery = "", [FromQuery] string selectedCabinetNumber = "")
        {
            try
            {
                CabinetActionLogColumn sortCol = Enum.Parse<CabinetActionLogColumn>(sortedBy);
                var query = new GetAllCabinetActionLogsQuery(page_limit, page, fromDT, untilDT, sortCol, sortDescending, filterQuery, selectedCabinetNumber);
                var cabinetLog = await _mediator.Send(query);

                return Ok(cabinetLog);
            }
            catch (Exception e)
            {
                return e.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("operational/download")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<FileResult> GetAllCabinetActionLogsDownload([FromQuery] DateTime? fromDT = null, [FromQuery] DateTime? untilDT = null, 
                                                                      [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                      [FromQuery] string filterQuery = "", [FromQuery] string selectedCabinetNumber = "", 
                                                                      [FromQuery] string columns = "", [FromQuery] string displayNames = "", 
                                                                      [FromQuery] string seperator = ";")
        {
            try
            {
                CabinetActionLogColumn sortCol = Enum.Parse<CabinetActionLogColumn>(sortedBy);
                var query = new GetAllCabinetActionLogsQuery(-1, 0, fromDT, untilDT, sortCol, sortDescending, filterQuery, selectedCabinetNumber);
                var cabinetLog = await _mediator.Send(query);
                byte[] totalBytes = GetCsvBytes(columns, displayNames, seperator, cabinetLog.Objects.ToList());

                return File(totalBytes, "text/csv", $"CabActionLogs_{DateTime.UtcNow.ToString("yyyyMMdd")}.csv");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private static byte[] GetCsvBytes(string columns, string displayNames, string seperator, List<CabinetActionDTO> logs)
        {
            byte[] totalBytes;
            using (var memoryStream = new MemoryStream())
            {
                var columnArr = columns.Split(",").Select(c => Enum.Parse<CabinetActionLogColumn>(c));
                byte[] bytes = Encoding.UTF8.GetBytes(displayNames.Replace(",", seperator) + "\r\n");
                memoryStream.Write(bytes);
                foreach (var log in logs)
                {
                    var row = log.ToColumnsString(columnArr, seperator);
                    bytes = Encoding.UTF8.GetBytes($"{row}\r\n");
                    memoryStream.Write(bytes);
                }
                totalBytes = memoryStream.ToArray();
            }

            return totalBytes;
        }

        // PATCH: api/operational/{id}
        [HttpPatch("operational/{id}")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetAction>> UpdateCabinetActionLog(UpdateCabinetActionLogByIdCommand updateCabinetActionLog, Guid id)
        {
            try
            {
                if (id.Equals(updateCabinetActionLog.ID))
                {
                    var log = await _mediator.Send(updateCabinetActionLog);
                    return Ok(log);
                } else
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinetActionLog_apiExceptions_notFound, 
                                          new Dictionary<string, string> { { "id", updateCabinetActionLog.ID.ToString() } });
                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.GetObjectResult(_logger, this, HttpStatusCode.NotFound);
            }
        }

        // GET: api/technical/1/50/50
        [Obsolete]
        [HttpGet("technical/{start_id}/{end_id}/{limit}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<IEnumerable<CabinetLogDTO>>> GetCabinetLog(int start_id, int end_id = -1, int limit = 50)
        {
            try
            { 
                var query = new GetCabinetLogsByIdQuery(start_id, end_id, limit);
                var cabinetLog = await _mediator.Send(query);

                return Ok(cabinetLog);
            }
            catch (Exception e)
            {
                return e.GetObjectResult(_logger, this);
            }
        }

        // GET: api/technical
        [HttpGet("technical")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<CabinetLogDTO>> GetAllCabinetLogs([FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] DateTime? fromDT = null,
                                                                         [FromQuery] DateTime? untilDT = null, [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                         [FromQuery] string filterQuery = "", [FromQuery] string selectedCabinetNumber = "")
        {
            try
            {
                CabinetLogColumn sortCol = Enum.Parse<CabinetLogColumn>(sortedBy);
                var query = new GetAllCabinetLogsQuery(page_limit, page, fromDT, untilDT, sortCol, sortDescending, filterQuery,selectedCabinetNumber);
                var cabinetLog = await _mediator.Send(query);

                return Ok(cabinetLog);
            }
            catch (Exception e)
            {
                return e.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("technical/download")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<FileResult> GetAllCabinetLogsDownload([FromQuery] DateTime? fromDT = null, [FromQuery] DateTime? untilDT = null, 
                                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                [FromQuery] string filterQuery = "", [FromQuery] string selectedCabinetNumber = "", 
                                                                [FromQuery] string columns = "", [FromQuery] string displayNames = "", 
                                                                [FromQuery] string seperator = ";")
        {
            try
            {
                CabinetLogColumn sortCol = Enum.Parse<CabinetLogColumn>(sortedBy);
                var query = new GetAllCabinetLogsQuery(-1, 0, fromDT, untilDT, sortCol, sortDescending, filterQuery, selectedCabinetNumber);
                var cabinetLog = await _mediator.Send(query);
                byte[] totalBytes = GetCsvBytes(columns, displayNames, seperator, cabinetLog.Objects.ToList());

                return File(totalBytes, "text/csv", $"CabLogs_{DateTime.UtcNow.ToString("yyyyMMdd")}.csv");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private static byte[] GetCsvBytes(string columns, string displayNames, string seperator, List<CabinetLogDTO> logs)
        {
            byte[] totalBytes;
            using (var memoryStream = new MemoryStream())
            {
                var columnArr = columns.Split(",").Select(c => Enum.Parse<CabinetLogColumn>(c));
                byte[] bytes = Encoding.UTF8.GetBytes(displayNames.Replace(",", seperator) + "\r\n");
                memoryStream.Write(bytes);
                foreach (var log in logs)
                {
                    var row = log.ToColumnsString(columnArr, seperator);
                    bytes = Encoding.UTF8.GetBytes($"{row}\r\n");
                    memoryStream.Write(bytes);
                }
                totalBytes = memoryStream.ToArray();
            }

            return totalBytes;
        }

        // PATCH: api/technical/{id}
        [HttpPatch("technical/{id}")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetLog>> UpdateCabinetLog(UpdateCabinetLogByIdCommand updateCabinetLog, int id)
        {
            try
            {
                if (id.Equals(updateCabinetLog.ID))
                {
                    var log = await _mediator.Send(updateCabinetLog);
                    return Ok(log);
                }
                else
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinetLog_apiExceptions_notFound, 
                                          new Dictionary<string, string> { { "id", updateCabinetLog.ID.ToString() } });
                }
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}