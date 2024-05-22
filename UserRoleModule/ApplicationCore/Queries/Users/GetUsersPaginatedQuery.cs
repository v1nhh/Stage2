using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using CTAM.Core.Utilities;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Utilities;
using UserRoleModule.ApplicationCore.DTO.Web;
using CTAM.Core;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUsersPaginatedQuery : IRequest<PaginatedResult<CTAMUser, UserWebDTO>>
    {
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public UserColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }

        public GetUsersPaginatedQuery(int pageLimit, int page, UserColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetUsersPaginatedHandler : IRequestHandler<GetUsersPaginatedQuery, PaginatedResult<CTAMUser, UserWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUsersPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetUsersPaginatedHandler(MainDbContext context, ILogger<GetUsersPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a sublist of users, optionally sorted and filtered on name, email, cardcode or role description. 
        /// </summary>
        /// <param name="request">If request.PageLimit < 0 all users are returned.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginatedResult<CTAMUser, UserWebDTO>> Handle(GetUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersPaginatedHandler called");

            bool desc = request.SortDescending;

            var user = _context.CTAMUser().AsNoTracking()
                .AsQueryable();

            switch (request.SortedBy)
            {
                case UserColumn.Name:
                    user = desc ? user.OrderByDescending(x => x.Name) : user.OrderBy(x => x.Name);
                    break;
                case UserColumn.Email:
                    user = desc ? user.OrderByDescending(x => x.Email) : user.OrderBy(x => x.Email);
                    break;
                case UserColumn.CardCode:
                    user = desc ? user.OrderByDescending(x => x.CardCode) : user.OrderBy(x => x.CardCode);
                    break;
                case UserColumn.CreateDT:
                    user = desc ? user.OrderByDescending(x => x.CreateDT) : user.OrderBy(x => x.CreateDT);
                    break;
                case UserColumn.UpdateDT:
                    user = desc ? user.OrderByDescending(x => x.UpdateDT) : user.OrderBy(x => x.UpdateDT);
                    break;
                case UserColumn.LoginCode:
                    user = desc ? user.OrderByDescending(x => x.LoginCode) : user.OrderBy(x => x.LoginCode);
                    break;
                case UserColumn.PhoneNumber:
                    user = desc ? user.OrderByDescending(x => x.PhoneNumber) : user.OrderBy(x => x.PhoneNumber);
                    break;
                case UserColumn.IsPasswordTemporary:
                    user = desc ? user.OrderByDescending(x => x.IsPasswordTemporary) : user.OrderBy(x => x.IsPasswordTemporary);
                    break;
                case UserColumn.LanguageCode:
                    user = desc ? user.OrderByDescending(x => x.LanguageCode) : user.OrderBy(x => x.LanguageCode);
                    break;
                default:
                    user = user.OrderBy(x => x.Name);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                user = user.Where(x => EF.Functions.Like(x.Email, $"%{request.FilterQuery}%") ||
                                   EF.Functions.Like(x.Name, $"%{request.FilterQuery}%") ||
                                   EF.Functions.Like(x.CardCode, $"%{request.FilterQuery}%") ||
                                   EF.Functions.Like(x.UID, $"%{request.FilterQuery}%")
                                   );
            }

            var result = await user.Paginate<UserWebDTO>(request.PageLimit, request.Page, _mapper);

            return result;
        }
    }

}
