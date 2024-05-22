using AutoMapper;
using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Profiles;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CloudAPI.ApplicationCore.Commands.Roles;
using CloudApiModule.ApplicationCore.DataManagers;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using ItemCabinetModule.ApplicationCore.Commands;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.DataManagers;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Profiles;
using ItemModule.ApplicationCore.Queries.ItemTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Profiles;
using UserRoleModule.ApplicationCore.Queries.Users;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class ModifyRoleTest : AbstractIntegrationTests
    {
        private const string TEST_USER_UID_1 = "TEST_USER_TINUS";
        private const string TEST_USER_UID_2 = "TEST_USER_TINA";
        private const string NEW_DESCRIPTION = "NEW DESCRIPTION";
        private const int TEST_ROLE_ID_1 = 100;
        private const int TEST_ROLE_ID_2 = 101;
        private const int TEST_PERMISSION_ID_1 = 201;
        private const int TEST_PERMISSION_ID_2 = 202;
        private int TEST_PERMISSION_ID_PICKUP;
        private int TEST_PERMISSION_ID_RETURN;
        readonly static string TEST_CABA1_CABINETNUMBER = "210809095541";

        private const int TEST_ITEMTYPE_ID_1 = 9001;
        private const string TEST_ITEMTYPE_DESC_1 = "Itemtype1";

        private const int TEST_ITEM_ID_1 = int.MaxValue;
        private const string TEST_ITEM_DESC_1 = "Item1 of type 1";
        private Guid TEST_USER_IN_POSSESSION_ID_1 = Guid.NewGuid();

        public ModifyRoleTest() : base("CTAM_ModifyRole")
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
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_1, Name = "Tinus Test", Email = "test@test.com", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = TEST_USER_UID_2, Name = "Tina Test", Email = "test@test.com", LanguageCode = "nl-NL" });

                // Add user role
                context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = TEST_USER_UID_2, CTAMRoleID = TEST_ROLE_ID_2 });

                // Add ItemType
                var itemType1 = new ItemType() { ID = TEST_ITEMTYPE_ID_1, Description = TEST_ITEMTYPE_DESC_1 };
                context.ItemType().Add(itemType1);

                // Add Item
                var item1 = new Item() { ID = TEST_ITEM_ID_1, Description = TEST_ITEM_DESC_1, ItemTypeID = TEST_ITEMTYPE_ID_1 };
                context.Item().Add(item1);

                // Add cabinet
                var cabA1 = new Cabinet { Name = "A1", CabinetType = CabinetType.Locker, Description = "Zie A1" };
                cabA1.CabinetNumber = TEST_CABA1_CABINETNUMBER;
                context.Cabinet().Add(cabA1);

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestRoleIDIsZeroOrNegativeShouldThrow()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = -1,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_notFound, ex.Message);
            }
        }

        [Fact]
        public async Task TestModifySingleFieldsOfRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                Description = NEW_DESCRIPTION,
                ValidFromDT = "2022-02-18T12:52",
                ValidUntilDT = "2022-02-22T12:52"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                var role = context.CTAMRole().Find(TEST_ROLE_ID_1);
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                Assert.Equal(NEW_DESCRIPTION, role.Description);
                Assert.NotNull(role.ValidFromDT);
                Assert.NotNull(role.ValidUntilDT);
                if (DateTime.TryParse(modifyRoleCommand.ValidFromDT, out DateTime dt))
                {
                    Assert.Equal(dt, role.ValidFromDT);
                }
                if (DateTime.TryParse(modifyRoleCommand.ValidUntilDT, out DateTime dt2))
                {
                    Assert.Equal(dt2, role.ValidUntilDT);
                }
            }
        }

        [Fact]
        public async Task TestAddPersmissionToRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddPermissionIDs = new List<int>() { TEST_PERMISSION_ID_1, TEST_PERMISSION_ID_2 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                var role = await context.CTAMRole().FindAsync(TEST_ROLE_ID_1);
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                var rolePermision = await context.CTAMRole_Permission().FindAsync(TEST_ROLE_ID_1, TEST_PERMISSION_ID_1);
                Assert.NotNull(rolePermision);

                rolePermision = await context.CTAMRole_Permission().FindAsync(TEST_ROLE_ID_1, TEST_PERMISSION_ID_2);
                Assert.NotNull(rolePermision);
            }
        }

        [Fact]
        public async Task TestRemovePersmissionFromRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemovePermissionIDs = new List<int>() { TEST_PERMISSION_ID_PICKUP, TEST_PERMISSION_ID_RETURN }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                var role = await context.CTAMRole().FindAsync(TEST_ROLE_ID_1);
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                var rolePermision = await context.CTAMRole_Permission().FindAsync(TEST_ROLE_ID_1, TEST_PERMISSION_ID_PICKUP);
                Assert.Null(rolePermision);

                rolePermision = await context.CTAMRole_Permission().FindAsync(TEST_ROLE_ID_1, TEST_PERMISSION_ID_RETURN);
                Assert.Null(rolePermision);
            }
        }

        [Fact]
        public async Task TestAddUserToRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddUserUIDs = new List<string>() { TEST_USER_UID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                var role = await context.CTAMRole().FindAsync(TEST_ROLE_ID_1);
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                var userRole = await context.CTAMUser_Role().FindAsync(TEST_USER_UID_1, TEST_ROLE_ID_1);
                Assert.NotNull(userRole);
            }
        }

        [Fact]
        public async Task TestRemoveUserFromRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_2,
                RemoveUserUIDs = new List<string>() { TEST_USER_UID_2 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new Mock<IMapper>().Object;

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                var role = await context.CTAMRole().FindAsync(TEST_ROLE_ID_2);
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                var userRole = await context.CTAMUser_Role().FindAsync(TEST_USER_UID_2, TEST_ROLE_ID_2);
                Assert.Null(userRole);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRole()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.NotNull(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleInTwoSteps()
        {
            //  Arrange
            var modifyRoleCommand1 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
            };
            var modifyRoleCommand2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand1, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.NotNull(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToTwoRoles()
        {
            //  Arrange
            var modifyRoleCommand1 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommand2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_2,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommandRemove1 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemoveCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
            };
            var modifyRoleCommandRemove2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_2,
                RemoveItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand1, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());

                //Assert
                var numberOfStocks = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).CountAsync();
                Assert.Equal(1, numberOfStocks);

                // Act
                await handler.Handle(modifyRoleCommandRemove1, It.IsAny<CancellationToken>());

                //Assert
                numberOfStocks = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).CountAsync();
                Assert.Equal(1, numberOfStocks);

                // Act
                await handler.Handle(modifyRoleCommandRemove2, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.Null(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleInTwoStepsReversed()
        {
            //  Arrange
            var modifyRoleCommand1 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommand2 = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand1, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.NotNull(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleAndRemoveCabinet()
        {
            //  Arrange
            var modifyRoleCommandAdd = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommandRemove = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemoveCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommandAdd, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommandRemove, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.Null(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleAndRemoveItemType()
        {
            //  Arrange
            var modifyRoleCommandAdd = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommandRemove = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemoveItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommandAdd, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommandRemove, It.IsAny<CancellationToken>());

                //Assert
                var stock = await context.CabinetStock().AsNoTracking().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                Assert.Null(stock);
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeToRoleSetStockAndTryToRemoveShouldThrow()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                AddCabinetNumbers = new List<string>() { TEST_CABA1_CABINETNUMBER },
                AddItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };
            var modifyRoleCommandRemove = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemoveItemTypeIDs = new List<int>() { TEST_ITEMTYPE_ID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());
                var stock = await context.CabinetStock().Where(cs => cs.CabinetNumber == TEST_CABA1_CABINETNUMBER && cs.ItemTypeID == TEST_ITEMTYPE_ID_1).SingleOrDefaultAsync();
                stock.ActualStock = 1;
                await context.SaveChangesAsync();

                //Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyRoleCommandRemove, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestAddCabinetAndItemTypeAndUserToTwoRolesSetPossessionAndTryToRemoveUser()
        {
            //  Arrange
            var modifyRoleCommand1 = new CheckAndModifyRoleCommand()
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
            var modifyRoleCommandRemove = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                RemoveUserUIDs = new List<string>() { TEST_USER_UID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                IMapper mapper = CreateCheckAndModifyRoleMapper();
                var managementLogger = CreateMockedManagementLogger();
                var mediator = new Mock<IMediator>();
                CheckAndModifyRoleHandler handler = CreateCheckAndModifyRoleHandler(context, mapper, mediator, managementLogger);

                // Act
                await handler.Handle(modifyRoleCommand1, It.IsAny<CancellationToken>());
                await handler.Handle(modifyRoleCommand2, It.IsAny<CancellationToken>());
                var poss = new CTAMUserInPossession() { ID = TEST_USER_IN_POSSESSION_ID_1, ItemID = TEST_ITEM_ID_1, CTAMUserUIDOut = TEST_USER_UID_1, Status = UserInPossessionStatus.Picked };
                await context.CTAMUserInPossession().AddAsync(poss);
                await context.SaveChangesAsync();

                //Assert
                var role = await handler.Handle(modifyRoleCommandRemove, It.IsAny<CancellationToken>());
                Assert.NotNull(role);
            }
        }

        public static CheckAndModifyRoleHandler CreateCheckAndModifyRoleHandler(MainDbContext context, IMapper mapper, Mock<IMediator> mediator, IManagementLogger managementLogger)
        {
            var itemcabinetManager = new ItemCabinetDataManager(context, new Mock<ILogger<ItemCabinetDataManager>>().Object);
            var itemManager = new ItemDataManager(context, new Mock<ILogger<ItemDataManager>>().Object);
            var userRoleManager = new UserRoleDataManager(context, new Mock<ILogger<UserRoleDataManager>>().Object);
            var cabinetManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
            var cloudManager = new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger);
            mediator.Setup(m => m.Send(It.IsAny<GetUserByUidQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetUserByUidQuery cmd, CancellationToken token) => new GetUserByUidHandler(context, new Mock<ILogger<GetUserByUidHandler>>().Object, mapper).Handle(cmd, token));

            // In CheckAndModifyCabinetStockCommand:
            mediator.Setup(m => m.Send(It.IsAny<AddCabinetStockCommand>(), It.IsAny<CancellationToken>()))
                .Returns((AddCabinetStockCommand cmd, CancellationToken token) => new AddCabinetStockHandler(new Mock<ILogger<AddCabinetStockHandler>>().Object, context).Handle(cmd, token));
            mediator.Setup(m => m.Send(It.IsAny<RemoveCabinetStockCommand>(), It.IsAny<CancellationToken>()))
                .Returns((RemoveCabinetStockCommand cmd, CancellationToken token) => new RemoveCabinetStockHandler(new Mock<ILogger<RemoveCabinetStockHandler>>().Object, context).Handle(cmd, token));
            // In GetItemCabinetRoleByIdQuery:
            mediator.Setup(m => m.Send(It.IsAny<GetItemTypesByRoleIdsQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetItemTypesByRoleIdsQuery cmd, CancellationToken token) => new GetItemTypesByRoleIdsHandler(context, new Mock<ILogger<GetItemTypesByRoleIdsHandler>>().Object, mapper).Handle(cmd, token));
            mediator.Setup(m => m.Send(It.IsAny<GetItemTypesMaxQtyByRoleIdsQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetItemTypesMaxQtyByRoleIdsQuery cmd, CancellationToken token) => new GetItemTypesMaxQtyByRoleIdsHandler(context, new Mock<ILogger<GetItemTypesMaxQtyByRoleIdsHandler>>().Object, mapper).Handle(cmd, token));
            mediator.Setup(m => m.Send(It.IsAny<GetCabinetsByRoleIdsQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetCabinetsByRoleIdsQuery cmd, CancellationToken token) => new GetCabinetsByRoleIdsHandler(context, new Mock<ILogger<GetCabinetsByRoleIdsHandler>>().Object, mapper).Handle(cmd, token));
            mediator.Setup(m => m.Send(It.IsAny<GetCabinetAccessIntervalsByRoleIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns((GetCabinetAccessIntervalsByRoleIdQuery cmd, CancellationToken token) => new GetCabinetAccessIntervalsByRoleIdHandler(new Mock<ILogger<GetCabinetAccessIntervalsByRoleIdHandler>>().Object, context, mediator.Object, mapper).Handle(cmd, token));


            var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper, cloudManager);
            return handler;
        }

        public static IMapper CreateCheckAndModifyRoleMapper()
        {
            return new MapperConfiguration(c =>
            {
                c.AddProfile<RoleProfile>();
                c.AddProfile<PermissionProfile>();
                c.AddProfile<ItemTypeProfile>();
                c.AddProfile<RoleItemTypeMaxQtyProfile>();
                c.AddProfile<CabinetProfile>();
                c.AddProfile<UserProfile>();
            }).CreateMapper();
        }

        [Fact]
        public async Task TestModifyRoleAllDataCorrect()
        {
            //  Arrange
            var modifyRoleCommand = new CheckAndModifyRoleCommand()
            {
                ID = TEST_ROLE_ID_1,
                Description = NEW_DESCRIPTION,
                AddUserUIDs = new List<string>() { TEST_USER_UID_1 }
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler

                var mapper = new MapperConfiguration(c => { c.AddProfile<RoleProfile>();
                                                            c.AddProfile<PermissionProfile>(); }).CreateMapper();
                                                            
                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndModifyRoleHandler(new Mock<ILogger<CheckAndModifyRoleHandler>>().Object, mapper,
                                                            new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act 
                var result = await handler.Handle(modifyRoleCommand, It.IsAny<CancellationToken>());
                
                // Assert
                Assert.NotNull(result);
                Assert.Equal(NEW_DESCRIPTION, result.Description);

                var userRole = await context.CTAMUser_Role().FindAsync(TEST_USER_UID_1, TEST_ROLE_ID_1);
                Assert.NotNull(userRole);
            }
        }


    }
}
