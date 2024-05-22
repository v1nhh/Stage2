using CloudApiModule.ApplicationCore.DataManagers;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CloudAPI.ApplicationCore.Commands.Roles
{
    public class CheckAndDeleteRoleCommand: IRequest
    {
        public int ID { get; set; }

        public CheckAndDeleteRoleCommand(int id)
        {
            ID = id;
        }
    }

    public class CheckAndDeleteRoleHandler : IRequestHandler<CheckAndDeleteRoleCommand>
    {
        private readonly ILogger<CheckAndDeleteRoleHandler> _logger;
        private readonly MainDbContext _context;
        private readonly CloudApiDataManager _cloudManager;

        public CheckAndDeleteRoleHandler(ILogger<CheckAndDeleteRoleHandler> logger, MainDbContext context, CloudApiDataManager cloudManager)
        {
            _logger = logger;
            _context = context;
            _cloudManager = cloudManager;
        }

        public async Task<Unit> Handle(CheckAndDeleteRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndDeleteRoleHandler called");
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var doesUserHaveRoles = await _context.CTAMRole_Cabinet()
                    .Where(rc => rc.CTAMRoleID.Equals(request.ID))
                    .FirstOrDefaultAsync();

                if (doesUserHaveRoles != null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_roleDeleteHasCabinets);
                }

                // Check dependencies, throws if dependencychecks fails
                await _cloudManager.CheckAndDeleteCabinetStock(request.ID, null, null, true);

                await _cloudManager.DeleteRole(request.ID);

                scope.Complete();
            }
            return new Unit();
        }
    }
}
