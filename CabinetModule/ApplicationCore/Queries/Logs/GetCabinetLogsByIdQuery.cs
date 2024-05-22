using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CTAM.Core;
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Queries.Logs
{
    public class GetCabinetLogsByIdQuery : IRequest<List<CabinetLogDTO>>
    {
        public int StartID { get; set; }
        public int EndID { get; set; }
        public int QueryLimit { get; set; }

        public GetCabinetLogsByIdQuery(int start, int end, int limit)
        {
            StartID = start;
            EndID = end;
            QueryLimit = limit;
        }
    }

    public class GetCabinetLogsByIdHandler : IRequestHandler<GetCabinetLogsByIdQuery, List<CabinetLogDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetLogsByIdQuery> _logger;
        private readonly IMapper _mapper;


        public GetCabinetLogsByIdHandler(MainDbContext context, ILogger<GetCabinetLogsByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CabinetLogDTO>> Handle(GetCabinetLogsByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetLogsQuery called");
            if (request.EndID == -1)
            {
                var result = await _context.CabinetLog().AsNoTracking()
                    .Where(x => x.ID == request.StartID)
                    .ToListAsync();
                return _mapper.Map<List<CabinetLogDTO>>(result);
            }
            else
            {
                var result = await _context.CabinetLog().AsNoTracking()
                .Take(request.QueryLimit)
                .Where(x => x.ID >= request.StartID && x.ID <= request.EndID)
                .ToListAsync();
                return _mapper.Map<List<CabinetLogDTO>>(result);

            }
        }
    }

}
