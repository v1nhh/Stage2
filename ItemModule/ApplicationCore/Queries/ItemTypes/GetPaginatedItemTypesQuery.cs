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

namespace ItemModule.ApplicationCore.Queries.ItemTypes
{
    public class GetPaginatedItemTypesQuery : IRequest<PaginatedResult<ItemType, ItemTypeDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.ItemTypeColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }

        public GetPaginatedItemTypesQuery(int pageLimit, int page, Enums.ItemTypeColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetPaginatedItemTypesHandler : IRequestHandler<GetPaginatedItemTypesQuery, PaginatedResult<ItemType, ItemTypeDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetPaginatedItemTypesHandler> _logger;
        private IMapper _mapper;

        /// <summary>
        /// Get a sublist of itemtypes, optionally sorted and filtered on description. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public GetPaginatedItemTypesHandler(MainDbContext context, ILogger<GetPaginatedItemTypesHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ItemType, ItemTypeDTO>> Handle(GetPaginatedItemTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllItemTypesHandler called");

            bool desc = request.SortDescending;

            var itemTypes = _context.ItemType().AsNoTracking().AsQueryable();

            switch (request.SortedBy)
            {
                case Enums.ItemTypeColumn.Id:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.ID) : itemTypes.OrderBy(x => x.ID);
                    break;
                case Enums.ItemTypeColumn.Description:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.Description) : itemTypes.OrderBy(x => x.Description);
                    break;
                case Enums.ItemTypeColumn.CreateDT:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.CreateDT) : itemTypes.OrderBy(x => x.CreateDT);
                    break;
                case Enums.ItemTypeColumn.UpdateDT:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.UpdateDT) : itemTypes.OrderBy(x => x.UpdateDT);
                    break;
                case Enums.ItemTypeColumn.TagType:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.TagType) : itemTypes.OrderBy(x => x.TagType);
                    break;
                case Enums.ItemTypeColumn.Depth:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.Depth) : itemTypes.OrderBy(x => x.Depth);
                    break;
                case Enums.ItemTypeColumn.Width:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.Width) : itemTypes.OrderBy(x => x.Width);
                    break;
                case Enums.ItemTypeColumn.Height:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.Height) : itemTypes.OrderBy(x => x.Height);
                    break;
                case Enums.ItemTypeColumn.MaxLendingTimeInMins:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.MaxLendingTimeInMins) : itemTypes.OrderBy(x => x.MaxLendingTimeInMins);
                    break;
                case Enums.ItemTypeColumn.RequiresMileageRegistration:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.RequiresMileageRegistration) : itemTypes.OrderBy(x => x.RequiresMileageRegistration);
                    break;
                case Enums.ItemTypeColumn.IsStoredInLocker:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.IsStoredInLocker) : itemTypes.OrderBy(x => x.IsStoredInLocker);
                    break;
                default:
                    itemTypes = itemTypes.OrderBy(x => x.Description);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                itemTypes = itemTypes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            return await itemTypes
                .Include(itemtype => itemtype.ItemType_ErrorCode)
                    .ThenInclude(errorCodes => errorCodes.ErrorCode)
                .Paginate<ItemTypeDTO>(request.PageLimit, request.Page, _mapper);
        }
    }

}
