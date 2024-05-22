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
    public class GetCabinetAccessIntervalByIdQuery : IRequest<CabinetAccessIntervalDTO>
    {
        public int ID { get; set; }

        public GetCabinetAccessIntervalByIdQuery (int id)
        {
            ID = id;
        }
    }

    public class GetCabinetAccessIntervalByIdHandler: IRequestHandler<GetCabinetAccessIntervalByIdQuery , CabinetAccessIntervalDTO>
    {
        private readonly ILogger<GetCabinetAccessIntervalByIdHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public GetCabinetAccessIntervalByIdHandler(ILogger<GetCabinetAccessIntervalByIdHandler> logger, MainDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabinetAccessIntervalDTO> Handle(GetCabinetAccessIntervalByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetAccessIntervalsByIdHandler called");
            var cabinetAccessIntervals = await _context.CabinetAccessIntervals().AsNoTracking().Where(c => c.ID == request.ID).FirstOrDefaultAsync();
            if (cabinetAccessIntervals == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_cabinetAccessIntervalNotFound,
                                          new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            return _mapper.Map<CabinetAccessIntervalDTO>(cabinetAccessIntervals);
        }
    }

}
