using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    /// Used in LiveSyncService to send SignalR messages with CabinetNumbers
    public class GetRelatedCabinetNumbersByErrorCodeIDsQuery : IRequest<Dictionary<int, List<string>>>
    {
        public IEnumerable<int> ErrorCodeIDs { get; set; }
        public GetRelatedCabinetNumbersByErrorCodeIDsQuery(IEnumerable<int> errorCodeIDs)
        {
            ErrorCodeIDs = errorCodeIDs;
        }

    }

    public class GetRelatedCabinetNumbersByErrorCodeIDsHandler : IRequestHandler<GetRelatedCabinetNumbersByErrorCodeIDsQuery, Dictionary<int, List<string>>>
    {
        private readonly ILogger<GetRelatedCabinetNumbersByErrorCodeIDsHandler> _logger;
        private readonly MainDbContext _context;

        public GetRelatedCabinetNumbersByErrorCodeIDsHandler(MainDbContext context, ILogger<GetRelatedCabinetNumbersByErrorCodeIDsHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Dictionary<int, List<string>>> Handle(GetRelatedCabinetNumbersByErrorCodeIDsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRelatedCabinetNumbersByErrorCodeIDsHandler called");

            var cabinetNumbersQuery = from ie in _context.ItemType_ErrorCode()
                                      join ri in _context.CTAMRole_ItemType()
                                      on ie.ItemTypeID equals ri.ItemTypeID
                                      where request.ErrorCodeIDs.Contains(ie.ErrorCodeID)
                                      join rc in _context.CTAMRole_Cabinet()
                                      on ri.CTAMRoleID equals rc.CTAMRoleID
                                      select new { ie.ErrorCodeID, rc.CabinetNumber };

            var groupedCabinetNumbers = (await cabinetNumbersQuery.AsNoTracking()
                .ToListAsync())
                .Distinct()
                .GroupBy(x => x.ErrorCodeID)
                .ToDictionary(g => g.Key, g => g.Select(x => x.CabinetNumber).ToList());
            return groupedCabinetNumbers;
        }
    }
}
