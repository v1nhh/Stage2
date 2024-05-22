using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Commands.Users;
using UserRoleModule.ApplicationCore.Queries.Users;
using UserRoleModule.Web.Security;

namespace UserRoleModule.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IMediator _mediator;
        private readonly UserAuthenticationService _authentication;
        private readonly IConfiguration _configuration;

        public LoginController(ILogger<LoginController> logger, IMediator mediator, UserAuthenticationService authentication,
            IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _authentication = authentication;
            _configuration = configuration;
        }

        private bool UserHasValidIntervalsAndRoles(ApplicationCore.DTO.UserDTO user)
        {
            var currentTime = DateTime.UtcNow;

            user.Roles = user.Roles.Where(r =>
            {
                if (r.ValidFromDT.HasValue && r.ValidUntilDT.HasValue)
                {
                    return currentTime >= r.ValidFromDT && currentTime <= r.ValidUntilDT;
                }
                else if (r.ValidFromDT.HasValue && !r.ValidUntilDT.HasValue)
                {
                    return currentTime >= r.ValidFromDT;
                }
                else if (!r.ValidFromDT.HasValue && r.ValidUntilDT.HasValue)
                {
                    return currentTime <= r.ValidUntilDT;
                }
                else
                {
                    return true;
                }
            }).ToList();

            var userHasAnyManagementPermissions = user.Roles.SelectMany(role => role.Permissions.Where((p) => p.CTAMModule == CTAMModule.Management)).Any();
            return userHasAnyManagementPermissions;
        }

        private void SetResponseCookies(string tenant, string accessToken, string refreshToken = null)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
#if !DEBUG
                        Secure = true,
#endif
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:AccessTokenValidityInSeconds", JwtToken.AccessTokenValidityInSeconds)),
            };

            Response.Cookies.Append("access_token_" + tenant, accessToken, cookieOptions);

            if (refreshToken != null)
            {
                cookieOptions.Expires = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:RefreshTokenValidityInSeconds", JwtToken.RefreshTokenValidityInSeconds));
                Response.Cookies.Append("refresh_token_" + tenant, refreshToken, cookieOptions);
            }
        }

        [HttpPost("web")]
        [AllowAnonymous]
        public async Task<IActionResult> Web([FromBody] GetUserByCredentialsQuery query, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var user = await _mediator.Send(query);
                if (user != null && UserHasValidIntervalsAndRoles(user))
                {
                    var accessToken = _authentication.GenerateWebToken(user, tenantID);
                    var refreshToken = await _mediator.Send(new CreateRefreshTokenForUserCommand() { User = user });

                    SetResponseCookies(tenantID, accessToken, refreshToken);
                    return Ok(new
                    {
                        token = accessToken,
                        userDetails = user,
                    });
                }
                throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_noValidIntervalAndRole);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("web/logout")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<IActionResult> WebLogout()
        {
            try
            {
                await _mediator.Send(new DeleteRefreshTokenForUserCommand() { HttpContext = Request.HttpContext });

                var tenant = HttpContext.Request.Headers["X-Tenant-ID"];
                Response.Cookies.Delete("access_token_" + tenant);
                Response.Cookies.Delete("refresh_token_" + tenant);

                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this, HttpStatusCode.Unauthorized);
            }
        }

        [HttpPost("web/refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var refreshToken = HttpContext.Request.Cookies["refresh_token_" + tenantID];

                if (refreshToken == null)
                {
                    throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_refreshTokenEmpty);
                }

                var query = new GetUserByTokenQuery()
                {
                    Token = refreshToken,
                    TokenType = JwtToken.RefreshToken
                };

                var user = await _mediator.Send(query);

                if (!UserHasValidIntervalsAndRoles(user))
                {
                    throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_noValidIntervalAndRole);
                }

                var newAccessToken = _authentication.GenerateWebToken(user, tenantID);

                SetResponseCookies(tenantID, newAccessToken);
                return Ok(new
                {
                    userDetails = user,
                    token = newAccessToken
                });
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("cardcode")]
        [AllowAnonymous]
        public async Task<IActionResult> Cardcode([FromBody] GetUserByCardcodeQuery query, [FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var user = await _mediator.Send(query);
                if (user != null)
                {
                    var tokenString = _authentication.GenerateWebToken(user, tenantID);
                    return Ok(new
                    {
                        token = tokenString,
                        userDetails = user,
                    });
                }
                throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_noValidCardCode);
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}