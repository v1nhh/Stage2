using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries
{
    public class GetRelatedCabinetNumbersByRoleIDsQuery : IRequest<Dictionary<int, List<string>>>
    {
        public GetRelatedCabinetNumbersByRoleIDsQuery(IEnumerable<int> CTAMRoleID)
        {
            this.CTAMRoleIDs = CTAMRoleID;
        }

        public IEnumerable<int> CTAMRoleIDs { get; set; }
    }

    public class GetRelatedCabinetNumbersByRoleIDsHandler : IRequestHandler<GetRelatedCabinetNumbersByRoleIDsQuery, Dictionary<int, List<string>>>
    {
        private readonly ILogger<GetRelatedCabinetNumbersByRoleIDsHandler> _logger;
        private readonly MainDbContext _context;

        public GetRelatedCabinetNumbersByRoleIDsHandler(MainDbContext context, ILogger<GetRelatedCabinetNumbersByRoleIDsHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Dictionary<int, List<string>>> Handle(GetRelatedCabinetNumbersByRoleIDsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRelatedCabinetNumbersByRoleIDHandler called");

            var cabinetNumbersQuery = from rc in _context.CTAMRole_Cabinet()
                                        where request.CTAMRoleIDs.Contains(rc.CTAMRoleID)
                                        select new { rc.CTAMRoleID, rc.CabinetNumber };

            var groupedCabinetNumbers = (await cabinetNumbersQuery.AsNoTracking()
                .ToListAsync())
                .Distinct()
                .GroupBy(x => x.CTAMRoleID)
                .ToDictionary(g => g.Key, g => g.Select(x => x.CabinetNumber).ToList());

            return groupedCabinetNumbers;
        }
    }
}
