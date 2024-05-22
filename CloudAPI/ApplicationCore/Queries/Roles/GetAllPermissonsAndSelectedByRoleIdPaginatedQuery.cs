using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using CTAM.Core.Utilities;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Utilities;
using UserRoleModule.ApplicationCore.DTO.Web;
using CTAM.Core;
using UserRoleModule.ApplicationCore.Enums;
using System;
using CTAM.Core.Interfaces;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetAllPermissonsAndSelectedByRoleIdPaginatedQuery : IRequest<PaginatedResult<CTAMPermission, PermissionWebDTO>>
    {
        public string RoleId { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllPermissonsAndSelectedByRoleIdPaginatedQuery(string roleId, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleId = roleId;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllPermissonsAndSelectedByRoleIdPaginatedHandler : IRequestHandler<GetAllPermissonsAndSelectedByRoleIdPaginatedQuery, PaginatedResult<CTAMPermission, PermissionWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllPermissonsAndSelectedByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetAllPermissonsAndSelectedByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetAllPermissonsAndSelectedByRoleIdPaginatedHandler> logger, IMapper mapper, ITenantContext tenantContext, ITenantService tenantService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _tenantContext = tenantContext;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Get a sublist of roles that are assigned to the user. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<CTAMPermission, PermissionWebDTO>> Handle(GetAllPermissonsAndSelectedByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllPermissonsAndSelectedByRoleIdPaginatedHandler called");
            var licensedPermissions = _tenantService.GetLicensePermissionsForTenant(_tenantContext.TenantId);
            int roleId = -1;
            if (int.TryParse(request.RoleId, out int id))
            {
                roleId = id;
            }

            bool desc = request.SortDescending;
            var total = await _context.CTAMPermission().AsNoTracking().Where(p => licensedPermissions.Contains(p.Description)).CountAsync();
            var permissions = _context.CTAMPermission().Where(p => licensedPermissions.Contains(p.Description)).Include(p => p.CTAMRole_Permission).AsNoTracking();
            var checkedtotal = await _context.CTAMPermission().AsNoTracking()
                                            .Where(x => licensedPermissions.Contains(x.Description) && 
                                                        x.CTAMRole_Permission.Any(rp => rp.CTAMRoleID.Equals(roleId))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                permissions = permissions.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var orderedPermissions = permissions.OrderByDescending(x => x.CTAMRole_Permission.Any(rp => rp.CTAMRoleID.Equals(roleId)));

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    orderedPermissions = desc ? orderedPermissions.ThenByDescending(x => x.Description) : orderedPermissions.ThenBy(x => x.Description);
                    break;
                default:
                    orderedPermissions = orderedPermissions.ThenBy(x => x.Description);
                    break;
            }

            Func<CTAMPermission, IMapper, PermissionWebDTO> mapperFun = (CTAMPermission p, IMapper map) =>
            {
                return map.Map(p, new PermissionWebDTO { IsChecked = (p.CTAMRole_Permission.Any(rp => rp.CTAMRoleID.Equals(roleId))) });
            };

            var result = await orderedPermissions.Paginate<PermissionWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
