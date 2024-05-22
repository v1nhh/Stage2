using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Queries.LiveSync;
using CTAM.Core.Utilities;
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
    [Authorize(Roles = "CABINET_ACTIONS")]
    public class LiveSyncController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LiveSyncController> _logger;

        public LiveSyncController(ILogger<LiveSyncController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("collected")]
        public async Task<ActionResult<CollectedLiveSyncEnvelope>> GetCollected(CollectedLiveSyncRequest request)
        {
            try
            {
                var query = new GetCollectedEnvelopeQuery(request);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
