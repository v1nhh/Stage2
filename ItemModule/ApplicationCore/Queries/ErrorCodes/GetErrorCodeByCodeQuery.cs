using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using CTAM.Core;
using ItemModule.ApplicationCore.DTO;

namespace ItemModule.ApplicationCore.Queries.ErrorCodes
{
    public class GetErrorCodeByCodeQuery : IRequest<ErrorCodeDTO>
    {
        public string Code { get; set; }

        public GetErrorCodeByCodeQuery(string code)
        {
            Code = code;
        }
    }

    public class GetErrorCodeByCodeHandler : IRequestHandler<GetErrorCodeByCodeQuery, ErrorCodeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetErrorCodeByCodeQuery> _logger;
        private readonly IMapper _mapper;

        public GetErrorCodeByCodeHandler(MainDbContext context, ILogger<GetErrorCodeByCodeQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorCodeDTO> Handle(GetErrorCodeByCodeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetErrorCodeByCodeHandler called");
            var result = await _context.ErrorCode().AsNoTracking()
                .Where(errorcode => errorcode.Code == request.Code)
                .FirstOrDefaultAsync();

            return _mapper.Map<ErrorCodeDTO>(result);
        }
    }

}
