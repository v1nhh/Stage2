using AutoMapper;
using CTAM.Core;
using ItemModule.ApplicationCore.DataManagers;
using ItemModule.ApplicationCore.DTO.Sync;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Queries.Sync
{
    public class GetItemCriticalDataQuery : IRequest<ItemCriticalDataEnvelope>
    {
        public GetItemCriticalDataQuery(List<int> roleIDs)
        {
            RoleIDs = roleIDs;
        }

        public List<int> RoleIDs { get; set; }
    }

    public class GetItemCriticalDataHandler : IRequestHandler<GetItemCriticalDataQuery, ItemCriticalDataEnvelope>
    {
        private readonly ILogger<GetItemCriticalDataHandler> _logger;
        private readonly ItemDataManager _itemManager;
        private readonly IMapper _mapper;
        private readonly MainDbContext _dbContext;

        public GetItemCriticalDataHandler(ItemDataManager itemManager, IMapper mapper, ILogger<GetItemCriticalDataHandler> logger, MainDbContext dbContext)
        {
            _itemManager = itemManager;
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ItemCriticalDataEnvelope> Handle(GetItemCriticalDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemCriticalDataHandler called");
            ItemCriticalDataEnvelope env = new ItemCriticalDataEnvelope();
            
            //ItemType
            var itemTypes = await _itemManager.GetItemTypesByRoleIDs(request.RoleIDs).ToListAsync();
            env.ItemTypeData = _mapper.Map(
                    itemTypes,
                    new List<ItemTypeBaseDTO>()
                );

            //Item
            var itemData = await _itemManager.GetItemsByItemTypeIDs(itemTypes.Select(it => it.ID)).ToListAsync();
            env.ItemData = _mapper.Map(
                    itemData,
                    new List<ItemBaseDTO>()
                );

            //ItemSet
            // TODO: Use partitioning(chunks) for contains when calling _itemManager.GetItemSetsByItemIDs
            var itemSets = new List<ItemSet>();
            env.ItemSetData = _mapper.Map(itemSets, new List<ItemSetBaseDTO>());

            var roleItemTypesForRoles = await _dbContext.CTAMRole_ItemType()
                .Where(roleItemType => request.RoleIDs.Contains(roleItemType.CTAMRoleID))
                .Include(roleItemType => roleItemType.ItemType)
                .ToListAsync();

            //CTAMRole_ItemType
            env.CTAMRole_ItemTypeData = _mapper.Map(
                    roleItemTypesForRoles,
                    new List<CTAMRole_ItemTypeBaseDTO>()
                );

            //ErrorCode
            env.ErrorCodeData = _mapper.Map(
                    await _itemManager.GetAllErrorCodesForRoleIDs(request.RoleIDs).ToListAsync(),
                    new List<ErrorCodeBaseDTO>()
                );

            //ItemType_ErrorCode
            var itemTypesIds = itemTypes.Select(it => it.ID);
            var errorCodesForItemTypes = await _itemManager.GetItemType_ErrorCodeListByItemTypeIDs(itemTypesIds).ToListAsync();
            env.ItemType_ErrorCodeData = _mapper.Map(errorCodesForItemTypes, new List<ItemType_ErrorCodeBaseDTO>());

            return env;
        }
    }

}
