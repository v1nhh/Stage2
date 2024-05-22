using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Queries.LiveSync;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Profiles;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests.LiveSync
{
    public class GetItemTypeEnvelopeTest : AbstractIntegrationTests
    {
        private const int ITEM_TYPE_FULL_ID = int.MaxValue;
        private const int ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID = int.MaxValue - 1;
        private const int ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID = int.MaxValue - 2;
        private const int ITEM_TYPE_WITHOUT_ITEM_ID = int.MaxValue - 3;
        private const int ITEM_TYPE_WITHOUT_CABINET_STOCK_ID = int.MaxValue - 4;
        private const int ITEM_TYPE_WITHOUT_ROLES_ID = int.MaxValue - 5;
        private const int NON_EXISTING_ITEM_TYPE_ID = int.MaxValue - 6;

        private const string USER_FULL_UID = "00000000-0000-0000-0000-000000000000";

        private const int ERROR_CODE_ID_1 = int.MaxValue;
        private const int ERROR_CODE_ID_2 = int.MaxValue - 1;
        private const int ERROR_CODE_ID_3 = int.MaxValue - 2;

        private const string CABINET_NUMBER = "000000000000";

        public GetItemTypeEnvelopeTest() : base("CTAM_GetItemTypeEnvelope")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ErrorCode().Add(new ErrorCode { ID = ERROR_CODE_ID_1, Code = "1", Description = "Whatever1", CreateDT = DateTime.UtcNow });
                context.ErrorCode().Add(new ErrorCode { ID = ERROR_CODE_ID_2, Code = "2", Description = "Whatever2", CreateDT = DateTime.UtcNow });
                context.ErrorCode().Add(new ErrorCode { ID = ERROR_CODE_ID_3, Code = "3", Description = "Whatever3", CreateDT = DateTime.UtcNow });

                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_FULL_ID, Description = "ITEM_TYPE_FULL_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, Description = "ITEM_TYPE_WITH_PARTIAL_ITEM_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, Description = "ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITHOUT_ITEM_ID, Description = "ITEM_TYPE_WITHOUT_ITEM_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, Description = "ITEM_TYPE_WITHOUT_CABINET_STOCK_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITHOUT_ROLES_ID, Description = "ITEM_TYPE_WITHOUT_ROLES_ID" });

                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode { ItemTypeID = ITEM_TYPE_FULL_ID, ErrorCodeID = ERROR_CODE_ID_1 });
                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode { ItemTypeID = ITEM_TYPE_FULL_ID, ErrorCodeID = ERROR_CODE_ID_2 });

                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_FULL_ID, CabinetNumber = CABINET_NUMBER });
                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, CabinetNumber = CABINET_NUMBER });
                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, CabinetNumber = CABINET_NUMBER });
                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_WITHOUT_ITEM_ID, CabinetNumber = CABINET_NUMBER });

                context.Item().Add(new Item { ID = ITEM_TYPE_FULL_ID, ItemTypeID = ITEM_TYPE_FULL_ID, Description = "ITEM_TYPE_FULL_ID" });
                context.Item().Add(new Item { ID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, Description = "ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID" });
                context.Item().Add(new Item { ID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, Description = "ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID" });

                context.CTAMUser().Add(new CTAMUser { UID = USER_FULL_UID, Name = "USER_FULL_UID", Email = "e@mail.com", LanguageCode = "nl-NL" });

                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_FULL_UID, ItemID = ITEM_TYPE_FULL_ID, Status = UserInPossessionStatus.Picked });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_FULL_UID, ItemID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, Status = UserInPossessionStatus.Picked });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_FULL_UID, CTAMUserUIDIn = USER_FULL_UID, ItemID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, Status = UserInPossessionStatus.Returned });

                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { ID = ITEM_TYPE_FULL_ID, ItemID = ITEM_TYPE_FULL_ID, CTAMUserUID = ""});

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_FULL_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_FULL_ID, CTAMRoleID = 2 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, CTAMRoleID = 2 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, CTAMRoleID = 2 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_ITEM_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_ITEM_ID, CTAMRoleID = 2 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, CTAMRoleID = 2 });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet{ CabinetNumber = CABINET_NUMBER, CTAMRoleID = 1});
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet{ CabinetNumber = CABINET_NUMBER, CTAMRoleID = 2});

                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_FULL_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_FULL_UID, CTAMRoleID = 2 });
                context.SaveChanges();
            }
        }

        public static readonly object[][] ItemTypeCorrectData =
        {
            new object[] { ITEM_TYPE_FULL_ID, true, true, 1, 1, 0, 2, 2 },
            new object[] { ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, true, true, 1, 1, 0, 2, 0 },
            new object[] { ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, true, true, 1, 0, 0, 2, 0 },
            new object[] { ITEM_TYPE_WITHOUT_ITEM_ID, true, true, 0, 0, 0, 2, 0 },
        };

        [Theory, MemberData(nameof(ItemTypeCorrectData))]
        public async Task TestGetItemTypeLiveSyncEnvelope(int itemTypeID, bool itemTypeFound, bool cabinetStockFound, int itemsCount, 
                                                          int userInPossessionsCount, int userPersonalItemsCount, int rolesCount, int itemTypeErrorsCount )
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { ItemTypeIDsForEnvelope = { itemTypeID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<ItemCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(itemTypeFound, envelope.ItemTypes.Exists(it => it.ID == itemTypeID));
                Assert.Equal(cabinetStockFound, envelope.CabinetStocks.Exists(cs => cs.ItemTypeID == itemTypeID && cs.CabinetNumber == CABINET_NUMBER));
                Assert.Equal(itemsCount, envelope.Items.Count);
                Assert.Equal(userInPossessionsCount, envelope.UserInPossessions.Count);
                Assert.Equal(userPersonalItemsCount, envelope.UserPersonalItems.Count);
                Assert.Equal(rolesCount, envelope.RoleItemTypes.Count);
                Assert.Equal(itemTypeErrorsCount, envelope.ErrorCodes.Count);
                Assert.Equal(itemTypeErrorsCount, envelope.ItemTypeErrorCodes.Count);
            }
        }

        public static readonly object[][] ItemTypeWrongData =
        {
            new object[] { ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, true, false, true },
            new object[] { ITEM_TYPE_WITHOUT_ROLES_ID, true, false, false },
            new object[] { NON_EXISTING_ITEM_TYPE_ID, false, false, false },
        };

        [Theory, MemberData(nameof(ItemTypeWrongData))]
        public async Task TestGetWrongItemTypeLiveSyncEnvelope(int itemTypeID, bool itemTypeFound, bool cabinetStockFound, bool rolesFound)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { ItemTypeIDsForEnvelope = { itemTypeID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<ItemCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(itemTypeFound, envelope.ItemTypes.Exists(it => it.ID == itemTypeID));
                Assert.Equal(cabinetStockFound, envelope.CabinetStocks.Exists(cs => cs.ItemTypeID == itemTypeID && cs.CabinetNumber == CABINET_NUMBER));
                Assert.Equal(rolesFound, envelope.RoleItemTypes.Count > 0);
            }
        }


        public static readonly object[][] RoleItemTypeCorrectData =
        {
            new object[] { 1, ITEM_TYPE_FULL_ID, true, true, 1, 1, 0, true },
            new object[] { 1, ITEM_TYPE_WITH_PARTIAL_ITEM_PICKED_ID, true, true, 1, 1, 0, true },
            new object[] { 1, ITEM_TYPE_WITH_PARTIAL_ITEM_RETURNED_ID, true, true, 1, 0, 0, true },
            new object[] { 1, ITEM_TYPE_WITHOUT_ITEM_ID, true, true, 0, 0, 0, true },
        };

        [Theory, MemberData(nameof(RoleItemTypeCorrectData))]
        public async Task TestGetRoleItemTypeLiveSyncEnvelope(int roleID, int itemTypeID, bool itemTypeFound, bool cabinetStockFound, int itemsCount, int userInPossessionsCount, int userPersonalItemsCount, bool roleFound)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { ItemTypeIDsForEnvelope = { itemTypeID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<ItemCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(itemTypeFound, envelope.ItemTypes.Any(it => it.ID == itemTypeID));
                Assert.Equal(cabinetStockFound, envelope.CabinetStocks.Any(cs => cs.ItemTypeID == itemTypeID));
                Assert.Equal(itemsCount, envelope.Items.Count);
                Assert.Equal(userInPossessionsCount, envelope.UserInPossessions.Count);
                Assert.Equal(userPersonalItemsCount, envelope.UserPersonalItems.Count);
                Assert.Equal(roleFound, envelope.RoleItemTypes.Any(rit => rit.CTAMRoleID == roleID && rit.ItemTypeID == itemTypeID));
            }
        }

        public static readonly object[][] RoleItemTypeWrongData =
        {
            new object[] { 1, ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, true, false },
            new object[] { 1, ITEM_TYPE_WITHOUT_ROLES_ID, true, false },
            new object[] { 1, NON_EXISTING_ITEM_TYPE_ID, false, false },
        };

        [Theory, MemberData(nameof(RoleItemTypeWrongData))]
        public async Task TestGetWrongRoleItemTypeLiveSyncEnvelope(int roleID, int itemTypeID, bool itemTypeFound, bool cabinetStockFound)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { ItemTypeIDsForEnvelope = { itemTypeID }, RoleIDs = { roleID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<ItemCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
                c.AddProfile<UserRoleLiveSyncDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(itemTypeFound, envelope.ItemTypes.Exists(it => it.ID == itemTypeID));
                Assert.Equal(cabinetStockFound, envelope.CabinetStocks.Exists(cs => cs.ItemTypeID == itemTypeID && cs.CabinetNumber == CABINET_NUMBER));
            }
        }
    }
}
