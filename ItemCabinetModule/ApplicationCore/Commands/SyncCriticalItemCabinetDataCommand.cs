using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Commands.Sync
{
    public class SyncCriticalItemCabinetDataCommand : IRequest
    {
        public string CabinetNumber
        {
            get; set;
        }

        public List<CabinetPositionContentDTO> AddedCabinetPositionContentsDTO
        {
            get; set;
        }

        public List<CabinetPositionContentDTO> RemovedCabinetPositionContentsDTO
        {
            get; set;
        }

        public List<CabinetStockDTO> CabinetStocksDTO
        {
            get; set;
        }

        public List<ItemToPickDTO> ItemsToPickDTO
        {
            get; set;
        }

        public List<UserInPossessionDTO> UserInPossesionsDTO
        {
            get; set;
        }

        public List<UserPersonalItemDTO> CTAMUserPersonalItemsDTO
        {
            get; set;
        }

        // Make sure no properties are missing
        public SyncCriticalItemCabinetDataCommand(string cabinetNumber,
            List<CabinetPositionContentDTO> addedCabinetPositionContentsDTO,
            List<CabinetPositionContentDTO> removedCabinetPositionContentsDTO,
            List<CabinetStockDTO> cabinetStocksDTO, List<ItemToPickDTO> itemsToPickDTO,
            List<UserInPossessionDTO> userInPossesionsDTO,
            List<UserPersonalItemDTO> cTAMUserPersonalItemsDTO)
        {
            CabinetNumber = cabinetNumber;
            AddedCabinetPositionContentsDTO = addedCabinetPositionContentsDTO;
            RemovedCabinetPositionContentsDTO = removedCabinetPositionContentsDTO;
            CabinetStocksDTO = cabinetStocksDTO;
            ItemsToPickDTO = itemsToPickDTO;
            UserInPossesionsDTO = userInPossesionsDTO;
            CTAMUserPersonalItemsDTO = cTAMUserPersonalItemsDTO;
        }
    }

    public class SyncCriticalItemCabinetDataHandler : IRequestHandler<SyncCriticalItemCabinetDataCommand>
    {
        private readonly ILogger<SyncCriticalItemCabinetDataHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public SyncCriticalItemCabinetDataHandler(MainDbContext context, ILogger<SyncCriticalItemCabinetDataHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;

        }

        public async Task<Unit> Handle(SyncCriticalItemCabinetDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCriticalItemCabinetDataHandler called");
            if (request == null || string.IsNullOrWhiteSpace(request.CabinetNumber))
            {
                var msg = "SyncCriticalItemCabinetDataHandler failed: provided data cannot be null";
                _logger.LogError(msg);
                throw new NullReferenceException(msg);
            }
            var foundCabinet = await _context.Cabinet()
                .AsNoTracking()
                .Where(c => c.CabinetNumber.Equals(request.CabinetNumber))
                .FirstOrDefaultAsync();

            await SyncCabinetPositionsContents(request, foundCabinet);
            await SyncUserPersonalItems(request, foundCabinet);
            await SyncUserInPossessions(request, foundCabinet);
            await SyncCabinetStocks(request);
            await SyncItemsToPick(request);

            await _context.SaveChangesAsync();
            return new Unit();
        }

        public async Task SyncUserPersonalItems(SyncCriticalItemCabinetDataCommand request, Cabinet cabinet)
        {
            var userPersonalItemsMap = _mapper.Map<List<CTAMUserPersonalItem>>(request.CTAMUserPersonalItemsDTO)
                .ToDictionary(i => i.ID);
            var userPersonalItemsIds = userPersonalItemsMap.Keys.ToList();
            var foundPersonalItems = await _context.CTAMUserPersonalItem()
                .Where(upItem => userPersonalItemsIds.Contains(upItem.ID)).ToListAsync();
            foreach (var personalItem in foundPersonalItems)
            {
                var persItemFromCabinet = userPersonalItemsMap[personalItem.ID];
                if (!_context.Item().Any(i => i.ID == persItemFromCabinet.ItemID))
                {
                    await AddCabinetLog(request, cabinet, $"CTAMUserPersonalItem kan niet aangepast worden omdat ItemID {persItemFromCabinet.ItemID} niet bestaat. {JsonConvert.SerializeObject(persItemFromCabinet)}");
                }
                else if (persItemFromCabinet.ReplacementItemID != null && !_context.Item().Any(i => i.ID == persItemFromCabinet.ReplacementItemID))
                {
                    await AddCabinetLog(request, cabinet, $"CTAMUserPersonalItem kan niet aangepast worden omdat ReplacementItemID {persItemFromCabinet.ReplacementItemID} niet bestaat. {JsonConvert.SerializeObject(persItemFromCabinet)}");
                }
                else if (personalItem.UpdateDT < persItemFromCabinet.UpdateDT
                    && (personalItem.ItemID != persItemFromCabinet.ItemID
                    || personalItem.ReplacementItemID != persItemFromCabinet.ReplacementItemID
                    || personalItem.CabinetNumber != persItemFromCabinet.CabinetNumber
                    || personalItem.Status != persItemFromCabinet.Status))
                {
                    personalItem.ItemID = persItemFromCabinet.ItemID;
                    personalItem.CabinetNumber = persItemFromCabinet.CabinetNumber;
                    personalItem.ReplacementItemID = persItemFromCabinet.ReplacementItemID;
                    personalItem.Status = persItemFromCabinet.Status;
                    personalItem.UpdateDT = persItemFromCabinet.UpdateDT;
                }
            }
        }

        public async Task SyncUserInPossessions(SyncCriticalItemCabinetDataCommand request, Cabinet cabinet)
        {
            var userInPosessions = _mapper.Map<List<CTAMUserInPossession>>(request.UserInPossesionsDTO);

            // Additional check for userInPossessions which have been updated from Picked to Removed (this happens in replacement flow on cabinet)
            var removedRecordIDs = userInPosessions.Where(uip => uip.Status == UserInPossessionStatus.Removed).Select(uip => uip.ID).ToList();
            var removedRecordsInDatabase = await _context.CTAMUserInPossession().Where(uip => removedRecordIDs.Contains(uip.ID)).ToListAsync();

            var addedRecords = userInPosessions.Where(uip => uip.Status == UserInPossessionStatus.Picked
                                || uip.Status == UserInPossessionStatus.Overdue
                                || uip.Status == UserInPossessionStatus.Added
                                || uip.Status == UserInPossessionStatus.Unjustified
                                || (uip.Status == UserInPossessionStatus.Removed && !removedRecordsInDatabase.Select(uip => uip.ID).Contains(uip.ID)))
                                .ToList();

            foreach (var uip in addedRecords)
            {
                if (_context.Item().Any(i => i.ID == uip.ItemID))
                {
                    var uipDb = await _context.CTAMUserInPossession().Where(p => p.ID == uip.ID).FirstOrDefaultAsync();
                    if (uipDb != null)
                    {
                        _mapper.Map<CTAMUserInPossession, CTAMUserInPossession>(uip, uipDb);
                    }
                    else
                    {
                        await _context.CTAMUserInPossession().AddAsync(uip);
                    }
                }
                else
                {
                    await AddCabinetLog(request, cabinet, $"CTAMUserInPossession kan niet toegevoegd worden omdat ItemID {uip.ItemID} niet bestaat. {JsonConvert.SerializeObject(uip)}");
                }
            }

            var updatedRecords = userInPosessions.Where(uip => uip.Status == UserInPossessionStatus.Returned
                                || uip.Status == UserInPossessionStatus.InCorrectReturned
                                || uip.Status == UserInPossessionStatus.DefectReturned
                                || (uip.Status == UserInPossessionStatus.Removed && removedRecordsInDatabase.Select(uip => uip.ID).Contains(uip.ID)));

            foreach (var updatedRecord in updatedRecords)
            {
                var foundRecord = await _context.CTAMUserInPossession().FindAsync(updatedRecord.ID);
                if (foundRecord != null)
                {
                    // This function has a cap at 3 offline routines!
                    // The swapback is earlier then in the cloud known swap
                    // And a swap on the cabinet is done later then in the cloud known swapback
                    // So it has to be moved to the correct record. This is 1 routine.
                    // This only is done when the closestRecord has a InDT
                    if (foundRecord.InDT != null && foundRecord.InDT > updatedRecord.InDT)
                    {
                        // Find the record closest to foundRecord from all UIPs
                        CTAMUserInPossession closestRecord = GetClosestRecordByItemId(userInPosessions, foundRecord);
                        if (closestRecord.InDT != null)
                        {
                            await HandleRecordFurtherUpTheLadder(userInPosessions, closestRecord);
                        }
                        MapDataToRecord(foundRecord, closestRecord);
                        MapDataToRecord(updatedRecord, foundRecord);
                    }
                    else if (foundRecord.InDT != null && foundRecord.InDT < updatedRecord.InDT)
                    {
                        // Find the record closest to updatedRecord from existing records
                        CTAMUserInPossession closestExistingRecord = await GetClosestExistingRecordByItemIdAsync(updatedRecord);
                        if (closestExistingRecord.InDT != null)
                        {
                            // Find the record closest to closestExistingRecord from all UIPs
                            CTAMUserInPossession closestRecord = GetClosestRecordByItemId(userInPosessions, closestExistingRecord);
                            if (closestRecord.InDT != null)
                            {
                                await HandleRecordFurtherUpTheLadder(userInPosessions, closestRecord);
                            }
                            MapDataToRecord(closestExistingRecord, closestRecord);
                        }
                        MapDataToRecord(updatedRecord, closestExistingRecord);
                    }
                    else
                    {
                        MapDataToRecord(updatedRecord, foundRecord);
                    }
                }
                else
                {
                    // Add returned record to db if item was picked and returned while cabinet was offline                    
                    await _context.CTAMUserInPossession().AddAsync(updatedRecord);
                }
            }
            _context.ChangeTracker.Entries();
        }

        private async Task AddCabinetLog(SyncCriticalItemCabinetDataCommand request, Cabinet cabinet, string log)
        {
            await _context.CabinetLog().AddAsync(new CabinetLog
            {
                LogDT = DateTime.UtcNow,
                CabinetNumber = request.CabinetNumber,
                CabinetName = cabinet.Name,
                Level = UserRoleModule.ApplicationCore.Enums.LogLevel.Error,
                Source = UserRoleModule.ApplicationCore.Enums.LogSource.CloudAPI,
                LogResourcePath = log
            });
        }

        private async Task HandleRecordFurtherUpTheLadder(List<CTAMUserInPossession> userInPosessions, CTAMUserInPossession closestRecord)
        {
            // Find the record closest to closestRecord from existing records
            var actuallyToUpdateRecord = await GetClosestExistingRecordByItemIdAsync(closestRecord);
            if (actuallyToUpdateRecord.InDT != null)
            {
                // Find the record closest to actuallyToUpdateRecord from all UIPs
                CTAMUserInPossession closestRecordToMoveActuallyToUpdateRecordInTo = GetClosestRecordByItemId(userInPosessions, actuallyToUpdateRecord);
                if (closestRecordToMoveActuallyToUpdateRecordInTo.InDT != null)
                {
                    // Find the record closest to closestRecordToMoveActuallyToUpdateRecordInToInDT from existing records
                    CTAMUserInPossession actuallyToUpdateRecordAfterOtherHasInDT = await GetClosestExistingRecordByItemIdAsync(closestRecordToMoveActuallyToUpdateRecordInTo);
                    if (actuallyToUpdateRecordAfterOtherHasInDT.InDT != null)
                    {
                        // Find the record closest to actuallyToUpdateRecordAfterOtherHasInDT from all UIPs
                        CTAMUserInPossession closestRecordToMoveActuallyToUpdateRecordAfterOtherHasInDTInTo = GetClosestRecordByItemId(userInPosessions, actuallyToUpdateRecordAfterOtherHasInDT);
                        MapDataToRecord(actuallyToUpdateRecordAfterOtherHasInDT, closestRecordToMoveActuallyToUpdateRecordAfterOtherHasInDTInTo);
                    }
                    MapDataToRecord(closestRecordToMoveActuallyToUpdateRecordInTo, actuallyToUpdateRecordAfterOtherHasInDT);
                }
                MapDataToRecord(actuallyToUpdateRecord, closestRecordToMoveActuallyToUpdateRecordInTo);
            }
            MapDataToRecord(closestRecord, actuallyToUpdateRecord);
        }

        private async Task<CTAMUserInPossession> GetClosestExistingRecordByItemIdAsync(CTAMUserInPossession closestRecord)
        {
            DateTime closestRecordInDT = (DateTime)closestRecord.InDT;
            var actuallyToUpdateRecord = await _context.CTAMUserInPossession()
                .Where(d => d.ItemID.Equals(closestRecord.ItemID) && d.OutDT < closestRecordInDT)
                .OrderByDescending(d => d.OutDT)
                .FirstOrDefaultAsync();

            return actuallyToUpdateRecord;
        }

        private static CTAMUserInPossession GetClosestRecordByItemId(List<CTAMUserInPossession> userInPosessions, CTAMUserInPossession foundRecord)
        {
            userInPosessions = userInPosessions.Where(r => r.ItemID.Equals(foundRecord.ItemID)).ToList();
            DateTime targetDT = (DateTime)foundRecord.InDT;
            TimeSpan closestDiff = TimeSpan.MaxValue;
            CTAMUserInPossession closestRecord = null;

            foreach (var userInPosession in userInPosessions)
            {
                TimeSpan diff = (DateTime)userInPosession.OutDT - targetDT;
                if (diff.Duration() < closestDiff.Duration())
                {
                    closestDiff = diff;
                    closestRecord = userInPosession;
                }
            }

            return closestRecord;
        }

        private static void MapDataToRecord(CTAMUserInPossession source, CTAMUserInPossession destination)
        {
            destination.Status = source.Status;
            destination.InDT = source.InDT;
            destination.CabinetPositionIDIn = source.CabinetPositionIDIn;
            destination.CabinetNumberIn = source.CabinetNumberIn;
            destination.CabinetNameIn = source.CabinetNameIn;
            destination.CTAMUserUIDIn = source.CTAMUserUIDIn;
            destination.CTAMUserNameIn = source.CTAMUserNameIn;
            destination.CTAMUserEmailIn = source.CTAMUserEmailIn;
        }

        public async Task SyncCabinetPositionsContents(SyncCriticalItemCabinetDataCommand request, Cabinet cabinet)
        {
            if (request.AddedCabinetPositionContentsDTO != null && request.AddedCabinetPositionContentsDTO.Count > 0)
            {
                var cabinetPositionContents = _mapper.Map<List<CabinetPositionContent>>(request.AddedCabinetPositionContentsDTO);
                var cabinetPositionIDs = request.AddedCabinetPositionContentsDTO.Select(rcpc => rcpc.CabinetPositionID);
                var existingCabinetPositionContents = await _context.CabinetPositionContent()
                    .Where(cpc => cabinetPositionIDs.Contains(cpc.CabinetPositionID))
                    .ToListAsync();
                // Add only CabinetPositionContents that don't exist yet in CloudDB
                var cabinetPositionContentsToAdd = cabinetPositionContents
                    .Where(cpc => !existingCabinetPositionContents
                                    .Where(ecpc => ecpc.CabinetPositionID == cpc.CabinetPositionID && ecpc.ItemID == cpc.ItemID)
                                    .Any())
                    .ToList();
                foreach (var cpc in cabinetPositionContentsToAdd)
                {
                    if (_context.Item().Any(i => i.ID == cpc.ItemID))
                    {
                        await _context.CabinetPositionContent().AddAsync(cpc);
                    }
                    else
                    {
                        await AddCabinetLog(request, cabinet, $"CabinetPosisitionContent kan niet toegevoegd worden omdat ItemID {cpc.ItemID} niet bestaat. {JsonConvert.SerializeObject(cpc)}");
                    }
                }
            }
            if (request.RemovedCabinetPositionContentsDTO != null && request.RemovedCabinetPositionContentsDTO.Count > 0)
            {
                //var cabinetPositionContents = _mapper.Map<List<CabinetPositionContent>>(request.RemovedCabinetPositionContentsDTO);
                var cabinetPositionIDs = request.RemovedCabinetPositionContentsDTO.Select(rcpc => rcpc.CabinetPositionID);
                var cabinetPositionContents = await _context.CabinetPositionContent()
                    .Where(cpc => cabinetPositionIDs.Contains(cpc.CabinetPositionID))
                    .ToListAsync();
                var cabinetPositionContentsToRemove = cabinetPositionContents
                    .Where(cpc => request.RemovedCabinetPositionContentsDTO
                                    .Where(rcpc => rcpc.CabinetPositionID == cpc.CabinetPositionID && rcpc.ItemID == cpc.ItemID)
                                    .Any())
                    .ToList();
                _context.CabinetPositionContent().RemoveRange(cabinetPositionContentsToRemove);
            }
        }

        public async Task SyncCabinetStocks(SyncCriticalItemCabinetDataCommand request)
        {
            var cabinetStockMap = _mapper.Map<List<CabinetStock>>(request.CabinetStocksDTO)
                .Where(cs => cs.CabinetNumber.Equals(request.CabinetNumber))
                .ToDictionary(cs => cs.ItemTypeID);

            var cabinetStockIds = cabinetStockMap.Keys.ToList();

            var foundCabinetStocks = await _context.CabinetStock()
                .Where(cs => cs.CabinetNumber.Equals(request.CabinetNumber) && cabinetStockIds.Contains(cs.ItemTypeID))
                .ToListAsync();

            var notFoundCabinetStocks = cabinetStockMap.Values.ToList();

            var cabinet = await _context.Cabinet().FirstOrDefaultAsync(c => c.CabinetNumber.Equals(request.CabinetNumber));

            foreach (var cabinetStock in foundCabinetStocks)
            {
                var itemType = await _context.ItemType().FirstOrDefaultAsync(itemtype => itemtype.ID == cabinetStock.ItemTypeID);

                notFoundCabinetStocks.RemoveAll(cs => cs.ItemTypeID == cabinetStock.ItemTypeID);
                var cabinetStockFromCabinet = cabinetStockMap[cabinetStock.ItemTypeID];
                if (cabinetStock.ActualStock != cabinetStockFromCabinet.ActualStock
                    || cabinetStock.Status != cabinetStockFromCabinet.Status)
                {
                    cabinetStock.ActualStock = cabinetStockFromCabinet.ActualStock;
                    cabinetStock.Status = cabinetStockFromCabinet.Status;

                    if (cabinetStock.ActualStock < cabinetStock.MinimalStock)
                    {
                        if (cabinetStock.Status != CabinetStockStatus.WarningBelowMinimumSend)
                        {
                            cabinetStock.Status = CabinetStockStatus.WarningBelowMinimumSend;
                        }
                    }
                    if ((cabinetStock.ActualStock == cabinetStock.MinimalStock) && cabinetStock.MinimalStock > 0)
                    {
                        if (cabinetStock.Status == CabinetStockStatus.WarningBelowMinimumSend)
                        {
                            cabinetStock.Status = CabinetStockStatus.OK;
                        }
                    }
                }
            }
            if (notFoundCabinetStocks.Count > 0)
            {
                await _context.CabinetStock().AddRangeAsync(notFoundCabinetStocks);
            }
        }

        /**
         * Check ItemsToPick status, if picked remove from db
         */
        public async Task SyncItemsToPick(SyncCriticalItemCabinetDataCommand request)
        {
            var pickedItemsIDs = request.ItemsToPickDTO
                .Where(itp => itp.Status == ItemToPickStatus.Picked)
                .Select(pickedItem => pickedItem.ID)
                .ToList();
            var pickedItems = await _context.ItemToPick()
                .Where(itp => pickedItemsIDs.Contains(itp.ID))
                .ToListAsync();
            _context.ItemToPick().RemoveRange(pickedItems);
        }
    }

}
