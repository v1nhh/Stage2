using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CTAM.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Queries.Roles;
using UserRoleModule.Controllers;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.UnitTests
{
    public class PermissionsTest
    {

        [Fact]
        public async Task TestGetPermissionsController()
        {
            //  Arrange
            var expectedResponse = new List<PermissionDTO>
            {
                new PermissionDTO { ID = 1, Description = "Test_Add", CTAMModule = CTAMModule.Cabinet },
                new PermissionDTO { ID = 2, Description = "Test_Add", CTAMModule = CTAMModule.Management },
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(m => m.Send(It.IsAny<GetAllPermissionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            var fakeLogger = new Mock<ILogger<PermissionsController>>();

            var controller = new PermissionsController(fakeLogger.Object, fakeMediator.Object);

            // Act
            OkObjectResult actualResult = (OkObjectResult)await controller.GetPermissions();

            // Assert
            fakeMediator.Verify(mediator => mediator.Send(It.IsAny<GetAllPermissionsQuery>(), It.IsAny<CancellationToken>()));

            Assert.Equal(actualResult.Value, expectedResponse);
        }
    }
}
