using AutoMapper;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DTO.Web;
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
    public class GetItemTypesByRoleIdPaginatedQuery : IRequest<PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>>
    {
        public int RoleID { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetItemTypesByRoleIdPaginatedQuery(int id, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleID = id;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetItemTypesByRoleIdPaginatedHandler : IRequestHandler<GetItemTypesByRoleIdPaginatedQuery, PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemTypesByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetItemTypesByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetItemTypesByRoleIdPaginatedHandler> logger, IMapper mapper)
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
        public async Task<PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>> Handle(GetItemTypesByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemTypesByRoleIdPaginatedHandler called");

            bool desc = request.SortDescending;

            var total = await _context.ItemType().AsNoTracking().CountAsync();

            var itemTypes = _context.CTAMRole_ItemType().AsNoTracking()
                              .Include(ri => ri.ItemType)
                              .Where(ri => ri.CTAMRoleID.Equals(request.RoleID))
                              .Select(ri => ri);


            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                itemTypes = itemTypes.Where(x => EF.Functions.Like(x.ItemType.Description, $"%{request.FilterQuery}%"));
            }

            switch (request.SortedBy)
            {
                case MainTabColumn.Description:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.ItemType.Description) : itemTypes.OrderBy(x => x.ItemType.Description);
                    break;
                case MainTabColumn.maxQtyToPick:
                    itemTypes = desc ? itemTypes.OrderByDescending(x => x.MaxQtyToPick) : itemTypes.OrderBy(x => x.MaxQtyToPick);
                    break;
                default:
                    itemTypes = itemTypes.OrderBy(x => x.ItemType.Description);
                    break;
            }

            var result = await itemTypes.Paginate<ItemTypeWebDTO>(request.PageLimit, request.Page, _mapper);
            result.OverallTotal = total;

            return result;
        }
    }

}
