using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Utilities;

namespace ItemModule.ApplicationCore.Queries.Items
{
    public class GetItemsPaginatedQuery : IRequest<PaginatedResult<Item, ItemDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.ItemColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }
        public int? ItemTypeID { get; private set; }
        public UserInPossessionStatus? UserInPossessionStatus { get; private set; }

        public GetItemsPaginatedQuery(int pageLimit, int page, Enums.ItemColumn? sortedBy, bool sortDescending, string filterQuery, int? itemTypeID, UserInPossessionStatus? userInPossessionStatus)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
            ItemTypeID = itemTypeID;
            UserInPossessionStatus = userInPossessionStatus;
        }
    }

    public class GetItemsPaginatedHandler : IRequestHandler<GetItemsPaginatedQuery, PaginatedResult<Item, ItemDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemsPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetItemsPaginatedHandler(MainDbContext context, ILogger<GetItemsPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<Item, ItemDTO>> Handle(GetItemsPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemsPaginatedHandler called");

            bool desc = request.SortDescending;

            var itemsWithUserInPossessions = from i in _context.Item()
                                              .Include(item => item.ItemType)
                                             select new
                                             {
                                                 Item = i,
                                                 UIP = _context.CTAMUserInPossession()
                                                       .Where(uip => uip.ItemID == i.ID)
                                                       .OrderByDescending(x => (x.OutDT ?? x.InDT) ?? DateTime.MinValue)
                                                       .FirstOrDefault()
                                             };

            switch (request.SortedBy)
            {
                case Enums.ItemColumn.Id:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.ID) : itemsWithUserInPossessions.OrderBy(x => x.Item.ID);
                    break;
                case Enums.ItemColumn.Description:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.Description) : itemsWithUserInPossessions.OrderBy(x => x.Item.Description);
                    break;
                case Enums.ItemColumn.ItemType:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.ItemType.Description) : itemsWithUserInPossessions.OrderBy(x => x.Item.ItemType.Description);
                    break;
                case Enums.ItemColumn.Barcode:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.Barcode) : itemsWithUserInPossessions.OrderBy(x => x.Item.Barcode);
                    break;
                case Enums.ItemColumn.Tagnumber:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.Tagnumber) : itemsWithUserInPossessions.OrderBy(x => x.Item.Tagnumber);
                    break;
                case Enums.ItemColumn.ErrorCode:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.ErrorCode) : itemsWithUserInPossessions.OrderBy(x => x.Item.ErrorCode);
                    break;
                case Enums.ItemColumn.MaxLendingTimeInMins:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.MaxLendingTimeInMins) : itemsWithUserInPossessions.OrderBy(x => x.Item.MaxLendingTimeInMins);
                    break;
                case Enums.ItemColumn.Status:
                    itemsWithUserInPossessions = desc ? itemsWithUserInPossessions.OrderByDescending(x => x.Item.Status) : itemsWithUserInPossessions.OrderBy(x => x.Item.Status);
                    break;
                default:
                    itemsWithUserInPossessions = itemsWithUserInPossessions.OrderBy(x => x.Item.Description);
                    break;
            }

            if (request.ItemTypeID.HasValue)
            {
                itemsWithUserInPossessions = itemsWithUserInPossessions.Where(item => item.Item.ItemTypeID == request.ItemTypeID.Value);
            }

            if (request.UserInPossessionStatus.HasValue)
            {
                itemsWithUserInPossessions = itemsWithUserInPossessions.Where(uip => uip.UIP != null && uip.UIP.Status == request.UserInPossessionStatus.Value);
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                itemsWithUserInPossessions = itemsWithUserInPossessions.Where(x => EF.Functions.Like(x.Item.Description, $"%{request.FilterQuery}%") ||
                                         EF.Functions.Like(x.Item.ItemType.Description, $"%{request.FilterQuery}%") ||
                                         EF.Functions.Like(x.Item.Barcode, $"%{request.FilterQuery}%") ||
                                         EF.Functions.Like(x.Item.Tagnumber, $"%{request.FilterQuery}%")
                                   );
            }

            var items = itemsWithUserInPossessions.Select(i => i.Item);

            return await items.Paginate<ItemDTO>(request.PageLimit, request.Page, _mapper);
        }
    }

}
