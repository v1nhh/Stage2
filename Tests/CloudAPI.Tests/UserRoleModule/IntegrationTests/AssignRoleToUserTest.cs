using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Commands.Users;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class AssignRoleToUserTest : AbstractIntegrationTests
    {
        const string USER_UID = "A_USER_UID";
        const int ROLE_ID = 123;

        public AssignRoleToUserTest() : base("CTAM_AssignRoleToUser")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { UID = USER_UID, Name="Bob", Email="user@company.com", LanguageCode = "nl-NL"} );
                context.CTAMRole().Add( new CTAMRole { ID= ROLE_ID, Description="TestRole" } );
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestAssignRoleToUser()
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var assignRoleToUserCommand = new AssignRoleToUserCommand()
                {
                    RoleID = ROLE_ID,
                    UserUID = USER_UID
                };
                
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<UserProfile>();
                    c.AddProfile<RoleProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new AssignRoleToUserHandler(context, 
                                                        new Mock<ILogger<AssignRoleToUserHandler>>().Object, 
                                                        mapper, 
                                                        managementLogger );

                // Act
                var rc = await handler.Handle( assignRoleToUserCommand, It.IsAny<CancellationToken>() );

                // Assert
                Assert.NotNull(rc);
                Assert.Equal(rc.UID, USER_UID);
                Assert.NotNull(rc.Roles?.FirstOrDefault( x=> x.ID == ROLE_ID ));
            }
        }

        [Fact]
        public async Task TestAssignRoleToInvalidUser()
        {
            //  Arrange
            var assignRoleToUserCommand = new AssignRoleToUserCommand()
            {
                RoleID = ROLE_ID,
                UserUID = USER_UID + "_ANY"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = It.IsAny<IMapper>();
                var managementLogger = CreateMockedManagementLogger();

                var handler = new AssignRoleToUserHandler(context, new Mock<ILogger<AssignRoleToUserHandler>>().Object, mapper, managementLogger);

                // Act
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(assignRoleToUserCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_notFoundUser, ex.Message);
            }
        }

        [Fact]
        public async Task TestAssignInvalidRoleToUser()
        {
            //  Arrange
            var assignRoleToUserCommand = new AssignRoleToUserCommand()
            {
                RoleID = ROLE_ID+1,
                UserUID = USER_UID
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var mapper = It.IsAny<IMapper>();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new AssignRoleToUserHandler(context, 
                                                        new Mock<ILogger<AssignRoleToUserHandler>>().Object, 
                                                        mapper, 
                                                        managementLogger);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(assignRoleToUserCommand, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.roles_apiExceptions_notFound, ex.Message);
            }
        }
    }
}

