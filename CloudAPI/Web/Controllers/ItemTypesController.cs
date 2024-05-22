using CloudAPI.ApplicationCore.Commands.ItemTypes;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DTO;
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

        //PATCH /api/itemtypes
        [HttpPatch]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<ItemTypeDTO>> CheckAndModifyItemType(CheckAndModifyItemTypeCommand request)
        {
            try
            {
                var modifiedItemType = await _mediator.Send(request);
                return Ok(modifiedItemType);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
