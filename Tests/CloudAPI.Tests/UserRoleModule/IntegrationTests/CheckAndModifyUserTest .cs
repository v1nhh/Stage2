using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Users;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using UserRoleModule.ApplicationCore.Queries.Users;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class CheckAndModifyUserTest : AbstractIntegrationTests
    {
        private const string TEST_USER_UID_1 = "TEST_USER_TINUS";
        private const string TEST_USER_NAME_1 = "Tinus Test";
        private const string TEST_USER_EMAIL_1 = "tinus@test.com";
        private const string TEST_USER_UID_2 = "TEST_USER_TINA";
        private const string TEST_USER_NAME_2 = "Tina Test";
        private const string TEST_USER_EMAIL_2 = "tina@test.com";

        private const string NEW_USER_NAME = "NEW_USER_NAME";
        private readonly List<int> NEW_USER_ROLES = new() { 1, 2 };
        private readonly List<int> EXISTING_USER_ROLES = new() { 3, 4 };

        private const int TEST_ITEMTYPE = 9000;

        private const int TEST_ITEM_STATUS_ACTIVE_ID_1 = int.MaxValue;
        private const int TEST_ITEM_STATUS_INITIAL_ID_2 = 2147483646;
        private const int TEST_ITEM_STATUS_ACTIVE_ID_3 = 2147483645;
        private const int TEST_ITEM_STATUS_INITIAL_ID_4 = 2147483644;
        private const int TEST_ITEM_STATUS_INITIAL_ID_5 = 2147483643;
        private const int TEST_ITEM_STATUS_INITIAL_ID_6 = 2147483642;
        private const int TEST_ITEM_STATUS_INITIAL_ID_7 = 2147483641;
        private const int TEST_ITEM_STATUS_INITIAL_ID_8 = 2147483640;
        private const int TEST_ITEM_STATUS_INITIAL_ID_9 = 2147483639;
        private const int TEST_ITEM_STATUS_INITIAL_ID_10 = 2147483638;
        private const int TEST_ITEM_STATUS_INITIAL_ID_11 = 2147483637;
        private const int TEST_ITEM_STATUS_INITIAL_ID_12 = 2147483636;
        private const int TEST_ITEM_STATUS_INITIAL_ID_13 = 2147483635;
        private const int TEST_ITEM_STATUS_INITIAL_ID_14 = 2147483634;
        private const int NON_EXISTING_TEST_ITEM = 0;

        private const string TEST_ITEM_STATUS_ACTIVE_DESC = "Item with status ACTIVE";
        private const string TEST_ITEM_STATUS_INITIAL_DESC = "Item with status INITIAL";

        public CheckAndModifyUserTest() : base("CTAM_ModifyRole")
        {

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Add User(s)
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_1, Name = TEST_USER_NAME_1, Email = TEST_USER_EMAIL_1, LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_2, Name = TEST_USER_NAME_2, Email = TEST_USER_EMAIL_2, LanguageCode = "nl-NL" });

                // Add Roles
                EXISTING_USER_ROLES.ForEach(roleID =>
                    context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = TEST_USER_UID_1, CTAMRoleID = roleID }));

                // Add Item(s)

                // Not assigned, Active
                var itemWithStatusActiveNotAssigned = new Item() { ID = TEST_ITEM_STATUS_ACTIVE_ID_1, Description = TEST_ITEM_STATUS_ACTIVE_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.ACTIVE };
                context.Item().Add(itemWithStatusActiveNotAssigned);

                // Not assigned, Initial
                var itemWithStatusInitialNotAssigned = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_4, Description = TEST_ITEM_STATUS_ACTIVE_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned);

                var itemWithStatusInitialNotAssigned2 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_6, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned2);

                var itemWithStatusInitialNotAssigned3 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_8, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned3);

                var itemWithStatusInitialNotAssigned4 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_9, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned4);

                var itemWithStatusInitialNotAssigned5 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_10, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned5);

                var itemWithStatusInitialNotAssigned6 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_12, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssigned6);

                var itemWithStatusInitialNotAssignedTestMultiple = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_13, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialNotAssignedTestMultiple);

                // Assigned, Initial
                var itemWithStatusInitialAssigned = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_2, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialAssigned);

                var itemWithStatusInitialAssigned2 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_5, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialAssigned2);

                var itemWithStatusInitialAssigned3 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_7, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialAssigned3);

                var itemWithStatusInitialAssigned4 = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_11, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialAssigned4);

                var itemWithStatusInitialAssignedTestMultiple = new Item() { ID = TEST_ITEM_STATUS_INITIAL_ID_14, Description = TEST_ITEM_STATUS_INITIAL_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.INITIAL };
                context.Item().Add(itemWithStatusInitialAssignedTestMultiple);

                // Assigned, Active
                var itemWithStatusActiveAlreadyAssigned = new Item() { ID = TEST_ITEM_STATUS_ACTIVE_ID_3, Description = TEST_ITEM_STATUS_ACTIVE_DESC, ItemTypeID = TEST_ITEMTYPE, Status = ItemStatus.ACTIVE };
                context.Item().Add(itemWithStatusActiveAlreadyAssigned);

                // Add Personal Items
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { CTAMUserUID = TEST_USER_UID_1, ItemID = TEST_ITEM_STATUS_INITIAL_ID_2 });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { CTAMUserUID = TEST_USER_UID_2, ItemID = TEST_ITEM_STATUS_ACTIVE_ID_3 });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { CTAMUserUID = TEST_USER_UID_2, ItemID = TEST_ITEM_STATUS_INITIAL_ID_5 });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { CTAMUserUID = TEST_USER_UID_1, ItemID = TEST_ITEM_STATUS_INITIAL_ID_7 });
                context.CTAMUserPersonalItem().Add(new CTAMUserPersonalItem { CTAMUserUID = TEST_USER_UID_1, ItemID = TEST_ITEM_STATUS_INITIAL_ID_14 });

                context.SaveChangesAsync();
            }
        }

        public static readonly object[][] modifyUserTestCases =
        {
            // Add single (wrong) item exception flows
            new object[] { TEST_USER_UID_2, new int[] { NON_EXISTING_TEST_ITEM }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_addPersonalItemNotFound },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_1 }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_addPersonalItemStatusIncorrect },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2 }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_personalItemAlreadyInUse},

            // Remove single (wrong) item exception flow
            new object[] { TEST_USER_UID_2, Array.Empty<int>(), new int[] { TEST_ITEM_STATUS_ACTIVE_ID_3 }, CloudTranslations.users_apiExceptions_removePersonalItemStatusIncorrect }, 
           
            //  Add single (wrong) item and remove single (right) item exception flows
            new object[] { TEST_USER_UID_1, new int[] { NON_EXISTING_TEST_ITEM }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2 }, CloudTranslations.users_apiExceptions_addPersonalItemNotFound },
            new object[] { TEST_USER_UID_1, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_1 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2 }, CloudTranslations.users_apiExceptions_addPersonalItemStatusIncorrect },
            new object[] { TEST_USER_UID_1, new int[] { TEST_ITEM_STATUS_INITIAL_ID_5 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2 }, CloudTranslations.users_apiExceptions_personalItemAlreadyInUse }, 

            // Add single (right) item and remove single (wrong) item exception flow
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_INITIAL_ID_4 }, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_3 }, CloudTranslations.users_apiExceptions_removePersonalItemStatusIncorrect }, 
            
            // Add multiple items (1 of which is wrong) exception flows
            new object[] { TEST_USER_UID_2, new int[] { NON_EXISTING_TEST_ITEM, TEST_ITEM_STATUS_INITIAL_ID_13 }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_addPersonalItemNotFound },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_1, TEST_ITEM_STATUS_INITIAL_ID_13 }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_addPersonalItemStatusIncorrect },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2, TEST_ITEM_STATUS_INITIAL_ID_13 }, Array.Empty<int>(), CloudTranslations.users_apiExceptions_personalItemAlreadyInUse },

            // Remove multiple items (1 of which is wrong) exception flow
            new object[] { TEST_USER_UID_2, Array.Empty<int>(), new int[] { TEST_ITEM_STATUS_ACTIVE_ID_3, TEST_ITEM_STATUS_INITIAL_ID_2 }, CloudTranslations.users_apiExceptions_removePersonalItemStatusIncorrect }, 

            // Add multiple items (1 of which is wrong) and remove multiple (right) items exception flows
            new object[] { TEST_USER_UID_2, new int[] { NON_EXISTING_TEST_ITEM, TEST_ITEM_STATUS_INITIAL_ID_13 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2, TEST_ITEM_STATUS_INITIAL_ID_14 }, CloudTranslations.users_apiExceptions_addPersonalItemNotFound },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_1, TEST_ITEM_STATUS_INITIAL_ID_13 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2, TEST_ITEM_STATUS_INITIAL_ID_14 }, CloudTranslations.users_apiExceptions_addPersonalItemStatusIncorrect },
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2, TEST_ITEM_STATUS_INITIAL_ID_13 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_2, TEST_ITEM_STATUS_INITIAL_ID_14 }, CloudTranslations.users_apiExceptions_personalItemAlreadyInUse},

            // Add multiple (right) items and remove multiple items (1 of which is wrong) exception flows
            new object[] { TEST_USER_UID_2, new int[] { TEST_ITEM_STATUS_INITIAL_ID_13, TEST_ITEM_STATUS_INITIAL_ID_6, TEST_ITEM_STATUS_INITIAL_ID_5 }, new int[] { TEST_ITEM_STATUS_ACTIVE_ID_3,  }, CloudTranslations.users_apiExceptions_removePersonalItemStatusIncorrect },

            // Happy flow add or remove single item
            new object[] { TEST_USER_UID_1, Array.Empty<int>(), new int[] { TEST_ITEM_STATUS_INITIAL_ID_2 }, null }, 
            new object[] { TEST_USER_UID_1, new int[] { TEST_ITEM_STATUS_INITIAL_ID_6 }, Array.Empty<int>(), null }, 

            // Happy flow add and remove single item
            new object[] { TEST_USER_UID_1, new int[] { TEST_ITEM_STATUS_INITIAL_ID_8 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_7 }, null }, 

            // Happy flow add and remove multiple items
            new object[] { TEST_USER_UID_1, new int[] { TEST_ITEM_STATUS_INITIAL_ID_8, TEST_ITEM_STATUS_INITIAL_ID_12 }, new int[] { TEST_ITEM_STATUS_INITIAL_ID_7, TEST_ITEM_STATUS_INITIAL_ID_11 }, null },
            new object[] { TEST_USER_UID_1, new int[] {TEST_ITEM_STATUS_INITIAL_ID_9, TEST_ITEM_STATUS_INITIAL_ID_10} , Array.Empty<int>(), null }, 
        };

        [Theory, MemberData(nameof(modifyUserTestCases))]
        public async Task TestModifyUserPersonalItems(string uid, int[] addItemIDs, int[] removeItemIDs, string exceptionMessage )
        {
            //  Arrange
            var command = new CheckAndModifyUserCommand()
            {
                UID = uid,
                AddPersonalItemIDs = new List<int>(addItemIDs),
                RemovePersonalItemIDs = new List<int>(removeItemIDs)
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateModifyHandler(context);

                // Act & Assert
                if(exceptionMessage != null)
                {
                    var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(command, It.IsAny<CancellationToken>()));
                    Assert.Equal(exceptionMessage, ex.Message);
                }
                else
                {
                    var ex = await handler.Handle(command, It.IsAny<CancellationToken>());
                    var resultAdded = await context.CTAMUserPersonalItem().Where(upi => addItemIDs.Contains(upi.ItemID)).Select(upi => upi.ItemID).ToListAsync();
                    var resultRemoved = await context.CTAMUserPersonalItem().Where(upi => removeItemIDs.Contains(upi.ItemID)).Select(upi => upi.ItemID).ToListAsync();
                    Assert.True(!addItemIDs.Except(resultAdded).Any());
                    Assert.True(!resultRemoved.Any());
                }
            }
        }

        [Fact]
        public async Task TestReplaceExistingUserWithRoles()
        {
            // Arrange
            var command = new CheckAndModifyUserCommand()
            {
                UID = TEST_USER_UID_1,
                Name = NEW_USER_NAME,
                AddRolesIDs = NEW_USER_ROLES
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateModifyHandler(context);

                // Act
                await handler.Handle(command, It.IsAny<CancellationToken>());

                var result = await context.CTAMUser()
                    .AsNoTracking()
                    .Where(u => u.Name.Equals(TEST_USER_NAME_1))
                    .FirstOrDefaultAsync();

                // Assert
                Assert.Null(result);
                Assert.NotNull(context.CTAMUser_Role().Find(TEST_USER_UID_1, EXISTING_USER_ROLES[0]));
                Assert.NotNull(context.CTAMUser_Role().Find(TEST_USER_UID_1, EXISTING_USER_ROLES[1]));
                Assert.Equal(NEW_USER_NAME, context.CTAMUser().Find(TEST_USER_UID_1).Name);
                Assert.NotNull(context.CTAMUser_Role().Find(TEST_USER_UID_1, NEW_USER_ROLES[0]));
                Assert.NotNull(context.CTAMUser_Role().Find(TEST_USER_UID_1, NEW_USER_ROLES[1]));
            }
        }

        [Fact]
        public async Task TestReplaceExistingUserWithRolesWithUserWithNoRoles()
        {
            // Arrange // empty list should replace existing roles
            var command = new CheckAndModifyUserCommand()
            {
                UID = TEST_USER_UID_1,
                Name = NEW_USER_NAME,
                RemoveRolesIDs = EXISTING_USER_ROLES
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateModifyHandler(context);

                // Act
                await handler.Handle(command, It.IsAny<CancellationToken>());

                var userResult = await context.CTAMUser()
                    .AsNoTracking()
                    .Where(u => u.Name.Equals(TEST_USER_NAME_1))
                    .FirstOrDefaultAsync();

                var rolesResult = await context.CTAMUser_Role()
                    .AsNoTracking()
                    .Where(u => u.CTAMUserUID.Equals(TEST_USER_UID_1))
                    .FirstOrDefaultAsync();

                // Assert
                Assert.Null(userResult);
                Assert.Null(rolesResult);
                Assert.Equal(NEW_USER_NAME, context.CTAMUser().Find(TEST_USER_UID_1).Name);
            }
        }

        [Fact]
        public async Task TestReplaceExistingUserWithRolesNotReplacingRoles()
        {
            // Arrange // no roles list should not change existing roles
            var command = new CheckAndModifyUserCommand()
            {
                UID = TEST_USER_UID_1,
                Name = NEW_USER_NAME,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateModifyHandler(context);

                // Act
                await handler.Handle(command, It.IsAny<CancellationToken>());

                var userResult = await context.CTAMUser()
                    .AsNoTracking()
                    .Where(u => u.Name.Equals(TEST_USER_NAME_1))
                    .FirstOrDefaultAsync();

                var rolesResult = await context.CTAMUser_Role()
                    .AsNoTracking()
                    .Where(u => u.CTAMUserUID.Equals(TEST_USER_UID_1))
                    .ToListAsync();

                // Assert
                Assert.Null(userResult);
                Assert.Equal(2, rolesResult.Count);
                Assert.Equal(NEW_USER_NAME, context.CTAMUser().Find(TEST_USER_UID_1).Name);
            }
        }

        private CheckAndModifyUserHandler CreateModifyHandler(MainDbContext context)
        {
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
                c.AddProfile<RoleProfile>();
                c.AddProfile<PermissionProfile>();
            }).CreateMapper();

            var managementLogger = CreateMockedManagementLogger();

            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<CheckRoleItemTypeDependencyQuery>(), It.IsAny<CancellationToken>()))
                .Returns((CheckRoleItemTypeDependencyQuery cmd, CancellationToken token) =>
                            new CheckRoleItemTypeDependencyHandler(context, new Mock<ILogger<CheckRoleItemTypeDependencyHandler>>().Object).Handle(cmd, token));
            mediator.Setup(m => m.Send(It.IsAny<GetUserByUidQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetUserByUidQuery cmd, CancellationToken token) =>
                            new GetUserByUidHandler(context, new Mock<ILogger<GetUserByUidHandler>>().Object, mapper).Handle(cmd, token));

            var handler = new CheckAndModifyUserHandler(
                new Mock<ILogger<CheckAndModifyUserHandler>>().Object,
                context,
                mediator.Object,
                mapper,
                managementLogger);

            return handler;
        }
    }
}
