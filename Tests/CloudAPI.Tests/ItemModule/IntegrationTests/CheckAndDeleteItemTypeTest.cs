using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.Commands.ItemTypes;
using ItemModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemModule.IntegrationTests
{
    public class CheckAndDeleteItemTypeTest: AbstractIntegrationTests
    {
        private const int UNLINKED_ITEM_TYPE_ID = int.MaxValue;
        private const int LINKED_ITEM_TYPE_ID = int.MaxValue - 1;
        private const int LINKED_ITEM_ID = int.MaxValue - 1;
        private const int PREVIOUSLY_LINKED_ITEM_TYPE_ID = int.MaxValue - 2;

        public CheckAndDeleteItemTypeTest() : base("CTAM_RemoveItemType")
        {

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Add missing item type to test
                context.ItemType().Add(new ItemType { ID = UNLINKED_ITEM_TYPE_ID, Description = "UNLINKED_ITEM_TYPE" });
                context.ItemType().Add(new ItemType { ID = LINKED_ITEM_TYPE_ID, Description = "LINKED_ITEM_TYPE" });
                context.Item().Add(new Item { ID = LINKED_ITEM_ID, ItemTypeID = LINKED_ITEM_TYPE_ID, Description = "LINKED_ITEM" });
                context.ItemType().Add(new ItemType { ID = PREVIOUSLY_LINKED_ITEM_TYPE_ID, Description = "PREVIOUSLY_LINKED_ITEM_TYPE_ID" });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestRemoveNonExistingItemType()
        {
            // Arrange
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CheckAndDeleteItemTypeHandler(context, new Mock<ILogger<CheckAndDeleteItemTypeHandler>>().Object, managementLogger);
                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(new CheckAndDeleteItemTypeCommand(-1), It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.itemTypes_apiExceptions_notFound, ex.Message);
            }
        }

        [Fact]
        public async Task TestRemoveUnlinkedItemType()
        {
            // Arrange
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CheckAndDeleteItemTypeHandler(context, new Mock<ILogger<CheckAndDeleteItemTypeHandler>>().Object, managementLogger);
                // Act
                await handler.Handle(new CheckAndDeleteItemTypeCommand(UNLINKED_ITEM_TYPE_ID), It.IsAny<CancellationToken>());

                // Assert
                Assert.False(await context.ItemType().AnyAsync(itemType => itemType.ID == UNLINKED_ITEM_TYPE_ID));
            }
        }

        [Fact]
        public async Task TestRemoveLinkedItemType()
        {
            // Arrange
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CheckAndDeleteItemTypeHandler(context, new Mock<ILogger<CheckAndDeleteItemTypeHandler>>().Object, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(new CheckAndDeleteItemTypeCommand(LINKED_ITEM_TYPE_ID), It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.itemTypes_apiExceptions_itemTypeInUseByItem, ex.Message);
                Assert.True(await context.ItemType().AnyAsync(itemType => itemType.ID == LINKED_ITEM_TYPE_ID));
            }
        }

        [Fact]
        public async Task TestRemovePreviouslyLinkedItemType()
        {
            // Arrange
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CheckAndDeleteItemTypeHandler(context, new Mock<ILogger<CheckAndDeleteItemTypeHandler>>().Object, managementLogger);

                // Act
                await handler.Handle(new CheckAndDeleteItemTypeCommand(PREVIOUSLY_LINKED_ITEM_TYPE_ID), It.IsAny<CancellationToken>());

                // Assert
                Assert.False(await context.ItemType().AnyAsync(itemType => itemType.ID == PREVIOUSLY_LINKED_ITEM_TYPE_ID));
            }
        }
    }
}
