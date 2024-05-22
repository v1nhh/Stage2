using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using UserRoleModule.ApplicationCore.DTO;
using CTAM.Core;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUserByUidQuery : IRequest<UserDTO>
    {
        public string UID { get; set; }

        public GetUserByUidQuery(string uid)
        {
            UID = uid;
        }
    }

    public class GetUserByUidHandler : IRequestHandler<GetUserByUidQuery, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserByUidHandler> _logger;
        private readonly IMapper _mapper;


        public GetUserByUidHandler(MainDbContext context, ILogger<GetUserByUidHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByUidQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserByUidHandler called");
            var result = await _context.CTAMUser().AsNoTracking()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                    .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .Where(user => user.UID == request.UID)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDTO>(result);
        }
    }

}
