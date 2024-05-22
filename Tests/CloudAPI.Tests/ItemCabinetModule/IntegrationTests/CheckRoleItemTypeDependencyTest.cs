using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
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
    public class CheckRoleItemTypeDependencyTest : AbstractIntegrationTests
    {
        private const int ITEM_TYPE_ID = int.MaxValue;
        private const string CABINET_NUMBER = "CABINET_NUMBER";
        private const int CABINET_CELLTYPE_ID_1 = int.MaxValue;
        private const int CABINET_POSITION_ID_1 = int.MaxValue;

        private const int ROLE_1_ID = int.MaxValue;
        private const string ROLE_1_DESC = "ROLE_1";
        private const int ROLE_2_ID = int.MaxValue - 1;
        private const string ROLE_2_DESC = "ROLE_2";

        private const string USER_WITHOUT_ROLES_ID = "USER_WITHOUT_ROLES_ID";

        private const string USER_ONLY_PICKED_WITH_ONE_ROLE_ID = "USER_ONLY_PICKED_WITH_ONE_ROLE_ID";
        private const int PICKED_ITEM_ID_1 = int.MaxValue - 1;

        private const string USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID = "USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID";
        private const int PICKED_ITEM_ID_2 = int.MaxValue - 2;

        private const string USER_ONLY_RETURNED_ID = "USER_ONLY_RETURNED_ID";
        private const int RETURNED_ITEM_ID_1 = int.MaxValue - 3;

        public CheckRoleItemTypeDependencyTest() : base("CTAM_CheckRoleItemTypeDependency")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { Name = USER_WITHOUT_ROLES_ID, UID = USER_WITHOUT_ROLES_ID, Password = "1234", LoginCode = "000001", PinCode = "1234", CardCode = "11111", CreateDT = DateTime.UtcNow, Email = $"{USER_WITHOUT_ROLES_ID}@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow });
                context.CTAMUser().Add(new CTAMUser { Name = USER_ONLY_PICKED_WITH_ONE_ROLE_ID, UID = USER_ONLY_PICKED_WITH_ONE_ROLE_ID, Password = "1234", LoginCode = "000002", PinCode = "1234", CardCode = "11112", CreateDT = DateTime.UtcNow, Email = $"{USER_ONLY_PICKED_WITH_ONE_ROLE_ID}@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow });
                context.CTAMUser().Add(new CTAMUser { Name = USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, UID = USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, Password = "1234", LoginCode = "000003", PinCode = "1234", CardCode = "11113", CreateDT = DateTime.UtcNow, Email = $"{USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID}@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow });
                context.CTAMUser().Add(new CTAMUser { Name = USER_ONLY_RETURNED_ID, UID = USER_ONLY_RETURNED_ID, Password = "1234", LoginCode = "000004", PinCode = "1234", CardCode = "11114", CreateDT = DateTime.UtcNow, Email = $"{USER_ONLY_RETURNED_ID}@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow });

                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_ID, Description = "Laptop" });
                context.Item().Add(new Item { ID = PICKED_ITEM_ID_1, ItemTypeID = ITEM_TYPE_ID, Description = "PICKED_ITEM_ID_1" });
                context.Item().Add(new Item { ID = PICKED_ITEM_ID_2, ItemTypeID = ITEM_TYPE_ID, Description = "PICKED_ITEM_ID_2" });
                context.Item().Add(new Item { ID = RETURNED_ITEM_ID_1, ItemTypeID = ITEM_TYPE_ID, Description = "RETURNED_ITEM_ID_1" });

                context.Cabinet().Add(new Cabinet { Name = "Test Cabinet", Description = "Test", CabinetNumber = CABINET_NUMBER, IsActive = true, Status = CabinetStatus.Online, CabinetType = CabinetType.Locker, LoginMethod = LoginMethod.CardCode });
                context.CabinetCellType().Add(new CabinetCellType() { ID = CABINET_CELLTYPE_ID_1, Color = "Grijs", Depth = 30, Height = 30, Width = 30, LockType = "Default", LongDescr = "", Material = "Metaal", ShortDescr = "Locker 30x30x30", SpecCode = "L30", SpecType = SpecType.Locker });
                context.CabinetPosition().Add(new CabinetPosition { ID = CABINET_POSITION_ID_1, CabinetNumber = CABINET_NUMBER, CabinetCellTypeID = CABINET_CELLTYPE_ID_1, PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 });

                context.CabinetPositionContent().Add(new CabinetPositionContent { CabinetPositionID = CABINET_POSITION_ID_1, ItemID = RETURNED_ITEM_ID_1 });

                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), CTAMUserUIDOut = USER_ONLY_PICKED_WITH_ONE_ROLE_ID, ItemID = PICKED_ITEM_ID_1, CabinetPositionIDOut = CABINET_POSITION_ID_1, Status = UserInPossessionStatus.Picked, OutDT = DateTime.UtcNow, });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), CTAMUserUIDOut = USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, ItemID = PICKED_ITEM_ID_2, CabinetPositionIDOut = CABINET_POSITION_ID_1, Status = UserInPossessionStatus.Picked, OutDT = DateTime.UtcNow, });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), CTAMUserUIDOut = USER_ONLY_RETURNED_ID, ItemID = RETURNED_ITEM_ID_1, CabinetPositionIDOut = CABINET_POSITION_ID_1, Status = UserInPossessionStatus.Returned, OutDT = DateTime.UtcNow, });

                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_1_ID, Description = ROLE_1_DESC, CreateDT = DateTime.UtcNow });
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_2_ID, Description = ROLE_2_DESC, CreateDT = DateTime.UtcNow });

                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_ONLY_PICKED_WITH_ONE_ROLE_ID, CTAMRoleID = ROLE_1_ID });
                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_ONLY_RETURNED_ID, CTAMRoleID = ROLE_1_ID });
                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, CTAMRoleID = ROLE_1_ID });
                context.CTAMUser_Role().Add(new CTAMUser_Role() { CTAMUserUID = USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, CTAMRoleID = ROLE_2_ID });

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_1_ID, ItemTypeID = ITEM_TYPE_ID, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_2_ID, ItemTypeID = ITEM_TYPE_ID, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_1_ID });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_2_ID });

                context.SaveChanges();
            }
        }

        public static readonly object[][] EmptyRoleChecks =
        {
            // Empty checks
            new object[] { USER_WITHOUT_ROLES_ID, new List<int>(), new List<string>() },
            new object[] { USER_WITHOUT_ROLES_ID, null, new List<string>() },
            new object[] { USER_WITHOUT_ROLES_ID, new List<int>() { ROLE_1_ID, ROLE_2_ID }, new List<string>() },

            // Single dependency picked check
            new object[] { USER_ONLY_PICKED_WITH_ONE_ROLE_ID, new List<int>() { ROLE_1_ID }, new List<string>() { ROLE_1_DESC } },

            // Multiple dependency picked check
            new object[] { USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, new List<int>() { ROLE_1_ID }, new List<string>() },
            new object[] { USER_ONLY_PICKED_WITH_MULTIPLE_ROLES_ID, new List<int>() { ROLE_1_ID, ROLE_2_ID }, new List<string>() { ROLE_1_DESC, ROLE_2_DESC } },

            // Single dependency returned check
            new object[] { USER_ONLY_RETURNED_ID, new List<int>() { ROLE_1_ID }, new List<string>() },
        };

        [Theory, MemberData(nameof(EmptyRoleChecks))]
        public async Task TestCheckRolesForUserWithoutRoles(string userUID, List<int> roleIDs, List<string> expectedResult)
        {
            //  Arrange
            var checkQuery = new CheckRoleItemTypeDependencyQuery(userUID, roleIDs);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<CheckRoleItemTypeDependencyHandler>>().Object;
                var handler = new CheckRoleItemTypeDependencyHandler(context, logger);

                // Act
                var result = await handler.Handle(checkQuery, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(expectedResult, result);
            }
        }
    }
}
