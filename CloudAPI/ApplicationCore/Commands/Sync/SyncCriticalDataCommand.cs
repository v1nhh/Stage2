using CabinetModule.ApplicationCore.Commands.Sync;
using CabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Commands.Sync;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemModule.ApplicationCore.Commands.Sync;
using ItemModule.ApplicationCore.DTO;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Commands.Sync;
using UserRoleModule.ApplicationCore.DTO;

namespace CloudAPI.ApplicationCore.Commands.Sync
{
    public class SyncCriticalDataCommand : IRequest
    {
        public string CabinetNumber { get; set; }

        public List<CabinetPositionDTO> CabinetPositionsDTO { get; set; }

        public List<CabinetDoorDTO> CabinetDoorsDTO { get; set; }

        public List<CabinetPositionContentDTO> AddedCabinetPositionContentsDTO { get; set; }

        public List<CabinetPositionContentDTO> RemovedCabinetPositionContentsDTO { get; set; }

        public List<CabinetStockDTO> CabinetStocksDTO { get; set; }

        public List<ItemToPickDTO> ItemsToPickDTO { get; set; }

        public List<ItemDTO> ItemsDTO { get; set; }

        public List<UserInPossessionDTO> UserInPossesionsDTO { get; set; }

        public List<UserPersonalItemDTO> CTAMUserPersonalItemsDTO { get; set; }

        public List<UserAndCardCodeDTO> UserAndCardCodeDTOs { get; set; }
    }

    public class SyncCriticalDataHandler : IRequestHandler<SyncCriticalDataCommand>
    {
        private readonly ILogger<SyncCriticalDataHandler> _logger;
        private readonly IMediator _mediator;

        public SyncCriticalDataHandler(ILogger<SyncCriticalDataHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncCriticalDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCriticalDataHandler called");
            if (request == null || string.IsNullOrWhiteSpace(request.CabinetNumber))
            {
                var msg = "SyncCriticalCabinetDataHandler failed: provided data cannot be null";
                _logger.LogError(msg);
                throw new NullReferenceException(msg);
            }
            // Cabinet
            await _mediator.Send(new SyncCriticalCabinetDataCommand(request.CabinetPositionsDTO, request.CabinetDoorsDTO));


            // Warning: if you change the order of SyncCriticalItemDataCommand and SyncCriticalItemCabinetDataCommand
            // the notification handler for changing item status will not work. This has to do with the order of
            // saveChangesAsync being called on the changed entities being tracked.
            // Item
            await _mediator.Send(new SyncCriticalItemDataCommand(request.ItemsDTO));

            // ItemCabinet
            await _mediator.Send(new SyncCriticalItemCabinetDataCommand(
                request.CabinetNumber,
                request.AddedCabinetPositionContentsDTO,
                request.RemovedCabinetPositionContentsDTO,
                request.CabinetStocksDTO,
                request.ItemsToPickDTO,
                request.UserInPossesionsDTO,
                request.CTAMUserPersonalItemsDTO
            ));

            await _mediator.Send(new SyncCriticalUserDataCommand(request.UserAndCardCodeDTOs));

            return new Unit();
        }
    }

}
