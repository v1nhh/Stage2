using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class ShowCabinetDoorsQuery : IRequest<bool>
    {
        public string CabinetNumber { get; set; }
    }

    public class ShowCabinetDoorsHandler : IRequestHandler<ShowCabinetDoorsQuery, bool>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<ShowCabinetDoorsHandler> _logger;

        public ShowCabinetDoorsHandler(MainDbContext context, ILogger<ShowCabinetDoorsHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(ShowCabinetDoorsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ShowCabinetDoorsHandler called");
            var cabType = await _context.Cabinet().AsNoTracking().Where(c => c.CabinetNumber == request.CabinetNumber).Select(c => c.CabinetType).FirstOrDefaultAsync();

            if (cabType == CabinetType.CombiLocker || cabType == CabinetType.KeyConductor)
            {
                var doors = await _context.CabinetDoor()
                    .AsNoTracking()
                    .Where(cd => cd.CabinetNumber == request.CabinetNumber)
                    .CountAsync();
                return doors > 0;
            }

            return false;
        }
    }

}
