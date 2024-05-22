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
using CabinetModule.ApplicationCore.DTO.Web;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetsByRoleIdsQuery : IRequest<Dictionary<int, List<CabinetWebDTO>>>
    {
        public IEnumerable<int> RoleIDs { get; set; }

        public GetCabinetsByRoleIdsQuery(IEnumerable<int> roleIDs)
        {
            RoleIDs = roleIDs;
        }
    }

    public class GetCabinetsByRoleIdsHandler : IRequestHandler<GetCabinetsByRoleIdsQuery, Dictionary<int, List<CabinetWebDTO>>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetsByRoleIdsHandler> _logger;
        private readonly IMapper _mapper;

        public GetCabinetsByRoleIdsHandler(MainDbContext context, ILogger<GetCabinetsByRoleIdsHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, List<CabinetWebDTO>>> Handle(GetCabinetsByRoleIdsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetsByRoleIdsHandler called");
            var cabinetsWithRoles = await _context.CTAMRole_Cabinet().AsNoTracking()
                .Where(ri => request.RoleIDs.Contains(ri.CTAMRoleID))
                .Include(ri => ri.Cabinet)
                .Select(ri => new { ri.CTAMRoleID, ri.Cabinet })
                .ToListAsync();
            var cabinetsPerRoleId = cabinetsWithRoles
                .GroupBy(kv => kv.CTAMRoleID, kv => kv.Cabinet)
                .ToDictionary(gr => gr.Key, gr => gr.ToList());
            return _mapper.Map<Dictionary<int, List<CabinetWebDTO>>>(cabinetsPerRoleId);
        }
    }

}
