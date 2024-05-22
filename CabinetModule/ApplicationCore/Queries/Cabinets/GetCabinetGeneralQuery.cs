using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
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

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetGeneralQuery : IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }

        public GetCabinetGeneralQuery(string cabinetNumber)
        {
            CabinetNumber = cabinetNumber;
        }
    }

    public class GetCabinetGeneralHandler : IRequestHandler<GetCabinetGeneralQuery, CabinetDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetGeneralHandler> _logger;
        private readonly IMapper _mapper;


        public GetCabinetGeneralHandler(MainDbContext context, ILogger<GetCabinetGeneralHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CabinetDTO> Handle(GetCabinetGeneralQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetGeneralHandler called");
            var result = await _context.Cabinet().AsNoTracking()
                .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } });
            }

            return _mapper.Map<CabinetDTO>(result);
        }
    }

}
