using AutoMapper;
using CTAM.Core;
using UserRoleModule.ApplicationCore.Utilities;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetAllItemTypesAndSelectedByRoleIdPaginatedQuery : IRequest<PaginatedResult<ItemType, ItemTypeWebDTO>>
    {
        public string RoleId { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllItemTypesAndSelectedByRoleIdPaginatedQuery(string roleId, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleId = roleId;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllItemTypesAndSelectedByRoleIdPaginatedHandler : IRequestHandler<GetAllItemTypesAndSelectedByRoleIdPaginatedQuery, PaginatedResult<ItemType, ItemTypeWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllItemTypesAndSelectedByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllItemTypesAndSelectedByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetAllItemTypesAndSelectedByRoleIdPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ItemType, ItemTypeWebDTO>> Handle(GetAllItemTypesAndSelectedByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllItemTypesAndSelectedByRoleIdPaginatedHandler called");
            int roleId = -1;
            if (int.TryParse(request.RoleId, out int id))
            {
                roleId = id;
            }

            bool desc = request.SortDescending;
            var total = await _context.ItemType().AsNoTracking().CountAsync();
            var itemtypes = _context.ItemType().Include(it => it.CTAMRole_ItemType).AsNoTracking();
            var checkedtotal = await _context.ItemType().AsNoTracking().Where(x => x.CTAMRole_ItemType.Any(rit => rit.CTAMRoleID.Equals(roleId))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                itemtypes = itemtypes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%"));
            }

            var orderedItemTypes = itemtypes.OrderByDescending(x => x.CTAMRole_ItemType.Any(rit => rit.CTAMRoleID.Equals(roleId)));

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    orderedItemTypes = desc ? orderedItemTypes.ThenByDescending(x => x.Description) : orderedItemTypes.ThenBy(x => x.Description);
                    break;
                default:
                    orderedItemTypes = orderedItemTypes.ThenBy(x => x.Description);
                    break;
            }

            Func<ItemType, IMapper, ItemTypeWebDTO> mapperFun = (ItemType it, IMapper map) =>
            {
                return map.Map(it, new ItemTypeWebDTO { IsChecked = (it.CTAMRole_ItemType.Any(rit => rit.CTAMRoleID.Equals(roleId))), 
                                                        MaxQtyToPick = it.CTAMRole_ItemType.Where(rit => rit.CTAMRoleID.Equals(roleId)).FirstOrDefault()?.MaxQtyToPick });
            };

            var result = await orderedItemTypes.Paginate<ItemTypeWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
