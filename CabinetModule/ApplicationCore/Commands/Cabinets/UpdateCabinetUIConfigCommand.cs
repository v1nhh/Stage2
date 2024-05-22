using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CabinetModule.ApplicationCore.Commands.Cabinets
{
    public class UpdateCabinetUIConfigCommand : IRequest<CabinetUIDTO>
    {
#nullable enable
        public string? CabinetNumber { get; set; }
        public string? LogoWhite { get; set; }
        public string? LogoBlack { get; set; }
        public string? MenuTemplate { get; set; }
        public string? ColorTemplate { get; set; }
        public string? Font { get; set; }
#nullable disable
    }

    public class UpdateCabinetUIConfigHandler : IRequestHandler<UpdateCabinetUIConfigCommand, CabinetUIDTO>
    {
        private readonly ILogger<UpdateCabinetUIConfigHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public UpdateCabinetUIConfigHandler(MainDbContext context, ILogger<UpdateCabinetUIConfigHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetUIDTO> Handle(UpdateCabinetUIConfigCommand request, CancellationToken cancellationToken)
        {
            //TODO: in de database wordt UI informatie opgeslagen onder CabinetNumber. Hier wordt geen rekening mee gehouden!
            // https://itinnovatorsbv.atlassian.net/browse/CTAM-538
            var cabinetConfig = await _context.CabinetUI().FirstOrDefaultAsync();
            if (cabinetConfig == null)
            {
                var newCabinetConfig = new CabinetUI
                {
                    CabinetNumber = "DEFAULT", // This is hardcoded for the default configuration for all cabinets
                    LogoWhite = request.LogoWhite ?? "",
                    LogoBlack = request.LogoBlack ?? "",
                    ColorTemplate = request.ColorTemplate ?? "",
                    MenuTemplate = request.MenuTemplate ?? "",
                    Font = request.Font ?? "",
                };
                await _context.CabinetUI().AddAsync(newCabinetConfig);
                await _context.SaveChangesAsync();

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdCabinetUI));
                return _mapper.Map<CabinetUIDTO>(newCabinetConfig);
            }
            else
            {
                cabinetConfig.LogoWhite = request.LogoWhite ?? cabinetConfig.LogoWhite;
                cabinetConfig.LogoBlack = request.LogoBlack ?? cabinetConfig.LogoBlack;
                cabinetConfig.ColorTemplate = request.ColorTemplate ?? cabinetConfig.ColorTemplate;
                cabinetConfig.MenuTemplate = request.MenuTemplate ?? cabinetConfig.MenuTemplate;
                cabinetConfig.Font = request.Font ?? cabinetConfig.Font;
                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedCabinetUI));
                return _mapper.Map<CabinetUIDTO>(cabinetConfig);
            }
        }
    }

}
