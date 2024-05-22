using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Web;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetAllCabinetsAndSelectedByRoleIdPaginatedQuery : IRequest<PaginatedResult<Cabinet, CabinetWebDTO>>
    {
        public string RoleId { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllCabinetsAndSelectedByRoleIdPaginatedQuery(string roleId, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleId = roleId;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllCabinetsAndSelectedByRoleIdPaginatedHandler : IRequestHandler<GetAllCabinetsAndSelectedByRoleIdPaginatedQuery, PaginatedResult<Cabinet, CabinetWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllCabinetsAndSelectedByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCabinetsAndSelectedByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetAllCabinetsAndSelectedByRoleIdPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<Cabinet, CabinetWebDTO>> Handle(GetAllCabinetsAndSelectedByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllCabinetsAndSelectedByRoleIdPaginatedHandler called");
            int roleId = -1;
            if (int.TryParse(request.RoleId, out int id))
            {
                roleId = id;
            }

            bool desc = request.SortDescending;
            var total = await _context.Cabinet().AsNoTracking().CountAsync();
            var cabinets = _context.Cabinet().Include(c => c.CTAMRole_Cabinets).AsNoTracking();
            var checkedtotal = await _context.Cabinet().AsNoTracking().Where(x => x.CTAMRole_Cabinets.Any(rc => rc.CTAMRoleID.Equals(roleId))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                cabinets = cabinets.Where(x => EF.Functions.Like(x.Name, $"%{request.FilterQuery}%"));
            }

            var orderedCabinets = cabinets.OrderByDescending(x => x.CTAMRole_Cabinets.Any(rc => rc.CTAMRoleID.Equals(roleId)));

            switch (request.SortedBy)
            {
                case MainTabColumn.Name:
                    orderedCabinets = desc ? orderedCabinets.ThenByDescending(x => x.Name) : orderedCabinets.ThenBy(x => x.Name);
                    break;
                default:
                    orderedCabinets = orderedCabinets.ThenBy(x => x.Name);
                    break;
            }

            Func<Cabinet, IMapper, CabinetWebDTO> mapperFun = (Cabinet c, IMapper map) =>
            {
                return map.Map(c, new CabinetWebDTO { IsChecked = (c.CTAMRole_Cabinets.Any(rc => rc.CTAMRoleID.Equals(roleId))) });
            };

            var result = await orderedCabinets.Paginate<CabinetWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
