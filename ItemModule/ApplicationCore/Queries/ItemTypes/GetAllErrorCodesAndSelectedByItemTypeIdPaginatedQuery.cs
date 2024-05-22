using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Utilities;
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
    public class GetAllErrorCodesAndSelectedByItemTypeIdPaginatedQuery : IRequest<PaginatedResult<ErrorCode, ErrorCodeWebDTO>>
    {
        public string ItemTypeID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllErrorCodesAndSelectedByItemTypeIdPaginatedQuery(string itemType_id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            ItemTypeID = itemType_id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllErrorCodesAndSelectedByItemTypeIdPaginatedHandler : IRequestHandler<GetAllErrorCodesAndSelectedByItemTypeIdPaginatedQuery, PaginatedResult<ErrorCode, ErrorCodeWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllErrorCodesAndSelectedByItemTypeIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllErrorCodesAndSelectedByItemTypeIdPaginatedHandler(MainDbContext context, ILogger<GetAllErrorCodesAndSelectedByItemTypeIdPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ErrorCode, ErrorCodeWebDTO>> Handle(GetAllErrorCodesAndSelectedByItemTypeIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllErrorCodesAndSelectedByItemTypeIdPaginatedHandler called");
            int itemType_id = -1;
            if (int.TryParse(request.ItemTypeID, out int id))
            {
                itemType_id = id;
            }

            bool desc = request.SortDescending;
            var total = await _context.ErrorCode().AsNoTracking().CountAsync();
            var errorCodes = _context.ErrorCode().Include(e => e.ItemType_ErrorCode).AsNoTracking();
            var checkedtotal = await _context.ErrorCode().AsNoTracking().Where(x => x.ItemType_ErrorCode.Any(ie => ie.ItemTypeID.Equals(itemType_id))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                errorCodes = errorCodes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%") ||
                                                   EF.Functions.Like(x.Code, $"%{request.FilterQuery}%"));
            }

            var orderedErrorCodes = errorCodes.OrderByDescending(x => x.ItemType_ErrorCode.Any(ie => ie.ItemTypeID.Equals(itemType_id)));

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    orderedErrorCodes = desc ? orderedErrorCodes.ThenByDescending(x => x.Description) : orderedErrorCodes.ThenBy(x => x.Description);
                    break;
                case MainTabColumn.Code:
                    orderedErrorCodes = desc ? orderedErrorCodes.ThenByDescending(x => x.Code) : orderedErrorCodes.ThenBy(x => x.Code);
                    break;
                default:
                    orderedErrorCodes = orderedErrorCodes.ThenBy(x => x.Description);
                    break;
            }

            Func<ErrorCode, IMapper, ErrorCodeWebDTO> mapperFun = (ErrorCode e, IMapper map) =>
            {
                return map.Map(e, new ErrorCodeWebDTO { IsChecked = (e.ItemType_ErrorCode.Any(ie => ie.ItemTypeID.Equals(itemType_id))) });
            };

            var result = await orderedErrorCodes.Paginate<ErrorCodeWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
