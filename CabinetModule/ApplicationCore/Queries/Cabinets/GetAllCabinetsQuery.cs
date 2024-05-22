using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Utilities;
using Microsoft.Extensions.Logging;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetAllCabinetsQuery : IRequest<PaginatedResult<Cabinet, CabinetDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.CabinetDisplayColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }

        public GetAllCabinetsQuery(int pageLimit, int page, Enums.CabinetDisplayColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllCabinetsHandler : IRequestHandler<GetAllCabinetsQuery, PaginatedResult<Cabinet, CabinetDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetAllCabinetsHandler> _logger;
        private IMapper _mapper;

        public GetAllCabinetsHandler(MainDbContext context, ILogger<GetAllCabinetsHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a sublist of cabinets, optionally sorted and filtered on number, name or description. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<Cabinet, CabinetDTO>> Handle(GetAllCabinetsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllCabinetsHandler called");

            bool desc = request.SortDescending;

            var cabinets = _context.Cabinet().AsNoTracking()
                .Include(cabinet => cabinet.CabinetProperties)
                .AsQueryable();

            switch (request.SortedBy)
            {
                case Enums.CabinetDisplayColumn.CabinetNumber:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.CabinetNumber) : cabinets.OrderBy(x => x.CabinetNumber);
                    break;
                case Enums.CabinetDisplayColumn.Name:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.Name) : cabinets.OrderBy(x => x.Name);
                    break;
                case Enums.CabinetDisplayColumn.CabinetType:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.CabinetType) : cabinets.OrderBy(x => x.CabinetType);
                    break;
                case Enums.CabinetDisplayColumn.Description:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.Description) : cabinets.OrderBy(x => x.Description);
                    break;
                case Enums.CabinetDisplayColumn.LoginMethod:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.LoginMethod) : cabinets.OrderBy(x => x.LoginMethod);
                    break;
                case Enums.CabinetDisplayColumn.CreateDT:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.CreateDT) : cabinets.OrderBy(x => x.CreateDT);
                    break;
                case Enums.CabinetDisplayColumn.UpdateDT:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.UpdateDT) : cabinets.OrderBy(x => x.UpdateDT);
                    break;
                case Enums.CabinetDisplayColumn.LocationDescr:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.LocationDescr) : cabinets.OrderBy(x => x.LocationDescr);
                    break;
                case Enums.CabinetDisplayColumn.CabinetConfiguration:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.CabinetConfiguration) : cabinets.OrderBy(x => x.CabinetConfiguration);
                    break;
                case Enums.CabinetDisplayColumn.Email:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.Email) : cabinets.OrderBy(x => x.Email);
                    break;
                case Enums.CabinetDisplayColumn.IsActive:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.IsActive) : cabinets.OrderBy(x => x.IsActive);
                    break;
                case Enums.CabinetDisplayColumn.Status:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.Status) : cabinets.OrderBy(x => x.Status);
                    break;
                case Enums.CabinetDisplayColumn.LastSyncTimeStamp:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.LastSyncTimeStamp) : cabinets.OrderBy(x => x.LastSyncTimeStamp);
                    break;
                case Enums.CabinetDisplayColumn.HasSwipeCardAssign:
                    cabinets = desc ? cabinets.OrderByDescending(x => x.HasSwipeCardAssign) : cabinets.OrderBy(x => x.HasSwipeCardAssign);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                cabinets = cabinets.Where(x => EF.Functions.Like(x.CabinetNumber, $"%{request.FilterQuery}%") ||
                                               EF.Functions.Like(x.Name, $"%{request.FilterQuery}%") ||
                                               EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            return await cabinets.Paginate<CabinetDTO>(request.PageLimit, request.Page, _mapper);
        }
    }

}
