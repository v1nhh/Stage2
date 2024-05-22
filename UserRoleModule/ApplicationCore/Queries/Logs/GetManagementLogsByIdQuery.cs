using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using CTAM.Core;

namespace UserRoleModule.ApplicationCore.Queries.Logs
{
    public class GetManagementLogsByIdQuery : IRequest<List<ManagementLogDTO>>
    {
        public int StartID { get; set; }
        public int EndID { get; set; }
        public int QueryLimit { get; set; }

        public GetManagementLogsByIdQuery(int start, int end, int limit)
        {
            StartID = start;
            EndID = end;
            QueryLimit = limit;
        }
    }

    public class GetManagementLogsByIdHandler : IRequestHandler<GetManagementLogsByIdQuery, List<ManagementLogDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetManagementLogsByIdQuery> _logger;
        private readonly IMapper _mapper;


        public GetManagementLogsByIdHandler(MainDbContext context, ILogger<GetManagementLogsByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<ManagementLogDTO>> Handle(GetManagementLogsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetManagementLogsByIdQuery called");
            if (request.EndID == -1)
            {
                var result = await _context.ManagementLog().AsNoTracking()
                    .Where(x => x.ID == request.StartID)
                    .ToListAsync();
                return _mapper.Map<List<ManagementLogDTO>>(result);
            }
            else
            {
                var result = await _context.ManagementLog().AsNoTracking()
                .Take(request.QueryLimit)
                .Where(x => x.ID >= request.StartID && x.ID <= request.EndID)
                .ToListAsync();
                return _mapper.Map<List<ManagementLogDTO>>(result);

            }
        }
    }

}
