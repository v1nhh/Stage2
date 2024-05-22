using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.DTO;
using CTAM.Core;

namespace ItemModule.ApplicationCore.Commands.Sync
{
    public class SyncCriticalItemDataCommand: IRequest
    {
        public List<ItemDTO> ItemsDTO { get; set; }

        public SyncCriticalItemDataCommand(List<ItemDTO> itemsDTO)
        {
            ItemsDTO = itemsDTO;
        }
    }

    public class SyncCriticalItemDataHandler : IRequestHandler<SyncCriticalItemDataCommand>
    {
        private readonly ILogger<SyncCriticalItemDataHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public SyncCriticalItemDataHandler(MainDbContext context, ILogger<SyncCriticalItemDataHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SyncCriticalItemDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCriticalItemDataHandler called");
            try
            {
                if (request != null && request.ItemsDTO != null && request.ItemsDTO.Count > 0)
                {
                    await SyncItems(request.ItemsDTO);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                _logger.LogWarning("Item data is not synced!");
            }
            return new Unit();
        }

        public async Task SyncItems(List<ItemDTO> itemsDTO)
        {
            var itemsFromCabinetMap = _mapper.Map<List<Item>>(itemsDTO)
                .ToDictionary(i => i.ID);
            var itemsIds = itemsFromCabinetMap.Keys.ToList();
            var foundItems = await _context.Item().Where(item => itemsIds.Contains(item.ID)).ToListAsync();
            foreach (var item in foundItems)
            {
                var itemFromCabinet = itemsFromCabinetMap[item.ID];
                if (item.UpdateDT < itemFromCabinet.UpdateDT 
                    && (item.ErrorCodeID != itemFromCabinet.ErrorCodeID
                    || item.Status != itemFromCabinet.Status))
                {
                    item.ErrorCodeID = itemFromCabinet.ErrorCodeID;
                    item.Status = itemFromCabinet.Status;
                    item.UpdateDT = itemFromCabinet.UpdateDT;
                }
            }
        }
    }

}
