using AutoMapper;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Profiles;
using ItemModule.ApplicationCore.Queries.Items;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemModule.IntegrationTests
{
    public class GetItemsTest : AbstractIntegrationTests
    {
        const int ITEM_ID_DINGAA = 9000;
        const string ITEM_DESC_DINGAA = "AADing";

        const int ITEM_ID_DINGAB = 9001;
        const string ITEM_DESC_DINGAB = "ABDing";

        const int ITEM_ID_DINGAC = 9002;
        const string ITEM_DESC_DINGAC = "ACDing";

        const int ITEM_ID_DINGZX = 9003;
        const string ITEM_DESC_DINGZX = "ZXDing";

        const int ITEM_ID_DINGZY = 9004;
        const string ITEM_DESC_DINGZY = "ZYDing";

        const int ITEM_ID_DINGZZ = 9005;
        const string ITEM_DESC_DINGZZ = "ZZDing";

        const int ITEMTYPE_ID_BLA = 9100;
        const string ITEMTYPE_DESC_BLA = "Blatype";

        const int ITEMTYPE_ID_BLUB = 9101;
        const string ITEMTYPE_DESC_BLUB = "Blubtype";

        public GetItemsTest() : base("CTAM_GetItems")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ItemType().Add(new ItemType { ID = ITEMTYPE_ID_BLA, Description = ITEMTYPE_DESC_BLA });
                context.ItemType().Add(new ItemType { ID = ITEMTYPE_ID_BLUB, Description = ITEMTYPE_DESC_BLUB });

                context.Item().Add(new Item { ID = ITEM_ID_DINGAA, Description = ITEM_DESC_DINGAA, ItemTypeID = ITEMTYPE_ID_BLA });
                context.Item().Add(new Item { ID = ITEM_ID_DINGAB, Description = ITEM_DESC_DINGAB, ItemTypeID = ITEMTYPE_ID_BLA });
                context.Item().Add(new Item { ID = ITEM_ID_DINGAC, Description = ITEM_DESC_DINGAC, ItemTypeID = ITEMTYPE_ID_BLA });
                context.Item().Add(new Item { ID = ITEM_ID_DINGZZ, Description = ITEM_DESC_DINGZZ, ItemTypeID = ITEMTYPE_ID_BLA });
                context.Item().Add(new Item { ID = ITEM_ID_DINGZY, Description = ITEM_DESC_DINGZY, ItemTypeID = ITEMTYPE_ID_BLA });
                context.Item().Add(new Item { ID = ITEM_ID_DINGZX, Description = ITEM_DESC_DINGZX, ItemTypeID = ITEMTYPE_ID_BLUB });
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, ItemColumn.Description, false, "", ITEM_ID_DINGAA, 2, null, null },
            new object[] { 2, 0, ItemColumn.Description, true, "", ITEM_ID_DINGZZ, 2, null, null },
            new object[] { 2, 0, null, true, "ZZD", ITEM_ID_DINGZZ, 1, null, null },
            new object[] { 2, 1, ItemColumn.Description, false, "", ITEM_ID_DINGAC, 2, null, null },
            new object[] { 2, 1, ItemColumn.Description, true, "", ITEM_ID_DINGZX, 2, null , null },
            new object[] { 2, 1, null, true, "", ITEM_ID_DINGZX, 1, ITEMTYPE_ID_BLUB, null },
            new object[] { 9, 0, null, true, "", 1, 9, null, UserInPossessionStatus.Picked },
            new object[] { 1, 0, null, true, "", 26, 1, null, UserInPossessionStatus.Returned },
            new object[] { 1, 0, null, true, "", 33, 1, null, UserInPossessionStatus.Removed },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, ItemColumn? column, bool desc, string filter, int firstID, int expectedCount, int? itemtypeId, UserInPossessionStatus? userInPossessionStatus)
        {
            // Arrange
            var query = new GetItemsPaginatedQuery(limit, page, column, desc, filter, itemtypeId, userInPossessionStatus);
            var logger = new Mock<ILogger<GetItemsPaginatedHandler>>().Object;
            var mapper = new MapperConfiguration(c => {
                c.AddProfile<ItemProfile>();
                c.AddProfile<ItemTypeProfile>();
                c.AddProfile<ItemDetailProfile>();
            }).CreateMapper();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetItemsPaginatedHandler(context, logger, mapper);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal(firstID, first.ID);
            }
        }
    }
}
