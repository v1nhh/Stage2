using CloudAPI.ApplicationCore.Commands.UserSettings;
using CloudAPI.ApplicationCore.Queries.Users;
using CTAM.Core.Constants;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;

namespace CloudAPI.Web.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserSettingsController : ControllerBase
    {
        private readonly ILogger<UserSettingsController> _logger;
        private readonly IMediator _mediator;

        public UserSettingsController(ILogger<UserSettingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [HttpPost("welcome")]
        public async Task<IActionResult> SendLoginDetails(SendLoginDetailsCommand request, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var hasOrigin = Request.Headers.TryGetValue("Origin", out var origin);
                if (hasOrigin)
                {
                    request.Origin = origin;
                    request.TenantID = tenantID;
                    await _mediator.Send(request);
                    return Ok();
                }
                throw new ArgumentNullException("Origin in HTTP header cannot be null");
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetUserPassword(ResetUserPasswordCommand request)
        {
            try
            {
                await _mediator.Send(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [HttpPost("pincode/reset")]
        public async Task<IActionResult> ResetUserPincode(ResetUserPincodeCommand request)
        {
            try
            {
                await _mediator.Send(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [HttpPost("logincode/next")]
        public async Task<IActionResult> NextFreeLoginCode(GetNextFreeLoginCodeCommand request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [Authorize(Roles = "MANAGEMENT_READ")]
        [HttpPost("passwordpolicy/minimalLength")]
        public async Task<IActionResult> GetMinimalPasswordLength()
        {
            try
            {
                var policy = await _mediator.Send(new GetCTAMSettingQuery { Key = CTAMSettingKeys.PasswordPolicy});
                var minimalLength = EmailRequirements.GetPasswordMinimalLength(policy);
                return Ok(minimalLength);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [Authorize(Roles = "MANAGEMENT_READ")]
        [HttpPost("password/change")]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordCommand request)
        {
            try
            {
                var hasOrigin = Request.Headers.TryGetValue("Origin", out var origin);
                if (hasOrigin)
                {
                    request.Origin = origin;
                    await _mediator.Send(request);
                    return Ok();
                }
                throw new ArgumentNullException("Origin in HTTP header cannot be null");
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [AllowAnonymous]
        [HttpPost("password/forgot")]
        public async Task<IActionResult> UserForgotPassword(ForgotPasswordCommand request, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var hasOrigin = Request.Headers.TryGetValue("Origin", out var origin);
                if (hasOrigin)
                {
                    request.Origin = origin;
                    request.TenantID = tenantID;
                    await _mediator.Send(request);
                    return Ok();
                }
                throw new ArgumentNullException("Origin in HTTP header cannot be null");
            }
            catch (Exception ex)
            {
                ex.GetObjectResult(_logger, this);
                return Ok(); //  ex.GetObjectResult(this); would return information about existence of email-addresses
            }
        }

        [AllowAnonymous]
        [HttpPost("password/forgot/change")]
        public async Task<IActionResult> ChangeUserForgottenPassword(ChangeForgottenPasswordCommand request, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var hasOrigin = Request.Headers.TryGetValue("Origin", out var origin);
                if (hasOrigin)
                {
                    request.Origin = origin;
                    request.TenantID = tenantID;
                    await _mediator.Send(request);
                    return Ok();
                }
                throw new ArgumentNullException("Origin in HTTP header cannot be null");
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
