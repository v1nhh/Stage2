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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.ItemTypes
{
    public class CheckAndModifyItemTypeCommand : IRequest<ItemTypeDTO>
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public TagType? TagType { get; set; }

        public double? Depth { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public int? MaxLendingTimeInMins { get; set; }

        public bool IsStoredInLocker { get; set; } = true;

        public bool RequiresMileageRegistration { get; set; } = false;

        public IEnumerable<int> AddErrorCodeIDs { get; set; }

        public IEnumerable<int> RemoveErrorCodeIDs { get; set; }
    }

    public class CheckAndModifyItemTypeHandler : IRequestHandler<CheckAndModifyItemTypeCommand, ItemTypeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckAndModifyItemTypeHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndModifyItemTypeHandler(MainDbContext context, ILogger<CheckAndModifyItemTypeHandler> logger, IMediator mediator, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<ItemTypeDTO> Handle(CheckAndModifyItemTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyItemTypeHandler called");

            if (request.Description != null && string.IsNullOrWhiteSpace(request.Description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.itemTypes_apiExceptions_emptyDescription);
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var itemType = await ModifyItemType(request);

                scope.Complete();
                return _mapper.Map<ItemTypeDTO>(itemType);
            }
        }
        public async Task<ItemType> ModifyItemType(CheckAndModifyItemTypeCommand request)
        {
            var itemType = await _context.ItemType().Where(i => i.ID == request.ID).Include(i => i.ItemType_ErrorCode).FirstOrDefaultAsync();

            if (itemType == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_notFound,
                                                                 new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            if (_context.ItemType().Any(it => it.ID != request.ID && it.Description == request.Description))
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

            if (request.RemoveErrorCodeIDs != null && request.RemoveErrorCodeIDs.Count() > 0)
            {
                var found = await _context.ErrorCode().CountAsync(e => request.RemoveErrorCodeIDs.Contains(e.ID));
                if (found != request.RemoveErrorCodeIDs.Count())
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_errorCodeNotFound);
                }
            }

            var changes = await ModifySingleFields(itemType, request);

            if (changes.Count > 0)
            {
                changes.Add(("description", itemType.Description));
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedItemType), changes.ToArray()); 
            }

            await ModifyErrorCodes(itemType, request, changes);
            await _context.SaveChangesAsync();

            return itemType;
        }

        public async Task<List<(string key, string value)>> ModifySingleFields(ItemType itemType, CheckAndModifyItemTypeCommand request)
        {
            var changes = new List<(string key, string value)>();

            if (request.Description != null && !request.Description.Equals(itemType.Description))
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing Description from '{itemType.Description}' to '{request.Description}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldDescription", itemType.Description),
                    ("newDescription", request.Description)
                });
                itemType.Description = request.Description;
            }
            if (request.TagType != null && request.TagType != itemType.TagType)
            {
                if (!Enum.IsDefined(typeof(TagType), request.TagType))
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_unknownTagType,
                                                                     new Dictionary<string, string> { { "tagType", ((int)request.TagType).ToString() } });
                }
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing TagType from '{itemType.TagType}' to '{request.TagType}'");

                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldTagType", itemType.TagType.ToString()),
                    ("newTagType", request.TagType.ToString())
                });
                itemType.TagType = (TagType)request.TagType;
            }
            if (request.MaxLendingTimeInMins != null && request.MaxLendingTimeInMins != itemType.MaxLendingTimeInMins)
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing MaxLendingTimeInMins from '{itemType.MaxLendingTimeInMins}' to '{request.MaxLendingTimeInMins}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldMaxLendingTimeInMins", itemType.MaxLendingTimeInMins.ToString()),
                    ("newMaxLendingTimeInMins", request.MaxLendingTimeInMins.ToString())
                });
                itemType.MaxLendingTimeInMins = (int)request.MaxLendingTimeInMins;
            }
            //if (request.IsStoredInLocker != itemType.IsStoredInLocker)
            //{
            //    var translateBoolReq = request.IsStoredInLocker ? "Aan" : "Uit";
            //    var translateBoolit = itemType.IsStoredInLocker ? "Aan" : "Uit";
            //    _logger.LogInformation($"ItemTypeID='{request.ItemTypeID}' > Changing IsStoredInLocker from '{translateBoolit}' to '{translateBoolReq}'");
            //    itemType.MaxLendingTimeInMins = request.MaxLendingTimeInMins;
            //    changes.Add("Maximale uitleentijd", translateBoolReq);
            //}
            if (!request.IsStoredInLocker) {
                request.Depth = 0;
                request.Height = 0;
                request.Width = 0;
            }

            if ((request.Depth != null && request.Depth != itemType.Depth) || (request.Width != null && request.Width != itemType.Width) || (request.Height != null && request.Height != itemType.Height))
            {
                var itemTypesInCabinetPositions = await _context.CabinetPositionContent().AsNoTracking()
                    .Include(cpc => cpc.Item)
                    .Include(cpc => cpc.CabinetPosition)
                    .Where(cpc => cpc.Item.ItemTypeID == itemType.ID).ToListAsync();
                if (itemTypesInCabinetPositions.Any())
                {
                    var itemIdsInCabinetNumbers = itemTypesInCabinetPositions.Select(cpc => cpc.CabinetPosition.CabinetNumber)
                        .Distinct()
                        .ToArray();
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.itemTypes_apiExceptions_existsInCabinetPosition, 
                        new Dictionary<string, string>() { { "cabinetNumbers",  string.Join(",", itemIdsInCabinetNumbers) } }
                    );
                }
             }

            if (request.Depth != null && request.Depth != itemType.Depth)
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing Depth from '{itemType.Depth}' to '{request.Depth}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldDepth", itemType.Depth.ToString()),
                    ("newDepth", request.Depth.ToString())
                });
                itemType.Depth = (double)request.Depth;
            }
            if (request.Width != null && request.Width != itemType.Width)
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing Width from '{itemType.Width}' to '{request.Width}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldWidth", itemType.Width.ToString()),
                    ("newWidth", request.Width.ToString())
                });
                itemType.Width = (double)request.Width;
            }
            if (request.Height != null && request.Height != itemType.Height)
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Changing Height from '{itemType.Height}' to '{request.Height}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldHeight", itemType.Height.ToString()),
                    ("newHeight", request.Height.ToString())
                });
                itemType.Height = (double)request.Height;
            }
            return changes;
        }

        public async Task ModifyErrorCodes(ItemType itemType, CheckAndModifyItemTypeCommand request, List<(string key, string value)> changes)
        {
            if (request.AddErrorCodeIDs != null && request.AddErrorCodeIDs.Any())
            {
                // do not try to add if already present
                var existingErrorCodeIDs = itemType.ItemType_ErrorCode.Where(itec => request.AddErrorCodeIDs.Contains(itec.ErrorCodeID))
                                                                      .Select(itec => itec.ErrorCodeID)
                                                                      .ToList();
                request.AddErrorCodeIDs = request.AddErrorCodeIDs.Except(existingErrorCodeIDs);
                if (request.AddErrorCodeIDs.Any())
                {
                    _logger.LogInformation($"ItemTypeID='{request.ID}' > Adding error codes: '{string.Join(", ", request.AddErrorCodeIDs)}");
                    var errorCodesToAdd = request.AddErrorCodeIDs.Select(errorCodeID => new ItemType_ErrorCode()
                    {
                        ErrorCodeID = errorCodeID,
                        ItemTypeID = itemType.ID,
                    }).ToArray();
                    await _context.ItemType_ErrorCode().AddRangeAsync(errorCodesToAdd);

                    var addedErrorCodes = await _context.ErrorCode().AsNoTracking()
                        .Where(e => request.AddErrorCodeIDs.Contains(e.ID))
                        .Select(e => e.Description)
                        .ToListAsync();

                    changes.Add(("addedErrorCodeDescriptions", string.Join(',', addedErrorCodes)));

                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedErrorCodesToItemType), 
                    ("errorCodes", string.Join(", ", addedErrorCodes)), 
                    ("description", itemType.Description)); 
                }
            }
            if (request.RemoveErrorCodeIDs != null && request.RemoveErrorCodeIDs.Any())
            {
                _logger.LogInformation($"ItemTypeID='{request.ID}' > Removing error codes: '{string.Join(',', request.RemoveErrorCodeIDs)}");
                var itemWithErrorCode = await _context.Item().AsNoTracking()
                                            .Where(i => i.ErrorCodeID != null && i.ItemTypeID == request.ID && 
                                                        request.RemoveErrorCodeIDs.ToList().Contains((int)i.ErrorCodeID))
                                            .Include(i => i.ErrorCode)
                                            .FirstOrDefaultAsync();
                if (itemWithErrorCode != null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_errorCodeInUse,
                                                                     new Dictionary<string, string> { { "item", itemWithErrorCode.Description },
                                                                                                      { "errorCode", itemWithErrorCode.ErrorCode.Code } });
                }

                var errorCodesToRemove = await _context.ItemType_ErrorCode()
                    .Where(ie => itemType.ID.Equals(ie.ItemTypeID) && request.RemoveErrorCodeIDs.Contains(ie.ErrorCodeID))
                    .Include(ie => ie.ErrorCode)
                    .ToArrayAsync();

                changes.Add(("removedErrorCodeDescriptions", string.Join(',', errorCodesToRemove.Select(ie => ie.ErrorCode.Description))));

                _context.ItemType_ErrorCode().RemoveRange(errorCodesToRemove);

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedErrorCodesFromItemType), 
                    ("deletedErrorCodes", string.Join(", ", errorCodesToRemove.Select(ie => ie.ErrorCode.Description))), 
                    ("itemTypeDescription", itemType.Description)); 
            }
        }
    }

}
