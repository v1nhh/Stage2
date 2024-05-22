using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Profiles;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Profiles;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
using ItemModule.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class GetCabinetStockTest : AbstractIntegrationTests
    {
        static Cabinet cabA1 = null;
        readonly static string cabA1_CabinetNumber = "210609095541";

        const int ITEMTYPE1_ID = 9001;
        const string ITEMTYPE1_DESC = "AA1Itemtype1";
        const int ITEMTYPE2_ID = 9002;
        const string ITEMTYPE2_DESC = "AA2Itemtype2";
        const int ITEMTYPE3_ID = 9003;
        const string ITEMTYPE3_DESC = "AA3Itemtype3";
        const int ITEMTYPE4_ID = 9004;
        const string ITEMTYPE4_DESC = "ZZ1Itemtype4";
        const int ITEMTYPE5_ID = 9005;
        const string ITEMTYPE5_DESC = "ZZ2Itemtype5";

        const int CABSTOCK1_MINIMAL = 1;
        const int CABSTOCK2_MINIMAL = 2;
        const int CABSTOCK3_MINIMAL = 3;
        const int CABSTOCK4_MINIMAL = 4;
        const int CABSTOCK5_MINIMAL = 5;

        public GetCabinetStockTest() : base("CTAM_GetCabinetStock")
        {
            cabA1 = new Cabinet { Name = "A1", CabinetType = CabinetType.Locker, Description = "Zie A1" };
            cabA1.CabinetNumber = cabA1_CabinetNumber;

            var now = DateTime.UtcNow;
            var yesterday = now.AddDays(-1);

            var itemType1 = new ItemType() { ID = ITEMTYPE1_ID, Description = ITEMTYPE1_DESC };
            var itemType2 = new ItemType() { ID = ITEMTYPE2_ID, Description = ITEMTYPE2_DESC };
            var itemType3 = new ItemType() { ID = ITEMTYPE3_ID, Description = ITEMTYPE3_DESC };
            var itemType4 = new ItemType() { ID = ITEMTYPE4_ID, Description = ITEMTYPE4_DESC };
            var itemType5 = new ItemType() { ID = ITEMTYPE5_ID, Description = ITEMTYPE5_DESC };

            var cabstock1 = new CabinetStock() { CabinetNumber = cabA1_CabinetNumber, ItemTypeID = ITEMTYPE1_ID, MinimalStock = CABSTOCK1_MINIMAL, ActualStock = 5, CreateDT = yesterday, UpdateDT = yesterday, Status = CabinetStockStatus.OK };
            var cabstock2 = new CabinetStock() { CabinetNumber = cabA1_CabinetNumber, ItemTypeID = ITEMTYPE2_ID, MinimalStock = CABSTOCK2_MINIMAL, ActualStock = 4, CreateDT = yesterday, UpdateDT = yesterday, Status = CabinetStockStatus.OK };
            var cabstock3 = new CabinetStock() { CabinetNumber = cabA1_CabinetNumber, ItemTypeID = ITEMTYPE3_ID, MinimalStock = CABSTOCK3_MINIMAL, ActualStock = 3, CreateDT = yesterday, UpdateDT = yesterday, Status = CabinetStockStatus.OK };
            var cabstock4 = new CabinetStock() { CabinetNumber = cabA1_CabinetNumber, ItemTypeID = ITEMTYPE4_ID, MinimalStock = CABSTOCK4_MINIMAL, ActualStock = 2, CreateDT = yesterday, UpdateDT = yesterday, Status = CabinetStockStatus.WarningBelowMinimumSend };
            var cabstock5 = new CabinetStock() { CabinetNumber = cabA1_CabinetNumber, ItemTypeID = ITEMTYPE5_ID, MinimalStock = CABSTOCK5_MINIMAL, ActualStock = 1, CreateDT = yesterday, UpdateDT = yesterday, Status = CabinetStockStatus.WarningBelowMinimumSend };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.Cabinet().Add(cabA1);
                context.ItemType().AddRange(itemType1, itemType2, itemType3, itemType4, itemType5);
                context.CabinetStock().AddRange(cabstock1, cabstock2, cabstock3, cabstock4, cabstock5);
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, CabinetStockColumn.ItemType, false, "", CABSTOCK1_MINIMAL, 2, null, null },
            new object[] { 2, 0, CabinetStockColumn.ItemType, true, "", CABSTOCK5_MINIMAL, 2, null, null },
            new object[] { 2, 0, null, true, "ZZ1", CABSTOCK4_MINIMAL, 1, null, null },
            new object[] { 2, 0, CabinetStockColumn.ItemType, false, "", CABSTOCK4_MINIMAL, 2, null, CabinetStockStatus.WarningBelowMinimumSend },
            new object[] { 2, 0, CabinetStockColumn.ItemType, false, "", CABSTOCK4_MINIMAL, 1, ITEMTYPE4_ID, null },
            new object[] { 2, 0, CabinetStockColumn.MinimalStock, true, "", CABSTOCK5_MINIMAL, 2, null, null },
            new object[] { 2, 1, CabinetStockColumn.ItemType, false, "", CABSTOCK3_MINIMAL, 2, null, null },
            new object[] { 2, 0, null, true, "", CABSTOCK5_MINIMAL, 1, ITEMTYPE5_ID, null },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, CabinetStockColumn? column, bool desc, string filter, 
                                           int minStock, int expectedCount, int? itemtypeId, int? status)
        {
            // Arrange
            var query = new GetCabinetStockPaginatedQuery(cabA1_CabinetNumber, limit, page, column, desc, filter, itemtypeId, status);
            var loggerManager = new Mock<ILogger<ItemCabinetDataManager>>().Object;
            var loggerHandler = new Mock<ILogger<GetCabinetStockPaginatedHandler>>().Object;
            var mapper = new MapperConfiguration(c => {
                c.AddProfile<CabinetStockProfile>();
                c.AddProfile<ItemTypeProfile>();
            }).CreateMapper();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var datamanager = new ItemCabinetDataManager(context, loggerManager);
                var handler = new GetCabinetStockPaginatedHandler(mapper, datamanager, loggerHandler);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal(minStock, first.MinimalStock);
            }
        }
    }
}
