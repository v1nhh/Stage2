using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Sync.Base;
using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CTAM.Core.Security;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CloudAPI.ApplicationCore.Queries.LiveSync
{
    public class GetCollectedEnvelopeQuery : IRequest<CollectedLiveSyncEnvelope>
    {
        public CollectedLiveSyncRequest CollectedRequest { get; set; }

        public GetCollectedEnvelopeQuery(CollectedLiveSyncRequest collectedRequest)
        {
            this.CollectedRequest = collectedRequest;
        }
    }

    public class GetCollectedEnvelopeHandler : IRequestHandler<GetCollectedEnvelopeQuery, CollectedLiveSyncEnvelope>
    {
        private readonly ILogger<GetCollectedEnvelopeHandler> _logger;
        private readonly IMapper _mapper;
        private readonly LiveSyncDataManager _liveManager;
        private readonly AuthenticationUtilities _authUtils;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCollectedEnvelopeHandler(LiveSyncDataManager liveManager, ILogger<GetCollectedEnvelopeHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _liveManager = liveManager;
            _authUtils = new AuthenticationUtilities(configuration);
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<CollectedLiveSyncEnvelope> Handle(GetCollectedEnvelopeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetCollectedEnvelopeHandler called {JsonConvert.SerializeObject(request)}");
            try
            {
                var collectedLiveSyncEnvelope = new CollectedLiveSyncEnvelope();
                var cabinetNumber = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);

                if (request.CollectedRequest.CabinetUI.Count() > 0)
                {
                    collectedLiveSyncEnvelope.CabinetUIs = _mapper.Map<List<CabinetUIBaseDTO>>(
                        await _liveManager.GetCabinetUIToSync(request.CollectedRequest.CabinetUI.First()).ToListAsync()); // For now only "DEFAULT"
                }

                if (request.CollectedRequest.CabinetNumbers.Count() > 0)
                {
                    collectedLiveSyncEnvelope.Cabinets = _mapper.Map<List<CabinetBaseDTO>>(
                        await _liveManager.GetCabinetToSync(request.CollectedRequest.CabinetNumbers.First()).ToListAsync());
                }

                if (request.CollectedRequest.CabinetStockItemTypeIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.CabinetStocks = _mapper.Map<List<CabinetStockBaseDTO>>(
                        await _liveManager.GetCabinetStocksToSync(request.CollectedRequest.CabinetStockItemTypeIDs, cabinetNumber).ToListAsync());
                }

                if (request.CollectedRequest.CabinetPositionIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.CabinetPositions = _mapper.Map<List<CabinetPositionBaseDTO>>(
                        await _liveManager.GetCabinetPositionsToSync(request.CollectedRequest.CabinetPositionIDs, cabinetNumber).ToListAsync());
                }

                if (request.CollectedRequest.CabinetDoorIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.CabinetDoors = _mapper.Map<List<CabinetDoorBaseDTO>>(
                        await _liveManager.GetCabinetDoorsToSync(request.CollectedRequest.CabinetDoorIDs, cabinetNumber).ToListAsync());
                }

                if (request.CollectedRequest.ErrorCodeIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.ErrorCodes = _mapper.Map<List<ErrorCodeBaseDTO>>(
                        await _liveManager.GetErrorCodesToSync(request.CollectedRequest.ErrorCodeIDs).ToListAsync());
                }

                if (request.CollectedRequest.SettingIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.Settings = _mapper.Map<List<CTAMSettingBaseDTO>>(
                        await _liveManager.GetSettingsToSync(request.CollectedRequest.SettingIDs).ToListAsync());
                }

                if (request.CollectedRequest.ItemIDs.Count() > 0)
                {
                    var itemBaseDTOs = new List<ItemBaseDTO>();
                    foreach (var chunk in  request.CollectedRequest.ItemIDs.Partition(15000))
                    {
                        var chunkItemBaseDTOs = _mapper.Map<List<ItemBaseDTO>>(
                            await _liveManager.GetItemsToSync(chunk).ToListAsync());
                        itemBaseDTOs.AddRange(chunkItemBaseDTOs);
                    }
                    collectedLiveSyncEnvelope.Items = itemBaseDTOs;
                }

                if (request.CollectedRequest.ItemTypeIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.ItemTypes = _mapper.Map<List<ItemTypeBaseDTO>>(
                        await _liveManager.GetItemTypesToSync(request.CollectedRequest.ItemTypeIDs).ToListAsync());
                }

                if (request.CollectedRequest.ItemTypeIDsForEnvelope.Count() > 0)
                {
                    var userUIDs = await _liveManager.GetUsersFromCabinetNumber(cabinetNumber).Select(u => u.UID).ToListAsync();
                    
                    foreach (var chunk in userUIDs.Partition(15000))
                    {
                        var (itEnvItemTypes,
                            itEnvCabinetStocks,
                            itEnvItems,
                            itEnvUserInPossessions,
                            itEnvUserPersonalItems,
                            itEnvErrorCodes,
                            itEnvItemTypeErrorCodes,
                            itEnvRoleItemTypes) = _liveManager.GetItemTypeEnvelopeByIDsAndUserUIDsForCabinet(request.CollectedRequest.ItemTypeIDsForEnvelope, cabinetNumber, chunk);

                        collectedLiveSyncEnvelope.ItemTypes.AddRange(_mapper.Map<List<ItemTypeBaseDTO>>(await itEnvItemTypes.ToListAsync()));
                        collectedLiveSyncEnvelope.CabinetStocks.AddRange(_mapper.Map<List<CabinetStockBaseDTO>>(await itEnvCabinetStocks.ToListAsync()));
                        collectedLiveSyncEnvelope.Items.AddRange(_mapper.Map<List<ItemBaseDTO>>(await itEnvItems.ToListAsync()));
                        collectedLiveSyncEnvelope.UserInPossessions.AddRange(_mapper.Map<List<CTAMUserInPossessionBaseDTO>>(await itEnvUserInPossessions.ToListAsync()));
                        collectedLiveSyncEnvelope.UserPersonalItems.AddRange(_mapper.Map<List<CTAMUserPersonalItemBaseDTO>>(await itEnvUserPersonalItems.ToListAsync()));
                        collectedLiveSyncEnvelope.ErrorCodes.AddRange(_mapper.Map<List<ErrorCodeBaseDTO>>(await itEnvErrorCodes.ToListAsync()));
                        collectedLiveSyncEnvelope.ItemTypeErrorCodes.AddRange(_mapper.Map<List<ItemType_ErrorCodeBaseDTO>>(await itEnvItemTypeErrorCodes.ToListAsync()));
                        collectedLiveSyncEnvelope.RoleItemTypes.AddRange(_mapper.Map<List<CTAMRole_ItemTypeBaseDTO>>(await itEnvRoleItemTypes.ToListAsync()));
                    }
                }

                if (request.CollectedRequest.UserIDs.Count() > 0)
                {
                    var cTAMUserBaseDTOs = new List<CTAMUserBaseDTO>();
                    foreach (var chunk in request.CollectedRequest.UserIDs.Partition(15000))
                    {
                        var chunkedCTAMUserBaseDTOs = _mapper.Map<List<CTAMUserBaseDTO>>(await _liveManager.GetUsersToSync(chunk).ToListAsync());
                        cTAMUserBaseDTOs.AddRange(chunkedCTAMUserBaseDTOs);

                    }
                    collectedLiveSyncEnvelope.Users = cTAMUserBaseDTOs;
                }
                if (request.CollectedRequest.UserIDsForEnvelope.Count() > 0)
                {
                    var (uEnvUsers, uEnvUserRoles, uEnvUserInPossessions, uEnvUserPersonalItems) = _liveManager.GetUserEnvelopeByUIDsForCabinet(request.CollectedRequest.UserIDsForEnvelope, cabinetNumber);
                    collectedLiveSyncEnvelope.Users.AddRange(_mapper.Map<List<CTAMUserBaseDTO>>(await uEnvUsers.ToListAsync()));
                    collectedLiveSyncEnvelope.UserRoles.AddRange(_mapper.Map<List<CTAMUser_RoleBaseDTO>>(await uEnvUserRoles.ToListAsync()));
                    collectedLiveSyncEnvelope.UserInPossessions.AddRange(_mapper.Map<List<CTAMUserInPossessionBaseDTO>>(await uEnvUserInPossessions.ToListAsync()));
                    collectedLiveSyncEnvelope.UserPersonalItems.AddRange(_mapper.Map<List<CTAMUserPersonalItemBaseDTO>>(await uEnvUserPersonalItems.ToListAsync()));
                }

                if (request.CollectedRequest.RoleIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.Roles.AddRange(_mapper.Map<List<CTAMRoleBaseDTO>>(
                        await _liveManager.GetRolesToSync(request.CollectedRequest.RoleIDs).ToListAsync()));
                }

                if (request.CollectedRequest.RoleIDsForEnvelope.Count() > 0)
                {
                    var (rEnvRoles,
                        rEnvRolePermissions,
                        rEnvRoleItemTypes,
                        rEnvItemTypes,
                        rEnvCabinetStocks,
                        rEnvItems,
                        rEnvUserInPossessions,
                        rEnvUserPersonalItems,
                        rEnvCabinetAccessIntervals,
                        rEnvErrorCodes,
                        rEnvItemTypeErrorCodes,
                        rEnvUserRoles,
                        rEnvUsers) = _liveManager.GetRoleEnvelopeByIDsForCabinet(request.CollectedRequest.RoleIDsForEnvelope, cabinetNumber);

                    collectedLiveSyncEnvelope.Roles.AddRange(_mapper.Map<List<CTAMRoleBaseDTO>>(await rEnvRoles.ToListAsync()));
                    collectedLiveSyncEnvelope.RolePermissions.AddRange(_mapper.Map<List<CTAMRole_PermissionBaseDTO>>(await rEnvRolePermissions.ToListAsync()));
                    collectedLiveSyncEnvelope.RoleItemTypes.AddRange(_mapper.Map<List<CTAMRole_ItemTypeBaseDTO>>(await rEnvRoleItemTypes.ToListAsync()));
                    collectedLiveSyncEnvelope.ItemTypes.AddRange(_mapper.Map<List<ItemTypeBaseDTO>>(await rEnvItemTypes.ToListAsync()));
                    collectedLiveSyncEnvelope.CabinetStocks.AddRange(_mapper.Map<List<CabinetStockBaseDTO>>(await rEnvCabinetStocks.ToListAsync()));
                    collectedLiveSyncEnvelope.Items.AddRange(_mapper.Map<List<ItemBaseDTO>>(await rEnvItems.ToListAsync()));
                    collectedLiveSyncEnvelope.UserInPossessions.AddRange(_mapper.Map<List<CTAMUserInPossessionBaseDTO>>(await rEnvUserInPossessions.ToListAsync()));
                    collectedLiveSyncEnvelope.UserPersonalItems.AddRange(_mapper.Map<List<CTAMUserPersonalItemBaseDTO>>(await rEnvUserPersonalItems.ToListAsync()));
                    collectedLiveSyncEnvelope.CabinetAccessIntervals.AddRange(_mapper.Map<List<CabinetAccessIntervalsBaseDTO>>(await rEnvCabinetAccessIntervals.ToListAsync()));
                    collectedLiveSyncEnvelope.ErrorCodes.AddRange(_mapper.Map<List<ErrorCodeBaseDTO>>(await rEnvErrorCodes.ToListAsync()));
                    collectedLiveSyncEnvelope.ItemTypeErrorCodes.AddRange(_mapper.Map<List<ItemType_ErrorCodeBaseDTO>>(await rEnvItemTypeErrorCodes.ToListAsync()));
                    collectedLiveSyncEnvelope.UserRoles.AddRange(_mapper.Map<List<CTAMUser_RoleBaseDTO>>(await rEnvUserRoles.ToListAsync()));
                    collectedLiveSyncEnvelope.Users.AddRange(_mapper.Map<List<CTAMUserBaseDTO>>(await rEnvUsers.ToListAsync()));
                }

                if (request.CollectedRequest.CabinetAccessIntervalIDs.Count() > 0)
                {
                    collectedLiveSyncEnvelope.CabinetAccessIntervals.AddRange(_mapper.Map<List<CabinetAccessIntervalsBaseDTO>>(
                        await _liveManager.GetCabinetAccessIntervalsToSync(request.CollectedRequest.CabinetAccessIntervalIDs).ToListAsync()));
                }

                if (request.CollectedRequest.UserPersonalItemIDs.Count() > 0)
                {
                    var cTAMUserPersonalItemBaseDTOs = new List<CTAMUserPersonalItemBaseDTO>();
                    foreach (var chunk in request.CollectedRequest.UserPersonalItemIDs.Partition(10000))
                    {
                        var chunked = _mapper.Map<List<CTAMUserPersonalItemBaseDTO>>(await _liveManager.GetUserPersonalItemsToSyncByID(chunk).ToListAsync());
                        cTAMUserPersonalItemBaseDTOs.AddRange(chunked);
                    }
                    collectedLiveSyncEnvelope.UserPersonalItems.AddRange(cTAMUserPersonalItemBaseDTOs);
                }

                if (request.CollectedRequest.UserInPossessionIDs.Count() > 0)
                {
                    var cTAMUserInPossessionBaseDTOs = new List<CTAMUserInPossessionBaseDTO>();
                    foreach (var chunk in request.CollectedRequest.UserInPossessionIDs.Partition(15000))
                    {
                        var chunkCTAMUserInPossessionBaseDTOs = _mapper.Map<List<CTAMUserInPossessionBaseDTO>>(
                            await _liveManager.GetUserInPossessionsToSyncByID(chunk).ToListAsync());
                        cTAMUserInPossessionBaseDTOs.AddRange(chunkCTAMUserInPossessionBaseDTOs);
                    }
                    collectedLiveSyncEnvelope.UserInPossessions = cTAMUserInPossessionBaseDTOs;
                }

                // Distinct
                collectedLiveSyncEnvelope.Cabinets = collectedLiveSyncEnvelope.Cabinets.GroupBy(c => c.CabinetNumber).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.CabinetAccessIntervals = collectedLiveSyncEnvelope.CabinetAccessIntervals.GroupBy(cai => cai.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.CabinetStocks = collectedLiveSyncEnvelope.CabinetStocks.GroupBy(cs => new { cs.CabinetNumber, cs.ItemTypeID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.CabinetPositions = collectedLiveSyncEnvelope.CabinetPositions.GroupBy(cp => new { cp.CabinetNumber, cp.ID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.CabinetUIs = collectedLiveSyncEnvelope.CabinetUIs.GroupBy(cu => cu.CabinetNumber).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.Items = collectedLiveSyncEnvelope.Items.GroupBy(i => i.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.ItemTypes = collectedLiveSyncEnvelope.ItemTypes.GroupBy(it => it.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.Users = collectedLiveSyncEnvelope.Users.GroupBy(u => u.UID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.Roles = collectedLiveSyncEnvelope.Roles.GroupBy(r => r.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.RolePermissions = collectedLiveSyncEnvelope.RolePermissions.GroupBy(rp => new { rp.CTAMRoleID, rp.CTAMPermissionID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.RoleItemTypes = collectedLiveSyncEnvelope.RoleItemTypes.GroupBy(rit => new { rit.CTAMRoleID, rit.ItemTypeID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.UserRoles = collectedLiveSyncEnvelope.UserRoles.GroupBy(ur => new { ur.CTAMUserUID, ur.CTAMRoleID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.ItemTypeErrorCodes = collectedLiveSyncEnvelope.ItemTypeErrorCodes.GroupBy(itec => new { itec.ItemTypeID, itec.ErrorCodeID }).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.Settings = collectedLiveSyncEnvelope.Settings.GroupBy(s => s.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.ErrorCodes = collectedLiveSyncEnvelope.ErrorCodes.GroupBy(ec => ec.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.UserPersonalItems = collectedLiveSyncEnvelope.UserPersonalItems.GroupBy(upi => upi.ID).Select(g => g.First()).ToList();
                collectedLiveSyncEnvelope.UserInPossessions = collectedLiveSyncEnvelope.UserInPossessions.GroupBy(uip => uip.ID).Select(g => g.First()).ToList();

                return collectedLiveSyncEnvelope;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetMostInnerException().Message);
            }

            return null;
        }
    }
}