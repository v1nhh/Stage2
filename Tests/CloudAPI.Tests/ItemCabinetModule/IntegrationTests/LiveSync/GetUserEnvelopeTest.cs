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
    public class GetUserEnvelopeTest : AbstractIntegrationTests
    {
        private const string USER_FULL_UID = "00000000-0000-0000-0000-000000000000";
        private const string USER_POSSESSION_ONLY_UID = "00000000-0000-0000-0000-000000000001";
        private const string USER_PERSONAL_ONLY_UID = "00000000-0000-0000-0000-000000000002";
        private const string USER_SINGLE_UID = "00000000-0000-0000-0000-000000000003";
        private const string USER_POSSESSION_RETURNED_ONLY_UID = "00000000-0000-0000-0000-000000000004";
        private const string USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED = "00000000-0000-0000-0000-000000000005";
        private const string NON_EXISTING_USER_UID = "00000000-0000-0000-0000-000000000006";
        private const string USER_WITHOUT_ROLE_UID = "00000000-0000-0000-0000-000000000007";


        private const int ITEM_FULL_ID = int.MaxValue;
        private const int ITEM_POSSESSION_ONLY_ID = int.MaxValue - 1;
        private const int ITEM_PERSONAL_ONLY_ID = int.MaxValue - 2;
        private const int ITEM_POSSESSION_RETURNED_ONLY_ID = int.MaxValue - 4;
        private const int ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID = int.MaxValue - 5;
        private const int ITEM_POSSESSION_ONLY_ID_ITEM_TYPE_NOT_ALLOWED = int.MaxValue - 6;

        private const int ITEM_TYPE_ID = int.MaxValue - 10;
        private const int ITEM_TYPE_ID_NOT_ALLOWED = int.MaxValue - 11;
        private const string ITEM_TYPE_DESCRIPTION = "ITEM_TYPE_DESCRIPTION";
        private const string ITEM_TYPE_DESCRIPTION_NOTALLOWED = "ITEM_TYPE_DESCRIPTION_NOTALLOWED";

        private const string CABINET_NUMBER = "000000000000";

        public GetUserEnvelopeTest() : base("CTAM_GetUserEnvelope")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { UID = USER_FULL_UID, Name = "USER_FULL_UID", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_POSSESSION_ONLY_UID, Name = "USER_POSSESSION_ONLY_UID", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_PERSONAL_ONLY_UID, Name = "USER_PERSONAL_ONLY_UID", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_SINGLE_UID, Name = "USER_SINGLE_UID", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_POSSESSION_RETURNED_ONLY_UID, Name = "USER_POSSESSION_RETURNED_ONLY_UID", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED, Name = "USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = USER_WITHOUT_ROLE_UID, Name = "USER_WITHOUT_ROLE_UID", Email = "test@test.com", LanguageCode = "nl-NL" });

                context.Item().Add(new Item { ID = ITEM_FULL_ID, Description = "ITEM_FULL_ID", ItemTypeID = ITEM_TYPE_ID });
                context.Item().Add(new Item { ID = ITEM_POSSESSION_ONLY_ID, Description = "ITEM_POSSESSION_ONLY_ID", ItemTypeID = ITEM_TYPE_ID });
                context.Item().Add(new Item { ID = ITEM_POSSESSION_ONLY_ID_ITEM_TYPE_NOT_ALLOWED, Description = "ITEM_POSSESSION_ONLY_ID_ITEM_TYPE_NOT_ALLOWED", ItemTypeID = ITEM_TYPE_ID_NOT_ALLOWED });
                context.Item().Add(new Item { ID = ITEM_PERSONAL_ONLY_ID, Description = "ITEM_PERSONAL_ONLY_ID", ItemTypeID = ITEM_TYPE_ID });
                context.Item().Add(new Item { ID = ITEM_POSSESSION_RETURNED_ONLY_ID, Description = "ITEM_POSSESSION_RETURNED_ONLY_ID", ItemTypeID = ITEM_TYPE_ID });
                context.Item().Add(new Item { ID = ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID, Description = "ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID", ItemTypeID = ITEM_TYPE_ID });

                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_ID, Description = ITEM_TYPE_DESCRIPTION });
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_ID_NOT_ALLOWED, Description = ITEM_TYPE_DESCRIPTION_NOTALLOWED });

                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_FULL_UID, ItemID = ITEM_FULL_ID, Status = UserInPossessionStatus.Picked });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_POSSESSION_ONLY_UID, ItemID = ITEM_POSSESSION_ONLY_ID, Status = UserInPossessionStatus.Picked });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_POSSESSION_RETURNED_ONLY_UID, ItemID = ITEM_POSSESSION_RETURNED_ONLY_ID, Status = UserInPossessionStatus.Returned });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_WITHOUT_ROLE_UID, ItemID = ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID, Status = UserInPossessionStatus.Picked });
                context.CTAMUserInPossession().Add(new CTAMUserInPossession { ID = new Guid(), CTAMUserUIDOut = USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED, ItemID = ITEM_POSSESSION_ONLY_ID_ITEM_TYPE_NOT_ALLOWED, Status = UserInPossessionStatus.Picked });

                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { ID = ITEM_FULL_ID, CTAMUserUID = USER_FULL_UID, ItemID = ITEM_FULL_ID });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { ID = ITEM_PERSONAL_ONLY_ID, CTAMUserUID = USER_PERSONAL_ONLY_UID, ItemID = ITEM_PERSONAL_ONLY_ID });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { ID = ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID, CTAMUserUID = USER_WITHOUT_ROLE_UID, ItemID = ITEM_PERSONAL_FOR_USER_WITHOUT_ROLE_ID });

                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_FULL_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_FULL_UID, CTAMRoleID = 2 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_POSSESSION_ONLY_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_POSSESSION_ONLY_UID, CTAMRoleID = 2 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_PERSONAL_ONLY_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_PERSONAL_ONLY_UID, CTAMRoleID = 2 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_SINGLE_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_POSSESSION_RETURNED_ONLY_UID, CTAMRoleID = 1 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_POSSESSION_RETURNED_ONLY_UID, CTAMRoleID = 2 });
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED, CTAMRoleID = 3 });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CabinetNumber = CABINET_NUMBER, CTAMRoleID = 1 });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CabinetNumber = CABINET_NUMBER, CTAMRoleID = 2 });
                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet { CabinetNumber = CABINET_NUMBER, CTAMRoleID = 3 });

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_ID, CTAMRoleID = 1 });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType { ItemTypeID = ITEM_TYPE_ID, CTAMRoleID = 2 });

                context.SaveChanges();
            }
        }

        public static readonly object[][] UserCorrectData =
        {
            new object[] { USER_FULL_UID, true, 2, 1, 1 },
            new object[] { USER_POSSESSION_ONLY_UID, true, 2, 1, 0 },
            new object[] { USER_PERSONAL_ONLY_UID, true, 2, 0, 1 },
            new object[] { USER_SINGLE_UID, true, 1, 0, 0 },
            new object[] { USER_POSSESSION_RETURNED_ONLY_UID, true, 2, 0, 0 },
            new object[] { USER_POSSESSION_ONLY_UID_ITEM_TYPE_NOT_ALLOWED, true, 1, 0, 0 },
        };

        [Theory, MemberData(nameof(UserCorrectData))]
        public async Task TestGetUserLiveSyncEnvelope(string userUID, bool userFound, int userRolesCount, int userInPossessionsCount, int userPersonalItemsCount)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { UserIDsForEnvelope = { userUID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserRoleCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(userFound, envelope.Users.Count == 1);
                Assert.Equal(userRolesCount, envelope.UserRoles.Count);
                Assert.Equal(userInPossessionsCount, envelope.UserInPossessions.Count);
                Assert.Equal(userPersonalItemsCount, envelope.UserPersonalItems.Count);
            }
        }

        public static readonly object[][] UserWrongData =
        {
            new object[] { NON_EXISTING_USER_UID },
            new object[] { USER_WITHOUT_ROLE_UID },
        };

        [Theory, MemberData(nameof(UserWrongData))]
        public async Task TestGetWrongUserLiveSyncEnvelope(string userUID)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { UserIDsForEnvelope = { userUID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserRoleCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.DoesNotContain(envelope.Users, u => u.UID == userUID); // Users will be filtered for cabinet, so without role also no user
                Assert.DoesNotContain(envelope.UserRoles, ur => ur.CTAMUserUID == userUID);
            }
        }

        public static readonly object[][] UserRoleCorrectData =
        {
            new object[] { 1, USER_FULL_UID, true, true, 1, 1 },
            new object[] { 1, USER_POSSESSION_ONLY_UID, true, true, 1, 0 },
            new object[] { 1, USER_PERSONAL_ONLY_UID, true, true, 0, 1 },
            new object[] { 1, USER_SINGLE_UID, true, true, 0, 0 },
            new object[] { 1, USER_POSSESSION_RETURNED_ONLY_UID, true, true, 0, 0 },
        };

        [Theory, MemberData(nameof(UserRoleCorrectData))]
        public async Task TestGetUserRoleLiveSyncEnvelope(int roleID, string userUID, bool userFound, bool userRoleFound, int userInPossessionsCount, int userPersonalItemsCount)
        {
            // Arrange
            var query = new GetCollectedEnvelopeQuery(new CollectedLiveSyncRequest() { UserIDsForEnvelope = { userUID } });
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserRoleCriticalDataProfile>();
                c.AddProfile<ItemCabinetCriticalDataProfile>();
            }).CreateMapper();
            var httpContextAccessor = CreateMockedHttpContextAccessor((JwtRegisteredClaimNames.Sub, CABINET_NUMBER));

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCollectedEnvelopeHandler(new LiveSyncDataManager(context), new Mock<ILogger<GetCollectedEnvelopeHandler>>().Object, mapper, httpContextAccessor, null);

                // Act
                var envelope = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(userFound, envelope.Users.Any(u => u.UID == userUID));
                Assert.Equal(userRoleFound, envelope.UserRoles.Any(ur => ur.CTAMUserUID == userUID && ur.CTAMRoleID == roleID));
                Assert.Equal(userInPossessionsCount, envelope.UserInPossessions.Count);
                Assert.Equal(userPersonalItemsCount, envelope.UserPersonalItems.Count);
            }
        }
    }
}
