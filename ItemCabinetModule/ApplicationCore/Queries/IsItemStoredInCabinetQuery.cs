using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;
using CTAM.Core;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class IsItemStoredInCabinetQuery : IRequest<bool>
    {
        public IsItemStoredInCabinetQuery(int itemId)
        {
            this.ItemID = itemId;
        }

        public int ItemID { get; set; }
    }

    public class IsItemStoredInCabinetHandler : IRequestHandler<IsItemStoredInCabinetQuery, bool>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<IsItemStoredInCabinetHandler> _logger;
        private readonly IMapper _mapper;

        public IsItemStoredInCabinetHandler(MainDbContext context, ILogger<IsItemStoredInCabinetHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> Handle(IsItemStoredInCabinetQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("IsItemStoredInCabinetHandler called");
            return ( await _context.CabinetPositionContent().AsNoTracking().Where(c => c.ItemID == request.ItemID).CountAsync())>0;
        }
    }

}
