using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Utilities;
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
    public class GetErrorCodesByItemTypeIdPaginatedQuery : IRequest<PaginatedResult<ErrorCode, ErrorCodeDTO>>
    {
        public int ItemTypeID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetErrorCodesByItemTypeIdPaginatedQuery(int itemType_id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            ItemTypeID = itemType_id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetErrorCodesByItemTypeIdPaginatedHandler : IRequestHandler<GetErrorCodesByItemTypeIdPaginatedQuery, PaginatedResult<ErrorCode, ErrorCodeDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetErrorCodesByItemTypeIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetErrorCodesByItemTypeIdPaginatedHandler(MainDbContext context, ILogger<GetErrorCodesByItemTypeIdPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a sublist of errorcodes that are assigned to the itemtype. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all errorcodes are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<ErrorCode, ErrorCodeDTO>> Handle(GetErrorCodesByItemTypeIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetErrorCodesByItemTypeIdPaginatedHandler called");

            bool desc = request.SortDescending;

            var total = await _context.ErrorCode().AsNoTracking().CountAsync();

            var errorCodes = _context.ItemType_ErrorCode().AsNoTracking()
                              .Include(ie => ie.ErrorCode)
                              .Where(ie => ie.ItemTypeID.Equals(request.ItemTypeID))
                              .Select(rc => rc.ErrorCode);

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                errorCodes = errorCodes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%") ||
                                                   EF.Functions.Like(x.Code, $"%{request.FilterQuery}%"));
            }

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.Description) : errorCodes.OrderBy(x => x.Description);
                    break;
                case MainTabColumn.Code:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.Code) : errorCodes.OrderBy(x => x.Code);
                    break;
                default:
                    errorCodes = errorCodes.OrderBy(x => x.Description);
                    break;
            }

            var result = await errorCodes.Paginate<ErrorCodeDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
