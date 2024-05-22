using CabinetModule.ApplicationCore.Commands.Cabinets;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Utilities;
using ItemModule.ApplicationCore.DataManagers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class CheckAndModifyCabinetCommand : IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationDescr { get; set; }

        public string Email { get; set; }

        public string CabinetErrorMessage { get; set; }

        public bool? HasSwipeCardAssign { get; set; }

        public string CabinetLanguage { get; set; }

        public LoginMethod? LoginMethod { get; set; }

        public bool? IsActive { get; set; }

        public CabinetType? CabinetType { get; set; }

        public string CabinetConfiguration { get; set; }

        public bool? RemoveConfiguration { get; set; }

        public IEnumerable<CabinetDoorDTO> CabinetDoors { get; set; }
    }

    public class CheckAndModifyCabinetHandler : IRequestHandler<CheckAndModifyCabinetCommand, CabinetDTO>
    {
        private readonly ILogger<CheckAndModifyCabinetHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly ItemDataManager _dataManager;

        public CheckAndModifyCabinetHandler(ILogger<CheckAndModifyCabinetHandler> logger, MainDbContext context, IMediator mediator, ItemDataManager dataManager)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
            _dataManager = dataManager;
        }

        public async Task<CabinetDTO> Handle(CheckAndModifyCabinetCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyCabinetHandler is called");
            if (command.Name != null && string.IsNullOrWhiteSpace(command.Name))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyName);
            }

            if (command.CabinetNumber == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyNumber);
            }

            bool cabinetExists = await _context.Cabinet().AsNoTracking().AnyAsync(cabinet => cabinet.CabinetNumber == command.CabinetNumber);
            if (!cabinetExists)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                                                new Dictionary<string, string> { { "cabinetNumber", command.CabinetNumber } });
            }

            if (!string.IsNullOrWhiteSpace(command.CabinetConfiguration))
            {
                try
                {
                    var options = new JsonSerializerOptions { AllowTrailingCommas = true };
                    JsonSerializer.Deserialize<CabinetConfigurationConfigFile>(command.CabinetConfiguration, options);
                }
                catch (Exception ex)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_invalidCabinetConfiguration,
                                              new Dictionary<string, string> { { "message", ex.GetMostInnerException().Message } });
                }
            }

            var modifyCommand = new ModifyCabinetCommand
            {
                CabinetNumber = command.CabinetNumber,
                Name = command.Name,
                Description = command.Description,
                LocationDescr = command.LocationDescr,
                Email = command.Email,
                CabinetErrorMessage = command.CabinetErrorMessage,
                HasSwipeCardAssign = command.HasSwipeCardAssign,
                CabinetLanguage = command.CabinetLanguage,
                LoginMethod = command.LoginMethod,
                CabinetType = command.CabinetType,
                IsActive = command.IsActive,
                CabinetConfiguration = command.CabinetConfiguration,
                RemoveConfiguration = command.RemoveConfiguration,
                CabinetDoors = command.CabinetDoors,
            };

            // Check for Items in AllowedCabinetPositions ONLY if user tries to set Cabinet.IsActive to 'false'
            if (command.IsActive.HasValue && !command.IsActive.Value)
            {
                var notReturnedRequiredItemIDs = await NotReturnedRequiredItemIDs(command.CabinetNumber);
                // Modify only if all required items are returned, otherwise throuw exception
                if (notReturnedRequiredItemIDs.Count == 0)
                {
                    return await _mediator.Send(modifyCommand);
                }
                var missingItemsDescriptions = await _dataManager.GetItemsByIDs(notReturnedRequiredItemIDs)
                    .Select(item => $"{item.Description} (Barcode: '{item.Barcode}',  Tagnummer: '{item.Tagnumber}')")
                    .ToListAsync();
                throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_itemsMissing,
                                          new Dictionary<string, string> { { "itemsMissing", String.Join("\n", missingItemsDescriptions) } });
            }
            return await _mediator.Send(modifyCommand);
        }

        public async Task<List<int>> NotReturnedRequiredItemIDs(string cabinetNumber)
        {
            // Left Join: CabinetPosition + AllowedCabinetPosition
            /*
                SELECT [cabinetPosition].[ID] AS CabinetPositionID, [allowedContent].[ItemID] AS AllowedContentItemID,
                       [actualContent].[CabinetPositionID] AS ActualContentPositionID, [actualContent].[ItemID] AS ActualContentItemID
                FROM [ctam].[Cabinet].[CabinetPosition] AS [cabinetPosition]
                LEFT JOIN [ctam].[ItemCabinet].[AllowedCabinetPosition] AS [allowedContent]
                        ON [cabinetPosition].[ID] = [allowedContent].[CabinetPositionID]
             */
            var cabinetPositionWithAllowedContentQuery = _context.CabinetPosition()
                .Where(cabinetPosition => cabinetPosition.CabinetNumber.Equals(cabinetNumber))
                .GroupJoin(_context.AllowedCabinetPosition(),
                    cabinetPosition => cabinetPosition.ID,
                    allowedContent => allowedContent.CabinetPositionID,
                    (cabinetPosition, allowedContent) => new { cabinetPosition, allowedContent })
                .SelectMany(
                    tuple => tuple.allowedContent.DefaultIfEmpty(),
                    (tuple, allowedContent) => new { tuple.cabinetPosition, allowedContent })
                .Where(tuple => tuple.allowedContent != null);

            // Left Join: (CabinetPosition + AllowedCabinetPosition) + CabinetPositionContent
            /*
                -- Part from query above
                SELECT [cabinetPosition].[ID] AS CabinetPositionID, [allowedContent].[ItemID] AS AllowedContentItemID,
                       [actualContent].[CabinetPositionID] AS ActualContentPositionID, [actualContent].[ItemID] AS ActualContentItemID
                FROM [ctam].[Cabinet].[CabinetPosition] AS [cabinetPosition]
                LEFT JOIN [ctam].[ItemCabinet].[AllowedCabinetPosition] AS [allowedContent]
                        ON [cabinetPosition].[ID] = [allowedContent].[CabinetPositionID]

                -- Part from new query below
                LEFT JOIN [ctam].[ItemCabinet].[CabinetPositionContent] AS [actualContent]
                        ON [allowedContent].[ItemID] = [actualContent].[ItemID]
             */
            var cabinetPositionWithAllowedAndActualContent = await cabinetPositionWithAllowedContentQuery
                .GroupJoin(_context.CabinetPositionContent(),
                    // Join only on ItemID to get also CabinetPositionContent from other Cabinets for cases
                    // when Item is returned to a different cabinet than the current one 
                    tuple => tuple.allowedContent.ItemID,
                    actualContent => actualContent.ItemID,
                    (tuple, actualContent) => new { tuple.cabinetPosition, tuple.allowedContent, actualContent })
                .SelectMany(
                    tuple => tuple.actualContent.DefaultIfEmpty(),
                    (tuple, actualContent) => new { tuple.cabinetPosition, tuple.allowedContent, actualContent })
                .ToListAsync();

            // Queries below (GroupBy and Any inside Select) cannot be translated to SQL queries
            var allowedWithActualContentGroupedByItemID = cabinetPositionWithAllowedAndActualContent
                // distinct on allowedContent.ItemID
                .GroupBy(tuple => tuple.allowedContent.ItemID, tuple => tuple.actualContent)
                // Filter with Any on actualContent null
                .Select(grouping => new { requiredItemID = grouping.Key, isInCabinetPosition = grouping.Any(actualContent => actualContent != null) });

            if (cabinetPositionWithAllowedAndActualContent.Count == 0)
            {
                return new List<int>();
            }
            //// Return false if at least 1 required item(allowedContent.ItemID) is not in cabinet position
            //bool areAllRequiredItemsInCabinet = allowedWithActualContentGroupedByItemID.All(tuple => tuple.isInCabinetPosition);
            var notReturnedRequiredItemIDs = allowedWithActualContentGroupedByItemID
                .Where(tuple => !tuple.isInCabinetPosition)
                .Select(tuple => tuple.requiredItemID)
                .ToList();
            return notReturnedRequiredItemIDs;
        }
    }
}
