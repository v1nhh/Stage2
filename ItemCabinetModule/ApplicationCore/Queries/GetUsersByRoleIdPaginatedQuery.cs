using AutoMapper;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetUsersByRoleIdPaginatedQuery : IRequest<PaginatedResult<CTAMUser, UserWebDTO>>
    {
        public int RoleID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetUsersByRoleIdPaginatedQuery(int id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleID = id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetUsersByRoleIdPaginatedHandler : IRequestHandler<GetUsersByRoleIdPaginatedQuery, PaginatedResult<CTAMUser, UserWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUsersByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetUsersByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetUsersByRoleIdPaginatedHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<CTAMUser, UserWebDTO>> Handle(GetUsersByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersByRoleIdPaginatedHandler called");

            bool desc = request.SortDescending;

            var total = await _context.CTAMUser().AsNoTracking().CountAsync();

            var users = _context.CTAMUser_Role().AsNoTracking()
                              .Include(ri => ri.CTAMUser)
                              .Where(ri => ri.CTAMRoleID.Equals(request.RoleID))
                              .Select(ri => ri.CTAMUser);

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                users = users.Where(x => EF.Functions.Like(x.Name, $"%{request.FilterQuery}%") ||
                                         EF.Functions.Like(x.Email, $"%{request.FilterQuery}%"));
            }

            switch (request.SortedBy)
            {
                case MainTabColumn.Name:
                    users = desc ? users.OrderByDescending(x => x.Name) : users.OrderBy(x => x.Name);
                    break;
                case MainTabColumn.Email:
                    users = desc ? users.OrderByDescending(x => x.Email) : users.OrderBy(x => x.Email);
                    break;
                default:
                    users = users.OrderBy(x => x.Name);
                    break;
            }

            var result = await users.Paginate<UserWebDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
