using AutoMapper;
using CTAM.Core;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Profiles;
using UserRoleModule.ApplicationCore.Queries.Roles;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class PermissionsTest: AbstractIntegrationTests
    {
        public PermissionsTest(): base("CTAM_Permissions")
        {
        }

        [Fact]
        public async Task TestGetPermissionsFromDbController()
        {
            //  Arrange

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var mapper = new MapperConfiguration(c => c.AddProfile<PermissionProfile>()).CreateMapper();
                var handler = new GetAllPermissionsHandler(context, mapper);

                // Act
                var actualResult = await handler.Handle(It.IsAny<GetAllPermissionsQuery>(), It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(actualResult);
            }
        }

    }
}
