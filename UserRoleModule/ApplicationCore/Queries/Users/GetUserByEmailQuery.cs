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
    public class GetUserByEmailQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }

    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserByEmailQuery> _logger;
        private readonly IMapper _mapper;


        public GetUserByEmailHandler(MainDbContext context, ILogger<GetUserByEmailQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserByEmailHandler called");
            var result = await _context.CTAMUser().AsNoTracking()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                    .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .Where(user => user.Email.Equals(request.Email))
                .FirstOrDefaultAsync();
            
            if (result == null)
            {
                return null;
            }

            return new UserDTO() // Same limited values as for login (GetUserByCredentialsQuery)
            {
                UID = result.UID,
                Email = result.Email,
                Name = result.Name,
                CardCode = result.CardCode,
                PhoneNumber = result.PhoneNumber,
                LanguageCode = result.LanguageCode,
                Roles = result.CTAMUser_Roles.Select(ur => _mapper.Map<RoleDTO>(ur.CTAMRole)).ToList(),
                IsPasswordTemporary = result.IsPasswordTemporary,
            };
        }
    }

}
