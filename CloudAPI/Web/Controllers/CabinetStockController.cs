using CloudAPI.ApplicationCore.Commands.ItemCabinet;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CloudAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetStockController : ControllerBase
    {
        private readonly ILogger<CabinetStockController> _logger;
        private readonly IMediator _mediator;

        public CabinetStockController(ILogger<CabinetStockController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET /api/cabinetstock
        [HttpGet("{cabinetNumber}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CabinetStock, CabinetStockDTO>>> GetCabinetStocksPaginated(
                                                string cabinetNumber, [FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] int? item_type_id = null,
                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                [FromQuery] string filterQuery = "", [FromQuery] int? cabinetStockStatus = null)
        {
            try
            {
                CabinetStockColumn? sortCol = null;
                if (Enum.TryParse<CabinetStockColumn>(sortedBy, out CabinetStockColumn col))
                {
                    sortCol = col;
                }
                var query = new GetCabinetStockPaginatedQuery(cabinetNumber, page_limit, page, sortCol, sortDescending, filterQuery, item_type_id, cabinetStockStatus);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET /api/cabinetstock/C1/itemType/2
        [HttpGet("{cabinetNumber}/itemType/{item_type_id}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult> GetCabinetStock(string cabinetNumber, int item_type_id)
        {
            try
            {
                var query = new GetCabinetStockQuery
                {
                    CabinetNumber = cabinetNumber,
                    ItemTypeID = item_type_id
                };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PATCH /api/cabinetstock/cabinet/C1/itemtype/1/minimalstock
        [HttpPatch("cabinet/{cabinetNumber}/itemtype/{itemTypeID}/minimalstock")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> UpdateMinimalStock(string cabinetNumber, int itemTypeID, UpdateMinimalStockDTO dto)
        {
            try
            {
                var command = new UpdateMinimalStockCommand()
                {
                    CabinetNumber = cabinetNumber,
                    ItemTypeID = itemTypeID,
                    MinimalStock = dto.MinimalStock
                };
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}