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
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.ItemTypes
{
    public class CheckAndCreateItemTypeCommand : IRequest<ItemTypeDTO>
    {
        public string Description { get; set; }

        public TagType TagType { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public int MaxLendingTimeInMins { get; set; } = 0;

        public bool IsStoredInLocker { get; set; }

        public bool RequiresMileageRegistration { get; set; }

        public List<int> AddErrorCodeIDs { get; set; }

    }

    public class CheckAndCreateItemTypeHandler : IRequestHandler<CheckAndCreateItemTypeCommand, ItemTypeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckAndCreateItemTypeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndCreateItemTypeHandler(MainDbContext context, ILogger<CheckAndCreateItemTypeHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ItemTypeDTO> Handle(CheckAndCreateItemTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndCreateItemTypeHandler called");
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.itemTypes_apiExceptions_emptyDescription);
            }

            if (_context.ItemType().Any(it => it.Description == request.Description))
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_duplicateDescription,
                                                                 new Dictionary<string, string> { { "description", request.Description.ToString() } });
            }

            if (request.AddErrorCodeIDs != null && request.AddErrorCodeIDs.Count() > 0)
            {
                var found = await _context.ErrorCode().CountAsync(e => request.AddErrorCodeIDs.Contains(e.ID));
                if (found != request.AddErrorCodeIDs.Count())
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_errorCodeNotFound);
                }
            }

            var itemType = CreateItemType(request);
            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdItemType), 
                ("description", request.Description));
            await AddErrorCodes(itemType, request.AddErrorCodeIDs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemTypeDTO>(itemType);
        }

        private ItemType CreateItemType(CheckAndCreateItemTypeCommand request)
        {
            var newItemType = new ItemType()
            {
                Description = request.Description,
                TagType = request.TagType,
                Depth = !request.IsStoredInLocker ? 0 : request.Depth,
                Width = !request.IsStoredInLocker ? 0 : request.Width,
                Height = !request.IsStoredInLocker ? 0 : request.Height,
                MaxLendingTimeInMins = request.MaxLendingTimeInMins,
                IsStoredInLocker = request.IsStoredInLocker,
                RequiresMileageRegistration = request.RequiresMileageRegistration,
            };
            var itemType = _context.ItemType().Add(newItemType).Entity;
            return itemType;
        }

        private async Task AddErrorCodes(ItemType itemType, List<int> addErrorCodesIDs)
        {
            if (addErrorCodesIDs != null && addErrorCodesIDs.Count != 0)
            {
                var errorCodesToAdd = addErrorCodesIDs.Select(errorCodeId => new ItemType_ErrorCode()
                {
                    ErrorCodeID = errorCodeId,
                    ItemTypeID = itemType.ID,
                }).ToArray();
                await _context.ItemType_ErrorCode().AddRangeAsync(errorCodesToAdd);

                var errorCode_descriptions = _context.ErrorCode().AsNoTracking().Where(e => addErrorCodesIDs.Contains(e.ID)).Select(e => e.Description).ToArray();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedErrorCodesToItemType), 
                    ("errorCodes", string.Join(" - ", errorCode_descriptions)),
                    ("description", itemType.Description));
            }
        }
    }

}
