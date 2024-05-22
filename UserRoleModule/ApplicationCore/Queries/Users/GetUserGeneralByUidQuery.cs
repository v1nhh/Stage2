using AutoMapper;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUserGeneralByUidQuery : IRequest<UserWebDTO>
    {
        public string UID { get; set; }

        public GetUserGeneralByUidQuery(string uid)
        {
            UID = uid;
        }
    }

    public class GetUserGeneralByUidHandler : IRequestHandler<GetUserGeneralByUidQuery, UserWebDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserGeneralByUidHandler> _logger;
        private readonly IMapper _mapper;


        public GetUserGeneralByUidHandler(MainDbContext context, ILogger<GetUserGeneralByUidHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserWebDTO> Handle(GetUserGeneralByUidQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserGeneralByUidHandler called");
            var result = await _context.CTAMUser().AsNoTracking()
                .Where(user => user.UID == request.UID)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserWebDTO>(result);
        }
    }

}
