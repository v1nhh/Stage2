using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Roles;
using CloudApiModule.ApplicationCore.DataManagers;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class CreateRoleTest : AbstractIntegrationTests
    {
        private const string TESTROLE_DESCRIPTION = "Testrole";
        private const int TESTROLE_ID = 100;
        private const string TESTPERMISSION_DESCRIPTION = "TestPermission";
        private const int TESTPERMISSION_ID = 101;

        public CreateRoleTest() : base("CTAM_CreateRole")
        {
        }

        [Fact]
        public async Task TestCreateRoleEmptyDescription()
        {
            // Arrange
            var createCommand = new CreateRoleWithDependenciesCommand() { Description = null, AddPermissionIDs = new int[] { TESTPERMISSION_ID } };
            var logger = new Mock<ILogger<CreateRoleWithDependenciesHandler>>().Object;
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediator>().Object;
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CreateRoleWithDependenciesHandler(logger, mapper, new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(createCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_emptyDescription, ex.Message);
            }
        }

        [Fact]
        public async Task TestCreateRoleEmptyPermissions()
        {
            // Arrange
            var createCommand = new CreateRoleWithDependenciesCommand() { Description = TESTROLE_DESCRIPTION };
            var logger = new Mock<ILogger<CreateRoleWithDependenciesHandler>>().Object;
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediator>().Object;
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CreateRoleWithDependenciesHandler(logger, mapper, new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(createCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_atLeastOnePermission, ex.Message);
            }
        }

        [Fact]
        public async Task TestCreateRoleDuplicate()
        {
            // Arrange
            var createCommand = new CreateRoleWithDependenciesCommand() { Description = TESTROLE_DESCRIPTION, AddPermissionIDs = new int[] { TESTPERMISSION_ID } };
            var logger = new Mock<ILogger<CreateRoleWithDependenciesHandler>>().Object;
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediator>().Object;
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CreateRoleWithDependenciesHandler(logger, mapper, new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));
                await handler.Handle(createCommand, It.IsAny<CancellationToken>());

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(createCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_duplicateDescription, ex.Message);
            }
        }

        public static readonly object[][] DTFromUntil =
        {
            new object[] { null, null },
            new object[] { "2022-02-17T12:52", null },
            new object[] { "2022-02-17T12:52", "2022-03-17T12:52" },
            new object[] { null, "2022-03-17T12:52" },
        };

        [Theory, MemberData(nameof(DTFromUntil))]
        public async Task TestCreateRoleValid(string from, string until)
        {
            // Arrange
            var createCommand = new CreateRoleWithDependenciesCommand() { 
                                        Description = TESTROLE_DESCRIPTION, 
                                        AddPermissionIDs = new int[] { TESTPERMISSION_ID }, 
                                        ValidFromDT = from,
                                        ValidUntilDT = until
                                };
            var logger = new Mock<ILogger<CreateRoleWithDependenciesHandler>>().Object;
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediator>().Object;
            var managementLogger = CreateMockedManagementLogger();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new CreateRoleWithDependenciesHandler(logger, mapper, new CloudApiDataManager(context, new Mock<ILogger<CloudApiDataManager>>().Object, managementLogger));

                // Act
                await handler.Handle(createCommand, It.IsAny<CancellationToken>());

                // Assert
                var role = context.CTAMRole().Where(r => r.Description == TESTROLE_DESCRIPTION).SingleOrDefault();
                Assert.NotNull(role);
                
                if (!string.IsNullOrEmpty(from))
                {
                    if (DateTime.TryParse(from, out DateTime dt))
                    {
                        Assert.Equal(role.ValidFromDT, dt);
                    }
                }

                if (!string.IsNullOrEmpty(until))
                {
                    if (DateTime.TryParse(until, out DateTime dt))
                    {
                        Assert.Equal(role.ValidUntilDT, dt);
                    }
                }

                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(until))
                {
                    Assert.True(role.ValidFromDT < role.ValidUntilDT);
                }
            }
        }
    }
}
