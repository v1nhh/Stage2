using AutoMapper;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CloudAPI.ApplicationCore.Commands.Users
{
    public class DeleteUserCommand: IRequest
    {
        public string UID { get; set; }

        public string InitiatorsEmail { get; set; }

        public DeleteUserCommand(string uID, string initiatorsEmail)
        {
            UID = uID;
            InitiatorsEmail = initiatorsEmail;
        }
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;
        private readonly MainDbContext _context;


        public DeleteUserHandler(MainDbContext context, ILogger<DeleteUserHandler> logger, IMediator mediator, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _managementLogger = managementLogger;
            _context = context;

        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("DeleteUserHandler called");

            var deleteInitiator = await _mediator.Send(new GetUserByEmailQuery(request.InitiatorsEmail));
            if (deleteInitiator.UID.Equals(request.UID))
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_userDeleteSelf);
            }

            var doesUserHaveRoles = await _context.CTAMUser_Role()
                .Where(ur => ur.CTAMUserUID.Equals(request.UID))
                .FirstOrDefaultAsync();

            if (doesUserHaveRoles != null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_userDeleteHasRoles);
            }

            var user = await _mediator.Send(new GetUserByUidQuery(request.UID));

            var userItemsInPossession = await _mediator.Send(new GetItemsInPossessionByCTAMUserQuery(request.UID, false));
            if (userItemsInPossession.Count > 0)
            {
                var itemsDescriptions = userItemsInPossession.Select(uip => uip.Item.Description).ToList();
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_userDeleteItemsInPossession,
                                          new Dictionary<string, string> { { "userName", user.Name },
                                                                           { "userEmail", user.Email },
                                                                           { "items", string.Join('\n', itemsDescriptions) } });
            }

            var readPermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Read")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();
            var writePermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Write")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();

            var userPermissionTuples = await _context.CTAMUser_Role().Where(ur => !ur.CTAMUserUID.Equals(request.UID))
                    .Join(_context.CTAMRole_Permission()
                        .Where(rp => rp.CTAMPermissionID == readPermissionID || rp.CTAMPermissionID == writePermissionID), ur => ur.CTAMRoleID, rp => rp.CTAMRoleID, (ur, rp) => new { User = ur.CTAMUserUID, Permission = rp.CTAMPermissionID })
                    .ToListAsync();

            // Get distinct users to check for latest user with access
            var users = userPermissionTuples.Select(upt => upt.User).Distinct().ToList();
            // Search for users with both read and write access
            var anotherUserWithAccess = users.Where(u => userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == readPermissionID).Any()
                                                        && userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == writePermissionID).Any());
            if (!anotherUserWithAccess.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_lastUserWithReadAndWriteAccessUser);
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _logger.LogInformation($"Proberen gebruiker met User UID: {user.UID} te verwijderen.");
                var userFromDb = await _context.CTAMUser().SingleOrDefaultAsync(ctamUser => ctamUser.UID.Equals(user.UID));
                user = _mapper.Map<UserDTO>(userFromDb);
                if(userFromDb != null)
                {
                    _context.CTAMUser().Remove(userFromDb);

                    await _context.SaveChangesAsync();
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedUser),
                    ("name", userFromDb.Name),
                    ("email", userFromDb.Email));
                }
                
                await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.UserDeleted.GetName(),
                    LanguageCode = user.LanguageCode,
                    // using default email template (ID = 1)
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name }
                    },
                });
                scope.Complete();
            }

            return new Unit();
        }
    }

}
