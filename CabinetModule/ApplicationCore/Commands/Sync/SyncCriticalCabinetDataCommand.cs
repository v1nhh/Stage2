using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Commands.Sync
{
    public class SyncCriticalCabinetDataCommand : IRequest
    {
        public List<CabinetPositionDTO> CabinetPositions { get; set; }
        public List<CabinetDoorDTO> CabinetDoors { get; set; }

        public SyncCriticalCabinetDataCommand(List<CabinetPositionDTO> cabinetPositions, List<CabinetDoorDTO> cabinetDoors)
        {
            CabinetPositions = cabinetPositions;
            CabinetDoors = cabinetDoors;
        }
    }

    public class SyncCriticalCabinetDataHandler : IRequestHandler<SyncCriticalCabinetDataCommand>
    {
        private readonly ILogger<SyncCriticalCabinetDataHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public SyncCriticalCabinetDataHandler(MainDbContext context, ILogger<SyncCriticalCabinetDataHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SyncCriticalCabinetDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCriticalCabinetDataHandler called");
            try
            {
                var save = false;

                if (request.CabinetPositions != null && request.CabinetPositions.Count > 0)
                {
                    await SyncCabinetPositions(request.CabinetPositions);
                    save = true;
                }

                if (request.CabinetDoors != null && request.CabinetDoors.Count > 0)
                {
                    await SyncCabinetDoors(request.CabinetDoors);
                    save = true;
                }

                if (save) 
                { 
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cabinet data is not synced!");
            }
            return new Unit();
        }

        private async Task SyncCabinetPositions(List<CabinetPositionDTO> cabinetPositions)
        {
            var cabinetPositionMap = _mapper.Map<List<CabinetPosition>>(cabinetPositions)
                .ToDictionary(i => i.ID);
            var cabinetPositionIds = cabinetPositionMap.Keys.ToList();
            var foundCabinetPositions = await _context.CabinetPosition().Where(cp => cabinetPositionIds.Contains(cp.ID)).ToListAsync();
            foreach (var cabinetPosition in foundCabinetPositions)
            {
                var cabinetPositionFromCabinet = cabinetPositionMap[cabinetPosition.ID];
                if (cabinetPosition.Status != cabinetPositionFromCabinet.Status)
                {
                    cabinetPosition.Status = cabinetPositionFromCabinet.Status;
                }
            }
        }

        private async Task SyncCabinetDoors(List<CabinetDoorDTO> cabinetDoors)
        {
            var cabinetDoorMap = _mapper.Map<List<CabinetDoor>>(cabinetDoors)
                .ToDictionary(i => i.ID);
            var cabinetDoorIds = cabinetDoorMap.Keys.ToList();
            var foundCabinetDoors = await _context.CabinetDoor().Where(cd => cabinetDoorIds.Contains(cd.ID)).ToListAsync();
            foreach (var cabinetDoor in foundCabinetDoors)
            {
                var cabinetDoorFromCabinet = cabinetDoorMap[cabinetDoor.ID];
                if (cabinetDoor.Status != cabinetDoorFromCabinet.Status)
                {
                    cabinetDoor.Status = cabinetDoorFromCabinet.Status;
                }
            }
        }
    }

}
