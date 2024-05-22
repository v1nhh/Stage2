using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemModule.ApplicationCore.DataManagers
{
    public class ItemDataManager
    {
        private readonly ILogger<ItemDataManager> _logger;
        private readonly MainDbContext _context;

        public ItemDataManager(MainDbContext context, ILogger<ItemDataManager> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<Entities.Item> GetItemsByIDs(IEnumerable<int> iDs)
        {
            return _context.Item()
                .AsNoTracking()
                .Where(i => iDs.Contains(i.ID));
        }

        public IQueryable<Entities.ItemType> GetItemTypesByIDs(IEnumerable<int> iDs)
        {
            return _context.ItemType()
                .AsNoTracking()
                .Where(it => iDs.Contains(it.ID));
        }

        public IQueryable<Entities.ItemType> GetAllItemTypes()
        {
            return _context.ItemType()
                .AsNoTracking();
        }

        public IQueryable<Entities.ItemType> GetItemTypesByRoleID(int roleID)
        {
            return (from i in _context.ItemType()
                    join ri in _context.CTAMRole_ItemType()
                    on i.ID equals ri.ItemTypeID
                    where ri.CTAMRoleID.Equals(roleID)
                    select i)
                    .AsNoTracking();
        }

        public IQueryable<Entities.ItemType> GetItemTypesByRoleIDs(IEnumerable<int> roleIDs)
        {
            return (from i in _context.ItemType()
                    join ri in _context.CTAMRole_ItemType()
                    on i.ID equals ri.ItemTypeID
                    where roleIDs.Contains(ri.CTAMRoleID)
                    select i)
                    .Distinct()
                    .AsNoTracking();
        }

        public IQueryable<Entities.Item> GetItemsByItemTypeIDs(IEnumerable<int> itemTypeIDs)
        {
            return _context.Item().Where(i => itemTypeIDs.Contains(i.ItemTypeID))
                    .AsNoTracking();
        }

        public IQueryable<Entities.CTAMRole_ItemType> GetCTAMRole_ItemType()
        {
            return (from ri in _context.CTAMRole_ItemType()
                    select ri)
                    .AsNoTracking();
        }

        public IQueryable<Entities.CTAMRole_ItemType> GetCTAMRole_ItemTypeByRoleID(int roleID)
        {
            return (from ri in _context.CTAMRole_ItemType()
                    where ri.CTAMRoleID.Equals(roleID)
                    select ri)
                    .AsNoTracking();
        }

        public IQueryable<Entities.ItemSet> GetItemSetsByItemIDs(IEnumerable<int> itemIDs)
        {
            return _context.ItemSet()
                .AsNoTracking()
                .Where(set => itemIDs.Contains(set.ItemID));
        }
        public IQueryable<Entities.ErrorCode> GetAllErrorCodesForRoleIDs(List<int> roleIDs)
        {
            return (from rit in _context.CTAMRole_ItemType()
                    join itec in _context.ItemType_ErrorCode()
                    on rit.ItemTypeID equals itec.ItemTypeID
                    join ec in _context.ErrorCode()
                    on itec.ErrorCodeID equals ec.ID
                    where roleIDs.Contains(rit.CTAMRoleID)
                    select ec)
                    .Distinct()
                    .AsNoTracking();
        }

        public IQueryable<Entities.ErrorCode> GetErrorCodesByIDs(IEnumerable<int> iDs)
        {
            return _context.ErrorCode()
                .AsNoTracking()
                .Where(ec => iDs.Contains(ec.ID));
        }


        public IQueryable<Entities.ItemType_ErrorCode> GetItemType_ErrorCodeListByItemTypeIDs(IEnumerable<int> itemTypeIDs)
        {
            return _context.ItemType_ErrorCode()
                .AsNoTracking()
                .Where(itec => itemTypeIDs.Contains(itec.ItemTypeID));
        }

        public IQueryable<Entities.CTAMRole_ItemType> GetCTAMRole_ItemTypeListByItemTypeIDs(IEnumerable<int> itemTypeIDs)
        {
            return _context.CTAMRole_ItemType()
                .AsNoTracking()
                .Where(rit => itemTypeIDs.Contains(rit.ItemTypeID));
        }
    }
}
