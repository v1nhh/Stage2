using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.Items
{
    public class CheckAndModifyItemCommand : IRequest<ItemDTO>
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public string Barcode { get; set; }

        public string Tagnumber { get; set; }

        public string ExternalReferenceID { get; set; }

        public int? MaxLendingTimeInMins { get; set; }

        public ItemStatus? Status { get; set; }
    }

    public class CheckAndModifyItemHandler : IRequestHandler<CheckAndModifyItemCommand, ItemDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckAndModifyItemHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndModifyItemHandler(MainDbContext context, ILogger<CheckAndModifyItemHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ItemDTO> Handle(CheckAndModifyItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyItemHandler called");

            var item = await ModifyItem(request);

            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<Item> ModifyItem(CheckAndModifyItemCommand request)
        {
            if (request.Description != null && string.IsNullOrWhiteSpace(request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_emptyDescription);
            }

            // check if barcode is still unique and not used by an other item.
            if (!string.IsNullOrEmpty(request.Barcode) && await _context.Item().AnyAsync(i => i.Barcode == request.Barcode && i.ID != request.ID))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.items_apiExceptions_duplicateBarcode,
                                                             new Dictionary<string, string> { { "barcode", request.Barcode } });
            }

            // check if tag number is still unique and not used by an other item.
            if (!string.IsNullOrEmpty(request.Tagnumber) && await _context.Item().AnyAsync(i => i.Tagnumber == request.Tagnumber && i.ID != request.ID))
            {
                throw new CustomException(HttpStatusCode.Conflict, CloudTranslations.items_apiExceptions_duplicateTagnumber,
                                          new Dictionary<string, string> { { "tagnumber", request.Tagnumber } });
            }

            var item = await _context.Item().Where(i => i.ID == request.ID).FirstOrDefaultAsync();

            if (item == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            if (request.Description != null && await _context.Item().AnyAsync(i => i.ID != request.ID && i.Description == request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.items_apiExceptions_duplicateDescription,
                                          new Dictionary<string, string> { { "description", request.Description } });
            }

            var changes = ModifySingleFields(item, request);
            if (changes.Count > 0)
            {
                changes.Add(("description", item.Description));
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedItem), changes.ToArray());
            }
            await _context.SaveChangesAsync();

            return item;
        }

        public List<(string key, string value)> ModifySingleFields(Item item, CheckAndModifyItemCommand request)
        {
            var changes = new List<(string key, string value)>();

            if (request.Description != null && !request.Description.Equals(item.Description))
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing Description from '{item.Description}' to '{request.Description}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldDescription", item.Description),
                    ("newDescription", request.Description)
                });
                item.Description = request.Description;
            }
            if (request.Barcode != null && !request.Barcode.Equals(item.Barcode))
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing Barcode from '{item.Barcode}' to '{request.Barcode}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldBarcode", item.Barcode),
                    ("newBarcode", request.Barcode)
                });
                item.Barcode = request.Barcode;
            }
            if (request.Tagnumber != null && !request.Tagnumber.Equals(item.Tagnumber))
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing Tagnumber from '{item.Tagnumber}' to '{request.Tagnumber}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldTagnumber", item.Tagnumber),
                    ("newTagnumber", request.Tagnumber)
                });
                item.Tagnumber = request.Tagnumber;
            }
            if (request.ExternalReferenceID != null && !request.ExternalReferenceID.Equals(item.ExternalReferenceID))
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing ExternalReferenceID from '{item.ExternalReferenceID}' to '{request.ExternalReferenceID}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldExternalReferenceID", item.ExternalReferenceID),
                    ("newExternalReferenceID", request.ExternalReferenceID)
                });
                item.ExternalReferenceID = request.ExternalReferenceID;
            }
            if (request.MaxLendingTimeInMins != null && request.MaxLendingTimeInMins != item.MaxLendingTimeInMins)
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing MaxLendingTimeInMins from '{item.MaxLendingTimeInMins}' to '{request.MaxLendingTimeInMins}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldMaxLendingTimeInMins", item.MaxLendingTimeInMins.ToString()),
                    ("newMaxLendingTimeInMins", request.MaxLendingTimeInMins.ToString())
                });
                item.MaxLendingTimeInMins = (int)request.MaxLendingTimeInMins;
            }
            if (request.Status != null && request.Status != item.Status)
            {
                _logger.LogInformation($"ItemID='{request.ID}' > Changing Status from '{item.Status}' to '{request.Status}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldStatus", ((int)item.Status).ToString()),
                    ("newStatus", ((int)request.Status).ToString())
                });
                item.Status = (ItemStatus)request.Status;
            }
            return changes;
        }
    }
}
