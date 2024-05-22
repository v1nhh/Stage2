using CTAM.Core.Constants;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace UserRoleModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMUser, UserWebDTO>>> GetPaginatedUsers([FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                                                                 [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                                                 [FromQuery] string filterQuery = "")
        {
            try
            {
                UserColumn? sortCol = null;
                if (Enum.TryParse<UserColumn>(sortedBy, out UserColumn col))
                {
                    sortCol = col;
                }
                var query = new GetUsersPaginatedQuery(page_limit, page, sortCol, sortDescending, filterQuery);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Users/user_123/general
        [HttpGet("{uid}/general")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<UserWebDTO>> GetUserGeneral(string uid)
        {
            try
            {
                var query = new GetUserGeneralByUidQuery(uid);
                var user = await _mediator.Send(query);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("current")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<IActionResult> GetCurrentUser([FromHeader(Name = "X-Tenant-ID")] string tenantID)
        {
            try
            {
                var accessToken = HttpContext.Request.Cookies["access_token_" + tenantID];
                if (accessToken == null)
                {
                    throw new Exception("No access token found.");
                }

                var user = await _mediator.Send(new GetUserByTokenQuery()
                {
                    Tenant = tenantID,
                    Token = accessToken,
                    TokenType = JwtToken.AccessToken
                });

                return Ok(new
                {
                    userDetails = user,
                });
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET /api/users/user_123/roles
        [HttpGet("{uid}/roles")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMRole, RoleWebDTO>>> GetRolesByUserIdPaginated(string uid = "", [FromQuery] int page_limit = 50,
                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                RoleColumn? sortCol = null;
                if (Enum.TryParse<RoleColumn>(sortedBy, out RoleColumn col))
                {
                    sortCol = col;
                }
                var query = new GetRolesByUserIdPaginatedQuery(uid, page_limit, page, sortCol, sortDescending, filterQuery);
                var roles = await _mediator.Send(query);

                if (roles == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                                              new Dictionary<string, string> { { "id", uid } });
                }

                return roles;
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET /api/users/user_123/allrolesandselected
        [HttpGet("{uid}/allrolesandselected")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMRole, RoleWebDTO>>> GetAllRolesAndSelectedByUserId(string uid = "", [FromQuery] int page_limit = 50,
                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                RoleColumn? sortCol = null;
                if (Enum.TryParse<RoleColumn>(sortedBy, out RoleColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllRolesAndSelectedByUserIdPaginatedQuery(uid, page_limit, page, sortCol, sortDescending, filterQuery);
                var role = await _mediator.Send(query);

                if (role == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                                              new Dictionary<string, string> { { "id", uid } });
                }

                return role;
            }

            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Users/role/5
        [HttpGet("role/{role_id}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<List<UserWebDTO>>> GetUsersByRoleId(int role_id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetUsersByRoleIdQuery(role_id)));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("checkcardcode")]
        [Authorize(Roles = "CABINET_ACTIONS")]
        public async Task<ActionResult<bool>> CheckCardcode([FromBody] CheckCardCodeInUseQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}