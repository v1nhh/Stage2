using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
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

namespace CloudAPI.ApplicationCore.Commands.ItemCabinet
{
    public class UpdateMinimalStockCommand : IRequest<CabinetStockDTO>
    {
        #region Primary Key
        public string CabinetNumber { get; set; }
        public int ItemTypeID { get; set; }
        #endregion

        #region Data to Update

        public int MinimalStock { get; set; }
        #endregion
    }

    public class UpdateMinimalStockHandler : IRequestHandler<UpdateMinimalStockCommand, CabinetStockDTO>
    {
        private ILogger<UpdateMinimalStockHandler> _logger;
        private readonly IMapper _mapper;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;


        public UpdateMinimalStockHandler(ILogger<UpdateMinimalStockHandler> logger, IMapper mapper, MainDbContext context, IMediator mediator, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _mediator = mediator;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetStockDTO> Handle(UpdateMinimalStockCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateMinimalStockHandler called");

            //Check arguments
            if (command.CabinetNumber == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyNumber,
                                                                 new Dictionary<string, string> { { "cabinetNumber", command.CabinetNumber } });
            }
            if (command.MinimalStock < 0 || command.MinimalStock > 99) // As discussed with: Ed Kortekaas.
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_minimalStockRange,
                                                                new Dictionary<string, string> { { "minimum", "0" },
                                                                                                 { "maximum", "99" } });
            }

            //Retrieve entity
            var cabinetStock = await _context.CabinetStock()
                               .Where(cs => cs.CabinetNumber.Equals(command.CabinetNumber)
                                         && cs.ItemTypeID.Equals(command.ItemTypeID)
                                )
                               .FirstOrDefaultAsync();

            if (cabinetStock == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_stockNotFound,
                                                                new Dictionary<string, string> { { "cabinetNumber", command.CabinetNumber },
                                                                                                 { "itemTypeID", command.ItemTypeID.ToString() } });
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Update MinimalStock
                cabinetStock.MinimalStock = command.MinimalStock;

                var cabinet = await _context.Cabinet().FirstOrDefaultAsync(c => c.CabinetNumber.Equals(command.CabinetNumber));
                if (cabinet == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                                                    new Dictionary<string, string> { { "cabinetNumber", command.CabinetNumber } });
                }

                var itemType = await _context.ItemType().FirstOrDefaultAsync(itemtype => itemtype.ID == command.ItemTypeID);
                if (itemType == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_itemTypeNotFound,
                                                                    new Dictionary<string, string> { { "itemTypeID", command.ItemTypeID.ToString() } });
                }

                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedMinimalStock),
                    ("cabinetNumber", command.CabinetNumber), 
                    ("name", cabinet.Name), 
                    ("location", cabinet.LocationDescr), 
                    ("minimalStock", command.MinimalStock.ToString()), 
                    ("itemTypeDescription", itemType.Description)); 
                //Check stock below minimal                
                if (cabinetStock.ActualStock < cabinetStock.MinimalStock)
                {
                    if(cabinetStock.Status != CabinetStockStatus.WarningBelowMinimumSend)
                    {
                        cabinetStock.Status = CabinetStockStatus.WarningBelowMinimumSend;
                        await _mediator.Send(new StockBelowMinimumCommand()
                        {
                            CabinetNumber = command.CabinetNumber,
                            ItemTypeID = command.ItemTypeID
                        });
                    }
                }
                else
                {
                    cabinetStock.Status = CabinetStockStatus.OK;
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
            //Return the result from the database.
            var query = _mapper.Map(command, new GetCabinetStockQuery());
            var result = await _mediator.Send(query);

            return result;
        }
    }
}
