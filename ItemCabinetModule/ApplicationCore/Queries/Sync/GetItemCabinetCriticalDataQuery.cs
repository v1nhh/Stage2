using AutoMapper;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.DTO.Sync;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemCabinetModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries.Sync
{
    public class GetItemCabinetCriticalDataQuery : IRequest<ItemCabinetCriticalDataEnvelope>
    {
        public string CabinetNumber { get; set; }
        public List<int> CabinetPositionIDs { get; set; }
        public List<string> UserUIDs { get; set; }
        public List<int> ItemTypeIDs { get; set; }

        public GetItemCabinetCriticalDataQuery(string cabinetNumber, List<int> cabinetPositionIDs, List<string> userUIDs, List<int> itemTypeIDs)
        {
            CabinetNumber = cabinetNumber;
            CabinetPositionIDs = cabinetPositionIDs;
            UserUIDs = userUIDs;
            ItemTypeIDs = itemTypeIDs;
        }
    }

    public class GetItemCabinetCriticalDataHandler : IRequestHandler<GetItemCabinetCriticalDataQuery, ItemCabinetCriticalDataEnvelope>
    {
        private readonly ILogger<GetItemCabinetCriticalDataHandler> _logger;        
        private readonly ItemCabinetDataManager _itemCabinetManager;
        private readonly IMapper _mapper;

        public GetItemCabinetCriticalDataHandler(ItemCabinetDataManager itemCabinetManager, IMapper mapper, ILogger<GetItemCabinetCriticalDataHandler> logger)
        {
            _itemCabinetManager = itemCabinetManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ItemCabinetCriticalDataEnvelope> Handle(GetItemCabinetCriticalDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemCabinetCriticalDataHandler called");
            ItemCabinetCriticalDataEnvelope env = new ItemCabinetCriticalDataEnvelope();

            //CabinetStock   
            env.CabinetStockData = _mapper.Map(
                    await _itemCabinetManager.GetCabinetStockByCabinetNumber(request.CabinetNumber).ToListAsync(),
                    new List<CabinetStockBaseDTO>()
                );

            //AllowedCabinetPosition
            env.AllowedCabinetPositionData = _mapper.Map(
                    await _itemCabinetManager.GetAllowedCabinetPositionsByCabinetPositionIDs(request.CabinetPositionIDs).ToListAsync(),
                    new List<AllowedCabinetPositionBaseDTO>()
                );

            //CabinetPositionContent
            env.CabinetPositionContentData = _mapper.Map(
                    await _itemCabinetManager.GetCabinetPositionContentByCabinetPositionIDs(request.CabinetPositionIDs).ToListAsync(),
                    new List<CabinetPositionContentBaseDTO>()
                );

            //CTAMUserInPossession
            List<CTAMUserInPossession> uips = new List<CTAMUserInPossession>();
            foreach (var chunk in request.UserUIDs.Partition(15000))
            {
                var chunkUips = await _itemCabinetManager.GetCTAMUserInPossessionByUUIDsAndItemTypeIDs(chunk, request.ItemTypeIDs)
                        // Added below where clause to exclude returned items from syncing to cabinet
                        .Where(cuip => cuip.CabinetPositionIDIn == null)
                        .ToListAsync();
                uips.AddRange(chunkUips);
            }
            env.CTAMUserInPossessionData = _mapper.Map(
                    uips,
                    new List<CTAMUserInPossessionBaseDTO>()
            );

            //CTAMUserPersonalItem
            List<CTAMUserPersonalItemBaseDTO> cTAMUserPersonalItemBaseDTOs = new List<CTAMUserPersonalItemBaseDTO>();
            foreach (var chunk in request.UserUIDs.Partition(15000))
            {
                var chunkcTAMUserPersonalItemBaseDTOs = _mapper.Map(
                    await _itemCabinetManager.GetCTAMUserPersonalItemsByUserUIDsAndItemTypeIDs(chunk, request.ItemTypeIDs)
                        .ToListAsync(),
                    new List<CTAMUserPersonalItemBaseDTO>()
                );
                cTAMUserPersonalItemBaseDTOs.AddRange(chunkcTAMUserPersonalItemBaseDTOs);
            }
            env.CTAMUserPersonalItemData = cTAMUserPersonalItemBaseDTOs;

            //ItemToPick
            List<ItemToPickBaseDTO> itemToPickBaseDTOs = new List<ItemToPickBaseDTO>();
            foreach (var chunk in request.UserUIDs.Partition(15000))
            {
                var chunkItemToPickBaseDTOs = _mapper.Map(
                    await _itemCabinetManager.GetItemsToPickByCabinetPositionIDs(request.CabinetPositionIDs)
                        .Where(itemToPick => chunk.Contains(itemToPick.CTAMUserUID))
                        .ToListAsync(),
                    new List<ItemToPickBaseDTO>()
                );
                itemToPickBaseDTOs.AddRange(chunkItemToPickBaseDTOs);
            }
            env.ItemToPickData = itemToPickBaseDTOs;

            return env;
        }
    }

}
