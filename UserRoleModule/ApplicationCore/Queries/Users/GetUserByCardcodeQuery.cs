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
    public class GetUserByCardcodeQuery : IRequest<UserDTO>
    {
        public string CardCode { get; set; }
    }

    public class GetUserByCardcodeHandler : IRequestHandler<GetUserByCardcodeQuery, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserByCardcodeQuery> _logger;
        private readonly IMapper _mapper;


        public GetUserByCardcodeHandler(MainDbContext context, ILogger<GetUserByCardcodeQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByCardcodeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserByCardcodeHandler called");
            var result = await _context.CTAMUser().AsNoTracking()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                    .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .Where(user => user.CardCode.Equals(request.CardCode))
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDTO>(result);
        }
    }

}
