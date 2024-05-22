using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Interfaces;

namespace UserRoleModule.ApplicationCore.Commands.Users
{
    public class RemoveRoleFromUserCommand : IRequest<UserDTO>
    {
        public int RoleID { get; set; }

        public string UserUID { get; set; }
    }

    public class RemoveRoleFromUserHandler : IRequestHandler<RemoveRoleFromUserCommand, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;


        public RemoveRoleFromUserHandler(MainDbContext context, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<UserDTO> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.CTAMUser()
                .SingleOrDefaultAsync(user => user.UID == request.UserUID);
            var role = await _context.CTAMRole()
                .SingleOrDefaultAsync(ctamRole => ctamRole.ID == request.RoleID);
            var userRole = await _context.CTAMUser_Role()
                .Include(userRole => userRole.CTAMUser)
                .SingleOrDefaultAsync(userRole => userRole.CTAMRoleID == request.RoleID
                                                && userRole.CTAMUser.UID.Equals(request.UserUID));
            if (role == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", request.RoleID.ToString() } });
            }
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "uid", request.UserUID } });
            }
            if (userRole == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_userRoleNotFound,
                                          new Dictionary<string, string> { { "uid", request.UserUID },
                                              { "roleID", request.RoleID.ToString() },
                                              { "roleDescription", role.Description } });
            }
            _context.CTAMUser_Role().Remove(userRole);

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedRoleFromUser), 
                    ("description", role.Description),
                    ("name", user.Name),
                    ("email", user.Email));

            var updatedUser = await _context.CTAMUser()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRole => userRole.CTAMRole)
                    .ThenInclude(role => role.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .SingleOrDefaultAsync(user => user.UID.Equals(request.UserUID));

            return _mapper.Map<UserDTO>(updatedUser);
        }
    }

}
