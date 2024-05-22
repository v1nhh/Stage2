using CabinetModule.ApplicationCore.DTO;
using CloudAPI.ApplicationCore.Commands.Cabinet;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Commands;
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
    public class CabinetsController : ControllerBase
    {
        private readonly ILogger<CabinetsController> _logger;
        private readonly IMediator _mediator;

        public CabinetsController(ILogger<CabinetsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("synchronize/{cabinetNumber}")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> synchronizeCabinet(string cabinetNumber)
        {
            try
            {
                await _mediator.Send(new SynchronizeCabinetCommand() { CabinetNumber = cabinetNumber });
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPatch("{cabinetNumber}")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetDTO>> ModifyCabinet(string cabinetNumber, CheckAndModifyCabinetCommand command)
        {
            try
            {
                if (cabinetNumber != command.CabinetNumber)
                {
                    throw new Exception("Provided cabinet numbers are not equal");
                }
                var modifiedCabinet = await _mediator.Send(command);
                return Ok(modifiedCabinet);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpDelete("{cabinetNumber}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult> DeleteCabinet(string cabinetNumber)
        {
            try
            {
                await _mediator.Send(new RemoveCabinetCommand(cabinetNumber));
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
