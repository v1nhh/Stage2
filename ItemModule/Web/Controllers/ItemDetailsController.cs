using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Commands.ItemDetails;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Queries.ItemDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ItemModule.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailsController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly ILogger<ErrorCodesController> _logger;
        private readonly IMediator _mediator;

        public ItemDetailsController(MainDbContext context, ILogger<ErrorCodesController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/itemdetails/5
        [HttpGet("{id}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<ItemDetailDTO>> GetItemDetail(int id)
        {
            try
            {
                var query = new GetItemDetailByIdQuery(id);
                var itemtype = await _mediator.Send(query);

                if (itemtype == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_itemDetailNotFound,
                                              new Dictionary<string, string> { { "id", id.ToString() } });
                }

                return itemtype;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }


        // GET: api/itemdetails/details/1
        [HttpGet("details/{item_id}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<IEnumerable<ItemDetailDTO>>> GetItemDetailsOfItem(int item_id)
        {
            try
            {
                return await _mediator.Send(new GetItemDetailsByItemIdQuery(item_id));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE /api/itemdetails/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult> DeleteItemDetail(int id)
        {
            try
            {
                await _mediator.Send(new RemoveItemDetailCommand { ID = id });
                return Ok();
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PUT /api/itemdetails/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> UpdateItemDetail(int id, CreateOrReplaceItemDetailCommand command)
        {
            try
            {
                if (id != command.ID)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_itemDetailNotSameAsData);
                }
                return Ok(await _mediator.Send(command));
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST /api/itemdetails
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CreateItemDetail(CreateOrReplaceItemDetailCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
