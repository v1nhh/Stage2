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

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetRolesByUserIdPaginatedQuery : IRequest<PaginatedResult<CTAMRole, RoleWebDTO>>
    {
        public string Uid { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public RoleColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetRolesByUserIdPaginatedQuery(string uid, int pageLimit, int page, RoleColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            Uid = uid;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetRolesByUserIdPaginatedHandler : IRequestHandler<GetRolesByUserIdPaginatedQuery, PaginatedResult<CTAMRole, RoleWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetRolesByUserIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetRolesByUserIdPaginatedHandler(MainDbContext context, ILogger<GetRolesByUserIdPaginatedHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<CTAMRole, RoleWebDTO>> Handle(GetRolesByUserIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRolesByUserIdPaginatedHandler called");

            bool desc = request.SortDescending;

            var total = await _context.CTAMRole().AsNoTracking().CountAsync();

            var roles = _context.CTAMUser_Role().AsNoTracking()
                .Include(ur => ur.CTAMRole)
                .Where(ur => ur.CTAMUserUID.Equals(request.Uid))
                .Select(ur => ur.CTAMRole)
                .AsQueryable();

            switch (request.SortedBy)
            {
                case RoleColumn.Description:
                    roles = desc ? roles.OrderByDescending(x => x.Description) : roles.OrderBy(x => x.Description);
                    break;
                default:
                    roles = roles.OrderBy(x => x.Description);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                roles = roles.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var result = await roles.Paginate<RoleWebDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
