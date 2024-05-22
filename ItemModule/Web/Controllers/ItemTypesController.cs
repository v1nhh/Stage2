using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.Commands.ItemTypes;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Queries.ItemTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace ItemModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly ILogger<ItemTypesController> _logger;
        private readonly IMediator _mediator;

        public ItemTypesController(MainDbContext context, ILogger<ItemTypesController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/itemtypes
        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<ItemType, ItemTypeDTO>>> GetItemTypes([FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                ItemTypeColumn? sortCol = null;
                if (Enum.TryParse<ItemTypeColumn>(sortedBy, out ItemTypeColumn col))
                {
                    sortCol = col;
                }
                var query = new GetPaginatedItemTypesQuery(page_limit, page, sortCol, sortDescending, filterQuery);
                var allItemTypes = await _mediator.Send(query);
                return Ok(allItemTypes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/itemtypes/descriptions
        [HttpGet("descriptions")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<ItemType, ItemTypeDTO>>> GetItemTypesDescriptions([FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                ItemTypeColumn? sortCol = null;
                if (Enum.TryParse<ItemTypeColumn>(sortedBy, out ItemTypeColumn col))
                {
                    sortCol = col;
                }
                var query = new GetPaginatedItemTypesDescriptionQuery(page_limit, page, sortCol, sortDescending, filterQuery);
                var allItemTypes = await _mediator.Send(query);
                return Ok(allItemTypes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/itemtypes/{itemtype_id}/general
        [HttpGet("{itemtype_id}/general")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<ItemTypeDTO>> GetItemTypeGeneralById(int itemtype_id)
        {
            try
            {
                var query = new GetItemTypeGeneralByIdQuery(itemtype_id);
                var itemtype = await _mediator.Send(query);

                if (itemtype == null)
                {
                    return NotFound();
                }

                return Ok(itemtype);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/itemtypes/{itemtype_id}/errorcodes
        [HttpGet("{itemtype_id}/errorcodes")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<ItemType_ErrorCode, ErrorCodeWebDTO>>> GetErrorCodesByItemTypeIdPaginated(int itemType_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetErrorCodesByItemTypeIdPaginatedQuery(itemType_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var errorCodes = await _mediator.Send(query);

                return Ok(errorCodes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/itemtypes/{itemtype_id}/allerrorcodesandselected
        [HttpGet("{itemtype_id}/allerrorcodesandselected")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<ErrorCode, ErrorCodeWebDTO>>> GetAllErrorCodesAndSelectedByItemTypeIdPaginated(string itemType_id, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllErrorCodesAndSelectedByItemTypeIdPaginatedQuery(itemType_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var errorCodes = await _mediator.Send(query);

                if (errorCodes == null)
                {
                    return NotFound();
                }

                return Ok(errorCodes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST /api/itemtypes
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<ItemTypeDTO>> CreateItemType(CheckAndCreateItemTypeCommand request)
        {
            try
            {
                var createdItemType = await _mediator.Send(request);
                return Ok(createdItemType);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE /api/itemtypes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult<ItemTypeDTO>> DeleteItemType(int id)
        {
            try
            {
                var command = new CheckAndDeleteItemTypeCommand(id);
                var itemType = await _mediator.Send(command);
                return Ok(itemType);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
