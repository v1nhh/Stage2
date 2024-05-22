using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Queries.Settings;

namespace UserRoleModule.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(IMediator mediator, ILogger<SettingsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult> GetSettings()
        {
            return Ok(await _mediator.Send(new GetSettingsByKeyQuery()));
        }

        [HttpGet("key/{key}")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult> GetSettingByKey(string key)
        {
            try
            {
                var result = await _mediator.Send(new GetSettingsByKeyQuery { Key = key });
                if (result.Count > 0)
                {
                    return Ok(result.ElementAt(0));
                }
                else
                {
                    return Ok(null);
                }
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult> GetSettingByID(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetSettingsByIdQuery { ID = id });
                return Ok(result);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
