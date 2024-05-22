using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Web;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetCabinetsByRoleIdPaginatedQuery : IRequest<PaginatedResult<Cabinet, CabinetWebDTO>>
    {
        public int RoleID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetCabinetsByRoleIdPaginatedQuery(int id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleID = id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetCabinetsByRoleIdPaginatedHandler : IRequestHandler<GetCabinetsByRoleIdPaginatedQuery, PaginatedResult<Cabinet, CabinetWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetsByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetCabinetsByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetCabinetsByRoleIdPaginatedHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<Cabinet, CabinetWebDTO>> Handle(GetCabinetsByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetsByRoleIdPaginatedHandler called");

            bool desc = request.SortDescending;

            var total = await _context.Cabinet().AsNoTracking().CountAsync();

            var cabinets = _context.CTAMRole_Cabinet().AsNoTracking()
                              .Include(rc => rc.Cabinet)
                              .Where(rc => rc.CTAMRoleID.Equals(request.RoleID))
                              .Select(rc => rc.Cabinet);

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                cabinets = cabinets.Where(x => EF.Functions.Like(x.Name, $"%{request.FilterQuery}%"));
            }

            switch (request.SortedBy)
            {
                case MainTabColumn.Name:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.Name) : cabinets.OrderBy(x => x.Name);
                    break;
                default:
                    cabinets = cabinets.OrderBy(x => x.Name);
                    break;
            }

            var result = await cabinets.Paginate<CabinetWebDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
