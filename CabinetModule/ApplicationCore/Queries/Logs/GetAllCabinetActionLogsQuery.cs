using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Utilities;

namespace CabinetModule.ApplicationCore.Queries.Logs
{
    public class GetAllCabinetActionLogsQuery : IRequest<PaginatedResult<CabinetAction, CabinetActionDTO>>
    {
        public int Page
        {
            get; set;
        }
        public int PageLimit
        {
            get; set;
        }
        public DateTime? FromDT
        {
            get; set;
        }
        public DateTime? UntilDT
        {
            get; set;
        }
        public CabinetActionLogColumn? SortedBy
        {
            get; set;
        }
        public bool SortDescending
        {
            get; set;
        }
        public string FilterQuery
        {
            get; set;
        }
        public string SelectedCabinetNumber
        {
            get; set;
        }

        public GetAllCabinetActionLogsQuery(int pageLimit, int page, DateTime? fromDT, DateTime? untilDT, CabinetActionLogColumn? sortedBy, bool sortDescending, string filterQuery, string selectedCabinetNumber)
        {
            PageLimit = pageLimit;
            Page = page;
            FromDT = fromDT;
            UntilDT = untilDT;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            SelectedCabinetNumber = selectedCabinetNumber;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllCabinetActionLogsHandler : IRequestHandler<GetAllCabinetActionLogsQuery, PaginatedResult<CabinetAction, CabinetActionDTO>>
    {
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger<GetAllCabinetActionLogsQuery> _logger;
        private readonly IMapper _mapper;


        public GetAllCabinetActionLogsHandler(MainDbContext context, IMediator mediator, ILogger<GetAllCabinetActionLogsQuery> logger, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CabinetAction, CabinetActionDTO>> Handle(GetAllCabinetActionLogsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllCabinetActionLogsQuery called");

            var sort = CabinetActionLogColumn.ActionDT;
            bool desc = false;
            if (request.SortedBy != null)
            {
                sort = (CabinetActionLogColumn)request.SortedBy;
                desc = request.SortDescending;
            }

            var cabinetActionQueryable = _context.CabinetAction().AsNoTracking();

            if (string.IsNullOrEmpty(request.SelectedCabinetNumber))
            {
                var accessibleCabinets = await _mediator.Send(new GetAccessibleCabinetsByLoggedInUserQuery());
                var accessibleCabinetNumbers = accessibleCabinets.Select(c => c.CabinetNumber);
                cabinetActionQueryable = cabinetActionQueryable.Where(x => accessibleCabinetNumbers.Contains(x.CabinetNumber));
            }
            else
            {
                cabinetActionQueryable = cabinetActionQueryable.Where(x => x.CabinetNumber == request.SelectedCabinetNumber);
            }

            if (request.FromDT != null)
            {
                cabinetActionQueryable = cabinetActionQueryable.Where(x => x.ActionDT > request.FromDT);
            }

            if (request.UntilDT != null)
            {
                cabinetActionQueryable = cabinetActionQueryable.Where(x => x.ActionDT < request.UntilDT);
            }

            switch (sort)
            {
                case CabinetActionLogColumn.CabinetNumber:
                    cabinetActionQueryable = desc ? cabinetActionQueryable.OrderByDescending(x => x.CabinetNumber) : cabinetActionQueryable.OrderBy(x => x.CabinetNumber);
                    break;
                case CabinetActionLogColumn.CabinetName:
                    cabinetActionQueryable = desc ? cabinetActionQueryable.OrderByDescending(x => x.CabinetName) : cabinetActionQueryable.OrderBy(x => x.CabinetName);
                    break;
                case CabinetActionLogColumn.UserUID:
                    cabinetActionQueryable = desc ? cabinetActionQueryable.OrderByDescending(x => x.CTAMUserUID) : cabinetActionQueryable.OrderBy(x => x.CTAMUserUID);
                    break;
                case CabinetActionLogColumn.ActionDT:
                    cabinetActionQueryable = desc ? cabinetActionQueryable.OrderByDescending(x => x.ActionDT) : cabinetActionQueryable.OrderBy(x => x.ActionDT);
                    break;
                case CabinetActionLogColumn.Action:
                    cabinetActionQueryable = desc ? cabinetActionQueryable.OrderByDescending(x => x.Action).ThenByDescending(x => x.ActionDT) : cabinetActionQueryable.OrderBy(x => x.Action).ThenByDescending(x => x.ActionDT);
                    break;
            }


            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                var resources = LocalTranslations.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
                var cabinetActionLogResources = new Dictionary<string, string>();
                foreach (DictionaryEntry entry in resources)
                {
                    if (entry.Key.ToString().StartsWith("cabinetActionLog", StringComparison.OrdinalIgnoreCase))
                    {
                        cabinetActionLogResources.Add(entry.Key.ToString(), entry.Value.ToString());
                    }
                }

                var filteredCabinetActionLogResourcePaths = cabinetActionLogResources
                    .Where(x => x.Value.Contains(request.FilterQuery, StringComparison.OrdinalIgnoreCase))
                    .Select(kv => kv.Key).ToList();

                cabinetActionQueryable = cabinetActionQueryable.Where(x =>
                    filteredCabinetActionLogResourcePaths.Contains(x.LogResourcePath) ||
                    EF.Functions.Like(x.CabinetNumber, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.CabinetName, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.PositionAlias, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.CTAMUserUID, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.CTAMUserEmail, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.ActionDT.AddHours(1).ToString(), $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.TakeItemDescription, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.PutItemDescription, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.ErrorCodeDescription, $"%{request.FilterQuery}%")
                );
            }
            var result = await cabinetActionQueryable.Paginate<CabinetActionDTO>(request.PageLimit, request.Page, _mapper);

            return result;
        }
    }
}
