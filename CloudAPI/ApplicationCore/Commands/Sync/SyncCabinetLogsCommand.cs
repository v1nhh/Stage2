using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using EFCore.BulkExtensions;
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
    public class SyncCabinetLogsCommand : IRequest
    {
        public string CabinetNumber { get; set; }

        public List<CabinetLogDTO> CabinetLogDTO { get; set; }
    }

    public class SyncCabinetLogsHandler : IRequestHandler<SyncCabinetLogsCommand>
    {
        private readonly ILogger<SyncCabinetLogsHandler> _logger;
        private readonly IMapper _mapper;
        private readonly MainDbContext _context;

        public SyncCabinetLogsHandler(ILogger<SyncCabinetLogsHandler> logger, IMapper mapper, MainDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Unit> Handle(SyncCabinetLogsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCabinetLogsHandler called");
            if (request == null || string.IsNullOrWhiteSpace(request.CabinetNumber))
            {
                var msg = "SyncCabinetLogsHandler failed: provided data cannot be null";
                _logger.LogError(msg);
                throw new NullReferenceException(msg);
            }

            var oldLogs = (from cld in request.CabinetLogDTO
                            join cl in _context.CabinetLog().AsNoTracking() on
                                new { cld.CabinetNumber, cld.LogDT, cld.Source, cld.LogResourcePath } equals
                                new { cl.CabinetNumber, cl.LogDT, cl.Source, cl.LogResourcePath }
                            select cld
                            ).ToList();
            var newLogs = request.CabinetLogDTO.Except(oldLogs);

            var cabinetLogs = _mapper.Map<List<CabinetLog>>(newLogs).Select(cabinetLog => {
                cabinetLog.ID = 0;
                return cabinetLog;
            });

            await _context.BulkInsertAsync(cabinetLogs.ToList());
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }
}