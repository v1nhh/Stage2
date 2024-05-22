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
    public class GetPaginatedItemTypesDescriptionQuery : IRequest<PaginatedResult<ItemType, ItemTypeDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.ItemTypeColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }

        public GetPaginatedItemTypesDescriptionQuery(int pageLimit, int page, Enums.ItemTypeColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetPaginatedItemTypesDescriptionHandler : IRequestHandler<GetPaginatedItemTypesDescriptionQuery, PaginatedResult<ItemType, ItemTypeDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetPaginatedItemTypesDescriptionHandler> _logger;
        private IMapper _mapper;

        /// <summary>
        /// Get a sublist of itemtypes, optionally sorted and filtered on description. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all roles are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public GetPaginatedItemTypesDescriptionHandler(MainDbContext context, ILogger<GetPaginatedItemTypesDescriptionHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ItemType, ItemTypeDTO>> Handle(GetPaginatedItemTypesDescriptionQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPaginatedItemTypesDescriptionHandler called");

            bool desc = request.SortDescending;

            var itemTypes = _context.ItemType().AsNoTracking().AsQueryable();

            switch (request.SortedBy)
            {
                case Enums.ItemTypeColumn.Description:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.Description) : itemTypes.OrderBy(x => x.Description);
                    break;
                default:
                    itemTypes = itemTypes.OrderBy(x => x.Description);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                itemTypes = itemTypes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            return await itemTypes.Paginate<ItemTypeDTO>(request.PageLimit, request.Page, _mapper);
        }
    }

}
