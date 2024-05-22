using CabinetModule.ApplicationCore.Commands.Cabinets;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.Commands;
using ItemModule.ApplicationCore.DataManagers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class CheckAndModifyCabinetTest : AbstractIntegrationTests
    {
        public CheckAndModifyCabinetTest() : base("CTAM_CheckAndModifyCabinet")
        {
        }

        [Fact]
        public async Task TestDisableCabinetMissingRequiredItems()
        {
            //  Arrange
            var modifyCabinetCommand = new CheckAndModifyCabinetCommand()
            {
                CabinetNumber = "210309081254",
                IsActive = false
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new ItemDataManager(context, new Mock<ILogger<ItemDataManager>>().Object);

                var fakeMediator = new Mock<IMediator>();
                fakeMediator.Setup(m => m.Send(It.IsAny<ModifyCabinetCommand>(), It.IsAny<CancellationToken>()));

                var handler = new CheckAndModifyCabinetHandler(new Mock<ILogger<CheckAndModifyCabinetHandler>>().Object, context, fakeMediator.Object, dataManager);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_itemsMissing, ex.Message);
            }
        }

        [Fact]
        public async Task TestDisableCabinetWithoutMissingRequiredItems()
        {
            //  Arrange
            var modifyCabinetCommand = new CheckAndModifyCabinetCommand()
            {
                CabinetNumber = "200214160401",
                IsActive = false
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new ItemDataManager(context, new Mock<ILogger<ItemDataManager>>().Object);

                var fakeMediator = new Mock<IMediator>();
                fakeMediator.Setup(m => m.Send(It.IsAny<ModifyCabinetCommand>(), It.IsAny<CancellationToken>()));

                var handler = new CheckAndModifyCabinetHandler(new Mock<ILogger<CheckAndModifyCabinetHandler>>().Object, context, fakeMediator.Object, dataManager);

                // Act
                await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>());

                // Assert
                fakeMediator.Verify(mediator => mediator.Send(It.IsAny<ModifyCabinetCommand>(), It.IsAny<CancellationToken>()));
            }
        }
    }
}
