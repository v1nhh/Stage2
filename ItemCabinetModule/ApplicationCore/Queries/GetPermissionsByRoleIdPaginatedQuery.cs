using AutoMapper;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetPermissionsByRoleIdPaginatedQuery : IRequest<PaginatedResult<CTAMPermission, PermissionWebDTO>>
    {
        public int RoleID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetPermissionsByRoleIdPaginatedQuery(int id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleID = id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetPermissionsByRoleIdPaginatedHandler : IRequestHandler<GetPermissionsByRoleIdPaginatedQuery, PaginatedResult<CTAMPermission, PermissionWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetPermissionsByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetPermissionsByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetPermissionsByRoleIdPaginatedHandler> logger, IMapper mapper, ITenantContext tenantContext, ITenantService tenantService)
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
        public async Task<PaginatedResult<CTAMPermission, PermissionWebDTO>> Handle(GetPermissionsByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPermissionsByRoleIdPaginatedHandler called");

            var licensedPermissions = _tenantService.GetLicensePermissionsForTenant(_tenantContext.TenantId);
            bool desc = request.SortDescending;

            var total = await _context.CTAMPermission().AsNoTracking().Where(p => licensedPermissions.Contains(p.Description)).CountAsync();

            var permissions = (from rp in _context.CTAMRole_Permission()
                               where rp.CTAMRoleID.Equals(request.RoleID)
                               join p in _context.CTAMPermission().Where(p => licensedPermissions.Contains(p.Description))
                               on rp.CTAMPermissionID equals p.ID
                               select p)
                               .AsNoTracking()
                               .Distinct();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                permissions = permissions.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var orderedPermissions = permissions.OrderByDescending(p => p.CTAMModule);

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    orderedPermissions = desc ? orderedPermissions.ThenByDescending(x => x.Description) : orderedPermissions.ThenBy(x => x.Description);
                    break;
                default:
                    orderedPermissions = orderedPermissions.ThenBy(x => x.Description);
                    break;
            }

            var result = await orderedPermissions.Paginate<PermissionWebDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
