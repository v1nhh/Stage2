using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetAllRolesAndSelectedByUserIdPaginatedQuery : IRequest<PaginatedResult<CTAMRole, RoleWebDTO>>
    {
        public string Uid { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public RoleColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllRolesAndSelectedByUserIdPaginatedQuery(string uid, int pageLimit, int page, RoleColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            Uid = uid;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllRolesAndSelectedByUserIdHandler : IRequestHandler<GetAllRolesAndSelectedByUserIdPaginatedQuery, PaginatedResult<CTAMRole, RoleWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllRolesAndSelectedByUserIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllRolesAndSelectedByUserIdHandler(MainDbContext context, ILogger<GetAllRolesAndSelectedByUserIdHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a sublist of roles that are assigned to the user. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<CTAMRole, RoleWebDTO>> Handle(GetAllRolesAndSelectedByUserIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllRolesAndSelectedByUserIdHandler called");

            bool desc = request.SortDescending;
            var total = await _context.CTAMRole().AsNoTracking().CountAsync();
            var roles = _context.CTAMRole().Include(r => r.CTAMUser_Roles).AsNoTracking();
            var checkedtotal = await _context.CTAMRole().AsNoTracking().Where(x => x.CTAMUser_Roles.Any(ur => ur.CTAMUserUID.Equals(request.Uid))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                roles = roles.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var orderedRoles = roles.OrderByDescending(x => x.CTAMUser_Roles.Any(ur => ur.CTAMUserUID.Equals(request.Uid)));

            switch (request.SortedBy)
            {
                case RoleColumn.Description:
                    orderedRoles = desc ? orderedRoles.ThenByDescending(x => x.Description) : orderedRoles.ThenBy(x => x.Description);
                    break;
                default:
                    orderedRoles = orderedRoles.ThenBy(x => x.Description);
                    break;
            }

            Func<CTAMRole, IMapper, RoleWebDTO> mapperFun = (CTAMRole r, IMapper map) =>
            {
                return map.Map(r, new RoleWebDTO { IsChecked = (r.CTAMUser_Roles.Any(ur => ur.CTAMUserUID.Equals(request.Uid))) });
            };

            var result = await orderedRoles.Paginate<RoleWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
