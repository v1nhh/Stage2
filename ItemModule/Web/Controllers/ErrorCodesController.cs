using CloudAPI.ApplicationCore.Commands.ErrorCodes;
using CTAM.Core.Commands.ErrorCodes;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Commands.ErrorCodes;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Queries.ErrorCodes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ItemModule.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorCodesController : ControllerBase
    {
        private readonly ILogger<ErrorCodesController> _logger;
        private readonly IMediator _mediator;

        public ErrorCodesController(ILogger<ErrorCodesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/errorcodes
        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<ErrorCode, ErrorCodeDTO>>> GetErrorCodes(
                                                [FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                ErrorCodeColumn? sortCol = null;
                if (Enum.TryParse<ErrorCodeColumn>(sortedBy, out ErrorCodeColumn col))
                {
                    sortCol = col;
                }

                var query = new GetAllErrorCodesQuery(page, page_limit, sortCol, sortDescending, filterQuery);
                var allErrorCodes = await _mediator.Send(query);
                return Ok(allErrorCodes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/errorcodes/5
        [HttpGet("{id}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<ErrorCodeDTO>> GetErrorCode(int id)
        {
            try
            {
                var query = new GetErrorCodeByIdQuery(id);
                var errorcode = await _mediator.Send(query);

                return errorcode;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PATCH /api/errorcode
        [HttpPatch]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CheckAndUpdateErrorCode(CheckAndModifyErrorCodeCommand request)
        {
            try
            {
                var modifiedErrorCode = await _mediator.Send(request);
                return Ok(modifiedErrorCode);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST /api/errorcode
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CreateErrorCode(CheckAndCreateErrorCodeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE /api/errorcodes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult<ErrorCodeDTO>> DeleteErrorCodes(int id)
        {
            try
            {
                var command = new CheckAndDeleteErrorCodeCommand(id);
                var errorCode = await _mediator.Send(command);
                return Ok(errorCode);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
