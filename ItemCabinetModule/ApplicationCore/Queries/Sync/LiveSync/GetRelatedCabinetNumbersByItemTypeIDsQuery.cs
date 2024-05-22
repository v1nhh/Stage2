using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    /// Used in LiveSyncService to send SignalR messages with CabinetNumbers
    public class GetRelatedCabinetNumbersByItemTypeIDsQuery : IRequest<Dictionary<int, List<string>>>
  {
    public GetRelatedCabinetNumbersByItemTypeIDsQuery(IEnumerable<int> ItemTypeIDs)
    {
      this.ItemTypeIDs = ItemTypeIDs;
    }

    public IEnumerable<int> ItemTypeIDs { get; set; }
  }

  public class GetRelatedCabinetNumbersByItemTypeIDsHandler : IRequestHandler<GetRelatedCabinetNumbersByItemTypeIDsQuery, Dictionary<int, List<string>>>
  {
    private readonly ILogger<GetRelatedCabinetNumbersByItemTypeIDsHandler> _logger;
    private readonly MainDbContext _context;

    public GetRelatedCabinetNumbersByItemTypeIDsHandler(MainDbContext context, ILogger<GetRelatedCabinetNumbersByItemTypeIDsHandler> logger)
    {
      _logger = logger;
      _context = context;
    }

    public async Task<Dictionary<int, List<string>>> Handle(GetRelatedCabinetNumbersByItemTypeIDsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("GetRelatedCabinetNumbersByItemTypeIDsHandler called");

      var cabinetNumbersQuery = from ri in _context.CTAMRole_ItemType()
                    join rc in _context.CTAMRole_Cabinet()
                    on ri.CTAMRoleID equals rc.CTAMRoleID
                    where request.ItemTypeIDs.Contains(ri.ItemTypeID)
                    select new { ri.ItemTypeID, rc.CabinetNumber };

      var groupedCabinetNumbers = (await cabinetNumbersQuery.AsNoTracking().ToListAsync())
        .Distinct()
        .GroupBy(x => x.ItemTypeID)
        .ToDictionary(g => g.Key, g => g.Select(x => x.CabinetNumber).ToList());

      return groupedCabinetNumbers;
    }
  }
}
