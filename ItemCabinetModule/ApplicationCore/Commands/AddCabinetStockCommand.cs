using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class AddCabinetStockCommand : IRequest
    {
        public string CabinetNumber { get; set; }
        public List<int> ItemTypeIds { get; set; }
    }
    
    public class AddCabinetStockHandler : IRequestHandler<AddCabinetStockCommand>
    {
        private readonly ILogger<AddCabinetStockHandler> _logger;
        private readonly MainDbContext _context;

        public AddCabinetStockHandler(ILogger<AddCabinetStockHandler> logger, MainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(AddCabinetStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("AddDefaultCabinetStockCommand called for CabinetNumber: {0}", command.CabinetNumber));

            var newCabinetStock = new List<CabinetStock>();
            foreach (var itemTypeId in command.ItemTypeIds)
            {
                newCabinetStock.Add(GetDefaultCabinetStock(command.CabinetNumber, itemTypeId));
            }

            if (newCabinetStock.Count > 0)
            {
                await _context.CabinetStock().AddRangeAsync(newCabinetStock);
                await _context.SaveChangesAsync();
            }

            return new Unit();
        }        

        private CabinetStock GetDefaultCabinetStock(string cabinetNumber, int itemTypeId)
        {
            return new CabinetStock()
            {
                CabinetNumber = cabinetNumber,
                ItemTypeID = itemTypeId,
                CreateDT = DateTime.UtcNow,
                MinimalStock = 0,
                ActualStock = 0,
                Status = Enums.CabinetStockStatus.OK                
            };
        }
    }
}
