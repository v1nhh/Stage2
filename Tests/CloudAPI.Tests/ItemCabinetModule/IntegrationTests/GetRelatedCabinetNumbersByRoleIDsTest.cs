using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Queries;
using CTAM.Core;
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
    public class GetRelatedCabinetNumbersByRoleIDsTest : AbstractIntegrationTests
    {
        private const int ROLE_1_ID = int.MaxValue;
        private const string ROLE_1_DESCRIPION = "RolWithCabinet";

        private const int ROLE_2_ID = int.MaxValue - 1;
        private const string ROLE_2_DESCRIPION = "RolWithoutCabinet";

        private const string CABINET_NAME = "IBK TEST 1";
        private const string CABINET_NUMBER = "01";

        public GetRelatedCabinetNumbersByRoleIDsTest() : base("CTAM_GetRelatedCabinetNumbersByRoleIDs")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_1_ID, Description = ROLE_1_DESCRIPION, CreateDT = DateTime.UtcNow });
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_2_ID, Description = ROLE_2_DESCRIPION, CreateDT = DateTime.UtcNow });

                context.Cabinet().Add(new Cabinet() { CabinetNumber = CABINET_NUMBER, Name = CABINET_NAME });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_1_ID });
                context.SaveChanges();
            }
        }

        public static readonly object[][] RoleCabinetsCheck =
        {
            new object[]{
                new List<int> { ROLE_1_ID }, new Dictionary<int, List<string>>() { { ROLE_1_ID, new List<string>() { CABINET_NUMBER } } },
            },
            new object[]{
                new List<int> { ROLE_2_ID }, new Dictionary<int, List<string>>() {  },
            },
        };

        [Theory, MemberData(nameof(RoleCabinetsCheck))]
        public async Task TestRoleCabinetsCheck(List<int> roleIDs, Dictionary<int, List<string>> groupedCabinetNumbers)
        {
            //  Arrange
            var checkQuery = new GetRelatedCabinetNumbersByRoleIDsQuery(roleIDs);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<GetRelatedCabinetNumbersByRoleIDsHandler>>().Object;
                var handler = new GetRelatedCabinetNumbersByRoleIDsHandler(context, logger);

                // Act
                var result = await handler.Handle(checkQuery, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(groupedCabinetNumbers, result);
            }
        }
    }
}
