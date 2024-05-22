using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Commands;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemCabinetModule.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CTAMUserInPossessionController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CTAMUserInPossessionController> _logger;
        private readonly IMediator _mediator;

        public CTAMUserInPossessionController(MainDbContext context, ILogger<CTAMUserInPossessionController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/CTAMUserInPossession/item_id={item_id}
        [HttpGet("item_id={item_id}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<UserInPossessionDTO>> GetPaginatedUserInPossessionHistory(
                                                                int item_id, [FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] DateTime? fromDT = null,
                                                                [FromQuery] DateTime? untilDT = null, [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                [FromQuery] string filterQuery = "", [FromQuery] int? status = null)
        {
            try
            {
                fromDT = (fromDT == null) ? null : DateTime.SpecifyKind((DateTime)fromDT, DateTimeKind.Utc);
                untilDT = (untilDT == null) ? null : DateTime.SpecifyKind((DateTime)untilDT, DateTimeKind.Utc);
                bool sortDesc = true;
                UserInPossessionColumn? sortCol = null;
                if (!string.IsNullOrEmpty(sortedBy))
                {
                    sortCol = Enum.Parse<UserInPossessionColumn>(sortedBy, true);
                    sortDesc = sortDescending;
                }

                UserInPossessionStatus? statusEnum = null;
                if (status != null)
                {
                    statusEnum = Enum.Parse<UserInPossessionStatus>(status.ToString(), true);
                }
                var query = new GetPaginatedUserInPossessionHistoryQuery(page_limit, page, item_id, fromDT, untilDT, sortCol, sortDescending, filterQuery, statusEnum);
                var inPossession = await _mediator.Send(query);

                return Ok(inPossession);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/CTAMUserInPossession/item_id={item_id}/download
        [HttpGet("item_id={item_id}/download")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<FileResult> GetPaginatedUserInPossessionHistoryDownload(int item_id, [FromQuery] DateTime? fromDT = null, [FromQuery] DateTime? untilDT = null,
                                                                   [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                   [FromQuery] string filterQuery = "", [FromQuery] int? status = null, [FromQuery] string columns = "", [FromQuery] string displayNames = "", 
                                                                   [FromQuery] string seperator = ";")
        {
            try
            {
                bool sortDesc = true;
                UserInPossessionColumn sortCol = UserInPossessionColumn.OutDT;
                if (!string.IsNullOrEmpty(sortedBy))
                {
                    sortCol = Enum.Parse<UserInPossessionColumn>(sortedBy);
                    sortDesc = sortDescending;
                }

                UserInPossessionStatus? statusEnum = null;
                if (status != null)
                {
                    statusEnum = Enum.Parse<UserInPossessionStatus>(status.ToString(), true);
                }
                var query = new GetPaginatedUserInPossessionHistoryQuery(-1, 0, item_id, fromDT, untilDT, sortCol, sortDescending, filterQuery, statusEnum);
                var poshist = await _mediator.Send(query);
                byte[] totalBytes;
                using (var memoryStream = new MemoryStream())
                {
                    var columnArr = columns.Split(",").Select(c => Enum.Parse<UserInPossessionColumn>(c));
                    byte[] bytes = Encoding.UTF8.GetBytes(displayNames.Replace(",", seperator) + "\r\n");
                    memoryStream.Write(bytes);
                    foreach (var log in poshist.Objects)
                    {
                        var row = log.ToColumnsString(columnArr, seperator);
                        bytes = Encoding.UTF8.GetBytes($"{row}\r\n");
                        memoryStream.Write(bytes);
                    }
                    totalBytes = memoryStream.ToArray();
                }

                return File(totalBytes, "text/csv", $"Item_{item_id}_History_{DateTime.UtcNow.ToString("yyyyMMdd")}.csv");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        // Endpoint made initially for testing purposes
        // POST: api/CTAMUserInPossession/link/item_id={item_id}/with/user_uid={uid}
        [HttpPost("linkUserPersonalItem")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult> LinkItemWithUser([FromQuery] int item_id, [FromQuery] string uid)
        {
            try
            {
                await _mediator.Send(new LinkPersonalItemToUserCommand(item_id, uid));
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/CTAMUserInPossession/uid={uid}
        [HttpGet("uid={uid}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<UserInPossessionDTO>> GetPaginatedUserInPossessionByUIDHistory(
                                                                string uid, [FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] DateTime? fromDT = null,
                                                                [FromQuery] DateTime? untilDT = null, [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                [FromQuery] string filterQuery = "", [FromQuery] int? status = null)
        {
            try
            {
                fromDT = (fromDT == null) ? null : DateTime.SpecifyKind((DateTime)fromDT, DateTimeKind.Utc);
                untilDT = (untilDT == null) ? null : DateTime.SpecifyKind((DateTime)untilDT, DateTimeKind.Utc);
                bool sortDesc = true;
                UserInPossessionColumn? sortCol = null;
                if (!string.IsNullOrEmpty(sortedBy))
                {
                    sortCol = Enum.Parse<UserInPossessionColumn>(sortedBy, true);
                    sortDesc = sortDescending;
                }

                UserInPossessionStatus? statusEnum = null;
                if (status != null)
                {
                    statusEnum = Enum.Parse<UserInPossessionStatus>(status.ToString(), true);
                }

                var query = new GetPaginatedUserInPossessionsByUIDHistoryQuery(page_limit, page, uid, fromDT, untilDT, sortCol, sortDesc, filterQuery, statusEnum);
                var inPossessions = await _mediator.Send(query);

                return Ok(inPossessions);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/CTAMUserInPossession/uid={uid}/download
        [HttpGet("uid={uid}/download")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<FileResult> GetPaginatedUserInPossessionByUIDHistoryDownload(string uid, [FromQuery] DateTime? fromDT = null, [FromQuery] DateTime? untilDT = null,
                                                                   [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                   [FromQuery] string filterQuery = "", [FromQuery] int? status = null,
                                                                   [FromQuery] string columns = "", [FromQuery] string displayNames = "",
                                                                   [FromQuery] string seperator = ";")
        {
            try
            {
                bool sortDesc = true;
                UserInPossessionColumn sortCol = UserInPossessionColumn.OutDT;
                if (!string.IsNullOrEmpty(sortedBy))
                {
                    sortCol = Enum.Parse<UserInPossessionColumn>(sortedBy);
                    sortDesc = sortDescending;
                }

                UserInPossessionStatus? statusEnum = null;
                if (status != null)
                {
                    statusEnum = Enum.Parse<UserInPossessionStatus>(status.ToString(), true);
                }

                var query = new GetPaginatedUserInPossessionsByUIDHistoryQuery(-1, 0, uid, fromDT, untilDT, sortCol, sortDesc, filterQuery, statusEnum);
                var poshist = await _mediator.Send(query);
                byte[] totalBytes;
                using ( var memoryStream = new MemoryStream())
                {
                    var columnArr = columns.Split(",").Select(c => Enum.Parse<UserInPossessionColumn>(c));
                    byte[] bytes = Encoding.UTF8.GetBytes(displayNames.Replace(",", seperator) + "\r\n");
                    memoryStream.Write(bytes);
                    foreach (var log in poshist.Objects)
                    {
                        var row = log.ToColumnsString(columnArr, seperator);
                        bytes = Encoding.UTF8.GetBytes($"{row}\r\n");
                        memoryStream.Write(bytes);
                    }
                    totalBytes = memoryStream.ToArray();
                }

                return File(totalBytes, "text/csv", $"User_{uid}_ItemsHist_{DateTime.UtcNow.ToString("yyyyMMdd")}.csv");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
