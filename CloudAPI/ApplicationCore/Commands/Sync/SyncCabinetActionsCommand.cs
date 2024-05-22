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

namespace CloudAPI.ApplicationCore.Commands.Sync
{
    public class SyncCabinetActionsCommand : IRequest
    {
        public string CabinetNumber { get; set; }

        public List<CabinetActionDTO> CabinetActionDTO { get; set; }
    }

    public class SyncCabinetActionsHandler : IRequestHandler<SyncCabinetActionsCommand>
    {
        private readonly ILogger<SyncCabinetActionsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly MainDbContext _context;

        public SyncCabinetActionsHandler(ILogger<SyncCabinetActionsHandler> logger, IMapper mapper, MainDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(SyncCabinetActionsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCabinetActionsHandler called");
            if (request == null || string.IsNullOrWhiteSpace(request.CabinetNumber))
            {
                var msg = "SyncCabinetActionsHandler failed: provided data cannot be null";
                _logger.LogError(msg);
                throw new NullReferenceException(msg);
            }

            var newCabinetActionDTOs = request.CabinetActionDTO.Except((from cad in request.CabinetActionDTO
                                                                        join ca in _context.CabinetAction().AsNoTracking() on
                                                                        new { cad.ID } equals new { ca.ID }
                                                                        select cad).ToList());
            var newCabinetActions = _mapper.Map<List<CabinetAction>>(newCabinetActionDTOs);

            await _context.AddRangeAsync(newCabinetActions);
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}