using AutoMapper;
using CTAM.Core;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Queries.ErrorCodes
{
    public class GetErrorCodeToLiveSyncByIdQuery : IRequest<ErrorCodeBaseDTO>
    {
        public int ID { get; set; }

        public GetErrorCodeToLiveSyncByIdQuery(int id)
        {
            ID = id;
        }
    }

    public class GetErrorCodeToLiveSyncByIdHandler : IRequestHandler<GetErrorCodeToLiveSyncByIdQuery, ErrorCodeBaseDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetErrorCodeToLiveSyncByIdQuery> _logger;
        private readonly IMapper _mapper;

        public GetErrorCodeToLiveSyncByIdHandler(MainDbContext context, ILogger<GetErrorCodeToLiveSyncByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorCodeBaseDTO> Handle(GetErrorCodeToLiveSyncByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetErrorCodeToLiveSyncByIdHandler called");
            var result = await _context.ErrorCode().AsNoTracking()
                .Where(errorcode => errorcode.ID == request.ID)
                .FirstOrDefaultAsync();

            return _mapper.Map<ErrorCodeBaseDTO>(result);
        }
    }

}
