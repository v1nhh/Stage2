using AutoMapper;
using CabinetModule.ApplicationCore.Commands.Sync;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Profiles;
using CloudAPI.ApplicationCore.Profiles;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Commands.Sync;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Profiles;
using ItemModule.ApplicationCore.Commands.Sync;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using Xunit;

namespace CloudAPI.Tests.CloudAPI.IntegrationTests.LiveSync
{
    public class SyncCriticalDataTest : AbstractIntegrationTests
    {
        private static readonly Cabinet NieuwVennep = new() { CabinetNumber = "210309081254", Name = "IBK Nieuw Vennep" };
        private static readonly Cabinet Breda = new() { CabinetNumber = "201515205156", Name = "Combilocker Breda" };

        private static readonly CTAMUser Gijs = new() { UID = "gijs_123", Name = "Gijs", Email = "gijs@nautaconnect.com" };
        private static readonly CTAMUser Sam = new() { UID = "sam_123", Name = "Sam", Email = "sam@nautaconnect.com" };
        private static readonly CTAMUser Agent5 = new() { UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", Name = "Agent005", Email = "agent005@politie.nl" };
        private static readonly CTAMUser Tlb = new() { UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", Name = "Tlb001", Email = "beheer001@politie.nl" };

        private static readonly Guid GUID_1 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb00");
        private static readonly Guid GUID_2 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc01");
        private static readonly Guid GUID_3 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb02");
        private static readonly Guid GUID_4 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc03");
        private static readonly Guid GUID_5 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb04");
        private static readonly Guid GUID_6 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc05");
        private static readonly Guid GUID_7 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb06");
        private static readonly Guid GUID_8 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc07");
        private static readonly Guid GUID_9 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb08");
        private static readonly Guid GUID_10 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc09");
        private static readonly Guid GUID_11 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb10");

        private static readonly Guid GUID_12 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb11");
        private static readonly Guid GUID_13 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc12");
        private static readonly Guid GUID_14 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb13");
        private static readonly Guid GUID_15 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc14");
        private static readonly Guid GUID_16 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb15");
        private static readonly Guid GUID_17 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc16");
        private static readonly Guid GUID_18 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb17");
        private static readonly Guid GUID_19 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc18");
        private static readonly Guid GUID_20 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb19");
        private static readonly Guid GUID_21 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc20");
        private static readonly Guid GUID_22 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb21");
        private static readonly Guid GUID_23 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fc22");
        private static readonly Guid GUID_24 = Guid.Parse("f0c23628-d2af-4281-9bc8-3dc0e863fb23");

        public SyncCriticalDataTest() : base("CTAM_SyncCriticalData")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_1, ItemID = 4, OutDT = new DateTime(2023, 1, 1), CabinetPositionIDOut = 1, InDT = new DateTime(2023, 1, 6), CabinetPositionIDIn = 2, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_4, ItemID = 4, OutDT = new DateTime(2023, 1, 7), CabinetPositionIDOut = 2, InDT = new DateTime(2023, 1, 8), CabinetPositionIDIn = 2, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Gijs.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_5, ItemID = 4, OutDT = new DateTime(2023, 1, 9), CabinetPositionIDOut = 2, InDT = new DateTime(2023, 1, 14), CabinetPositionIDIn = 3, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_8, ItemID = 4, OutDT = new DateTime(2023, 1, 15), CabinetPositionIDOut = 3, InDT = new DateTime(2023, 1, 16), CabinetPositionIDIn = 3, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Gijs.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_9, ItemID = 4, OutDT = new DateTime(2023, 1, 17), CabinetPositionIDOut = 3, InDT = new DateTime(2023, 1, 22), CabinetPositionIDIn = 4, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });

                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_12, ItemID = 5, OutDT = new DateTime(2023, 1, 1), CabinetPositionIDOut = 1, InDT = new DateTime(2023, 1, 2), CabinetPositionIDIn = 2, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_13, ItemID = 5, OutDT = new DateTime(2023, 1, 3), CabinetPositionIDOut = 2, InDT = new DateTime(2023, 1, 4), CabinetPositionIDIn = 2, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Gijs.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_14, ItemID = 5, OutDT = new DateTime(2023, 1, 5), CabinetPositionIDOut = 2, InDT = new DateTime(2023, 1, 10), CabinetPositionIDIn = 3, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_17, ItemID = 5, OutDT = new DateTime(2023, 1, 11), CabinetPositionIDOut = 3, InDT = new DateTime(2023, 1, 12), CabinetPositionIDIn = 3, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Gijs.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_18, ItemID = 5, OutDT = new DateTime(2023, 1, 13), CabinetPositionIDOut = 3, InDT = new DateTime(2023, 1, 18), CabinetPositionIDIn = 4, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_21, ItemID = 5, OutDT = new DateTime(2023, 1, 19), CabinetPositionIDOut = 4, InDT = new DateTime(2023, 1, 20), CabinetPositionIDIn = 4, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Gijs.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = GUID_22, ItemID = 5, OutDT = new DateTime(2023, 1, 21), CabinetPositionIDOut = 4, InDT = new DateTime(2023, 1, 26), CabinetPositionIDIn = 5, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDOut = Sam.UID, CabinetNumberIn = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Sam.UID, Status = UserInPossessionStatus.Returned });
                context.SaveChanges();
            }
        }

        public static readonly object[][] CabinetPositionData =
        {
            new object[] { new CabinetPositionDTO { ID = 1, Status = CabinetPositionStatus.ReadError}, CabinetPositionStatus.ReadError },
            new object[] { new CabinetPositionDTO { ID = 1, Status = CabinetPositionStatus.Disabled }, CabinetPositionStatus.Disabled },
        };

        [Theory, MemberData(nameof(CabinetPositionData))]
        public async Task TestSyncCriticalCabinetDataCabinetPositions(CabinetPositionDTO cabinetPositionDTO, CabinetPositionStatus cabinetPositionStatus)
        {
            //  Arrange command
            var syncCriticalCabinetDataCommand = new SyncCriticalCabinetDataCommand(new List<CabinetPositionDTO>() { cabinetPositionDTO }, new List<CabinetDoorDTO> { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<CabinetPositionProfile>();
                }).CreateMapper();

                SyncCriticalCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var cabinetPosition = await context.CabinetPosition()
                    .Where(cp => cp.ID.Equals(cabinetPositionDTO.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(cabinetPosition);
                Assert.Equal(cabinetPositionStatus, cabinetPosition.Status);
            }
        }

        public static readonly object[][] CabinetDoorData =
        {
            new object[] { new CabinetDoorDTO { ID = 1, Status = CabinetDoorStatus.Defect}, CabinetDoorStatus.Defect },
            new object[] { new CabinetDoorDTO { ID = 1, Status = CabinetDoorStatus.Disabled }, CabinetDoorStatus.Disabled },
            new object[] { new CabinetDoorDTO { ID = 1, Status = CabinetDoorStatus.OK }, CabinetDoorStatus.OK },
            new object[] { new CabinetDoorDTO { ID = 1, Status = CabinetDoorStatus.ReadError }, CabinetDoorStatus.ReadError },
        };

        [Theory, MemberData(nameof(CabinetDoorData))]
        public async Task TestSyncCriticalCabinetDataCabinetDoors(CabinetDoorDTO doorDTO, CabinetDoorStatus status)
        {
            //  Arrange command
            var syncCriticalCabinetDataCommand = new SyncCriticalCabinetDataCommand(new List<CabinetPositionDTO>() {}, new List<CabinetDoorDTO> { doorDTO });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<CabinetDoorProfile>();
                }).CreateMapper();

                SyncCriticalCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var cabinetDoor = await context.CabinetDoor()
                    .Where(cd => cd.ID.Equals(doorDTO.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(cabinetDoor);
                Assert.Equal(status, cabinetDoor.Status);
            }
        }

        public static readonly object[][] ItemData =
        {
            new object[] { new ItemDTO { ID = 25, ErrorCodeID = 1, Status = ItemStatus.DEFECT, UpdateDT = DateTime.UtcNow.AddDays(1) }, 1, ItemStatus.DEFECT },
            new object[] { new ItemDTO { ID = 26, ErrorCodeID = 9, Status = ItemStatus.IN_REPAIR, UpdateDT = DateTime.UtcNow.AddDays(1) }, 9, ItemStatus.IN_REPAIR },
            new object[] { new ItemDTO { ID = 33, ErrorCodeID = null, Status = ItemStatus.ACTIVE, UpdateDT = DateTime.UtcNow.AddDays(1) }, null, ItemStatus.ACTIVE },
            new object[] { new ItemDTO { ID = 26, ErrorCodeID = null, Status = ItemStatus.ACTIVE, UpdateDT = DateTime.UtcNow.AddDays(-1) }, 9, ItemStatus.DEFECT },
        };

        [Theory, MemberData(nameof(ItemData))]
        public async Task TestSyncCriticalItemDataItems(ItemDTO itemDTO, int? errorCodeID, ItemStatus itemStatus)
        {
            //  Arrange command
            var syncCriticalItemDataCommand = new SyncCriticalItemDataCommand(new List<ItemDTO>() { itemDTO });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                SyncCriticalItemDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var item = await context.Item()
                    .Where(i => i.ID.Equals(itemDTO.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(item);
                Assert.Equal(errorCodeID, item.ErrorCodeID);
                Assert.Equal(itemStatus, item.Status);
            }
        }

        public static readonly object[][] CabinetPositionContentData =
        {
            new object[] { new CabinetPositionContentDTO { CabinetPositionID = 1, ItemID = 25 }, 1 },
            new object[] { new CabinetPositionContentDTO { CabinetPositionID = 5, ItemID = 26 }, 1 },
        };

        [Theory, MemberData(nameof(CabinetPositionContentData))]
        public async Task TestSyncCriticalItemCabinetDataCabinetPositionContents(CabinetPositionContentDTO cabinetPositionContentDTO, int amountOfRecords)
        {
            //  Arrange command
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { cabinetPositionContentDTO },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<CabinetPositionContentProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var count = context.CabinetPositionContent()
                    .Where(cpc => cpc.CabinetPositionID.Equals(cabinetPositionContentDTO.CabinetPositionID) && cpc.ItemID.Equals(cabinetPositionContentDTO.ItemID))
                    .Count();
                Assert.Equal(amountOfRecords, count);

                // Assert
                var cabinetPositionContent = await context.CabinetPositionContent()
                    .Where(cpc => cpc.CabinetPositionID.Equals(cabinetPositionContentDTO.CabinetPositionID) && cpc.ItemID.Equals(cabinetPositionContentDTO.ItemID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(cabinetPositionContent);
                Assert.Equal(cabinetPositionContentDTO.CabinetPositionID, cabinetPositionContent.CabinetPositionID);
                Assert.Equal(cabinetPositionContentDTO.ItemID, cabinetPositionContent.ItemID);
            }
        }

        private readonly CabinetPositionContentDTO REMOVED_CPC = new() 
        { 
            CabinetPositionID = 5, 
            ItemID = 26 
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataRemoveCPC()
        {
            //  Arrange command
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { REMOVED_CPC },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<CabinetPositionContentProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());
                
                // Assert
                var result = await context.CabinetPositionContent()
                    .Where(cpc => cpc.CabinetPositionID.Equals(REMOVED_CPC.CabinetPositionID) && cpc.ItemID.Equals(REMOVED_CPC.ItemID))
                    .SingleOrDefaultAsync();
                Assert.Null(result);
            }
        }

        public static readonly object[][] UserPersonalItemData =
        {
            new object[] { new UserPersonalItemDTO { ID = 1, ItemID = 25, CabinetNumber = NieuwVennep.CabinetNumber, ReplacementItemID = 4, Status = UserPersonalItemStatus.Defect, UpdateDT = DateTime.UtcNow.AddDays(1) }, NieuwVennep.CabinetNumber, 25, 4, UserPersonalItemStatus.Defect },
            new object[] { new UserPersonalItemDTO { ID = 6, ItemID = 33, CabinetNumber = NieuwVennep.CabinetNumber, ReplacementItemID = 34, Status = UserPersonalItemStatus.Defect, UpdateDT = DateTime.UtcNow.AddDays(1) }, NieuwVennep.CabinetNumber, 33, 34, UserPersonalItemStatus.Defect },
            new object[] { new UserPersonalItemDTO { ID = 6, ItemID = 33, CabinetNumber = NieuwVennep.CabinetNumber, ReplacementItemID = 34, Status = UserPersonalItemStatus.Repaired, UpdateDT = DateTime.UtcNow.AddDays(1) }, NieuwVennep.CabinetNumber, 33, 34, UserPersonalItemStatus.Repaired },
            new object[] { new UserPersonalItemDTO { ID = 6, ItemID = 35, CabinetNumber = NieuwVennep.CabinetNumber, ReplacementItemID = 34, Status = UserPersonalItemStatus.Replaced, UpdateDT = DateTime.UtcNow.AddDays(1) }, NieuwVennep.CabinetNumber, 35, 34, UserPersonalItemStatus.Replaced },
            new object[] { new UserPersonalItemDTO { ID = 6, ItemID = 33, CabinetNumber = null, ReplacementItemID = null, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow.AddDays(1) }, null, 33, null, UserPersonalItemStatus.OK },
            new object[] { new UserPersonalItemDTO { ID = 3, ItemID = 26, CabinetNumber = null, ReplacementItemID = null, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow.AddDays(-1) }, NieuwVennep.CabinetNumber, 26, 28, UserPersonalItemStatus.Defect }
        };

        [Theory, MemberData(nameof(UserPersonalItemData))]
        public async Task TestSyncCriticalItemCabinetDataPersonalItems(UserPersonalItemDTO userPersonalItemDTO, string cabinetNumber,
            int itemID, int? replacementItemID, UserPersonalItemStatus userPersonalItemStatus)
        {
            //  Arrange command
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { },
                                                                                            new List<UserPersonalItemDTO>() { userPersonalItemDTO });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserPersonalItemProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var ctamUserPersonalItem = await context.CTAMUserPersonalItem()
                    .Where(upi => upi.ID.Equals(userPersonalItemDTO.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(ctamUserPersonalItem);
                Assert.Equal(cabinetNumber, ctamUserPersonalItem.CabinetNumber);
                Assert.Equal(itemID, ctamUserPersonalItem.ItemID);
                Assert.Equal(replacementItemID, ctamUserPersonalItem.ReplacementItemID);
                Assert.Equal(userPersonalItemStatus, ctamUserPersonalItem.Status);
            }
        }

        private readonly UserInPossessionDTO UIP_SWAP = new()
        {
            // PPortofoon 5 is picked door Agent005
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc05"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            OutDT = DateTime.UtcNow,
            CabinetNumberOut = NieuwVennep.CabinetNumber,
            CTAMUserUIDIn = Agent5.UID,
            InDT = DateTime.UtcNow.AddDays(1),
            CabinetNumberIn = NieuwVennep.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPSwap()
        {
            //  Arrange command
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_SWAP },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());
                
                // Assert
                var result = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(result);
                Assert.Equal(Agent5.UID, result.CTAMUserUIDIn);
                Assert.NotNull(result.InDT);
                Assert.Equal(NieuwVennep.CabinetNumber, result.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, result.Status);

                var count = context.CTAMUserInPossession()
                    .Where(upi => upi.ItemID.Equals(UIP_SWAP.ItemID) && upi.Status.Equals(UserInPossessionStatus.Picked))
                    .Count();
                Assert.Equal(0, count);
            }
        }

        private readonly UserInPossessionDTO UIP_REPAIR = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"),
            ItemID = 25,
            CTAMUserUIDOut = Gijs.UID,
            OutDT = DateTime.UtcNow.AddDays(2),
            CabinetNumberOut = NieuwVennep.CabinetNumber,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPRepair()
        {
            //  Arrange command swap
            var syncCriticalItemCabinetDataCommandSwap = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_SWAP },
                                                                                            new List<UserPersonalItemDTO>() { });
            //  Arrange command repair
            var syncCriticalItemCabinetDataCommandRepair = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_REPAIR },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handleSwap = await handler.Handle(syncCriticalItemCabinetDataCommandSwap, It.IsAny<CancellationToken>());
                var handleRepair = await handler.Handle(syncCriticalItemCabinetDataCommandRepair, It.IsAny<CancellationToken>());

                // Assert
                var result = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_REPAIR.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(result);
                Assert.Equal(25, result.ItemID);
                Assert.Equal(Gijs.UID, result.CTAMUserUIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, result.CabinetNumberOut);

                Assert.Null(result.CTAMUserUIDIn);
                Assert.Null(result.InDT);
                Assert.Null(result.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Picked, result.Status);

                var count = context.CTAMUserInPossession()
                    .Where(upi => upi.ItemID.Equals(UIP_REPAIR.ItemID) && upi.Status.Equals(UserInPossessionStatus.Picked))
                    .Count();
                Assert.Equal(1, count);
            }
        }

        public static readonly object[][] UserInPossessionRepairedData =
        {
            new object[] { new UserInPossessionDTO { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"), ItemID = 25, CTAMUserUIDOut = Gijs.UID, OutDT = DateTime.UtcNow, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, InDT = DateTime.UtcNow.AddDays(1), CabinetNumberIn = NieuwVennep.CabinetNumber, Status = UserInPossessionStatus.Returned }, Gijs, Gijs, NieuwVennep, NieuwVennep, UserInPossessionStatus.Returned },
            new object[] { new UserInPossessionDTO { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"), ItemID = 25, CTAMUserUIDOut = Gijs.UID, OutDT = DateTime.UtcNow, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Tlb.UID, InDT = DateTime.UtcNow.AddDays(1), CabinetNumberIn = NieuwVennep.CabinetNumber, Status = UserInPossessionStatus.Returned }, Gijs, Tlb, NieuwVennep, NieuwVennep, UserInPossessionStatus.Returned },
            new object[] { new UserInPossessionDTO { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"), ItemID = 25, CTAMUserUIDOut = Gijs.UID, OutDT = DateTime.UtcNow, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Gijs.UID, InDT = DateTime.UtcNow.AddDays(1), CabinetNumberIn = Breda.CabinetNumber, Status = UserInPossessionStatus.Returned }, Gijs, Gijs, NieuwVennep, Breda, UserInPossessionStatus.Returned },
            new object[] { new UserInPossessionDTO { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"), ItemID = 25, CTAMUserUIDOut = Gijs.UID, OutDT = DateTime.UtcNow, CabinetNumberOut = NieuwVennep.CabinetNumber, CTAMUserUIDIn = Tlb.UID, InDT = DateTime.UtcNow.AddDays(1), CabinetNumberIn = Breda.CabinetNumber, Status = UserInPossessionStatus.Returned }, Gijs, Tlb, NieuwVennep, Breda, UserInPossessionStatus.Returned }
        };

        [Theory, MemberData(nameof(UserInPossessionRepairedData))]
        public async Task TestSyncCriticalItemCabinetDataUIPRepaireds(UserInPossessionDTO userInPossessionDTO, 
            CTAMUser userOut, CTAMUser userIn, Cabinet cabinetOut, Cabinet cabinetIn, UserInPossessionStatus userInPossessionStatusResult)
        {
            //  Arrange command swap
            var syncCriticalItemCabinetDataCommandSwap = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_SWAP },
                                                                                            new List<UserPersonalItemDTO>() { });
            //  Arrange command repair
            var syncCriticalItemCabinetDataCommandRepair = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_REPAIR },
                                                                                            new List<UserPersonalItemDTO>() { });
            //  Arrange command repaired
            var syncCriticalItemCabinetDataCommandRepaired = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { userInPossessionDTO },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handleSwap = await handler.Handle(syncCriticalItemCabinetDataCommandSwap, It.IsAny<CancellationToken>());
                var handleRepair = await handler.Handle(syncCriticalItemCabinetDataCommandRepair, It.IsAny<CancellationToken>());
                var handleRepaired = await handler.Handle(syncCriticalItemCabinetDataCommandRepaired, It.IsAny<CancellationToken>());
                
                // Assert
                var result = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(userInPossessionDTO.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(result);
                Assert.Equal(userInPossessionDTO.ItemID, result.ItemID);
                Assert.Equal(userOut.UID, result.CTAMUserUIDOut);
                Assert.Equal(cabinetOut.CabinetNumber, result.CabinetNumberOut);

                Assert.Equal(userIn.UID, result.CTAMUserUIDIn);
                Assert.Equal(cabinetIn.CabinetNumber, result.CabinetNumberIn);
                Assert.Equal(userInPossessionStatusResult, result.Status);

                var count = context.CTAMUserInPossession()
                    .Where(upi => upi.ItemID.Equals(userInPossessionDTO.ItemID) && upi.Status.Equals(UserInPossessionStatus.Picked))
                    .Count();
                Assert.Equal(0, count);
            }
        }

        private readonly UserInPossessionDTO UIP_SWAP_BACK = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            OutDT = DateTime.UtcNow,
            CabinetNumberOut = NieuwVennep.CabinetNumber,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPSwapBack()
        {
            //  Arrange command swap
            var syncCriticalItemCabinetDataCommandSwap = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_SWAP },
                                                                                            new List<UserPersonalItemDTO>() { });
            //  Arrange command
            var syncCriticalItemCabinetDataCommandSwapback = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { UIP_SWAP_BACK },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handleSwap = await handler.Handle(syncCriticalItemCabinetDataCommandSwap, It.IsAny<CancellationToken>());
                var handleSwapBack = await handler.Handle(syncCriticalItemCabinetDataCommandSwapback, It.IsAny<CancellationToken>());
                
                // Assert
                var result = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(result);
                Assert.Equal(25, result.ItemID);
                Assert.Equal(Agent5.UID, result.CTAMUserUIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, result.CabinetNumberOut);

                Assert.Null(result.CTAMUserUIDIn);
                Assert.Null(result.InDT);
                Assert.Null(result.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Picked, result.Status);

                var count = context.CTAMUserInPossession()
                    .Where(upi => upi.ItemID.Equals(UIP_SWAP_BACK.ItemID) && upi.Status.Equals(UserInPossessionStatus.Picked))
                    .Count();
                Assert.Equal(1, count);
            }
        }

        // Swap in Combilocker Breda while offline and record is already closed with InDT later
        private readonly UserInPossessionDTO UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc15"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-6),
            CabinetPositionIDOut = 1,
            CabinetNumberOut = NieuwVennep.CabinetNumber,
            CabinetNameOut = NieuwVennep.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(-5),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Swap replacement 
        private readonly UserInPossessionDTO UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("03595e44-7be0-4b38-a976-4fdd0a5c1184"),
            ItemID = 45,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-5),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(-2),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Repair and Repaired in Combilocker Breda while offline
        private readonly UserInPossessionDTO UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"),
            ItemID = 25,
            CTAMUserUIDOut = Gijs.UID,
            CTAMUserNameOut = Gijs.Name,
            CTAMUserEmailOut = Gijs.Email,
            OutDT = DateTime.UtcNow.AddDays(-4),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Gijs.UID,
            CTAMUserNameIn = Gijs.Name,
            CTAMUserEmailIn = Gijs.Email,
            InDT = DateTime.UtcNow.AddDays(-3),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // SwapBack in Combilocker Breda while offline
        private readonly UserInPossessionDTO UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc17"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2).AddHours(-6),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPOfflineToOnlineAfterFullSwapRoutineThatIsEarlier()
        {
            //  Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() 
                                                                                            {
                                                                                                UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION, 
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION, 
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION, 
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION 
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var originalRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(originalRecord);
                Assert.Equal(25, originalRecord.ItemID);
                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailOut);
                Assert.Equal(1, originalRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailIn);
                Assert.Equal(44, originalRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, originalRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, originalRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, originalRecord.Status);

                // Assert 2
                var updatedRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(updatedRecord);
                Assert.Equal(25, updatedRecord.ItemID);
                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailOut);
                Assert.Equal(46, updatedRecord.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, updatedRecord.CabinetNumberOut);
                Assert.Equal(Breda.Name, updatedRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailIn);
                Assert.Equal(1, updatedRecord.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, updatedRecord.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, updatedRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, updatedRecord.Status);
            }
        }

        // SwapBack in Combilocker Breda while offline and while offline a new swap is done
        private readonly UserInPossessionDTO UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION_AND_SWAP = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc17"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2).AddHours(-6),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(-2).AddHours(-5),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Swap replacement 
        private readonly UserInPossessionDTO SECOND_UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("03595e44-7be0-4b38-a976-4fdd0a5c1185"),
            ItemID = 45,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2).AddHours(-5),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(-2).AddHours(-2),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Repair and Repaired in Combilocker Breda while offline
        private readonly UserInPossessionDTO SECOND_UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc18"),
            ItemID = 25,
            CTAMUserUIDOut = Gijs.UID,
            CTAMUserNameOut = Gijs.Name,
            CTAMUserEmailOut = Gijs.Email,
            OutDT = DateTime.UtcNow.AddDays(-2).AddHours(-4),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Gijs.UID,
            CTAMUserNameIn = Gijs.Name,
            CTAMUserEmailIn = Gijs.Email,
            InDT = DateTime.UtcNow.AddDays(-2).AddHours(-3),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // SwapBack in Combilocker Breda while offline for the second full routine offline
        private readonly UserInPossessionDTO SECOND_UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc19"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2).AddHours(-2),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPOfflineToOnlineAfterMultipleFullSwapRoutinesThatAreEarlier()
        {
            // Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>()
                                                                                            {
                                                                                                UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION_AND_SWAP,
                                                                                                SECOND_UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                SECOND_UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                SECOND_UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var originalRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(originalRecord);
                Assert.Equal(25, originalRecord.ItemID);
                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailOut);
                Assert.Equal(1, originalRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailIn);
                Assert.Equal(44, originalRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, originalRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, originalRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, originalRecord.Status);

                // Assert 2
                var swapBackRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION_AND_SWAP.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackRecord);
                Assert.Equal(25, swapBackRecord.ItemID);
                Assert.Equal(Agent5.UID, swapBackRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, swapBackRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, swapBackRecord.CTAMUserEmailOut);
                Assert.Equal(46, swapBackRecord.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackRecord.CabinetNumberOut);
                Assert.Equal(Breda.Name, swapBackRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, swapBackRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, swapBackRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, swapBackRecord.CTAMUserEmailIn);
                Assert.Equal(44, swapBackRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, swapBackRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackRecord.Status);

                // Assert 3
                var updatedRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(SECOND_UIP_SWAP_BACK_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(updatedRecord);
                Assert.Equal(25, updatedRecord.ItemID);
                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailOut);
                Assert.Equal(46, updatedRecord.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, updatedRecord.CabinetNumberOut);
                Assert.Equal(Breda.Name, updatedRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailIn);
                Assert.Equal(1, updatedRecord.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, updatedRecord.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, updatedRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, updatedRecord.Status);
            }
        }

        // SwapBack in Combilocker Breda while offline and while offline a new swap is done that is later then known action
        private readonly UserInPossessionDTO UIP_SWAP_BACK_FROM_OFFLINE_CABINET_CLOSED_EARLIER_THEN_KNOWN_ACTION_AND_SWAP_LATER = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc17"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(1),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPOfflineToOnlineAfterMultipleFullSwapRoutinesOneEarlierAndOneLater()
        {
            // Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>()
                                                                                            {
                                                                                                UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION,
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_CLOSED_EARLIER_THEN_KNOWN_ACTION_AND_SWAP_LATER,
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION,
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var originalRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_FROM_OFFLINE_CABINET_EARLIER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(originalRecord);
                Assert.Equal(25, originalRecord.ItemID);
                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailOut);
                Assert.Equal(1, originalRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailIn);
                Assert.Equal(44, originalRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, originalRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, originalRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, originalRecord.Status);

                // Assert 2
                var swapBackRecordWithNewSwap = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FROM_OFFLINE_CABINET_CLOSED_EARLIER_THEN_KNOWN_ACTION_AND_SWAP_LATER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackRecordWithNewSwap);
                Assert.Equal(25, swapBackRecordWithNewSwap.ItemID);
                Assert.Equal(Agent5.UID, swapBackRecordWithNewSwap.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, swapBackRecordWithNewSwap.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, swapBackRecordWithNewSwap.CTAMUserEmailOut);
                Assert.Equal(46, swapBackRecordWithNewSwap.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackRecordWithNewSwap.CabinetNumberOut);
                Assert.Equal(Breda.Name, swapBackRecordWithNewSwap.CabinetNameOut);

                Assert.Equal(Agent5.UID, swapBackRecordWithNewSwap.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, swapBackRecordWithNewSwap.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, swapBackRecordWithNewSwap.CTAMUserEmailIn);
                Assert.Equal(1, swapBackRecordWithNewSwap.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackRecordWithNewSwap.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, swapBackRecordWithNewSwap.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackRecordWithNewSwap.Status);

                // Assert 3
                var updatedRecordWithLaterSwap = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc05")))
                    .SingleOrDefaultAsync();
                Assert.NotNull(updatedRecordWithLaterSwap);
                Assert.Equal(25, updatedRecordWithLaterSwap.ItemID);
                Assert.Equal(Agent5.UID, updatedRecordWithLaterSwap.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, updatedRecordWithLaterSwap.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, updatedRecordWithLaterSwap.CTAMUserEmailOut);
                Assert.Equal(1, updatedRecordWithLaterSwap.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, updatedRecordWithLaterSwap.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, updatedRecordWithLaterSwap.CabinetNameOut);

                Assert.Equal(Agent5.UID, updatedRecordWithLaterSwap.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, updatedRecordWithLaterSwap.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, updatedRecordWithLaterSwap.CTAMUserEmailIn);
                Assert.Equal(44, updatedRecordWithLaterSwap.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, updatedRecordWithLaterSwap.CabinetNumberIn);
                Assert.Equal(Breda.Name, updatedRecordWithLaterSwap.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, updatedRecordWithLaterSwap.Status);

                // Assert 4
                var swapBackRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackRecord);
                Assert.Equal(25, swapBackRecord.ItemID);
                Assert.Equal(Agent5.UID, swapBackRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, swapBackRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, swapBackRecord.CTAMUserEmailOut);
                Assert.Equal(46, swapBackRecord.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackRecord.CabinetNumberOut);
                Assert.Equal(Breda.Name, swapBackRecord.CabinetNameOut);

                Assert.Null(swapBackRecord.CTAMUserUIDIn);
                Assert.Null(swapBackRecord.CTAMUserNameIn);
                Assert.Null(swapBackRecord.CTAMUserEmailIn);
                Assert.Null(swapBackRecord.CabinetPositionIDIn);
                Assert.Null(swapBackRecord.CabinetNumberIn);
                Assert.Null(swapBackRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Picked, swapBackRecord.Status);
            }
        }

        // Swap in Combilocker Breda while offline and record is already closed with InDT earlier
        private readonly UserInPossessionDTO UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc15"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-6),
            CabinetPositionIDOut = 1,
            CabinetNumberOut = NieuwVennep.CabinetNumber,
            CabinetNameOut = NieuwVennep.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(1),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Swap replacement 
        private readonly UserInPossessionDTO UIP_REPLACEMENT_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("03595e44-7be0-4b38-a976-4fdd0a5c1155"),
            ItemID = 45,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(1),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(4),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // Repair and Repaired in Combilocker Breda while offline
        private readonly UserInPossessionDTO UIP_REPAIRED_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f822222"),
            ItemID = 25,
            CTAMUserUIDOut = Gijs.UID,
            CTAMUserNameOut = Gijs.Name,
            CTAMUserEmailOut = Gijs.Email,
            OutDT = DateTime.UtcNow.AddDays(2),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,

            CTAMUserUIDIn = Gijs.UID,
            CTAMUserNameIn = Gijs.Name,
            CTAMUserEmailIn = Gijs.Email,
            InDT = DateTime.UtcNow.AddDays(3),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // SwapBack in Combilocker Breda while offline
        private readonly UserInPossessionDTO UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc20"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(4),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPOfflineToOnlineAfterFullSwapRoutineThatIsLater()
        {
            // Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() 
                                                                                            { 
                                                                                                UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION, 
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION, 
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION,
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION 
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var originalRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(originalRecord);
                Assert.Equal(25, originalRecord.ItemID);
                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailOut);
                Assert.Equal(1, originalRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailIn);
                Assert.Equal(1, originalRecord.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, originalRecord.Status);

                // Assert 2
                var updatedRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc05")))
                    .SingleOrDefaultAsync();
                Assert.NotNull(updatedRecord);
                Assert.Equal(25, updatedRecord.ItemID);
                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailOut);
                Assert.Equal(1, updatedRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, updatedRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, updatedRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailIn);
                Assert.Equal(44, updatedRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, updatedRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, updatedRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, updatedRecord.Status);
            }
        }

        // Swap in Combilocker Breda while offline and record is already closed with InDT earlier
        private readonly UserInPossessionDTO UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc30"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-12),
            CabinetNameOut = "Initiele import",

            CTAMUserUIDIn = Agent5.UID,
            CTAMUserNameIn = Agent5.Name,
            CTAMUserEmailIn = Agent5.Email,
            InDT = DateTime.UtcNow.AddDays(-5),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            CabinetNameIn = Breda.Name,
            Status = UserInPossessionStatus.Returned
        };

        // SwapBack in Combilocker Breda while offline
        private readonly UserInPossessionDTO UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN = new()
        {
            ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc17"),
            ItemID = 25,
            CTAMUserUIDOut = Agent5.UID,
            CTAMUserNameOut = Agent5.Name,
            CTAMUserEmailOut = Agent5.Email,
            OutDT = DateTime.UtcNow.AddDays(-2),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            CabinetNameOut = Breda.Name,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataUIPOfflineToOnlineAfterFullSwapRoutineThatIsLaterAndActuallyToUpdateRecordIsFilledIn()
        {
            //  Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>()
                                                                                            {
                                                                                                UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN,
                                                                                                UIP_REPLACEMENT_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION,
                                                                                                UIP_REPAIRED_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION,
                                                                                                UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var originalRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(originalRecord);
                Assert.Equal(25, originalRecord.ItemID);
                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailOut);
                Assert.Null(originalRecord.CabinetPositionIDOut);
                Assert.Null(originalRecord.CabinetNumberOut);
                Assert.Equal("Initiele import", originalRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, originalRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, originalRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, originalRecord.CTAMUserEmailIn);
                Assert.Equal(1, originalRecord.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, originalRecord.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, originalRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, originalRecord.Status);

                // Assert 2
                var updatedRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc15")))
                    .SingleOrDefaultAsync();
                Assert.NotNull(updatedRecord);
                Assert.Equal(25, updatedRecord.ItemID);
                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailOut);
                Assert.Equal(1, updatedRecord.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, updatedRecord.CabinetNumberOut);
                Assert.Equal(NieuwVennep.Name, updatedRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, updatedRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, updatedRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, updatedRecord.CTAMUserEmailIn);
                Assert.Equal(44, updatedRecord.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, updatedRecord.CabinetNumberIn);
                Assert.Equal(Breda.Name, updatedRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, updatedRecord.Status);

                // Assert 3
                var swapBackRecord = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FROM_OFFLINE_CABINET_LATER_THEN_KNOWN_ACTION_AND_NEXT_FILLED_IN.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackRecord);
                Assert.Equal(25, swapBackRecord.ItemID);
                Assert.Equal(Agent5.UID, swapBackRecord.CTAMUserUIDOut);
                Assert.Equal(Agent5.Name, swapBackRecord.CTAMUserNameOut);
                Assert.Equal(Agent5.Email, swapBackRecord.CTAMUserEmailOut);
                Assert.Equal(46, swapBackRecord.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackRecord.CabinetNumberOut);
                Assert.Equal(Breda.Name, swapBackRecord.CabinetNameOut);

                Assert.Equal(Agent5.UID, swapBackRecord.CTAMUserUIDIn);
                Assert.Equal(Agent5.Name, swapBackRecord.CTAMUserNameIn);
                Assert.Equal(Agent5.Email, swapBackRecord.CTAMUserEmailIn);
                Assert.Equal(1, swapBackRecord.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackRecord.CabinetNumberIn);
                Assert.Equal(NieuwVennep.Name, swapBackRecord.CabinetNameIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackRecord.Status);
            }
        }

        private readonly UserInPossessionDTO UIP_SWAP_BACK_ORIGINAL_EARLIER = new()
        {
            ID = GUID_1,
            ItemID = 4,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 1),
            CabinetPositionIDOut = 1,
            CabinetNumberOut = NieuwVennep.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 2),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_FIRST_EARLIER = new()
        {
            ID = GUID_2,
            ItemID = 4,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 3),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 4),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_FIRST_EARLIER = new()
        {
            ID = GUID_3,
            ItemID = 4,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 5),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 10),
            CabinetPositionIDIn = 45,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_SECOND_EARLIER = new()
        {
            ID = GUID_6,
            ItemID = 4,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 11),
            CabinetPositionIDOut = 45,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 12),
            CabinetPositionIDIn = 45,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_SECOND_EARLIER = new()
        {
            ID = GUID_7,
            ItemID = 4,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 13),
            CabinetPositionIDOut = 45,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 18),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_THIRD_EARLIER = new()
        {
            ID = GUID_10,
            ItemID = 4,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 19),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 20),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_THIRD_EARLIER = new()
        {
            ID = GUID_11,
            ItemID = 4,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 21),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataThreeRoutinesEarlier()
        {
            // Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>()
                                                                                            {
                                                                                                UIP_SWAP_BACK_ORIGINAL_EARLIER,
                                                                                                UIP_REPAIRED_FIRST_EARLIER,
                                                                                                UIP_SWAP_BACK_FIRST_EARLIER,
                                                                                                UIP_REPAIRED_SECOND_EARLIER,
                                                                                                UIP_SWAP_BACK_SECOND_EARLIER,
                                                                                                UIP_REPAIRED_THIRD_EARLIER,
                                                                                                UIP_SWAP_BACK_THIRD_EARLIER,
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var swapBackOriginal = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_ORIGINAL_EARLIER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackOriginal);
                Assert.Equal(4, swapBackOriginal.ItemID);
                Assert.Equal(Sam.UID, swapBackOriginal.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 1), swapBackOriginal.OutDT);
                Assert.Equal(1, swapBackOriginal.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackOriginal.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackOriginal.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 2), swapBackOriginal.InDT);
                Assert.Equal(44, swapBackOriginal.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackOriginal.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackOriginal.Status);

                // Assert 2
                var swapBackFirst = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FIRST_EARLIER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackFirst);
                Assert.Equal(4, swapBackFirst.ItemID);
                Assert.Equal(Sam.UID, swapBackFirst.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 5), swapBackFirst.OutDT);
                Assert.Equal(44, swapBackFirst.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackFirst.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackFirst.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 6), swapBackFirst.InDT);
                Assert.Equal(2, swapBackFirst.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackFirst.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackFirst.Status);

                // Assert 3
                var swapBackCloudFirst = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(GUID_5))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackCloudFirst);
                Assert.Equal(4, swapBackCloudFirst.ItemID);
                Assert.Equal(Sam.UID, swapBackCloudFirst.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 9), swapBackCloudFirst.OutDT);
                Assert.Equal(2, swapBackCloudFirst.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackCloudFirst.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackCloudFirst.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 10), swapBackCloudFirst.InDT);
                Assert.Equal(45, swapBackCloudFirst.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackCloudFirst.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackCloudFirst.Status);

                // Assert 4
                var swapBackSecond = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_SECOND_EARLIER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackSecond);
                Assert.Equal(4, swapBackSecond.ItemID);
                Assert.Equal(Sam.UID, swapBackSecond.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 13), swapBackSecond.OutDT);
                Assert.Equal(45, swapBackSecond.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackSecond.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackSecond.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 14), swapBackSecond.InDT);
                Assert.Equal(3, swapBackSecond.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackSecond.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackSecond.Status);

                // Assert 5
                var swapBackCloudSecond = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(GUID_9))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackCloudSecond);
                Assert.Equal(4, swapBackCloudSecond.ItemID);
                Assert.Equal(Sam.UID, swapBackCloudSecond.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 17), swapBackCloudSecond.OutDT);
                Assert.Equal(3, swapBackCloudSecond.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackCloudSecond.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackCloudSecond.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 18), swapBackCloudSecond.InDT);
                Assert.Equal(46, swapBackCloudSecond.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackCloudSecond.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackCloudSecond.Status);

                // Assert 6
                var swapBackThird = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_THIRD_EARLIER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackThird);
                Assert.Equal(4, swapBackThird.ItemID);
                Assert.Equal(Sam.UID, swapBackThird.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 21), swapBackThird.OutDT);
                Assert.Equal(46, swapBackThird.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackThird.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackThird.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 22), swapBackThird.InDT);
                Assert.Equal(4, swapBackThird.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackThird.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackThird.Status);
            }
        }

        private readonly UserInPossessionDTO UIP_SWAP_BACK_ORIGINAL_LATER = new()
        {
            ID = GUID_12,
            ItemID = 5,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 1),
            CabinetPositionIDOut = 1,
            CabinetNumberOut = NieuwVennep.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 6),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_FIRST_LATER = new()
        {
            ID = GUID_15,
            ItemID = 5,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 7),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 8),
            CabinetPositionIDIn = 44,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_FIRST_LATER = new()
        {
            ID = GUID_16,
            ItemID = 5,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 9),
            CabinetPositionIDOut = 44,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 14),
            CabinetPositionIDIn = 45,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_SECOND_LATER = new()
        {
            ID = GUID_19,
            ItemID = 5,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 15),
            CabinetPositionIDOut = 45,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 16),
            CabinetPositionIDIn = 45,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_SECOND_LATER = new()
        {
            ID = GUID_20,
            ItemID = 5,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 17),
            CabinetPositionIDOut = 45,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Sam.UID,
            InDT = new DateTime(2023, 1, 22),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_REPAIRED_THIRD_LATER = new()
        {
            ID = GUID_23,
            ItemID = 5,
            CTAMUserUIDOut = Tlb.UID,
            OutDT = new DateTime(2023, 1, 23),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,

            CTAMUserUIDIn = Tlb.UID,
            InDT = new DateTime(2023, 1, 24),
            CabinetPositionIDIn = 46,
            CabinetNumberIn = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Returned
        };

        private readonly UserInPossessionDTO UIP_SWAP_BACK_THIRD_LATER = new()
        {
            ID = GUID_24,
            ItemID = 5,
            CTAMUserUIDOut = Sam.UID,
            OutDT = new DateTime(2023, 1, 25),
            CabinetPositionIDOut = 46,
            CabinetNumberOut = Breda.CabinetNumber,
            Status = UserInPossessionStatus.Picked
        };

        [Fact]
        public async Task TestSyncCriticalItemCabinetDataThreeRoutinesLater()
        {
            // Arrange command & cabinetnumber is not combilocker breda because it isnt in the test data but it isnt used
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>()
                                                                                            {
                                                                                                UIP_SWAP_BACK_ORIGINAL_LATER,
                                                                                                UIP_REPAIRED_FIRST_LATER,
                                                                                                UIP_SWAP_BACK_FIRST_LATER,
                                                                                                UIP_REPAIRED_SECOND_LATER,
                                                                                                UIP_SWAP_BACK_SECOND_LATER,
                                                                                                UIP_REPAIRED_THIRD_LATER,
                                                                                                UIP_SWAP_BACK_THIRD_LATER,
                                                                                            },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<UserInPossessionProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert 1
                var swapBackOriginal = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_ORIGINAL_LATER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackOriginal);
                Assert.Equal(5, swapBackOriginal.ItemID);
                Assert.Equal(Sam.UID, swapBackOriginal.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 1), swapBackOriginal.OutDT);
                Assert.Equal(1, swapBackOriginal.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackOriginal.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackOriginal.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 2), swapBackOriginal.InDT);
                Assert.Equal(2, swapBackOriginal.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackOriginal.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackOriginal.Status);

                // Assert 2
                var swapBackCloudFirst = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(GUID_14))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackCloudFirst);
                Assert.Equal(5, swapBackCloudFirst.ItemID);
                Assert.Equal(Sam.UID, swapBackCloudFirst.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 5), swapBackCloudFirst.OutDT);
                Assert.Equal(2, swapBackCloudFirst.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackCloudFirst.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackCloudFirst.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 6), swapBackCloudFirst.InDT);
                Assert.Equal(44, swapBackCloudFirst.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackCloudFirst.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackCloudFirst.Status);

                // Assert 3
                var swapBackFirst = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_FIRST_LATER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackFirst);
                Assert.Equal(5, swapBackFirst.ItemID);
                Assert.Equal(Sam.UID, swapBackFirst.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 9), swapBackFirst.OutDT);
                Assert.Equal(44, swapBackFirst.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackFirst.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackFirst.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 10), swapBackFirst.InDT);
                Assert.Equal(3, swapBackFirst.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackFirst.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackFirst.Status);

                // Assert 4
                var swapBackCloudSecond = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(GUID_18))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackCloudSecond);
                Assert.Equal(5, swapBackCloudSecond.ItemID);
                Assert.Equal(Sam.UID, swapBackCloudSecond.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 13), swapBackCloudSecond.OutDT);
                Assert.Equal(3, swapBackCloudSecond.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackCloudSecond.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackCloudSecond.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 14), swapBackCloudSecond.InDT);
                Assert.Equal(45, swapBackCloudSecond.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackCloudSecond.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackCloudSecond.Status);

                // Assert 5
                var swapBackSecond = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_SECOND_LATER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackSecond);
                Assert.Equal(5, swapBackSecond.ItemID);
                Assert.Equal(Sam.UID, swapBackSecond.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 17), swapBackSecond.OutDT);
                Assert.Equal(45, swapBackSecond.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackSecond.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackSecond.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 18), swapBackSecond.InDT);
                Assert.Equal(4, swapBackSecond.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackSecond.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackSecond.Status);

                // Assert 6
                var swapBackCloudThird = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(GUID_22))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackCloudThird);
                Assert.Equal(5, swapBackCloudThird.ItemID);
                Assert.Equal(Sam.UID, swapBackCloudThird.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 21), swapBackCloudThird.OutDT);
                Assert.Equal(4, swapBackCloudThird.CabinetPositionIDOut);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackCloudThird.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackCloudThird.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 22), swapBackCloudThird.InDT);
                Assert.Equal(46, swapBackCloudThird.CabinetPositionIDIn);
                Assert.Equal(Breda.CabinetNumber, swapBackCloudThird.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackCloudThird.Status);

                // Assert 7
                var swapBackThird = await context.CTAMUserInPossession()
                    .Where(upi => upi.ID.Equals(UIP_SWAP_BACK_THIRD_LATER.ID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(swapBackThird);
                Assert.Equal(5, swapBackThird.ItemID);
                Assert.Equal(Sam.UID, swapBackThird.CTAMUserUIDOut);
                Assert.Equal(new DateTime(2023, 1, 25), swapBackThird.OutDT);
                Assert.Equal(46, swapBackThird.CabinetPositionIDOut);
                Assert.Equal(Breda.CabinetNumber, swapBackThird.CabinetNumberOut);

                Assert.Equal(Sam.UID, swapBackThird.CTAMUserUIDIn);
                Assert.Equal(new DateTime(2023, 1, 26), swapBackThird.InDT);
                Assert.Equal(5, swapBackThird.CabinetPositionIDIn);
                Assert.Equal(NieuwVennep.CabinetNumber, swapBackThird.CabinetNumberIn);
                Assert.Equal(UserInPossessionStatus.Returned, swapBackThird.Status);
            }
        }

        public static readonly object[][] CabinetStockData =
        {
            new object[] { new CabinetStockDTO { CabinetNumber = NieuwVennep.CabinetNumber, ItemTypeID = 5, ActualStock = 0, MinimalStock = 1, Status = CabinetStockStatus.WarningBelowMinimumSend }, CabinetStockStatus.WarningBelowMinimumSend, 0, 1 },
            new object[] { new CabinetStockDTO { CabinetNumber = NieuwVennep.CabinetNumber, ItemTypeID = 1, ActualStock = 1, MinimalStock = 1, Status = CabinetStockStatus.OK }, CabinetStockStatus.OK, 1, 1 },
            new object[] { new CabinetStockDTO { CabinetNumber = NieuwVennep.CabinetNumber, ItemTypeID = 1, ActualStock = 10, MinimalStock = 1, Status = CabinetStockStatus.OK }, CabinetStockStatus.OK, 10, 1 },
        };

        [Theory, MemberData(nameof(CabinetStockData))]
        public async Task TestSyncCabinetStocks(CabinetStockDTO cabinetStockDTO, CabinetStockStatus cabinetStockStatus, int actualStock, int minimalStock)
        {
            //  Arrange command
            var syncCriticalItemCabinetDataCommand = new SyncCriticalItemCabinetDataCommand(NieuwVennep.CabinetNumber,
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetPositionContentDTO>() { },
                                                                                            new List<CabinetStockDTO>() { cabinetStockDTO },
                                                                                            new List<ItemToPickDTO>() { },
                                                                                            new List<UserInPossessionDTO>() { },
                                                                                            new List<UserPersonalItemDTO>() { });

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<SyncCriticalItemCabinetDataHandler>>().Object;
                var mapper = new MapperConfiguration(c =>
                {
                    c.AddProfile<CabinetStockProfile>();
                }).CreateMapper();

                SyncCriticalItemCabinetDataHandler handler = new(context, logger, mapper);

                // Act
                var handle = await handler.Handle(syncCriticalItemCabinetDataCommand, It.IsAny<CancellationToken>());

                // Assert
                var result = await context.CabinetStock()
                    .Where(cs => cs.CabinetNumber.Equals(NieuwVennep.CabinetNumber) && cs.ItemTypeID.Equals(cabinetStockDTO.ItemTypeID))
                    .SingleOrDefaultAsync();
                Assert.NotNull(result);
                Assert.Equal(cabinetStockStatus, result.Status);
                Assert.Equal(actualStock, result.ActualStock);
                Assert.Equal(minimalStock, result.MinimalStock);
            }
        }
    }
}
