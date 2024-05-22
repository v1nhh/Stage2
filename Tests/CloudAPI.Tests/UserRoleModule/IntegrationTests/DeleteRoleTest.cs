using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Commands.Roles;
using CloudApiModule.ApplicationCore.DataManagers;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class DeleteRoleTest : AbstractIntegrationTests
    {
        private const string TEST_USER_UID_1 = "TEST_USER_TINUS";
        private const string TEST_USER_UID_2 = "TEST_USER_TINA";
        private const string NEW_DESCRIPTION = "NEW DESCRIPTION";
        private const int TEST_ROLE_ID_1 = 100;
        private const int TEST_ROLE_ID_2 = 101;
        private int TEST_PERMISSION_ID_PICKUP;
        private int TEST_PERMISSION_ID_RETURN;
        readonly static string TEST_CABA1_CABINETNUMBER = "210809095541";
        readonly static string TEST_CABA2_CABINETNUMBER = "210809095542";

        private const int TEST_ITEMTYPE_ID_1 = 9001;
        private const string TEST_ITEMTYPE_DESC_1 = "Itemtype1";
        private const int TEST_ITEMTYPE_ID_2 = 9002;
        private const string TEST_ITEMTYPE_DESC_2 = "Itemtype2";

        private const int TEST_ITEM_ID_1 = int.MaxValue;
        private const string TEST_ITEM_DESC_1 = "Item1 of type 1";
        private Guid TEST_USER_IN_POSSESSION_ID_1 = Guid.NewGuid();

        public DeleteRoleTest() : base("CTAM_DeleteRole")
        {

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Add Roles
                context.CTAMRole().Add(new CTAMRole { ID = TEST_ROLE_ID_1, Description = "TEST ROLE 1" });
                context.CTAMRole().Add(new CTAMRole { ID = TEST_ROLE_ID_2, Description = "TEST ROLE 2" });

                // Get Permissions
                TEST_PERMISSION_ID_PICKUP = context.CTAMPermission().Where(p => p.Description.Equals("Pickup")).Select(p => p.ID).Single();
                TEST_PERMISSION_ID_RETURN = context.CTAMPermission().Where(p => p.Description.Equals("Return")).Select(p => p.ID).Single();

                // Add Role permission
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = TEST_ROLE_ID_1, CTAMPermissionID = TEST_PERMISSION_ID_PICKUP });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = TEST_ROLE_ID_1, CTAMPermissionID = TEST_PERMISSION_ID_RETURN });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = TEST_ROLE_ID_2, CTAMPermissionID = TEST_PERMISSION_ID_PICKUP });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = TEST_ROLE_ID_2, CTAMPermissionID = TEST_PERMISSION_ID_RETURN });

                // Add User
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_1, Name = "Tinus Test", Email = "test@test.com", LanguageCode = "nl-NL"});
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_2, Name = "Tina Test", Email = "test@test.com", LanguageCode = "nl-NL"});

                // Add user role
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = TEST_USER_UID_2, CTAMRoleID = TEST_ROLE_ID_2 });

                // Add ItemType
                var itemType1 = new ItemType() { ID = TEST_ITEMTYPE_ID_1, Description = TEST_ITEMTYPE_DESC_1 };
                context.ItemType().Add(itemType1);
                var itemType2 = new ItemType() { ID = TEST_ITEMTYPE_ID_2, Description = TEST_ITEMTYPE_DESC_2 };
                context.ItemType().Add(itemType2);

                // Add Item
                var item1 = new Item() { ID = TEST_ITEM_ID_1, Description = TEST_ITEM_DESC_1, ItemTypeID = TEST_ITEMTYPE_ID_1 };
                context.Item().Add(item1);

                // Add cabinet
                var cabA1 = new Cabinet { Name = "A1", CabinetType = CabinetType.Locker, Description = "Zie A1" };
                cabA1.CabinetNumber = TEST_CABA1_CABINETNUMBER;
                context.Cabinet().Add(cabA1);
                var cabA2 = new Cabinet { Name = "A2", CabinetType = CabinetType.Locker, Description = "Zie A2" };
                cabA2.CabinetNumber = TEST_CABA2_CABINETNUMBER;
                context.Cabinet().Add(cabA2);

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestRoleIDIsNegative()
        {
            //  Arrange
            var deleteRoleCommand = new CheckAndDeleteRoleCommand(-1);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndDeleteRoleHandler(new Mock<ILogger<CheckAndDeleteRoleHandler>>().Object, context,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(deleteRoleCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeAndUserToTwoRoleSetPossessionAndTryToRemoveRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 },
                AddUserUIDs = new List<string>() { TEST_USER_UID_1 }
            }; 
            var modifyRoleCommand2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_2,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 },
                AddUserUIDs = new List<string>() { TEST_USER_UID_1 }
            };
            var deleteRoleCommand = new CheckAndDeleteRoleCommand(TEST_ROLE_ID_1);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper modifyMapper = ModifyRoleTest.CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler modifyHandler = ModifyRoleTest.CreateCheckAndModifyRoleHandler(context, modifyMapper, mediator, managementLogger);
                // Adapt 2 roles for user, itemtype and cabinet
                await modifyHandler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());
                await modifyHandler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());

                var deleteHandler = new CheckAndDeleteRoleHandler(new Mock<ILogger<CheckAndDeleteRoleHandler>>().Object, context,
                                                                  new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Add possession
                var poss = new CTAMUserInPossession() { ID = TEST_USER_IN_POSSESSION_ID_1, ItemID = TEST_ITEM_ID_1, CTAMUserUIDOut = TEST_USER_UID_1, Status = UserInPossessionStatus.Picked };
                await context.CTAMUserInPossession().AddAsync(poss);
                await context.SaveChangesAsync();

                // Act and Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await deleteHandler.Handle(deleteRoleCommand, It.IsAny<CancellationToken>()));
            }
        }


        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleSetActualStockAndTryToRemoveRoleShouldThrow()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 },
            };
            var deleteRoleCommand = new CheckAndDeleteRoleCommand(TEST_ROLE_ID_1);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper modifyMapper = ModifyRoleTest.CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler modifyHandler = ModifyRoleTest.CreateCheckAndModifyRoleHandler(context, modifyMapper, mediator, managementLogger);
                // Adapt role for user, itemtype and cabinet
                await modifyHandler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());
                var deleteHandler = new CheckAndDeleteRoleHandler(new Mock<ILogger<CheckAndDeleteRoleHandler>>().Object, context,
                                                                  new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Add stock
                var stock = context.CabinetStock().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).FirstOrDefault();
                stock.ActualStock = 1;
                await context.SaveChangesAsync();

                // Act and Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await deleteHandler.Handle(deleteRoleCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToTwoRoleSetActualStockAndTryToRemoveRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 },
            };
            var modifyRoleCommand2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_2,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER, TEST_CABA2_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1, TEST_ITEMTYPE_ID_2 },
            };
            var deleteRoleCommand = new CheckAndDeleteRoleCommand(TEST_ROLE_ID_1);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper modifyMapper = ModifyRoleTest.CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler modifyHandler = ModifyRoleTest.CreateCheckAndModifyRoleHandler(context, modifyMapper, mediator, managementLogger);
                // Adapt 2 roles for user, itemtype and cabinet
                await modifyHandler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());
                await modifyHandler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());

                var deleteHandler = new CheckAndDeleteRoleHandler(new Mock<ILogger<CheckAndDeleteRoleHandler>>().Object, context,
                                                                  new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Add stock
                var stock = context.CabinetStock().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).FirstOrDefault();
                stock.ActualStock = 1;
                await context.SaveChangesAsync();

                // Act and Assert
                // Role cannot be deleted because it still has cabinets
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await deleteHandler.Handle(deleteRoleCommand, It.IsAny<CancellationToken>()));
            }
        }

        private static IMapper CreateDeleteRoleMapper()
        {
            return new MapperConfiguration(c =>
            {
                c.AddProfile<RoleProfile>();
            }).CreateMapper();
        }

        [Fact]
        public async Task TestDeleteRoleCorrect()
        {
            //  Arrange
            var deleteRoleCommand = new CheckAndDeleteRoleCommand(TEST_ROLE_ID_1);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndDeleteRoleHandler(new Mock<ILogger<CheckAndDeleteRoleHandler>>().Object, context,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act 
                var result = await handler.Handle(deleteRoleCommand, It.IsAny<CancellationToken>());
                
                // Assert
                var role = await context.CTAMRole().FindAsync(TEST_ROLE_ID_1);
                Assert.Null(role);
            }
        }


    }
}
