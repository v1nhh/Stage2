using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.DTO.Web;
using CabinetModule.ApplicationCore.Entities;
using CloudAPI.ApplicationCore.Commands.Roles;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CloudAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IMediator _mediator;

        public RolesController(ILogger<RolesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Roles
        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMRole, ItemCabinetRoleDTO>>> GetRoles([FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                RoleColumn? sortCol = null;
                if (Enum.TryParse<RoleColumn>(sortedBy, out RoleColumn col))
                {
                    sortCol = col;
                }
                var query = new GetPaginatedItemCabinetRolesQuery(page_limit, page, sortCol, sortDescending, filterQuery);
                var allRoles = await _mediator.Send(query);
                return Ok(allRoles);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/general
        [HttpGet("{role_id}/general")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<ItemCabinetRoleDTO>> GetRoleGeneral(int role_id)
        {
            try
            {
                var query = new GetItemCabinetRoleGeneralByIdQuery(role_id);
                var role = await _mediator.Send(query);
                
                if (role == null)
                {
                    return NotFound();
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/permissions
        [HttpGet("{role_id}/permissions")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMPermission, PermissionWebDTO>>> GetPermissionsByRoleIdPaginated(int role_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetPermissionsByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var permissions = await _mediator.Send(query);

                if (permissions == null)
                {
                    return NotFound();
                }

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET /api/roles/{role_id}/allpermissionsandselected
        [HttpGet("{role_id}/allpermissionsandselected")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<CTAMPermission, PermissionWebDTO>>> GetAllPermissonsAndSelectedByRoleId(string role_id, [FromQuery] int page_limit = 50,
                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse<MainTabColumn>(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllPermissonsAndSelectedByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var permissions = await _mediator.Send(query);

                if (permissions == null)
                {
                    return BadRequest();
                }

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/cabinetaccessintervals
        [HttpGet("{role_id}/cabinetaccessintervals")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>>> GetCabinetAccessIntervalsByRoleIdPaginated(int role_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] int startWeekDayNr = -1, 
                                                                                                                 [FromQuery] int endWeekDayNr = -1)
        {
            try
            {
                var query = new GetCabinetAccessIntervalsByRoleIdPaginatedQuery(role_id, page_limit, page, startWeekDayNr, endWeekDayNr);
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/cabinets
        [HttpGet("{role_id}/cabinets")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMRole_Cabinet, CabinetWebDTO>>> GetCabinetsByRoleIdPaginated(int role_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetCabinetsByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var cabinets = await _mediator.Send(query);

                if (cabinets == null)
                {
                    return NotFound();
                }

                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/allcabinetsandselected
        [HttpGet("{role_id}/allcabinetsandselected")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMRole_Cabinet, CabinetWebDTO>>> GetAllCabinetsAndSelectedByRoleIdPaginated(string role_id, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllCabinetsAndSelectedByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var cabinets = await _mediator.Send(query);

                if (cabinets == null)
                {
                    return NotFound();
                }

                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/itemtypes
        [HttpGet("{role_id}/itemtypes")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>>> GetItemTypesByRoleIdPaginated(int role_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetItemTypesByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var itemTypes = await _mediator.Send(query);

                if (itemTypes == null)
                {
                    return NotFound();
                }

                return Ok(itemTypes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/allitemtypesandselected
        [HttpGet("{role_id}/allitemtypesandselected")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>>> GetAllItemTypesAndSelectedByRoleIdPaginated(string role_id, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllItemTypesAndSelectedByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var itemTypes = await _mediator.Send(query);

                if (itemTypes == null)
                {
                    return NotFound();
                }

                return Ok(itemTypes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/users
        [HttpGet("{role_id}/users")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMUser_Role, UserWebDTO>>> GetUsersByRoleIdPaginated(int role_id = 0, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetUsersByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var users = await _mediator.Send(query);

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // GET: api/Roles/{role_id}/allusersandselected
        [HttpGet("{role_id}/allusersandselected")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CTAMUser_Role, UserWebDTO>>> GetAllUsersAndSelectedByRoleIdPaginated(string role_id, [FromQuery] int page_limit = 50,
                                                                                                                 [FromQuery] int page = 0, [FromQuery] string sortedBy = "",
                                                                                                                 [FromQuery] bool sortDescending = false, [FromQuery] string filterQuery = "")
        {
            try
            {
                MainTabColumn? sortCol = null;
                if (Enum.TryParse(sortedBy, out MainTabColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllUsersAndSelectedByRoleIdPaginatedQuery(role_id, page_limit, page, sortCol, sortDescending, filterQuery);
                var users = await _mediator.Send(query);

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST: api/Roles
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<RoleWebDTO>> CreateRole(CreateRoleWithDependenciesCommand  role)
        {
            try
            {
                var result = await _mediator.Send(role);
                return CreatedAtAction("CreateRole", result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // PATCH /api/roles
        [HttpPatch]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<RoleWebDTO>> CheckAndModifyRole(CheckAndModifyRoleCommand request)
        {
            try
            {
                var modifiedRole = await _mediator.Send(request);
                return Ok(modifiedRole);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // DELETE: api/Roles/{role_id}
        [HttpDelete("{role_id}")]
        [Authorize(Roles = "MANAGEMENT_DELETE")]
        public async Task<ActionResult> DeleteRole(int role_id)
        {
            try
            {
                var command = new CheckAndDeleteRoleCommand(role_id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}