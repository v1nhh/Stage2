using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Utilities;
using ItemCabinetModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetCabinetAccessIntervalsByRoleIdPaginatedQuery : IRequest<PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>>
    {
        public int RoleID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public int StartWeekDayNr { get; set; }
        public int EndWeekDayNr { get; set; }

        public GetCabinetAccessIntervalsByRoleIdPaginatedQuery(int id, int pageLimit, int page, int startWeekDayNr, int endWeekDayNr)
        {
            RoleID = id;
            PageLimit = pageLimit;
            Page = page;
            StartWeekDayNr = startWeekDayNr;
            EndWeekDayNr = endWeekDayNr;
        }
    }

    public class GetCabinetAccessIntervalsByRoleIdPaginatedHandler : IRequestHandler<GetCabinetAccessIntervalsByRoleIdPaginatedQuery, PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetAccessIntervalsByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetCabinetAccessIntervalsByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetCabinetAccessIntervalsByRoleIdPaginatedHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>> Handle(GetCabinetAccessIntervalsByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetAccessIntervalsByRoleIdPaginatedHandler called");

            var cabinetAccessIntervals = _context.CabinetAccessIntervals().AsNoTracking()
                              .Where(rcai => rcai.CTAMRoleID.Equals(request.RoleID))
                              .Select(rcai => rcai);

            if (request.StartWeekDayNr >= 0)
            {
                cabinetAccessIntervals = cabinetAccessIntervals.Where(cai => cai.StartWeekDayNr >= request.StartWeekDayNr || cai.EndWeekDayNr >= request.StartWeekDayNr);
            }

            if (request.EndWeekDayNr >= 0)
            {
                cabinetAccessIntervals = cabinetAccessIntervals.Where(cai => cai.StartWeekDayNr <= request.EndWeekDayNr || cai.EndWeekDayNr <= request.EndWeekDayNr);
            }

            cabinetAccessIntervals = cabinetAccessIntervals.OrderBy(cai => cai.StartWeekDayNr).ThenBy(cai => cai.StartTime);

            var result = await cabinetAccessIntervals.Paginate<CabinetAccessIntervalDTO>(request.PageLimit, request.Page, _mapper);

            return result;
        }
    }

}
