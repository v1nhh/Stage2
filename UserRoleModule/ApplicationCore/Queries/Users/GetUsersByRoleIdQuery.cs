using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using UserRoleModule.ApplicationCore.DTO.Web;
using CTAM.Core;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUsersByRoleIdQuery : IRequest<List<UserWebDTO>>
    {
        public int RoleID { get; set; }

        public GetUsersByRoleIdQuery(int roleId)
        {
            RoleID = roleId;
        }
    }

    public class GetUsersByRoleIdHandler : IRequestHandler<GetUsersByRoleIdQuery, List<UserWebDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUsersByRoleIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetUsersByRoleIdHandler(MainDbContext context, ILogger<GetUsersByRoleIdHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserWebDTO>> Handle(GetUsersByRoleIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersByRoleIdHandler called");
            var result = await _context.CTAMUser()
                .AsNoTracking()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRole => userRole.CTAMRole)
                    .ThenInclude(role => role.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .Where(user => user.CTAMUser_Roles.Any(userRole => userRole.CTAMRoleID == request.RoleID))
                .ToListAsync();
            return _mapper.Map<List<UserWebDTO>>(result);
        }
    }

}
