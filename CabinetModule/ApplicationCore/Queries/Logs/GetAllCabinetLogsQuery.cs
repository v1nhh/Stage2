using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries.Logs
{
    public class GetAllCabinetLogsQuery : IRequest<PaginatedResult<CabinetLog, CabinetLogDTO>>
    {
       public int Page { get; set; }
       public int PageLimit { get; set; }
        public DateTime? FromDT { get; set; }
        public DateTime? UntilDT { get; set; }
        public CabinetLogColumn? SortedBy { get; set; }
        public bool SortDescending { get; set; }
        public string FilterQuery { get; set; }
        public string SelectedCabinetNumber { get; set; }

        public GetAllCabinetLogsQuery(int pageLimit, int page, DateTime? fromDT, DateTime? untilDT, CabinetLogColumn? sortedBy, bool sortDescending, string filterQuery, string selectedCabinetNumber)
        {
            PageLimit = pageLimit;
            Page = page;
            FromDT = fromDT;
            UntilDT = untilDT;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
            SelectedCabinetNumber = selectedCabinetNumber;
        }
    }

    public class GetAllCabinetLogsHandler : IRequestHandler<GetAllCabinetLogsQuery, PaginatedResult<CabinetLog, CabinetLogDTO>>
    {
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly ILogger<GetAllCabinetLogsQuery> _logger;
        private readonly IMapper _mapper;


        public GetAllCabinetLogsHandler(MainDbContext context, IMediator mediator, ILogger<GetAllCabinetLogsQuery> logger, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CabinetLog, CabinetLogDTO>> Handle(GetAllCabinetLogsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllCabinetLogsQuery called");
            var sort = CabinetLogColumn.LogDT;
            bool desc = false;
            if (request.SortedBy != null)
            {
                sort = (CabinetLogColumn)request.SortedBy;
                desc = request.SortDescending;
            }

            var cabinetLogQueryable = _context.CabinetLog().AsNoTracking();

            if (string.IsNullOrEmpty(request.SelectedCabinetNumber))
            {
                var accessibleCabinets = await _mediator.Send(new GetAccessibleCabinetsByLoggedInUserQuery());
                var accessibleCabinetNumbers = accessibleCabinets.Select(c => c.CabinetNumber);
                cabinetLogQueryable = cabinetLogQueryable.Where(x => accessibleCabinetNumbers.Contains(x.CabinetNumber));
            }
            else
            {
                cabinetLogQueryable = cabinetLogQueryable.Where(x => x.CabinetNumber == request.SelectedCabinetNumber);
            }

            if (request.FromDT != null)
            {
                cabinetLogQueryable = cabinetLogQueryable.Where(x => x.LogDT > request.FromDT);
            }

            if (request.UntilDT != null)
            {
                cabinetLogQueryable = cabinetLogQueryable.Where(x => x.LogDT < request.UntilDT);
            }

            switch (sort)
            {
                case CabinetLogColumn.LogDT:
                    cabinetLogQueryable = desc ? cabinetLogQueryable.OrderByDescending(x => x.LogDT) : cabinetLogQueryable.OrderBy(x => x.LogDT);
                    break;
                case CabinetLogColumn.Level:
                    cabinetLogQueryable = desc ? cabinetLogQueryable.OrderByDescending(x => x.Level).ThenByDescending(x => x.ID) : cabinetLogQueryable.OrderBy(x => x.Level).ThenByDescending(x => x.ID);
                    break;
                case CabinetLogColumn.CabinetNumber:
                    cabinetLogQueryable = desc ? cabinetLogQueryable.OrderByDescending(x => x.CabinetNumber).ThenByDescending(x => x.ID) : cabinetLogQueryable.OrderBy(x => x.CabinetNumber).ThenByDescending(x => x.ID);
                    break;
                case CabinetLogColumn.CabinetName:
                    cabinetLogQueryable = desc ? cabinetLogQueryable.OrderByDescending(x => x.CabinetName).ThenByDescending(x => x.ID) : cabinetLogQueryable.OrderBy(x => x.CabinetName).ThenByDescending(x => x.ID);
                    break;
                case CabinetLogColumn.Source:
                    cabinetLogQueryable = desc ? cabinetLogQueryable.OrderByDescending(x => x.Source).ThenByDescending(x => x.ID) : cabinetLogQueryable.OrderBy(x => x.Source).ThenByDescending(x => x.ID);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                var resources = LocalTranslations.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
                var cabinetLogResources = new Dictionary<string, string>();
                foreach (DictionaryEntry entry in resources)
                {
                    if (entry.Key.ToString().StartsWith("cabinetLog", StringComparison.OrdinalIgnoreCase))
                    {
                        cabinetLogResources.Add(entry.Key.ToString(), entry.Value.ToString());
                    }
                }

                var filteredCabinetLogResourcePaths = cabinetLogResources
                    .Where(x => x.Value.Contains(request.FilterQuery, StringComparison.OrdinalIgnoreCase))
                    .Select(kv => kv.Key);

                cabinetLogQueryable = cabinetLogQueryable.Where(x => filteredCabinetLogResourcePaths.Contains(x.LogResourcePath) ||
                // Used for backwards compatibility for logs that were saved in the database with the old 'Log' column.
                    (EF.Functions.Like(x.LogResourcePath, $"%{request.FilterQuery}%") && !x.LogResourcePath.StartsWith("cabinetLog")) ||
                    EF.Functions.Like(x.Parameters, $"%{request.FilterQuery}%")
                );
            }

            var result = await cabinetLogQueryable.Paginate<CabinetLogDTO>(request.PageLimit, request.Page, _mapper);
            return result;
        }
    }

}
