using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Queries.Roles;

namespace UserRoleModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IMediator _mediator;

        public PermissionsController(ILogger<PermissionsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/permissions
        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult> GetPermissions()
        {
            try
            {
                var query = new GetAllPermissionsQuery();
                var allPermissions = await _mediator.Send(query);
                return Ok(allPermissions);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
