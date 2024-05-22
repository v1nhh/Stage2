using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CabinetModule.ApplicationCore.Commands.Cabinets
{
    public class UpdateCabinetStatusCommand : IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }
        public CabinetStatus Status { get; set; }

        public UpdateCabinetStatusCommand(string cabinetNumber, CabinetStatus status)
        {
            CabinetNumber = cabinetNumber;
            Status = status;
        }
    }

    public class UpdateCabinetStatusHandler : IRequestHandler<UpdateCabinetStatusCommand, CabinetDTO>
    {
        private readonly ILogger<UpdateCabinetStatusHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public UpdateCabinetStatusHandler(MainDbContext context, ILogger<UpdateCabinetStatusHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetDTO> Handle(UpdateCabinetStatusCommand request, CancellationToken cancellationToken)
        {
            var cabinet = await _context.Cabinet()
                .Where(c => c.CabinetNumber.Equals(request.CabinetNumber))
                .FirstOrDefaultAsync();
            cabinet.Status = request.Status;
            await _context.SaveChangesAsync();
            var msg = $"IBK '{cabinet.CabinetNumber}' '{cabinet.Name}' status naar '{cabinet.Status.ToString()}'";
            _logger.LogInformation(msg);
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_cabinetStatusChanged), ("cabinetNumber", cabinet.CabinetNumber), ("name", cabinet.Name), ("status", cabinet.Status.ToString()));
            return _mapper.Map<CabinetDTO>(cabinet);
        }
    }

}
