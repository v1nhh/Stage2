using CTAM.Core;
using CTAM.Core.Exceptions;
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
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.ItemTypes
{
    public class CheckAndDeleteItemTypeCommand : IRequest
    {
        public int ID { get; set; }

        public CheckAndDeleteItemTypeCommand(int id)
        {
            ID = id;
        }
    }

    public class CheckAndDeleteItemTypeHandler : IRequestHandler<CheckAndDeleteItemTypeCommand>
    {
        private readonly ILogger<CheckAndDeleteItemTypeHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public CheckAndDeleteItemTypeHandler(MainDbContext context, ILogger<CheckAndDeleteItemTypeHandler> logger, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(CheckAndDeleteItemTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndDeleteItemTypeHandler called");
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var itemType = await _context.ItemType()
                    .Where(itemtype => itemtype.ID == request.ID)
                    .Include(e => e.ItemType_ErrorCode)
                    .SingleOrDefaultAsync();

                if (itemType == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_notFound,
                                                                     new Dictionary<string, string> { { "id", request.ID.ToString() } });
                }

                var doesItemTypeHaveRoles = await _context.CTAMRole_ItemType()
                .Where(ri => ri.ItemTypeID.Equals(request.ID))
                .FirstOrDefaultAsync();

                if (doesItemTypeHaveRoles != null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.itemTypes_apiExceptions_itemTypeDeleteHasRoles);
                }

                var itemsOfType = await _context.Item().AsNoTracking()
                                                .AnyAsync(i => i.ItemTypeID == request.ID);
                if (itemsOfType)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_itemTypeInUseByItem,
                                                                     new Dictionary<string, string> { { "itemType", itemType.Description } });
                }

                var role = await _context.CTAMRole_ItemType().AsNoTracking()
                                         .Where(rit => rit.ItemTypeID == request.ID)
                                         .Include(rit => rit.CTAMRole)
                                         .Select(rit => rit.CTAMRole)
                                         .FirstOrDefaultAsync();
                if (role != null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_itemTypeInUseByRole,
                                                                     new Dictionary<string, string> { { "itemType", itemType.Description },
                                                                                                      { "role", role.Description } });
                }

                _context.ItemType_ErrorCode().RemoveRange(itemType.ItemType_ErrorCode);
                _context.ItemType().Remove(itemType);
                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedItemType), 
                    ("description", itemType.Description));
                scope.Complete();
            }
            return new Unit();
        }
    }
}
