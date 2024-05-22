using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Utilities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Utilities;
using ItemCabinetModule.ApplicationCore.DTO.Web;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetPersonalItemsByUidPaginatedQuery : IRequest<PaginatedResult<CTAMUserPersonalItem, UserPersonalItemWebDTO>>
    {
        public string Uid { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public ItemColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetPersonalItemsByUidPaginatedQuery(string uid, int pageLimit, int page, ItemColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            Uid = uid;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetPersonalItemsByUidHandler : IRequestHandler<GetPersonalItemsByUidPaginatedQuery, PaginatedResult<CTAMUserPersonalItem, UserPersonalItemWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetPersonalItemsByUidHandler> _logger;
        private readonly IMapper _mapper;

        public GetPersonalItemsByUidHandler(MainDbContext context, ILogger<GetPersonalItemsByUidHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<CTAMUserPersonalItem, UserPersonalItemWebDTO>> Handle(GetPersonalItemsByUidPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPersonalItemsByUidHandler  called");

            bool desc = request.SortDescending;
            var personalItems = _context.CTAMUserPersonalItem().AsNoTracking()
                .Include(upi => upi.Item)
                    .ThenInclude(item => item.ItemType)
                .Include(upi => upi.ReplacementItem)
                    .ThenInclude (item => item.ItemType)
                .Where(uid => uid.CTAMUserUID == request.Uid);

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                personalItems = personalItems.Where(x => EF.Functions.Like(x.Item.Description, $"%{request.FilterQuery}%") ||
                                                         EF.Functions.Like(x.Item.ItemType.Description, $"%{request.FilterQuery}%") ||
                                                         EF.Functions.Like(x.ReplacementItem.Description, $"%{request.FilterQuery}%"
                                                         ));
            }

            var orderedPersonalItems = personalItems.OrderByDescending(ur => ur.Item.Description.Equals(request.Uid));

            switch (request.SortedBy)
            {
                case ItemColumn.Description:
                    orderedPersonalItems = desc ? orderedPersonalItems.ThenByDescending(x => x.Item.Description) : orderedPersonalItems.ThenBy(x => x.Item.Description);
                    break;
                case ItemColumn.ItemType:
                    orderedPersonalItems = desc ? orderedPersonalItems.ThenByDescending(x => x.Item.ItemType) : orderedPersonalItems.ThenBy(x => x.Item.ItemType);
                    break;
                case ItemColumn.ReplacementItem:
                    orderedPersonalItems = desc ? orderedPersonalItems.ThenByDescending(x => x.ReplacementItem.Description) : orderedPersonalItems.ThenBy(x => x.ReplacementItem.Description);
                    break;
                default:
                    orderedPersonalItems = orderedPersonalItems.ThenBy(x => x.Item.Description);
                    break;
            }

            var result = await orderedPersonalItems.Paginate<UserPersonalItemWebDTO>(request.PageLimit, request.Page, _mapper);

            return result;
        }
    }
}