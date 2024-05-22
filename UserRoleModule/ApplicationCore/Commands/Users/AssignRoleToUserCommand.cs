using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;

namespace UserRoleModule.ApplicationCore.Commands.Users
{
    public class AssignRoleToUserCommand : IRequest<UserDTO>
    {
        public int RoleID { get; set; }

        public string UserUID { get; set; }
    }

    public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserCommand, UserDTO>
    {
        private readonly ILogger<AssignRoleToUserHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;


        public AssignRoleToUserHandler(MainDbContext context, ILogger<AssignRoleToUserHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<UserDTO> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AssignRoleToUserHandler called");

            var role = await _context.CTAMRole()
                .Include(r => r.CTAMUser_Roles)
                .SingleOrDefaultAsync(ctamRole => ctamRole.ID == request.RoleID);

            if (role == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", request.RoleID.ToString() } });
            }

            var user = await _context.CTAMUser()
                .SingleOrDefaultAsync(user => user.UID == request.UserUID);

            if (user == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFoundUser,
                                          new Dictionary<string, string> { { "uid", request.UserUID } });
            }

            if (role.CTAMUser_Roles.Any(ur => ur.CTAMUserUID == request.UserUID))
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_userRoleDuplicate,
                                          new Dictionary<string, string> { { "uid", request.UserUID },
                                              { "roleID", request.RoleID.ToString() },
                                              { "roleDescription", role.Description } });
            }

            var newUserRole = new CTAMUser_Role()
            {
                CTAMRoleID = request.RoleID,
                CTAMUserUID = user.UID,
            };
            _context.CTAMUser_Role().Add(newUserRole);
            await _context.SaveChangesAsync();

            var resultUser = await _context.CTAMUser()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                .SingleOrDefaultAsync(user => user.UID == request.UserUID);

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedRoleToUser), 
                    ("description", role.Description),
                    ("name", user.Name),
                    ("email", user.Email));

            return _mapper.Map<UserDTO>(resultUser);
        }
    }
}
