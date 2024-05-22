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
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetAllUsersAndSelectedByRoleIdPaginatedQuery : IRequest<PaginatedResult<CTAMUser, UserWebDTO>>
    {
        public string RoleId { get; set; }
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public MainTabColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetAllUsersAndSelectedByRoleIdPaginatedQuery(string roleId, int pageLimit, int page, MainTabColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            RoleId = roleId;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllUsersAndSelectedByRoleIdPaginatedHandler : IRequestHandler<GetAllUsersAndSelectedByRoleIdPaginatedQuery, PaginatedResult<CTAMUser, UserWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllUsersAndSelectedByRoleIdPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllUsersAndSelectedByRoleIdPaginatedHandler(MainDbContext context, ILogger<GetAllUsersAndSelectedByRoleIdPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CTAMUser, UserWebDTO>> Handle(GetAllUsersAndSelectedByRoleIdPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllUsersAndSelectedByRoleIdPaginatedHandler called");
            int roleId = -1;
            if (int.TryParse(request.RoleId, out int id))
            {
                roleId = id;
            }

            bool desc = request.SortDescending;
            var total = await _context.CTAMUser().AsNoTracking().CountAsync();
            var users = _context.CTAMUser().Include(c => c.CTAMUser_Roles).AsNoTracking();
            var checkedtotal = await _context.CTAMUser().AsNoTracking().Where(x => x.CTAMUser_Roles.Any(ru => ru.CTAMRoleID.Equals(roleId))).CountAsync();

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                users = users.Where(x => EF.Functions.Like(x.Name, $"%{request.FilterQuery}%") ||
                                         EF.Functions.Like(x.Email, $"%{request.FilterQuery}%"));
            }

            var orderedUsers = users.OrderByDescending(x => x.CTAMUser_Roles.Any(ru => ru.CTAMRoleID.Equals(roleId)));

            switch (request.SortedBy)
            {
                case MainTabColumn.Name:
                    orderedUsers = desc ? orderedUsers.ThenByDescending(x => x.Name) : orderedUsers.ThenBy(x => x.Name);
                    break;
                default:
                    orderedUsers = orderedUsers.ThenBy(x => x.Name);
                    break;
            }

            Func<CTAMUser, IMapper, UserWebDTO> mapperFun = (CTAMUser c, IMapper map) =>
            {
                return map.Map(c, new UserWebDTO { IsChecked = (c.CTAMUser_Roles.Any(ru => ru.CTAMRoleID.Equals(roleId))) });
            };

            var result = await orderedUsers.Paginate<UserWebDTO>(request.PageLimit, request.Page, _mapper, mapperFun);
            result.OverallTotal = total;
            result.OverallCheckedTotal = checkedtotal;

            return result;
        }
    }

}
