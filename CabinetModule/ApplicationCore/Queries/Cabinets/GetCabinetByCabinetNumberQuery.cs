using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetByCabinetNumberQuery : IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }

        public GetCabinetByCabinetNumberQuery(string cabinetNumber)
        {
            CabinetNumber = cabinetNumber;
        }
    }

    public class GetCabinetByCabinetNumberHandler : IRequestHandler<GetCabinetByCabinetNumberQuery, CabinetDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetByCabinetNumberHandler> _logger;
        private readonly IMapper _mapper;


        public GetCabinetByCabinetNumberHandler(MainDbContext context, ILogger<GetCabinetByCabinetNumberHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CabinetDTO> Handle(GetCabinetByCabinetNumberQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetByCabinetNumberHandler called");
            try
            {
                var result = await _context.Cabinet().AsNoTracking()
                    .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                    .Include(cabinet => cabinet.CabinetProperties)
                    .FirstOrDefaultAsync();
                return result == null
                    ? throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } })
                    : _mapper.Map<CabinetDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
