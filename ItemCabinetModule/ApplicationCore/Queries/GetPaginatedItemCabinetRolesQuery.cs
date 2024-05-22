using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using UserRoleModule.ApplicationCore.Utilities;
using ItemCabinetModule.ApplicationCore.Utilities;
using UserRoleModule.ApplicationCore.Entities;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Enums;
using CTAM.Core.Interfaces;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetPaginatedItemCabinetRolesQuery : IRequest<PaginatedResult<CTAMRole, ItemCabinetRoleDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public RoleColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }


        public GetPaginatedItemCabinetRolesQuery(int pageLimit, int page, RoleColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetPaginatedItemCabinetRolesHandler : IRequestHandler<GetPaginatedItemCabinetRolesQuery , PaginatedResult<CTAMRole, ItemCabinetRoleDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetPaginatedItemCabinetRolesHandler > _logger;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetPaginatedItemCabinetRolesHandler (MainDbContext context, ILogger<GetPaginatedItemCabinetRolesHandler > logger, IMapper mapper, ITenantContext tenantContext, ITenantService tenantService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _tenantContext = tenantContext;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Get a sublist of roles, optionally sorted and filtered on name. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<CTAMRole, ItemCabinetRoleDTO>> Handle(GetPaginatedItemCabinetRolesQuery  request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPaginatedItemCabinetRolesHandler called");

            var licensedPermissions = _tenantService.GetLicensePermissionsForTenant(_tenantContext.TenantId);
            bool desc = request.SortDescending;

            var total = await _context.CTAMRole().AsNoTracking().CountAsync();
            var roles = _context.CTAMRole().AsNoTracking()
                .Include(role => role.CTAMRole_Permission.Where(rp => licensedPermissions.Contains(rp.CTAMPermission.Description)))
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .AsQueryable();

            switch (request.SortedBy)
            {
                // for now only possible sorting is Description since in the UI all other columns are for linked tables
                case RoleColumn.Description:
                    roles = desc ? roles.OrderByDescending(x => x.Description) : roles.OrderBy(x => x.Description);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                roles = roles.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var itemCabinetRoleDTOs = await roles.Paginate<ItemCabinetRoleDTO>(request.PageLimit, request.Page, _mapper);

            itemCabinetRoleDTOs.OverallTotal = total;

            return itemCabinetRoleDTOs;
        }
    }

}
