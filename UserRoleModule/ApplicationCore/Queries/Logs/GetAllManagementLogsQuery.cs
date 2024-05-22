using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.Queries.Logs
{
    public class GetAllManagementLogsQuery : IRequest<PaginatedResult<ManagementLog, ManagementLogDTO>>
    {
        public HttpContext HttpContext { get; set; }

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
        public ManagementLogColumn? SortedBy
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

        public GetAllManagementLogsQuery(int pageLimit, int page, DateTime? fromDT, DateTime? untilDT, ManagementLogColumn? sortedBy, bool sortDescending, string filterQuery, HttpContext httpContext)
        {
            PageLimit = pageLimit;
            Page = page;
            FromDT = fromDT;
            UntilDT = untilDT;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
            HttpContext = httpContext;
        }
    }

    public class GetAllManagementLogsHandler : IRequestHandler<GetAllManagementLogsQuery, PaginatedResult<ManagementLog, ManagementLogDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetAllManagementLogsQuery> _logger;
        private readonly IMapper _mapper;


        public GetAllManagementLogsHandler(MainDbContext context, ILogger<GetAllManagementLogsQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ManagementLog, ManagementLogDTO>> Handle(GetAllManagementLogsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllManagementLogsQuery called");
            var sort = ManagementLogColumn.LogDT;
            bool desc = false;
            if (request.SortedBy != null)
            {
                sort = (ManagementLogColumn)request.SortedBy;
                desc = request.SortDescending;
            }

            var managementLogQueryable = _context.ManagementLog().AsNoTracking();
            if (request.FromDT != null)
            {
                managementLogQueryable = managementLogQueryable.Where(x => x.LogDT > request.FromDT);
            }

            if (request.UntilDT != null)
            {
                managementLogQueryable = managementLogQueryable.Where(x => x.LogDT < request.UntilDT);
            }

            switch (sort)
            {
                case ManagementLogColumn.LogDT:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.LogDT) : managementLogQueryable.OrderBy(x => x.LogDT);
                    break;
                case ManagementLogColumn.Level:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.Level).ThenByDescending(x => x.ID) : managementLogQueryable.OrderBy(x => x.Level).ThenByDescending(x => x.ID);
                    break;
                case ManagementLogColumn.Source:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.Source).ThenByDescending(x => x.ID) : managementLogQueryable.OrderBy(x => x.Source).ThenByDescending(x => x.ID);
                    break;
                case ManagementLogColumn.UserUID:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.CTAMUserUID).ThenByDescending(x => x.ID) : managementLogQueryable.OrderBy(x => x.CTAMUserUID).ThenByDescending(x => x.ID);
                    break;
                case ManagementLogColumn.UserName:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.CTAMUserName).ThenByDescending(x => x.ID) : managementLogQueryable.OrderBy(x => x.CTAMUserName).ThenByDescending(x => x.ID);
                    break;
                case ManagementLogColumn.UserEmail:
                    managementLogQueryable = desc ? managementLogQueryable.OrderByDescending(x => x.CTAMUserEmail).ThenByDescending(x => x.ID) : managementLogQueryable.OrderBy(x => x.CTAMUserEmail).ThenByDescending(x => x.ID);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                var resources = CloudTranslations.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true);
                var managementLogResources = new Dictionary<string, string>();
                foreach (DictionaryEntry entry in resources)
                {
                    if (entry.Key.ToString().StartsWith("managementLog", StringComparison.OrdinalIgnoreCase))
                    {
                        managementLogResources.Add(entry.Key.ToString(), entry.Value.ToString());
                    }
                }

                var filteredManagementLogResourcePaths = managementLogResources
                    .Where(x => x.Value.Contains(request.FilterQuery, StringComparison.OrdinalIgnoreCase))
                    .Select(kv => kv.Key).ToList();

                managementLogQueryable = managementLogQueryable.Where(x => filteredManagementLogResourcePaths.Contains(x.LogResourcePath) ||
                    (EF.Functions.Like(x.LogResourcePath, $"%{request.FilterQuery}%") && !x.LogResourcePath.StartsWith("managementLog")) ||
                    EF.Functions.Like(x.Parameters, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.CTAMUserName, $"%{request.FilterQuery}%") ||
                    EF.Functions.Like(x.CTAMUserEmail, $"%{request.FilterQuery}%"));
            }

            var mapperOptions = new Dictionary<string, object>
            {
                { "User-Timezone", request.HttpContext.Request.Headers["User-Timezone"] }
            };
            var result = await managementLogQueryable
                .Paginate<ManagementLogDTO>(request.PageLimit, request.Page, _mapper, mapperOptions);
            return result;
        }
    }

}
