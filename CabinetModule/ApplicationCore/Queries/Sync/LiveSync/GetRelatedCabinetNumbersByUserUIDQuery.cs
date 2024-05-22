using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries
{
    public class GetRelatedCabinetNumbersByUserUIDsQuery : IRequest<Dictionary<string, List<string>>>
    {
        public GetRelatedCabinetNumbersByUserUIDsQuery(IEnumerable<string> CTAMUserUIDs)
        {
            this.CTAMUserUIDs = CTAMUserUIDs;
        }

        public IEnumerable<string> CTAMUserUIDs { get; set; }
    }

    public class GetRelatedCabinetNumbersByUserUIDsHandler : IRequestHandler<GetRelatedCabinetNumbersByUserUIDsQuery, Dictionary<string, List<string>>>
    {
        private readonly ILogger<GetRelatedCabinetNumbersByUserUIDsHandler> _logger;
        private readonly MainDbContext _context;

        public GetRelatedCabinetNumbersByUserUIDsHandler(MainDbContext context, ILogger<GetRelatedCabinetNumbersByUserUIDsHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        private class UserCabinetNumber
        {
            public string CTAMUserUID { get; set; }
            public string CabinetNumber { get; set; }
        }

        public async Task<Dictionary<string, List<string>>> Handle(GetRelatedCabinetNumbersByUserUIDsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRelatedCabinetNumbersByUserUIDHandler called");

            
            var userCabinetNumbers = new List<UserCabinetNumber>();

            foreach (var chunk in request.CTAMUserUIDs.ToList().Partition(15000))
            {
                var cabinetNumbersQuery =
                                    from ur in _context.CTAMUser_Role()
                                    where chunk.Contains(ur.CTAMUserUID)
                                    join rc in _context.CTAMRole_Cabinet() on ur.CTAMRoleID equals rc.CTAMRoleID
                                    select new UserCabinetNumber { CTAMUserUID = ur.CTAMUserUID, CabinetNumber = rc.CabinetNumber };

                var res = await cabinetNumbersQuery.AsNoTracking().ToListAsync();
                userCabinetNumbers.AddRange(res);
            }

            var groupedCabinetNumbers = userCabinetNumbers
                .Distinct()
                .GroupBy(x => x.CTAMUserUID)
                .ToDictionary(g => g.Key, g => g.Select(x => x.CabinetNumber).ToList());

            return groupedCabinetNumbers;
        }
    }
}
