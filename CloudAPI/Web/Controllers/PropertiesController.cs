using CTAM.Core.Exceptions;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Reflection;

namespace CloudAPI.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Settings _settings;
        private readonly ILogger<PropertiesController> _logger;
        private ITenantService _tenantService;


        public PropertiesController(IOptionsSnapshot<Settings> settings, IConfiguration configuration, ILogger<PropertiesController> logger, ITenantService tenantService)
        {
            _settings = settings.Value;
            _configuration = configuration;
            _logger = logger;
            _tenantService = tenantService;
        }

        [HttpGet("version")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public ActionResult GetVersion()
        {
            try
            {
                return Ok(EnvironmentUtils.GetVersion(Assembly.GetEntryAssembly()));
            }
            catch(Exception ex)
            {
                return ex.GetObjectResult(_logger, this, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("validateTenant/{candidateTenant}")]
        [AllowAnonymous]
        public ActionResult ValidateTenant(string candidateTenant)
        {
            try
            {
                var connectionString = _tenantService.GetConnectionStringForTenant(candidateTenant);
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return Ok();
                }

                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.properties_apiExceptions_noTenant);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
