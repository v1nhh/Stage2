using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Profiles;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CabinetModule.Web.Controllers;
using CabinetModule.Web.Security;
using CTAM.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CloudAPI.Tests.CabinetModule.IntegrationTests
{
    public class LoginControllerTest : AbstractIntegrationTests
    {
        private const string CABINET_NUMBER_NOT_ACTIVE = "CABINET_NUMBER_NOT_ACTIVE";
        private const string CABINET_NUMBER_ACTIVE = "CABINET_NUMBER_ACTIVE";


        public LoginControllerTest(): base("Cabinet_LoginControllerTest")
        {

            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER_NOT_ACTIVE, IsActive = false, Name = "Cabinet Not Active" });
                context.Cabinet().Add(new Cabinet { CabinetNumber = CABINET_NUMBER_ACTIVE, IsActive = true, Name = "Cabinet Active" });
                context.SaveChanges();
            }
        }

        public static readonly object[][] CabinetsData =
        {
            new object[] { CABINET_NUMBER_NOT_ACTIVE, null, HttpStatusCode.ServiceUnavailable },
            new object[] { CABINET_NUMBER_ACTIVE, null, HttpStatusCode.Unauthorized },
            // "CABINET_API_KEY" is equal to the value defined in CreateConfigurationStub()
            new object[] { CABINET_NUMBER_ACTIVE, "CABINET_API_KEY", HttpStatusCode.OK },
        };

        [Theory, MemberData(nameof(CabinetsData))]
        public async Task TestLoginDisabledCabinet(string cabinetNumber, string apiKey, HttpStatusCode statusCode)
        {
            //  Arrange
            var cabinetAuthenticationRequest = new CabinetAuthenticationRequest()
            {
                CabinetNumber = cabinetNumber,
                ApiKey = apiKey
            };
            var cabinetNumberQuery = new GetCabinetByCabinetNumberQuery(cabinetNumber);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var getCabinetByCabinetNumberHandler = new GetCabinetByCabinetNumberHandler(context,
                    new Mock<ILogger<GetCabinetByCabinetNumberHandler>>().Object,
                    new MapperConfiguration(c => c.AddProfile<CabinetProfile>()).CreateMapper());

                var mediator = new Mock<IMediator>() { CallBase = true };
                mediator.Setup(m => m.Send(It.IsAny<GetCabinetByCabinetNumberQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(await getCabinetByCabinetNumberHandler.Handle(cabinetNumberQuery, It.IsAny<CancellationToken>()));

                var cabinetAuthenticationService = new CabinetAuthenticationService(CreateConfigurationStub());
                var loginController = new LoginController(new Mock<ILogger<LoginController>>().Object, mediator.Object, cabinetAuthenticationService);

                // Act
                ObjectResult response = await loginController.Cabinet(cabinetAuthenticationRequest, "ct") as ObjectResult;
                // Assert
                Assert.Equal((int)statusCode, response.StatusCode);
            }
        }
    }
}
