using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO.Web;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
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
    public class CTAMUserPersonalItemsController : ControllerBase
    {
        private readonly ILogger<CTAMUserPersonalItemsController> _logger;
        private readonly IMediator _mediator;

        public CTAMUserPersonalItemsController(ILogger<CTAMUserPersonalItemsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET /api/CTAMUserPersonalItem/kamran_123
        [HttpGet("{uid}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMUserPersonalItem, UserPersonalItemWebDTO>>> GetPersonalItemsByUid(string uid = "", [FromQuery] int page_limit = 50,
                                                                             [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                             [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                ItemColumn? sortCol = null;
                if (Enum.TryParse<ItemColumn>(sortedBy, out ItemColumn col))
                {
                    sortCol = col;
                }
                return await _mediator.Send(new GetPersonalItemsByUidPaginatedQuery(uid, page_limit, page, sortCol, sortDescending, filterQuery));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET /api/CTAMUserPersonalItem/availableitemsandselected/kamran_123
        [HttpGet("availableitemsandselected/{uid}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<Item, ItemWebDTO>>> GetAvailablePersonalItemsAndSelectedByUid(string uid, [FromQuery] int page_limit = 50,
                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                ItemColumn? sortCol = null;
                if (Enum.TryParse<ItemColumn>(sortedBy, out ItemColumn col))
                {
                    sortCol = col;
                }
                return await _mediator.Send(new GetAvailablePersonalItemsAndSelectedByUidPaginatedQuery(uid, page_limit, page, sortCol, sortDescending, filterQuery));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}