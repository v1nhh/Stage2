using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Utilities;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetAvailablePersonalItemsAndSelectedByUidPaginatedQuery : IRequest<PaginatedResult<Item, ItemWebDTO>>
    {
        public string Uid { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public ItemColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAvailablePersonalItemsAndSelectedByUidPaginatedQuery(string uid,int pageLimit, int page, ItemColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            Uid = uid;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAvailablePersonalItemsAndSelectedByUidPaginatedHandler : IRequestHandler<GetAvailablePersonalItemsAndSelectedByUidPaginatedQuery, PaginatedResult<Item, ItemWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAvailablePersonalItemsAndSelectedByUidPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetAvailablePersonalItemsAndSelectedByUidPaginatedHandler(MainDbContext context, ILogger<GetAvailablePersonalItemsAndSelectedByUidPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a sublist of items that are available with the selected status. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all items are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<Item, ItemWebDTO>> Handle(GetAvailablePersonalItemsAndSelectedByUidPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemsByStatusPaginatedHandler called");

            bool desc = request.SortDescending;

            var personalItemIDs = await _context.CTAMUserPersonalItem().AsNoTracking()
                .Where(upi => upi.CTAMUserUID == request.Uid)
                .Select(upi => upi.ItemID)
                .ToListAsync();

            var items = (from item in _context.Item()
                         join personalItem in _context.CTAMUserPersonalItem().Where(upi => upi.CTAMUserUID == request.Uid)
                         on item.ID equals personalItem.ItemID into personalItemGroup
                         from subPersonalItem in personalItemGroup.DefaultIfEmpty()

                         join pickedItemOthers in _context.CTAMUserInPossession().Where(uip => uip.CTAMUserUIDOut != request.Uid 
                            && (uip.Status == Enums.UserInPossessionStatus.Picked || uip.Status == Enums.UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified))
                         on item.ID equals pickedItemOthers.ItemID into pickedItemGroup
                         from subPickedItem in pickedItemGroup.DefaultIfEmpty()

                         join personalItemOthers in _context.CTAMUserPersonalItem().Where(upi => upi.CTAMUserUID != request.Uid)
                         on item.ID equals personalItemOthers.ItemID into personalItemOthersGroup
                         from subPersonalItemOthers in personalItemOthersGroup.DefaultIfEmpty()

                         join roleItemType in _context.CTAMUser_Role().Where(ur => ur.CTAMUserUID.Equals(request.Uid))
                             .Join(_context.CTAMRole_ItemType().Include(ri => ri.ItemType), ur => ur.CTAMRoleID, ri => ri.CTAMRoleID, (ur, ri) => ri)
                         on item.ItemTypeID equals roleItemType.ItemTypeID into roleItemTypeGroup
                         from subRoleItemType in roleItemTypeGroup.DefaultIfEmpty()

                         where
                         (subPersonalItemOthers == null && subPickedItem == null && subRoleItemType != null && (item.Status == ItemStatus.INITIAL || item.Status == ItemStatus.NOT_ACTIVE)) ||
                         subPersonalItem != null

                         select item)
                        .Include(i => i.ItemType)
                        .Distinct();

            var checkedtotal = await _context.CTAMUserPersonalItem().AsNoTracking()
                .Where(uid => uid.CTAMUserUID == request.Uid)
                .CountAsync();

            var total = await items.CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                items = items.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%") ||
                                        EF.Functions.Like(x.ItemType.Description, $"%{request.FilterQuery}%"));
            }

            var orderedItems = items.OrderByDescending(it => it.Description);

            if (personalItemIDs != null && personalItemIDs.Count() != 0)
            {
                orderedItems = items.OrderByDescending(it => personalItemIDs.Contains(it.ID));
            }

            switch (request.SortedBy)
            {
                case ItemColumn.Description:
                    orderedItems = desc ? orderedItems.ThenByDescending(x => x.Description) : orderedItems.ThenBy(x => x.Description);
                    break;
                case ItemColumn.ItemType:
                    orderedItems = desc ? orderedItems.ThenByDescending(x => x.ItemType) : orderedItems.ThenBy(x => x.ItemType.Description);
                    break;
                default:
                    orderedItems = orderedItems.ThenBy(x => x.Description);
                    break;
            }

            Func<Item, IMapper, ItemWebDTO> mapperFun = (Item i, IMapper map) =>
            {
                return map.Map(i, new ItemWebDTO { IsChecked = (personalItemIDs.Any(pit => pit == i.ID)) });
            };

            var result = await orderedItems.Paginate<ItemWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallCheckedTotal = checkedtotal;
            result.OverallTotal = total;
            return result;
        }
    }
}
