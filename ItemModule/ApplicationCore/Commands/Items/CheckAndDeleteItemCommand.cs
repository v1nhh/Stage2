using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CTAM.Core.Commands.Items
{
    public class CheckAndDeleteItemCommand : IRequest<ItemDTO>
    {
        public int ID { get; set; }

        public CheckAndDeleteItemCommand(int id)
        {
            ID = id;
        }

    }

    public class CheckAndDeleteItemHandler : IRequestHandler<CheckAndDeleteItemCommand, ItemDTO>
    {
        private readonly ILogger<CheckAndDeleteItemHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public CheckAndDeleteItemHandler(MainDbContext context, ILogger<CheckAndDeleteItemHandler> logger, IManagementLogger managementLogger )
        {
            _logger = logger;
            _context = context;
            _managementLogger = managementLogger;
        }

        public async Task<ItemDTO> Handle(CheckAndDeleteItemCommand request, CancellationToken cancellationToken)
        {
            ItemDTO result = null;
            _logger.LogInformation("CheckAndDeleteItemHandler called");

            var item = await _context.Item().AsNoTracking()
                .Where(item => item.ID == request.ID)
                .SingleOrDefaultAsync();

            if (item == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_notFound,
                                                             new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            // further discussion about when a item cannot be deleted
            if (item.Status == ItemStatus.ACTIVE || item.Status == ItemStatus.DEFECT || item.Status == ItemStatus.IN_REPAIR || item.Status == ItemStatus.BLOCKED_FOR_SERVICE)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_deleteStatusError,
                                                             new Dictionary<string, string> { { "description", item.Description.ToString() }, { "status", item.Status.ToString() } });
            }

            _context.Item().Remove(item);
            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedItem), 
                    ("description", item.Description),
                    ("id", item.ID.ToString()));
            return result;
        }
    }

}
