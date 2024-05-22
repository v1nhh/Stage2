using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using CTAM.Core.Enums;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UserRoleModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.DataManagers
{
    public class LiveSyncDataManager
    {
        private readonly MainDbContext _context;

        public LiveSyncDataManager(MainDbContext context)
        {
            _context = context;
            if (!_context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                _context.Database.SetCommandTimeout(180);
            }
        }

        /// ItemEnvelope
        public (IQueryable<Item>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>) GetItemEnvelopeByItemTypeIDsAndUserUIDs(IEnumerable<int> itemTypeIDs, IEnumerable<string> userUIDs)
        {
            var items = _context.Item().AsNoTracking()
                .Where(item => itemTypeIDs.Contains(item.ItemTypeID));
            var itemIDs = items.Select(item => item.ID);
            var (userInPossessions, userPersonalItems) = GetUserInPossessionAndPersonalItemsByItemIDsAndUserUIDs(itemIDs, userUIDs);
            return (items, userInPossessions, userPersonalItems);
        }

        /// Helper method to get CTAMUserInPossession and CTAMUserPersonalItem records using item ID's and UserUID's
        private (IQueryable<CTAMUserInPossession>, IQueryable<CTAMUserPersonalItem>) GetUserInPossessionAndPersonalItemsByItemIDsAndUserUIDs(IEnumerable<int> itemIDs, IEnumerable<string> userUIDs)
        {
            var userInPossessions = _context.CTAMUserInPossession().AsNoTracking()
                .Where(uip => itemIDs.Contains(uip.ItemID) && (uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified) &&
                              userUIDs.Contains(uip.CTAMUserUIDOut));
            var userPersonalItems = _context.CTAMUserPersonalItem().AsNoTracking()
                .Where(upi => (itemIDs.Contains(upi.ItemID) || (upi.ReplacementItemID != null && itemIDs.Contains((int)upi.ReplacementItemID))) && (userUIDs != null ? userUIDs.Contains(upi.CTAMUserUID) : true));
            return (userInPossessions, userPersonalItems);
        }

        /// ItemTypeEnvelope
        /// RoleItemTypeEnvelope - single CTAMRole_ItemType value should be filtered outside of the DataManager
        public (IQueryable<ItemType>,
            IQueryable<CabinetStock>,
            IQueryable<Item>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>,
            IQueryable<ErrorCode>,
            IQueryable<ItemType_ErrorCode>,
            IQueryable<CTAMRole_ItemType>) GetItemTypeEnvelopeByIDsAndUserUIDs(IEnumerable<int> itemTypeIDs, string forCabinetNumber, IEnumerable<string> userUIDs)
        {
            var itemTypes = _context.ItemType().AsNoTracking()
                .Where(itemType => itemTypeIDs.Contains(itemType.ID));
            var cabinetStocks = _context.CabinetStock().AsNoTracking()
                .Where(cabinetStock => itemTypeIDs.Contains(cabinetStock.ItemTypeID));
            var (items, userInPossessions, userPersonalItems) = GetItemEnvelopeByItemTypeIDsAndUserUIDs(itemTypeIDs, userUIDs);
            var roleItemTypes = _context.CTAMRole_ItemType().AsNoTracking()
                .Where(roleItemType => itemTypeIDs.Contains(roleItemType.ItemTypeID));
            var itemTypeErrorCodes = _context.ItemType_ErrorCode().AsNoTracking()
                .Where(itec => itemTypeIDs.Contains(itec.ItemTypeID));
            var errorCodes = itemTypeErrorCodes
                            .Include(itec => itec.ErrorCode)
                            .Select(itec => itec.ErrorCode)
                            .Distinct();

            var roleItemTypesForCabinet = from rit in roleItemTypes
                                          join rc in _context.CTAMRole_Cabinet() on rit.CTAMRoleID equals rc.CTAMRoleID
                                          where rc.CabinetNumber.Equals(forCabinetNumber)
                                          select rit;

            return (itemTypes, cabinetStocks, items, userInPossessions, userPersonalItems, errorCodes, itemTypeErrorCodes, roleItemTypesForCabinet);
        }

        /// ItemTypeEnvelope
        public (IQueryable<ItemType>,
            IQueryable<CabinetStock>,
            IQueryable<Item>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>,
            IQueryable<ErrorCode>,
            IQueryable<ItemType_ErrorCode>,
            IQueryable<CTAMRole_ItemType>) GetItemTypeEnvelopeByIDsAndUserUIDsForCabinet(IEnumerable<int> itemTypeIDs, string forCabinetNumber, IEnumerable<string> userUIDs)
        {
            var itemTypes = _context.ItemType().AsNoTracking()
                .Where(itemType => itemTypeIDs.Contains(itemType.ID));
            var cabinetStocks = _context.CabinetStock().AsNoTracking()
                .Where(cabinetStock => itemTypeIDs.Contains(cabinetStock.ItemTypeID) && cabinetStock.CabinetNumber == forCabinetNumber);
            var (items, userInPossessions, userPersonalItems) = GetItemEnvelopeByItemTypeIDsAndUserUIDs(itemTypeIDs, userUIDs);
            var roleItemTypes = (from rit in _context.CTAMRole_ItemType().AsNoTracking().Where(roleItemType => itemTypeIDs.Contains(roleItemType.ItemTypeID))
                                 join rc in _context.CTAMRole_Cabinet() on rit.CTAMRoleID equals rc.CTAMRoleID
                                 where rc.CabinetNumber.Equals(forCabinetNumber)
                                 select rit).Distinct();

            var itemTypeErrorCodes = _context.ItemType_ErrorCode().AsNoTracking()
                .Where(itec => itemTypeIDs.Contains(itec.ItemTypeID));
            var errorCodes = itemTypeErrorCodes
                            .Include(itec => itec.ErrorCode)
                            .Select(itec => itec.ErrorCode)
                            .Distinct();

            var roleItemTypesForCabinet = from rit in roleItemTypes
                                          join rc in _context.CTAMRole_Cabinet() on rit.CTAMRoleID equals rc.CTAMRoleID
                                          where rc.CabinetNumber.Equals(forCabinetNumber)
                                          select rit;

            return (itemTypes, cabinetStocks, items, userInPossessions, userPersonalItems, errorCodes, itemTypeErrorCodes, roleItemTypesForCabinet);
        }

        /// UserEnvelope
        /// RoleUserEnvelope - single CTAMRole_ItemType value should be filtered outside of the DataManager
        public (IQueryable<CTAMUser>,
            IQueryable<CTAMUser_Role>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>) GetUserEnvelopeByUIDs(IEnumerable<string> uuids)
        {
            var users = _context.CTAMUser().AsNoTracking()
                .Where(user => uuids.Contains(user.UID));
            var userRoles = _context.CTAMUser_Role().AsNoTracking()
                .Include(userRole => userRole.CTAMRole)
                .Where(userRole => uuids.Contains(userRole.CTAMUserUID));
            var userInPossessions = _context.CTAMUserInPossession().AsNoTracking()
                .Where(uip => uuids.Contains(uip.CTAMUserUIDOut)
                            && (uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified));
            var userPersonalItems = _context.CTAMUserPersonalItem().AsNoTracking()
                .Where(upi => uuids.Contains(upi.CTAMUserUID));
            return (users, userRoles, userInPossessions, userPersonalItems);
        }

        public IQueryable<ItemType> GetAllowedItemTypeByCabinetNumber(string cabinetNumber)
        {
            return (from rc in _context.CTAMRole_Cabinet().AsNoTracking().Where(rc => rc.CabinetNumber.Equals(cabinetNumber))
                            join ri in _context.CTAMRole_ItemType().AsNoTracking() on rc.CTAMRoleID equals ri.CTAMRoleID
                            select ri.ItemType).Distinct();
        }

        /// UserEnvelope for Cabinet
        public (IQueryable<CTAMUser>,
            IQueryable<CTAMUser_Role>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>) GetUserEnvelopeByUIDsForCabinet(IEnumerable<string> uuids, string cabinetNumber)
        {
            var users = (from u in _context.CTAMUser().AsNoTracking().Where(user => uuids.Contains(user.UID))
                         join ur in _context.CTAMUser_Role().AsNoTracking() on u.UID equals ur.CTAMUserUID
                         join rc in _context.CTAMRole_Cabinet() on ur.CTAMRoleID equals rc.CTAMRoleID
                         where rc.CabinetNumber.Equals(cabinetNumber)
                         select u).Distinct();
            var userRoles = (from ur in _context.CTAMUser_Role().AsNoTracking().Include(userRole => userRole.CTAMRole).Where(userRole => uuids.Contains(userRole.CTAMUserUID))
                             join rc in _context.CTAMRole_Cabinet() on ur.CTAMRoleID equals rc.CTAMRoleID
                             where rc.CabinetNumber.Equals(cabinetNumber)
                             select ur).Distinct();
            var allowedItemTypeIdsForCabinet = GetAllowedItemTypeByCabinetNumber(cabinetNumber).Select(it => it.ID);
            var userInPossessions = _context.CTAMUserInPossession().AsNoTracking()
                .Where(uip => uuids.Contains(uip.CTAMUserUIDOut)
                            && (uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified)
                            && allowedItemTypeIdsForCabinet.Contains(uip.Item.ItemTypeID));
            var userPersonalItems = _context.CTAMUserPersonalItem().AsNoTracking()
                .Where(upi => uuids.Contains(upi.CTAMUserUID)
                            && allowedItemTypeIdsForCabinet.Contains(upi.Item.ItemTypeID));
            return (users, userRoles, userInPossessions, userPersonalItems);
        }

        /// RoleEnvelope
        /// CTAMRole_Cabinet should be filtered outside of the DataManager
        public (IQueryable<CTAMRole>,
            IQueryable<CTAMRole_Permission>,
            IQueryable<CTAMRole_ItemType>,
            IQueryable<ItemType>,
            IQueryable<CabinetStock>,
            IQueryable<Item>,
            IQueryable<CTAMUserInPossession>,
            IQueryable<CTAMUserPersonalItem>,
            IQueryable<CabinetAccessInterval>,
            IQueryable<ErrorCode>,
            IQueryable<ItemType_ErrorCode>,
            IQueryable<CTAMUser_Role>,
            IQueryable<CTAMUser>) GetRoleEnvelopeByIDsForCabinet(List<int> roleIDs, string forCabinetNumber)
        {
            var roleCabinetsForCabinet = _context.CTAMRole_Cabinet().AsNoTracking()
                .Where(roleCabinet => roleCabinet.CabinetNumber.Equals(forCabinetNumber));
            // Search from Cabinet perspective
            var role = roleCabinetsForCabinet
                .Where(roleCabinet => roleIDs.Contains(roleCabinet.CTAMRoleID))
                .Include(roleCabinet => roleCabinet.CTAMRole)
                .Select(roleCabinet => roleCabinet.CTAMRole);
            var rolePermissions = _context.CTAMRole_Permission()
                .AsNoTracking()
                .Where(rolePermission => roleIDs.Contains(rolePermission.CTAMRoleID))
                .Include(rp => rp.CTAMPermission)
                .Where(rp => rp.CTAMPermission.CTAMModule.Equals(CTAMModule.Cabinet));
            var roleItemTypes = _context.CTAMRole_ItemType().AsNoTracking()
                .Include(roleItemType => roleItemType.ItemType)
                .Where(roleItemType => roleIDs.Contains(roleItemType.CTAMRoleID));
            var itemTypeIDs = roleItemTypes.Select(roleItemType => roleItemType.ItemTypeID);

            var cabinetAccessIntervals = _context.CabinetAccessIntervals().AsNoTracking()
                .Where(cai => roleIDs.Contains(cai.CTAMRoleID));

            var userRoles = _context.CTAMUser_Role().AsNoTracking()
                .Include(userRole => userRole.CTAMUser)
                .Where(userRole => roleIDs.Contains(userRole.CTAMRoleID));

            var userUIDs = userRoles.Select(userRole => userRole.CTAMUserUID);
            // Test if method "GetItemTypeEnvelopeByIDsAndUserUIDsForCabinet" potentially could throw an exception due to userUIDs passed to an inner .Contains
            var users = _context.CTAMUser().AsNoTracking()
                .Where(user => userUIDs.Contains(user.UID));

            var (itemTypes, cabinetStocks, items, userInPossessions, userPersonalItems, errorCodes, itemTypeErrorCodes, _)
                = GetItemTypeEnvelopeByIDsAndUserUIDsForCabinet(itemTypeIDs, forCabinetNumber, userUIDs);

            return (role,
                rolePermissions,
                roleItemTypes,
                itemTypes,
                cabinetStocks,
                items,
                userInPossessions,
                userPersonalItems,
                cabinetAccessIntervals,
                errorCodes,
                itemTypeErrorCodes,
                userRoles,
                users);
        }

        public IQueryable<CTAMUser> GetUsersFromCabinetNumber(string cabinetNumber)
        {
            return _context.CTAMUser_Role().Include(ur => ur.CTAMUser).AsNoTracking()
                .Join(_context.CTAMRole_Cabinet().Where(rc => rc.CabinetNumber.Equals(cabinetNumber)), 
                    ur => ur.CTAMRoleID, 
                    rc => rc.CTAMRoleID, 
                    (ur, rc) => ur.CTAMUser)
                .Distinct();
        }

        public IQueryable<CTAMUserInPossession> GetUserInPossessionsToSyncByID(List<Guid> userInPossessionIDs)
        {
            return _context.CTAMUserInPossession()
                .Where(uip => userInPossessionIDs.Contains(uip.ID)
                            && (uip.Status == UserInPossessionStatus.Picked || uip.Status == UserInPossessionStatus.Overdue || uip.Status == UserInPossessionStatus.Unjustified));
        }

        public IQueryable<CTAMUserPersonalItem> GetUserPersonalItemsToSyncByID(List<int> userPersonalItemIDs)
        {
            return _context.CTAMUserPersonalItem()
                .Where(upi => userPersonalItemIDs.Contains(upi.ID));
        }

        public IQueryable<Cabinet> GetCabinetToSync(string cabinetNumber)
        {
            return _context.Cabinet()
                .Where(c => c.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<CabinetUI> GetCabinetUIToSync(string cabinetNumber)
        {
            var cabinetUI = _context.CabinetUI();
            return cabinetUI;
        }

        public IQueryable<CabinetStock> GetCabinetStocksToSync(List<int> itemTypeIDs, string cabinetNumber)
        {
            return _context.CabinetStock()
                   .Where(cs => itemTypeIDs.Contains(cs.ItemTypeID) && cs.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<CabinetPosition> GetCabinetPositionsToSync(List<int> cabinetPositionsIDs, string cabinetNumber)
        {
            return _context.CabinetPosition()
                   .Where(cp => cabinetPositionsIDs.Contains(cp.ID) && cp.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<CabinetDoor> GetCabinetDoorsToSync(List<int> cabinetDoorsIDs, string cabinetNumber)
        {
            return _context.CabinetDoor()
                   .Where(cd => cabinetDoorsIDs.Contains(cd.ID) && cd.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<ErrorCode> GetErrorCodesToSync(List<int> ids)
        {
            return _context.ErrorCode()
                .Where(ec => ids.Contains(ec.ID));
        }

        public IQueryable<CabinetAccessInterval> GetCabinetAccessIntervalsToSync(List<int> cabinetAccessIntervalIDs)
        {
            return _context.CabinetAccessIntervals()
                .Where(cai => cabinetAccessIntervalIDs.Contains(cai.ID));
        }

        public IQueryable<CTAMSetting> GetSettingsToSync(List<int> ids)
        {
            return _context.CTAMSetting()
                .Where(cs => ids.Contains(cs.ID));
        }

        public IQueryable<Item> GetItemsToSync(List<int> ids)
        {
            return _context.Item()
                .Where(i => ids.Contains(i.ID));
        }

        public IQueryable<ItemType> GetItemTypesToSync(List<int> ids)
        {
            return _context.ItemType()
                .Where(it => ids.Contains(it.ID));
        }

        public IQueryable<CTAMUser> GetUsersToSync(List<string> uids)
        {
            return _context.CTAMUser()
                .Where(u => uids.Contains(u.UID));
        }

        public IQueryable<CTAMRole> GetRolesToSync(List<int> ids)
        {
            return _context.CTAMRole()
                .Where(r => ids.Contains(r.ID));
        }

    }
}
