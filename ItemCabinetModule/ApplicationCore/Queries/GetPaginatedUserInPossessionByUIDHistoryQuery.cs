using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetPaginatedUserInPossessionsByUIDHistoryQuery : IRequest<PaginatedResult<CTAMUserInPossession, UserInPossessionDTO>>
    {
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public DateTime? FromDT { get; set; }
        public DateTime? UntilDT { get; set; }
        public UserInPossessionColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }
        public UserInPossessionStatus? Status { get; set; }
        public string UID { get; set; }

        public GetPaginatedUserInPossessionsByUIDHistoryQuery(int pageLimit, int page, string uid, DateTime? fromDT, DateTime? untilDT, 
                                                        UserInPossessionColumn? sortedBy, bool sortDescending, string filterQuery, UserInPossessionStatus? status)
        {
            PageLimit = pageLimit;
            Page = page;
            FromDT = fromDT;
            UntilDT = untilDT;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
            Status = status;
            UID = uid;
        }
    }

    public class GetPaginatedUserInPossessionsByUIDHistoryHandler : IRequestHandler<GetPaginatedUserInPossessionsByUIDHistoryQuery, PaginatedResult<CTAMUserInPossession, UserInPossessionDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetPaginatedUserInPossessionsByUIDHistoryHandler> _logger;
        private IMapper _mapper;

        public GetPaginatedUserInPossessionsByUIDHistoryHandler(MainDbContext context, ILogger<GetPaginatedUserInPossessionsByUIDHistoryHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CTAMUserInPossession, UserInPossessionDTO>> Handle(GetPaginatedUserInPossessionsByUIDHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPaginatedUserInPossessionsByUIDHistoryHandler called");

            var upl = _context.CTAMUserInPossession().AsNoTracking()
                            .Include(uip => uip.Item)
                            .ThenInclude(i => i.ItemType)
                            .Where(x => x.CTAMUserUIDOut == request.UID || x.CTAMUserUIDIn == request.UID);

            UserInPossessionColumn sort = UserInPossessionColumn.None;
            bool desc = false;
            if (request.SortedBy != null)
            {
                sort = (UserInPossessionColumn)request.SortedBy;
                desc = request.SortDescending;
            }

            if (request.FromDT != null)
            {
                upl = upl.Where(x => x.OutDT > request.FromDT || x.InDT > request.FromDT);
            }

            if (request.UntilDT != null)
            {
                upl = upl.Where(x => x.OutDT < request.UntilDT || x.InDT < request.UntilDT);
            }

            if (request.Status != null)
            {
                upl = upl.Where(x => x.Status == request.Status);
            }

            switch (sort)
            {
                case UserInPossessionColumn.OutDT:
                    upl = desc ? upl.OrderByDescending(x => x.OutDT) : upl.OrderBy(x => x.OutDT);
                    break;
                case UserInPossessionColumn.InDT:
                    upl = desc ? upl.OrderByDescending(x => x.InDT) : upl.OrderBy(x => x.InDT);
                    break;
                case UserInPossessionColumn.OutCTAMUserName:
                    upl = desc ? upl.OrderByDescending(x => x.CTAMUserNameOut) : upl.OrderBy(x => x.CTAMUserNameOut);
                    break;
                case UserInPossessionColumn.OutCTAMUserEmail:
                    upl = desc ? upl.OrderByDescending(x => x.CTAMUserEmailOut) : upl.OrderBy(x => x.CTAMUserEmailOut);
                    break;
                case UserInPossessionColumn.InCTAMUserName:
                    upl = desc ? upl.OrderByDescending(x => x.CTAMUserNameIn) : upl.OrderBy(x => x.CTAMUserNameIn);
                    break;
                case UserInPossessionColumn.InCTAMUserEmail:
                    upl = desc ? upl.OrderByDescending(x => x.CTAMUserEmailIn) : upl.OrderBy(x => x.CTAMUserEmailIn);
                    break;
                case UserInPossessionColumn.Status:
                    upl = desc ? upl.OrderByDescending(x => x.Status).ThenByDescending(x => x.OutDT) : upl.OrderBy(x => x.Status).ThenByDescending(x => x.OutDT);
                    break;
                case UserInPossessionColumn.CabinetPositionOutCabinetName:
                    upl = desc ? upl.OrderByDescending(x => x.CabinetNameOut).ThenByDescending(x => x.OutDT) : upl.OrderBy(x => x.CabinetNameOut).ThenByDescending(x => x.OutDT);
                    break;
                case UserInPossessionColumn.CabinetPositionInCabinetName:
                    upl = desc ? upl.OrderByDescending(x => x.CabinetNameIn).ThenByDescending(x => x.OutDT) : upl.OrderBy(x => x.CabinetNameIn).ThenByDescending(x => x.OutDT);
                    break;
                case UserInPossessionColumn.Item_Description:
                    upl = desc ? upl.OrderByDescending(x => x.Item.Description).ThenByDescending(x => x.OutDT) : upl.OrderBy(x => x.Item.Description).ThenByDescending(x => x.OutDT);
                    break;
                default:
                    upl = upl.OrderByDescending(x => (x.OutDT ?? DateTime.MinValue) > (x.InDT ?? DateTime.MinValue) ? x.OutDT : x.InDT);
                    _logger.LogInformation("GetPaginatedUserInPossessionsByUIDHistoryHandler: No column sorting selected");
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                upl = upl.Where(x => EF.Functions.Like(x.Item.Description, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.Item.ItemType.Description, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CabinetNameIn, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CabinetNameOut, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CTAMUserEmailIn, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CTAMUserEmailOut, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CTAMUserNameIn, $"%{request.FilterQuery}%") ||
                                     EF.Functions.Like(x.CTAMUserNameOut, $"%{request.FilterQuery}%")
                                     );
            }

            var result = await upl.Paginate<UserInPossessionDTO>(request.PageLimit, request.Page, _mapper);

            return result;
        }
    }

}
