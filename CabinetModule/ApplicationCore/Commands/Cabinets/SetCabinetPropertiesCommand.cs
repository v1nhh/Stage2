using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Commands.Cabinets
{
    public class SetCabinetPropertiesCommand : IRequest<CabinetPropertiesDTO>
    {
        public string CabinetNumber { get; set; }

        public string LocalApiVersion { get; set; }

        public string LocalUiVersion { get; set; }
    }

    public class SetCabinetPropertiesHandler : IRequestHandler<SetCabinetPropertiesCommand, CabinetPropertiesDTO>
    {
        private readonly ILogger<SetCabinetPropertiesHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public SetCabinetPropertiesHandler(ILogger<SetCabinetPropertiesHandler> logger, MainDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabinetPropertiesDTO> Handle(SetCabinetPropertiesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SetCabinetPropertiesHandler called");
            if (string.IsNullOrWhiteSpace(request.CabinetNumber))
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_emptyNumber);
            }
            var cabinetExists = await _context.Cabinet()
                .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                .AnyAsync();
            if (!cabinetExists)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } });
            }
            var cabinetProperties = await _context.CabinetProperties().FirstOrDefaultAsync(cp => cp.CabinetNumber.Equals(request.CabinetNumber));
            if (cabinetProperties == null)
            {
                cabinetProperties = new CabinetProperties()
                {
                    CabinetNumber = request.CabinetNumber
                };
                FillVersionFields(cabinetProperties, request);
                await _context.CabinetProperties().AddAsync(cabinetProperties);
            }
            else
            {
                FillVersionFields(cabinetProperties, request);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<CabinetPropertiesDTO>(cabinetProperties);
        }

        public void FillVersionFields(CabinetProperties cabinetProperties, SetCabinetPropertiesCommand request)
        {
            cabinetProperties.LocalApiVersion = request.LocalApiVersion;
            cabinetProperties.LocalUiVersion = request.LocalUiVersion;
        }
    }
}
