using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Queries.Items;
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
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IMediator _mediator;

        public ItemsController(ILogger<ItemsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET /api/items
        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<Item, ItemDTO>>> GetItemsPaginated(
                                                [FromQuery] int page_limit = 50, [FromQuery] int page = 0, [FromQuery] int? item_type_id = null,
                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                [FromQuery] string filterQuery = "", [FromQuery] UserInPossessionStatus? uipstatus = null)
        {
            try
            {
                ItemColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out ItemColumn col))
                {
                    sortCol = col;
                }
                var query = new GetItemsPaginatedQuery(page_limit, page, sortCol, sortDescending, filterQuery, item_type_id, uipstatus);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
