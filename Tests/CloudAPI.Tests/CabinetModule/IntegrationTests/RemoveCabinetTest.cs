using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Commands.Cabinet;
using CloudAPI.Web.Controllers;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ReservationModule.ApplicationCore.DataManagers;
using CTAMSharedLibrary.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.CabinetModule.IntegrationTests
{
    public class RemoveCabinetTest: AbstractIntegrationTests
    {
        private const string CABINET_NUMBER_OFFLINE_ACTIVE = "CABINET_NUMBER_OFFLINE_ACTIVE";
        private const string CABINET_NUMBER_ONLINE_INACTIVE = "CABINET_NUMBER_ONLINE_INACTIVE";

        public RemoveCabinetTest() : base("CTAM_RemoveCabinet")
        {
            using(var context = new MainDbContext(ContextOptions, null))
            {
                // Add missing cabinet to test
                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER_OFFLINE_ACTIVE, Status = CabinetStatus.Offline, IsActive = true, Name = "Cabinet offline active"});
                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER_ONLINE_INACTIVE, Status = CabinetStatus.Online, IsActive = false, Name = "Cabinet online inactive"});
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestRemoveOfflineCabinet()
        {
            //  Arrange
            var removeCabinetTilburgCommand = new RemoveCabinetCommand("190404194045");

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var cabinetDataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var reservationDataManager = new ReservationDataManager(context, new Mock<ILogger<ReservationDataManager>>().Object);
                var fakemanagementLogger = CreateMockedManagementLogger();
                var handler = new RemoveCabinetHandler(new Mock<ILogger<RemoveCabinetHandler>>().Object, context, cabinetDataManager, reservationDataManager, fakemanagementLogger);

                // Arrange controller
                var fakeMediator = new Mock<IMediator>();
                fakeMediator.Setup(m => m.Send(removeCabinetTilburgCommand, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(await handler.Handle(removeCabinetTilburgCommand, It.IsAny<CancellationToken>()));
                var controller = new CabinetsController(new Mock<ILogger<CabinetsController>>().Object, fakeMediator.Object);

                // Act
                var actualResult = await controller.DeleteCabinet(removeCabinetTilburgCommand.CabinetNumber);

                var removedCabinetSearchResult = await context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(removeCabinetTilburgCommand.CabinetNumber))
                    .FirstOrDefaultAsync();

                // Assert
                Assert.IsType<OkResult>(actualResult);
                Assert.Null(removedCabinetSearchResult);
            }
        }

        [Fact]
        public async Task TestRemoveOnlineCabinet()
        {
            //  Arrange
            var removeCabinetNieuwVennepCommand = new RemoveCabinetCommand("210309081254");

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var cabinetDataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var reservationDataManager = new ReservationDataManager(context, new Mock<ILogger<ReservationDataManager>>().Object);
                var fakemanagementLogger = CreateMockedManagementLogger();
                var handler = new RemoveCabinetHandler(new Mock<ILogger<RemoveCabinetHandler>>().Object, context, cabinetDataManager, reservationDataManager, fakemanagementLogger);

                // Act
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(removeCabinetNieuwVennepCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_CabinetActiveOrNotOfflineAndNotInitialAndNotOnline, ex.Message);

                var cabinetSearchResult = await context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(removeCabinetNieuwVennepCommand.CabinetNumber))
                    .FirstOrDefaultAsync();

                // Assert
                Assert.NotNull(cabinetSearchResult);
            }
        }

        [Fact]
        public async Task TestRemoveOfflineActiveCabinet()
        {
            //  Arrange
            var removeCabinetCommand = new RemoveCabinetCommand(CABINET_NUMBER_OFFLINE_ACTIVE);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var cabinetDataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var reservationDataManager = new ReservationDataManager(context, new Mock<ILogger<ReservationDataManager>>().Object);
                var fakemanagementLogger = CreateMockedManagementLogger();
                var handler = new RemoveCabinetHandler(new Mock<ILogger<RemoveCabinetHandler>>().Object, context, cabinetDataManager, reservationDataManager, fakemanagementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(removeCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_CabinetActiveOrNotOfflineAndNotInitialAndNotOnline, ex.Message);

                var cabinetSearchResult = context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(removeCabinetCommand.CabinetNumber))
                    .FirstOrDefault();

                Assert.NotNull(cabinetSearchResult);
            }
        }
    }
}
