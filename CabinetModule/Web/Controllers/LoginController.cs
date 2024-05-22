using CabinetModule.ApplicationCore.Queries.Cabinets;
using CabinetModule.Web.Security;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CabinetModule.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMediator _mediator;
        private readonly CabinetAuthenticationService _authentication;

        public LoginController(ILogger<LoginController> logger, IMediator mediator, CabinetAuthenticationService authentication)
        {
            _logger = logger;
            _mediator = mediator;
            _authentication = authentication;
        }

        [HttpPost("cabinet")]
        [AllowAnonymous]
        public async Task<IActionResult> Cabinet(CabinetAuthenticationRequest request, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var query = new GetCabinetByCabinetNumberQuery(request.CabinetNumber);
                var cabinet = await _mediator.Send(query);
                if (cabinet != null)
                {
                    if (!cabinet.IsActive)
                    {
                        _logger.LogError("IBK is niet actief");
                        throw new CustomException(HttpStatusCode.ServiceUnavailable, CloudTranslations.cabinets_apiExceptions_notActive);
                    }

                    var tokenString = _authentication.GenerateCabinetToken(request, tenantID);
                    return Ok(new
                    {
                        Token = tokenString,
                        cabinetDetails = cabinet
                    });
                }
                else
                {
                    _logger.LogError("IBK is niet gevonden");
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound);
                }
            }
            // Exception thrown from GenerateCabinetToken if API key from request 
            // is not equal to CabinetAPIKey from appsettings.*.json
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.Unauthorized, new
                {
                    Message = CloudTranslations.cabinets_apiExceptions_apiKeyNotValid
                });
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}