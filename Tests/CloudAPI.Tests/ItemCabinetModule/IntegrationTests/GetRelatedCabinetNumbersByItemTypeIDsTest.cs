using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class GetRelatedCabinetNumbersByItemTypeIDsTest : AbstractIntegrationTests
    {
        private const int ITEM_TYPE_ID = int.MaxValue;
        private const string ITEM_TYPE_DESCRIPTION = "Portofoon";

        private const int ITEM_TYPE_2_ID = int.MaxValue - 1;
        private const string ITEM_TYPE_2_DESCRIPTION = "Laptop";

        private const int ROLE_1_ID = int.MaxValue;
        private const string ROLE_1_DESCRIPION = "RolWithCabinet";

        private const int ROLE_2_ID = int.MaxValue - 1;
        private const string ROLE_2_DESCRIPION = "RolWithoutCabinet";

        private const string CABINET_NAME = "IBK TEST 1";
        private const string CABINET_NUMBER = "01";

        public GetRelatedCabinetNumbersByItemTypeIDsTest() : base("CTAM_GetRelatedCabinetNumbersByItemTypeIDs")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ItemType().Add(new ItemType() { ID = ITEM_TYPE_ID, Description = ITEM_TYPE_DESCRIPTION });
                context.ItemType().Add(new ItemType() { ID = ITEM_TYPE_2_ID, Description = ITEM_TYPE_2_DESCRIPTION });

                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_1_ID, Description = ROLE_1_DESCRIPION, CreateDT = DateTime.UtcNow });
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_2_ID, Description = ROLE_2_DESCRIPION, CreateDT = DateTime.UtcNow });

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_1_ID, ItemTypeID = ITEM_TYPE_ID });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_2_ID, ItemTypeID = ITEM_TYPE_2_ID });

                context.Cabinet().Add(new Cabinet() { CabinetNumber = CABINET_NUMBER, Name = CABINET_NAME });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_1_ID });
                context.SaveChanges();
            }
        }

        public static readonly object[][] RoleCabinetsCheck =
        {
            new object[]{
                new List<int> { ITEM_TYPE_ID }, new Dictionary<int, List<string>>() { { ITEM_TYPE_ID, new List<string>() { CABINET_NUMBER } } },
            },
            new object[]{
                new List<int> { ITEM_TYPE_2_ID }, new Dictionary<int, List<string>>() {  },
            },
        };

        [Theory, MemberData(nameof(RoleCabinetsCheck))]
        public async Task TestItemTypeCabinetsCheck(List<int> itemTypeIDs, Dictionary<int, List<string>> groupedCabinetNumbers)
        {
            //  Arrange
            var checkQuery = new GetRelatedCabinetNumbersByItemTypeIDsQuery(itemTypeIDs);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<GetRelatedCabinetNumbersByItemTypeIDsHandler>>().Object;
                var handler = new GetRelatedCabinetNumbersByItemTypeIDsHandler(context, logger);

                // Act
                var result = await handler.Handle(checkQuery, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(groupedCabinetNumbers, result);
            }
        }
    }
}
