using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class IsItemInPossessionByCTAMAdminQuery : IRequest<bool>
    {
        public IsItemInPossessionByCTAMAdminQuery(int itemId)
        {
            this.ItemID = itemId;
        }

        public int ItemID { get; set; }
    }

    public class IsItemInPossessionByCTAMAdminHandler : IRequestHandler<IsItemInPossessionByCTAMAdminQuery, bool>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<IsItemInPossessionByCTAMAdminHandler> _logger;
        private readonly IMapper _mapper;

        public IsItemInPossessionByCTAMAdminHandler(MainDbContext context, ILogger<IsItemInPossessionByCTAMAdminHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<bool> Handle(IsItemInPossessionByCTAMAdminQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("IsItemInPossessionByCTAMAdminHandler called!");
            var lastCtamUserInPossession = await _context.CTAMUserInPossession().AsNoTracking().OrderByDescending(uip => uip.CreatedDT).FirstOrDefaultAsync();

            if (lastCtamUserInPossession != null && lastCtamUserInPossession.Status == UserInPossessionStatus.Removed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
