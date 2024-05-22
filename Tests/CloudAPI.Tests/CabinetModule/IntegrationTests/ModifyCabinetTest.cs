using AutoMapper;
using CabinetModule.ApplicationCore.Commands.Cabinets;
using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Profiles;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.CabinetModule.IntegrationTests
{
    public class ModifyCabinetTest : AbstractIntegrationTests
    {
        private const string CABINET_NUMBER_DISABLED_WITH_CONFIG = "CABINET_NUMBER_DISABLED_WITH_CONFIG";

        public ModifyCabinetTest() : base("CTAM_ModifyCabinet")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Add missing cabinet to test
                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER_DISABLED_WITH_CONFIG, IsActive = false, CabinetConfiguration = "test", Name = "Cabinet disabled with config"});
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestModifyCabinetWithoutCabinetNumber()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_emptyNumber, ex.Message);
            }
        }

        [Fact]
        public async Task TestModifyCabinetWithOnlyCabinetNumber()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = "210103102111"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_noData, ex.Message);
            }
        }

        [Fact]
        public async Task TestModifyNonExistingCabinet()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = "C_non_existing",
                CabinetType = CabinetType.Locker
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_notFound, ex.Message);
            }
        }

        [Fact]
        public async Task TestModifyCabinetTypeHavingCabinetPositions()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = "210309081254",
                CabinetType = CabinetType.Locker
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_modifyCabinetTypeWithPositions, ex.Message);
            }
        }

        [Fact]
        public async Task TestRemoveConfigurationsFromEnabledCabinet()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = "210309081254",
                RemoveConfiguration = true
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.cabinets_apiExceptions_cabinetConfigRemoveWhileActive, ex.Message);
            }
        }

        [Fact]
        public async Task TestRemoveConfigurationsFromDisabledCabinet()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = CABINET_NUMBER_DISABLED_WITH_CONFIG,
                RemoveConfiguration = true
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act
                var cabinetSearchResultBefore = context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(modifyCabinetCommand.CabinetNumber))
                    .FirstOrDefault();

                var cabinetDtoAfter = await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>());

                var cabinetSearchResultAfter = context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(modifyCabinetCommand.CabinetNumber))
                    .FirstOrDefault();

                // Assert
                Assert.NotNull(cabinetSearchResultBefore.CabinetConfiguration);
                Assert.Null(cabinetSearchResultAfter.CabinetConfiguration);
                Assert.Null(cabinetDtoAfter.CabinetConfiguration);
            }
        }

        [Fact]
        public async Task TestDisableCabinet()
        {
            //  Arrange
            var modifyCabinetCommand = new ModifyCabinetCommand()
            {
                CabinetNumber = "200214160401",
                IsActive = false
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var dataManager = new CabinetDataManager(context, new Mock<ILogger<CabinetDataManager>>().Object);
                var mapper = new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper();
                var managementLogger = CreateMockedManagementLogger();
                var handler = new ModifyCabinetHandler(new Mock<ILogger<ModifyCabinetHandler>>().Object, mapper, context, managementLogger);

                // Act
                var cabinetSearchResultBefore = context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(modifyCabinetCommand.CabinetNumber))
                    .FirstOrDefault();

                var cabinetDtoAfter = await handler.Handle(modifyCabinetCommand, It.IsAny<CancellationToken>());

                var cabinetSearchResultAfter = context.Cabinet()
                    .AsNoTracking()
                    .Where(c => c.CabinetNumber.Equals(modifyCabinetCommand.CabinetNumber))
                    .FirstOrDefault();

                // Assert
                Assert.True(cabinetSearchResultBefore.IsActive);
                Assert.False(cabinetSearchResultAfter.IsActive);
                Assert.False(cabinetDtoAfter.IsActive);
            }
        }
    }
}
