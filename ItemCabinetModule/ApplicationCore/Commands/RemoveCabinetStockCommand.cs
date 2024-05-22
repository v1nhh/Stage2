using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class RemoveCabinetStockCommand : IRequest
    {
        public string CabinetNumber { get; set; }
        public List<int> ItemTypeIds { get; set; }
    }

    public class RemoveCabinetStockHandler : IRequestHandler<RemoveCabinetStockCommand>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<RemoveCabinetStockHandler> _logger;

        public RemoveCabinetStockHandler(ILogger<RemoveCabinetStockHandler> logger, MainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(RemoveCabinetStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("RemoveCabinetStockCommand called for CabinetNumber: {0}", command.CabinetNumber));

            var cabinetStockToDelete = await _context.CabinetStock().Where(cs => 
                                                cs.CabinetNumber.Equals(command.CabinetNumber)
                                                  && command.ItemTypeIds.Contains(cs.ItemTypeID))
                                                .ToListAsync();

            _context.CabinetStock().RemoveRange(cabinetStockToDelete);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }

}