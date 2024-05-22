using CTAM.Core;
using CTAM.Core.Commands.Items;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Commands.Items;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Queries.Items;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ItemModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly ILogger<ItemsController> _logger;
        private readonly IMediator _mediator;

        public ItemsController(MainDbContext context, ILogger<ItemsController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/item/5/general
        [HttpGet("{id}/general")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<ItemDTO>> GetItemGeneral(int id)
        {
            try
            {
                return await _mediator.Send(new GetItemByIdQuery(id));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PATCH /api/items
        [HttpPatch]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CheckAndModifyItem(CheckAndModifyItemCommand request)
        {
            try
            {
                var modifiedItem = await _mediator.Send(request);
                return Ok(modifiedItem);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST /api/items
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CreateItem(CreateItemCommand request)
        {
            try
            {
                var createdItem = await _mediator.Send(request);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE /api/items/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult<ItemDTO>> DeleteItem(int id)
        {
            try
            {
                await _mediator.Send(new CheckAndDeleteItemCommand(id));
                return Ok();
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

    }
}
