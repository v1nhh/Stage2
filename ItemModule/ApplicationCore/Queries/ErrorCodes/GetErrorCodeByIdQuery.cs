using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Queries.ErrorCodes
{
    public class GetErrorCodeByIdQuery : IRequest<ErrorCodeDTO>
    {
        public int ID { get; set; }

        public GetErrorCodeByIdQuery(int id)
        {
            ID = id;
        }
    }

    public class GetErrorCodeByIdHandler : IRequestHandler<GetErrorCodeByIdQuery, ErrorCodeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetErrorCodeByIdQuery> _logger;
        private readonly IMapper _mapper;

        public GetErrorCodeByIdHandler(MainDbContext context, ILogger<GetErrorCodeByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ErrorCodeDTO> Handle(GetErrorCodeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetErrorCodeByIdHandler called");
            var result = await _context.ErrorCode().AsNoTracking()
                .Where(errorcode => errorcode.ID == request.ID)
                .FirstOrDefaultAsync();

            if(result == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.errorCodes_apiExceptions_notFound,
                                                                  new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            return _mapper.Map<ErrorCodeDTO>(result);
        }
    }

}
