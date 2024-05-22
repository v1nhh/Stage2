using CloudAPI.ApplicationCore.Commands.Users;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.Web.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Authorize(Roles = "MANAGEMENT_READ,MANAGEMENT_WRITE")]
    public class MutateUserController : ControllerBase
    {
        private readonly ILogger<MutateUserController> _logger;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;

        public MutateUserController(ILogger<MutateUserController> logger, IMediator mediator, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mediator = mediator;
            _managementLogger = managementLogger;
        }
         
        // POST /api/users/modify
        [HttpPost("create")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<UserWebDTO>> CreateUser(CheckAndCreateUserCommand request)
        {
            try
            {
                var modifiedUser = await _mediator.Send(request);
                return Ok(modifiedUser);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PATCH /api/users/modify
        [HttpPatch("modify")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<UserWebDTO>> CheckAndModifyUserData(CheckAndModifyUserCommand request)
        {
            try
            {
                var modifiedUser = await _mediator.Send(request);
                return Ok(modifiedUser);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{uid}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult> DeleteUser(string uid)
        {
            var initiatorsEmail = HttpContext.User.Claims?.Where(c => c.Properties.Values.Contains(JwtRegisteredClaimNames.Sub))?.FirstOrDefault()?.Value;
            try
            {
                await _mediator.Send(new DeleteUserCommand(uid, initiatorsEmail));
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
