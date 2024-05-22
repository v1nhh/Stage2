using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.Items
{
    public class CreateItemCommand : IRequest<ItemDTO>
    {
        public string Description { get; set; }

        public int ItemTypeID { get; set; }

        public string Barcode { get; set; }

        public string Tagnumber { get; set; }

        public string ExternalReferenceID { get; set; }

        public int MaxLendingTimeInMins { get; set; } = 0;

        public int NrOfSubItems { get; set; } = 0;
    }

    public class CreateItemHandler : IRequestHandler<CreateItemCommand, ItemDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CreateItemHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CreateItemHandler(MainDbContext context, ILogger<CreateItemHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ItemDTO> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateItemHandler called");

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_emptyDescription);
            }

            if (request.Description != null && await _context.Item().AnyAsync(i => i.Description == request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_duplicateDescription,
                                          new Dictionary<string, string> { { "description", request.Description } });
            }

            var existingItemType = await _context.ItemType().AsNoTracking().SingleOrDefaultAsync(itemtype => itemtype.ID == request.ItemTypeID);
            if (existingItemType == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_itemTypeNotFound,
                                          new Dictionary<string, string> { { "itemTypeID", request.ItemTypeID.ToString() } });
            }

            // check if barcode is still unique and not used by an other item.
            if (!string.IsNullOrEmpty(request.Barcode) && await _context.Item().AnyAsync(i => i.Barcode == request.Barcode))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.items_apiExceptions_duplicateBarcode,
                                          new Dictionary<string, string> { { "barcode", request.Barcode } });
            }

            // check if tag number is still unique and not used by an other item.
            if (!string.IsNullOrEmpty(request.Tagnumber) && await _context.Item().AnyAsync(i => i.Tagnumber == request.Tagnumber))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.items_apiExceptions_duplicateTagnumber,
                                          new Dictionary<string, string> { { "tagnumber", request.Tagnumber } });
            }

            var item = CreateItem(request);
            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_itemCreated), ( "description", request.Description ));

            return _mapper.Map<ItemDTO>(item);
        }

        private Item CreateItem(CreateItemCommand request)
        {
            _logger.LogInformation($"CreateItem called! Item description: {request.Description}");
            var newItem = new Item()
            {
                Description = request.Description,
                Tagnumber = request.Tagnumber,
                Barcode = request.Barcode,
                ExternalReferenceID = request.ExternalReferenceID,
                ItemTypeID = request.ItemTypeID,
                MaxLendingTimeInMins = request.MaxLendingTimeInMins,
                NrOfSubItems = request.NrOfSubItems,
                Status = ItemStatus.INITIAL,
            };
            var item = _context.Item().Add(newItem).Entity;
            return item;
        }
    }

}
