using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.Commands.Items;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemModule.IntegrationTests
{
    public class CreateAndRemoveItemsTest : AbstractIntegrationTests
    {
        private const int ITEM_TYPE_ID = 999;
        private const int ITEM_ID = int.MaxValue;
        private const int ITEM_ID_2 = int.MaxValue - 1;
        private const int ITEM_ID_3 = int.MaxValue - 2;
        private const string TAGNUMBER_BARCODE = "999";
        private const string TAGNUMBER_BARCODE_2 = "1000";
        private const string TAGNUMBER_BARCODE_3 = "1001";

        public CreateAndRemoveItemsTest() : base("CTAM_CreateAndRemoveItems")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ItemType().Add(new ItemType { ID = ITEM_TYPE_ID, Description = "Laptop" });
                context.Item().Add(new Item { ID = ITEM_ID_2, ItemTypeID = ITEM_TYPE_ID, Description = "laptop 2", Tagnumber = TAGNUMBER_BARCODE_2, Barcode = TAGNUMBER_BARCODE_2 });
                context.Item().Add(new Item { ID = ITEM_ID_3, ItemTypeID = ITEM_TYPE_ID, Description = "laptop 3", Tagnumber = TAGNUMBER_BARCODE_3, Barcode = TAGNUMBER_BARCODE_3 });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestCreateNewItemWithRemovedTagnumberBarcode()
        {
            // Arrange
            var createItemCommand = new CreateItemCommand()
            {
                Description = "New item to replace",
                ItemTypeID = ITEM_TYPE_ID,
                Tagnumber = TAGNUMBER_BARCODE,
                Barcode = TAGNUMBER_BARCODE,
            };


            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();
                                
                var handler = new CreateItemHandler(context, new Mock<ILogger<CreateItemHandler>>().Object, mapper, managementLogger);

                // Act
                var createdItem = await handler.Handle(createItemCommand, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(createItemCommand.Description, createdItem.Description);
                Assert.Equal(TAGNUMBER_BARCODE, createdItem.Tagnumber);
                Assert.Equal(TAGNUMBER_BARCODE, createdItem.Barcode);
            }
        }

        [Fact]
        public async Task TestCreateNewItemWithExistingTagnumber()
        {
            // Arrange
            var createItemOneCommand = new CreateItemCommand()
            {
                Description = "Item 1",
                ItemTypeID = ITEM_TYPE_ID,
                Tagnumber = TAGNUMBER_BARCODE,
            };
            var createItemTwoCommand = new CreateItemCommand()
            {
                Description = "Item 2",
                ItemTypeID = ITEM_TYPE_ID,
                Tagnumber = TAGNUMBER_BARCODE,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CreateItemHandler(context, new Mock<ILogger<CreateItemHandler>>().Object, mapper, managementLogger);

                // Act
                var createdItem = await handler.Handle(createItemOneCommand, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(createItemOneCommand.Description, createdItem.Description);
                Assert.Equal(TAGNUMBER_BARCODE, createdItem.Tagnumber);
                await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(createItemTwoCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestUpdateItemWithExistingTagnumber()
        {
            // Arrange
            var modifyItemCommand = new CheckAndModifyItemCommand()
            {
                Description = "laptop 2",
                ID = ITEM_ID_2,
                Tagnumber = TAGNUMBER_BARCODE_3,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var modifyHandler = new CheckAndModifyItemHandler(context, new Mock<ILogger<CheckAndModifyItemHandler>>().Object, mapper, managementLogger);

                // Act & Assert
                await Assert.ThrowsAsync<CustomException>(async () => await modifyHandler.Handle(modifyItemCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestCreateNewItemWithExistingBarcode()
        {
            // Arrange
            var createItemOneCommand = new CreateItemCommand()
            {
                Description = "Item 1",
                ItemTypeID = ITEM_TYPE_ID,
                Barcode = TAGNUMBER_BARCODE,
            };
            var createItemTwoCommand = new CreateItemCommand()
            {
                Description = "Item 2",
                ItemTypeID = ITEM_TYPE_ID,
                Barcode = TAGNUMBER_BARCODE,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CreateItemHandler(context, new Mock<ILogger<CreateItemHandler>>().Object, mapper, managementLogger);

                // Act
                var createdItem = await handler.Handle(createItemOneCommand, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(createItemOneCommand.Description, createdItem.Description);
                Assert.Equal(TAGNUMBER_BARCODE, createdItem.Barcode);
                await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(createItemTwoCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestUpdateItemWithExistingBarcode()
        {
            // Arrange
            var modifyItemCommand = new CheckAndModifyItemCommand()
            {
                Description = "laptop 2",
                ID = ITEM_ID_2,
                Barcode = TAGNUMBER_BARCODE_3,
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var modifyHandler = new CheckAndModifyItemHandler(context, new Mock<ILogger<CheckAndModifyItemHandler>>().Object, mapper, managementLogger);

                // Act & Assert
                await Assert.ThrowsAsync<CustomException>(async () => await modifyHandler.Handle(modifyItemCommand, It.IsAny<CancellationToken>()));
            }
        }
    }
}
