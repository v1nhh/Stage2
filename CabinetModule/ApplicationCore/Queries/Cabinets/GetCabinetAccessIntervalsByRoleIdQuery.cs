using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CTAM.Core;
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetAccessIntervalsByRoleIdQuery : IRequest<Dictionary<int, List<CabinetAccessIntervalDTO>>>
    {
        public IEnumerable<int> RoleIDs { get; set; }

        public GetCabinetAccessIntervalsByRoleIdQuery(IEnumerable<int> roleIDs)
        {
            RoleIDs = roleIDs;
        }
    }

    public class GetCabinetAccessIntervalsByRoleIdHandler: IRequestHandler<GetCabinetAccessIntervalsByRoleIdQuery, Dictionary<int, List<CabinetAccessIntervalDTO>>>
    {
        private readonly ILogger<GetCabinetAccessIntervalsByRoleIdHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetCabinetAccessIntervalsByRoleIdHandler(ILogger<GetCabinetAccessIntervalsByRoleIdHandler> logger, MainDbContext context, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, List<CabinetAccessIntervalDTO>>> Handle(GetCabinetAccessIntervalsByRoleIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetAccessIntervalsByRoleIdHandler called");
            var cabinetAccessIntervals = await _context.CabinetAccessIntervals().AsNoTracking()
                .Where(ca => request.RoleIDs.Contains(ca.CTAMRoleID))
                .Include(ca => ca.CTAMRole)
                .ToListAsync();
            var cabinetAccessIntervalsPerRoleId = cabinetAccessIntervals
                .GroupBy(kv => kv.CTAMRoleID, kv => kv)
                .ToDictionary(gr => gr.Key, gr => gr.ToList());
            return _mapper.Map<Dictionary<int, List<CabinetAccessIntervalDTO>>>(cabinetAccessIntervalsPerRoleId);
        }
    }

}
