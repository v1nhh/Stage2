using System.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CTAM.Core;
using LogLevel = UserRoleModule.ApplicationCore.Enums.LogLevel;
using UserRoleModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Commands.Logs
{
    public class UpdateCabinetLogByIdCommand : IRequest<CabinetLogDTO>
    {
        public int ID { get; set; }

        public DateTime LogDT { get; set; }

        public LogLevel Level { get; set; }

        public string CabinetNumber { get; set; }

        public LogSource Source { get; set; }

        public string Log { get; set; }
    }

    public class UpdateCabinetLogByIdHandler : IRequestHandler<UpdateCabinetLogByIdCommand, CabinetLogDTO>
    {
        private readonly ILogger<UpdateCabinetLogByIdHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCabinetLogByIdHandler(MainDbContext context, ILogger<UpdateCabinetLogByIdHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabinetLogDTO> Handle(UpdateCabinetLogByIdCommand request, CancellationToken cancellationToken)
        {
            var cabinetLog = await _context.CabinetLog().SingleOrDefaultAsync( x => x.ID == request.ID);
            if (cabinetLog == null)
            {
                throw new Exception("UpdateCabinetLogByIdHandler encountered null response from CabinetLog table");
            }

            if (request.LogDT != cabinetLog.LogDT)
            {
                cabinetLog.LogDT = request.LogDT;
            }

            if (Enum.IsDefined(typeof(LogLevel), request.Level))
            {
                cabinetLog.Level = request.Level;
            }

            if (!string.IsNullOrEmpty(request.CabinetNumber))
            {
                cabinetLog.CabinetNumber = request.CabinetNumber;
            }

            cabinetLog.Source = request.Source;

            if (request.Log != null)
            {
                cabinetLog.LogResourcePath = request.Log;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var cabinetLogExists = _context.CabinetLog().Any(e => e.ID == request.ID);
                if (!cabinetLogExists)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return _mapper.Map<CabinetLogDTO>(cabinetLog);
        }
    }

}
