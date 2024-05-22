using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Commands;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetPositionsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CabinetPositionsController> _logger;

        public CabinetPositionsController(IMediator mediator, ILogger<CabinetPositionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{cabinet_number}/positions")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CabinetPosition, CabinetPositionDTO>>> GetCabinetPositions(string cabinet_number,
                                                [FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                CabinetPositionDetailColumn? sortCol = null;
                if (Enum.TryParse<CabinetPositionDetailColumn>(sortedBy, out CabinetPositionDetailColumn col))
                {
                    sortCol = col;
                }
                var query = new GetCabinetPositionContentQuery(cabinet_number, page_limit, page, sortCol, sortDescending, filterQuery);
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }


        [HttpGet("{cabinet_number}/positions/{position_id}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<List<CabinetPositionDTO>>> GetPositionDetails(string cabinet_number, int position_id)
        {
            try
            { 
            return Ok(await _mediator.Send(new GetPositionDetailsQuery
            {
                CabinetNumber = cabinet_number,
                PositionID = position_id
            }));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPatch("positions")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> UpdateMultipleCabinetPositions(UpdateMultipleCabinetPositionsCommand query)
        {
            try
            {
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPatch("position")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> UpdateCabinetPosition(UpdateCabinetPositionCommand query)
        {
            try
            {
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
