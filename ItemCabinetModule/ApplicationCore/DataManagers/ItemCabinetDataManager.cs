using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using ItemModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemCabinetModule.ApplicationCore.DataManagers
{
    public class ItemCabinetDataManager
    {
        private readonly ILogger<ItemCabinetDataManager> _logger;
        private readonly MainDbContext _context;

        public ItemCabinetDataManager(MainDbContext context, ILogger<ItemCabinetDataManager> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<Entities.CabinetStock> GetCabinetStockByCabinetNumber(string cabinetNumber, Enums.CabinetStockColumn? sortedBy = null,
                                                                                bool sortDescending = false, string filterQuery = null, int? itemTypeID = null, int? cabinetStockStatus = null)
        {
            bool desc = sortDescending;

            var stock = _context.CabinetStock()
                .AsNoTracking()
                .Include(s => s.ItemType)
                .Where(s => s.CabinetNumber.Equals(cabinetNumber));


            switch (sortedBy)
            {
                case Enums.CabinetStockColumn.ItemType:
                    stock = desc ? stock.OrderByDescending(x => x.ItemType.Description) : stock.OrderBy(x => x.ItemType.Description);
                    break;
                case Enums.CabinetStockColumn.CreateDT:
                    stock = desc ? stock.OrderByDescending(x => x.CreateDT) : stock.OrderBy(x => x.CreateDT);
                    break;
                case Enums.CabinetStockColumn.UpdateDT:
                    stock = desc ? stock.OrderByDescending(x => x.UpdateDT) : stock.OrderBy(x => x.UpdateDT);
                    break;
                case Enums.CabinetStockColumn.MinimalStock:
                    stock = desc ? stock.OrderByDescending(x => x.MinimalStock) : stock.OrderBy(x => x.MinimalStock);
                    break;
                case Enums.CabinetStockColumn.ActualStock:
                    stock = desc ? stock.OrderByDescending(x => x.ActualStock) : stock.OrderBy(x => x.ActualStock);
                    break;
                case Enums.CabinetStockColumn.Status:
                    stock = desc ? stock.OrderByDescending(x => x.Status) : stock.OrderBy(x => x.Status);
                    break;
            }

            if (itemTypeID.HasValue)
            {
                stock = stock.Where(s => s.ItemTypeID == itemTypeID.Value);
            }

            if (cabinetStockStatus.HasValue)
            {
                stock = stock.Where(cabinetStock => (int)cabinetStock.Status == cabinetStockStatus.Value);
            }

            if (!string.IsNullOrEmpty(filterQuery))
            {
                stock = stock.Where(x => EF.Functions.Like(x.ItemType.Description, $"%{filterQuery}%"));
            }

            return stock;
        }

        public IQueryable<Entities.CabinetStock> GetAllCabinetStocks()
        {
            return _context.CabinetStock()
                .AsNoTracking();
        }

        public IQueryable<Entities.CabinetStock> GetCabinetStockByCabinetNumberAndItemTypeID(string cabinetNumber, int itemTypeID)
        {
            return _context.CabinetStock()
                .AsNoTracking()
                .Where(cs => cs.CabinetNumber.Equals(cabinetNumber) && cs.ItemTypeID.Equals(itemTypeID));
        }

        public (IQueryable<CTAMRole_Cabinet>, IQueryable<CTAMRole_ItemType>) GetCabinetNumbersItemTypesGroupedByOtherRoleIds(IEnumerable<string> cabinetNumbers, int roleID)
        {
            var otherRoleIDs = _context.CTAMRole_Cabinet().AsNoTracking()
                .Where(rc => rc.CTAMRoleID != roleID && cabinetNumbers.Contains(rc.CabinetNumber))
                .Select(rc => rc.CTAMRoleID);
            var itemTypesByRoleId = _context.CTAMRole_ItemType().AsNoTracking()
                .Where(ri => otherRoleIDs.Contains(ri.CTAMRoleID));
            var cabinetsByRoleId = _context.CTAMRole_Cabinet().AsNoTracking()
                .Where(rc => otherRoleIDs.Contains(rc.CTAMRoleID) && cabinetNumbers.Contains(rc.CabinetNumber));
            return (cabinetsByRoleId, itemTypesByRoleId);
        }

        public IQueryable<Entities.CabinetStock> GetCabinetStocksByCabinetNumberAndItemTypeIDs(string cabinetNumber, List<int> itemTypeIDs)
        {
            return _context.CabinetStock()
                .AsNoTracking()
                .Where(cs => cs.CabinetNumber.Equals(cabinetNumber) && itemTypeIDs.Contains(cs.ItemTypeID));
        }

        public IQueryable<Entities.AllowedCabinetPosition> GetAllowedCabinetPositionsByCabinetPositionIDs(List<int> cabinetPositionIDs)
        {
            return _context.AllowedCabinetPosition()
                .AsNoTracking()
                .Where(acp => cabinetPositionIDs.Contains(acp.CabinetPositionID));
        }

        public IQueryable<Entities.CabinetPositionContent> GetCabinetPositionContentByCabinetPositionIDs(List<int> cabinetPositionIDs)
        {
            return _context.CabinetPositionContent()
                .AsNoTracking()
                .Where(cpc => cabinetPositionIDs.Contains(cpc.CabinetPositionID));
        }

        public IQueryable<Entities.ItemToPick> GetItemsToPickByCabinetPositionIDs(List<int> cabinetPositionIDs)
        {
            return _context.ItemToPick()
                .AsNoTracking()
                .Where(itp => cabinetPositionIDs.Contains(itp.CabinetPositionID));
        }

        public IQueryable<Entities.CTAMUserPersonalItem> GetCTAMUserPersonalItemsByCabinetNumber(string cabinetNumber)
        {
            return _context.CTAMUserPersonalItem()
                .AsNoTracking()
                .Where(upi => upi.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<Entities.CTAMUserPersonalItem> GetCTAMUserPersonalItemsByUserUID(string userUID)
        {
            return _context.CTAMUserPersonalItem()
                .AsNoTracking()
                .Where(upi => upi.CTAMUserUID.Equals(userUID));
        }

        public IQueryable<Entities.CTAMUserPersonalItem> GetCTAMUserPersonalItemsByUserUIDsAndItemTypeIDs(List<string> userUIDs, List<int> itemTypeIDs)
        {
            return _context.CTAMUserPersonalItem()
                .AsNoTracking()
                .Where(upi => userUIDs.Contains(upi.CTAMUserUID) && itemTypeIDs.Contains(upi.Item.ItemTypeID));
        }

        public IQueryable<Entities.CTAMUserInPossession> GetCTAMUserInPossessionByUUIDs(List<string> userUIDs)
        {
            return _context.CTAMUserInPossession()
                .AsNoTracking()
                .Where(uip => userUIDs.Contains(uip.CTAMUserUIDOut));
        }

        public IQueryable<Entities.CTAMUserInPossession> GetCTAMUserInPossessionByUUIDsAndItemTypeIDs(List<string> userUIDs, List<int> itemTypeIDs)
        {
            return _context.CTAMUserInPossession()
                .AsNoTracking()
                .Where(uip => userUIDs.Contains(uip.CTAMUserUIDOut) && itemTypeIDs.Contains(uip.Item.ItemTypeID));
        }
    }
}
