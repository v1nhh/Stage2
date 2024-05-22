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
    public class GetRelatedCabinetNumbersByUserUIDsTest : AbstractIntegrationTests
    {
        private const string USER_UID = "USER_GUID";
        private const string USER_2_UID = "USER_GUID_2";

        private const int ROLE_1_ID = int.MaxValue;
        private const string ROLE_1_DESCRIPION = "RolWithCabinet";

        private const int ROLE_2_ID = int.MaxValue - 1;
        private const string ROLE_2_DESCRIPION = "RolWithoutCabinet";

        private const string CABINET_NAME = "IBK TEST 1";
        private const string CABINET_NUMBER = "01";

        public GetRelatedCabinetNumbersByUserUIDsTest() : base("CTAM_GetRelatedCabinetNumbersByUserUIDs")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser() { UID = USER_UID, Email = "Mail1", LanguageCode = "L1", Name = "Name1" });
                context.CTAMUser().Add(new CTAMUser() { UID = USER_2_UID, Email = "Mail2", LanguageCode = "L1", Name = "Name2" });

                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_1_ID, Description = ROLE_1_DESCRIPION, CreateDT = DateTime.UtcNow });
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_2_ID, Description = ROLE_2_DESCRIPION, CreateDT = DateTime.UtcNow });

                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_UID, CTAMRoleID = ROLE_1_ID });
                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_2_UID, CTAMRoleID = ROLE_2_ID });

                context.Cabinet().Add(new Cabinet() { CabinetNumber = CABINET_NUMBER, Name = CABINET_NAME });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_1_ID });
                context.SaveChanges();
            }
        }

        public static readonly object[][] UserRoleCabinetsCheck =
        {
            new object[]{
                new List<string> { USER_UID }, new Dictionary<string, List<string>>() { { USER_UID , new List<string>() { CABINET_NUMBER } } },
            },
            new object[]{
                new List<string> { USER_2_UID }, new Dictionary<string, List<string>>() {  },
            },
        };

        [Theory, MemberData(nameof(UserRoleCabinetsCheck))]
        public async Task TestUserRoleCabinetsCheck(List<string> userUIDs, Dictionary<string, List<string>> groupedCabinetNumbers)
        {
            //  Arrange
            var checkQuery = new GetRelatedCabinetNumbersByUserUIDsQuery(userUIDs);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<GetRelatedCabinetNumbersByUserUIDsHandler>>().Object;
                var handler = new GetRelatedCabinetNumbersByUserUIDsHandler(context, logger);

                // Act
                var result = await handler.Handle(checkQuery, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(groupedCabinetNumbers, result);
            }
        }
    }
}
