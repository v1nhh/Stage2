using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetUIConfigQuery : IRequest<CabinetUIDTO>
    {
    }

    public class GetCabinetUIConfigHandler : IRequestHandler<GetCabinetUIConfigQuery, CabinetUIDTO>
    {
        private readonly ILogger<GetCabinetUIConfigHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public GetCabinetUIConfigHandler(MainDbContext context, ILogger<GetCabinetUIConfigHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabinetUIDTO> Handle(GetCabinetUIConfigQuery request, CancellationToken cancellationToken)
        {
            var cabinetUIConfig = await _context.CabinetUI()
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return _mapper.Map<CabinetUIDTO>(cabinetUIConfig);
        }
    }

}
