using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Profiles;
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
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.CloudAPI.IntegrationTests.LiveSync
{
    public class GetCollectedEnvelopeTest : AbstractIntegrationTests
    {
        private const int ROLE_FULL_ID = int.MaxValue;
        private const int ROLE_SINGLE_ID = int.MaxValue - 1;
        private const int ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID = int.MaxValue - 2;
        // For exception tests
        private const int ROLE_WITHOUT_CABINET_ID = int.MaxValue - 3;
        private const int ROLE_WITH_ITEM_TYPE_WITHOUT_CABINET_STOCK_ID = int.MaxValue - 4;

        private const string USER_FULL_UID = "00000000-0000-0000-0000-000000000000";

        private const string CABINET_NUMBER = "000000000000";

        private const int ITEM_TYPE_FULL_ID = int.MaxValue;
        private const int ITEM_TYPE_WITHOUT_ITEM_ID = int.MaxValue - 1;
        // For exception tests
        private const int ITEM_TYPE_WITHOUT_CABINET_STOCK_ID = int.MaxValue - 2;


        public GetCollectedEnvelopeTest() : base("CTAM_GetRoleEnvelope")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_FULL_ID, Description = "ROLE_FULL_ID" });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_SINGLE_ID, Description = "ROLE_SINGLE_ID" });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, Description = "ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID" });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_WITHOUT_CABINET_ID, Description = "ROLE_WITHOUT_CABINET_ID" });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_WITH_ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, Description = "ROLE_WITH_ITEM_TYPE_WITHOUT_CABINET_STOCK_ID" });

                // Pickup & return permissions for all
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_FULL_ID, CTAMPermissionID = 6 });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_FULL_ID, CTAMPermissionID = 7 });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, CTAMPermissionID = 6 });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, CTAMPermissionID = 7 });

                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER, Name = CABINET_NUMBER, Description = $"Test Cabinet {CABINET_NUMBER}" });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CTAMRoleID = ROLE_FULL_ID, CabinetNumber = CABINET_NUMBER });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CTAMRoleID = ROLE_SINGLE_ID, CabinetNumber = CABINET_NUMBER });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, CabinetNumber = CABINET_NUMBER });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, CabinetNumber = CABINET_NUMBER });

                context.CTAMUser().Add(new CTAMUser { UID = USER_FULL_UID, Name = "USER_FULL_UID", Email = "e@mail.com", LanguageCode = "nl-NL" });

                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_FULL_UID, CTAMRoleID = ROLE_FULL_ID });

                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_FULL_ID, Description = "ITEM_TYPE_FULL_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITHOUT_ITEM_ID, Description = "ITEM_TYPE_WITHOUT_ITEM_ID" });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, Description = "ITEM_TYPE_WITHOUT_CABINET_STOCK_ID" });

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_FULL_ID, CTAMRoleID = ROLE_FULL_ID });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_ITEM_ID, CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_WITHOUT_CABINET_STOCK_ID, CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_CABINET_STOCK_ID });

                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_FULL_ID, CabinetNumber = CABINET_NUMBER });
                context.CabinetStock().Add(new CabinetStock { ItemTypeID = ITEM_TYPE_WITHOUT_ITEM_ID, CabinetNumber = CABINET_NUMBER });

                context.Item().Add(new Item { ID = ITEM_TYPE_FULL_ID, ItemTypeID = ITEM_TYPE_FULL_ID, Description = "ITEM_TYPE_FULL_ID" });

                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_FULL_UID, ItemID = ITEM_TYPE_FULL_ID, Status = UserInPossessionStatus.Picked });
                
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { ID = ITEM_TYPE_FULL_ID, CTAMUserUID = USER_FULL_UID, ItemID = ITEM_TYPE_FULL_ID });

                context.CabinetAccessIntervals().Add(new CabinetAccessInterval() { ID = int.MaxValue - 1, CTAMRoleID = ROLE_FULL_ID, StartWeekDayNr = 0, EndWeekDayNr = 3 });
                context.CabinetAccessIntervals().Add(new CabinetAccessInterval() { ID = int.MaxValue - 2, CTAMRoleID = ROLE_FULL_ID, StartWeekDayNr = 2, EndWeekDayNr = 5 });
                context.CabinetAccessIntervals().Add(new CabinetAccessInterval() { ID = int.MaxValue - 3, CTAMRoleID = ROLE_FULL_ID, StartWeekDayNr = 3, EndWeekDayNr = 7 });
                context.CabinetAccessIntervals().Add(new CabinetAccessInterval() { ID = int.MaxValue - 4, CTAMRoleID = ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, StartWeekDayNr = 0, EndWeekDayNr = 3 });

                context.SaveChanges();
            }
        }

        public static readonly object[][] RoleCorrectData =
        {
            new object[] { ROLE_FULL_ID, true,
                2, 1, 1, 1,
                1, 1, 1, 3,
                1, 1
            },
            new object[] { ROLE_SINGLE_ID, true,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0,
            },
            new object[] { ROLE_WITH_ITEM_TYPE_WITHOUT_ITEM_ID, true,
                2, 1, 1, 1,
                0, 0, 0, 1,
                0, 0
            },
        };

        [Theory, MemberData(nameof(RoleCorrectData))]
        public async Task TestGetCollectedEnvelope(int roleID, bool roleFound,
            int rolePermissionsCount, int roleItemTypesCount, int itemTypesCount, int cabinetStocksCount,
            int itemsCount, int userInPossessionsCount, int userPersonalItemsCount, int cabinetAccessIntervalsCount,
            int userRolesCount, int usersCount)
        {
            // Arrange
            var req = new CollectedLiveSyncRequest()
            {
                RoleIDsForEnvelope = new List<int> { roleID }
            };
            var query = new GetCollectedEnvelopeQuery(req);
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserRoleCriticalDataProfile>();
                c.AddProfile<CabinetCriticalDataProfile>();
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
                Assert.Equal(roleFound, envelope.Roles.Count == 1);
                Assert.Equal(rolePermissionsCount, envelope.RolePermissions.Count);
                Assert.Equal(roleItemTypesCount, envelope.RoleItemTypes.Count);
                Assert.Equal(itemTypesCount, envelope.ItemTypes.Count);
                Assert.Equal(cabinetStocksCount, envelope.CabinetStocks.Count);
                Assert.Equal(itemsCount, envelope.Items.Count);
                Assert.Equal(userInPossessionsCount, envelope.UserInPossessions.Count);
                Assert.Equal(userPersonalItemsCount, envelope.UserPersonalItems.Count);
                Assert.Equal(cabinetAccessIntervalsCount, envelope.CabinetAccessIntervals.Count);
                Assert.Equal(userRolesCount, envelope.UserRoles.Count);
                Assert.Equal(usersCount, envelope.Users.Count);
            }
        }
    }
}
