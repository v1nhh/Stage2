using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Users;
using CTAM.Core;
using CTAM.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class CheckAndCreateUserCommandTest : AbstractIntegrationTests
    {
        private const string EXISTING_USER_UID = "EXISTING_USER_UID";
        private const string NEW_USER_NAME = "NEW_USER_NAME";
        private const string EXISTING_USER_NAME = "EXISTING_USER_NAME";
        private const string EXISTING_USER_EMAIL = "test@test.com";
        private const int EXISTING_USER_LOGINCODE = 1009;
        private const string EXISTING_USER_CARDCODE = "00009";
        private const string NEW_USER_EMAIL = "test2@test.com";
        private List<int> NEW_USER_ROLES = new List<int>() { 1, 2 };
        private List<int> EXISTING_USER_ROLES = new List<int>() { 3, 4 };

        public CheckAndCreateUserCommandTest() : base("CTAM_CreateOrReplaceUser")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { UID = EXISTING_USER_UID, Name = EXISTING_USER_NAME, Email = EXISTING_USER_EMAIL, LanguageCode = "nl-NL",
                                                      LoginCode = EXISTING_USER_LOGINCODE.ToString("000000"), CardCode = EXISTING_USER_CARDCODE });
                EXISTING_USER_ROLES.ForEach(roleID =>
                    context.CTAMUser_Role().Add(new CTAMUser_Role { CTAMUserUID = EXISTING_USER_UID, CTAMRoleID = roleID }));
                context.SaveChanges();
            }

        }

        [Fact]
        public async Task TestCreateNewUser()
        {
            // Arrange
            var command = new CheckAndCreateUserCommand()
            {
                Name = NEW_USER_NAME,
                AddRolesIDs = NEW_USER_ROLES,
                Email = NEW_USER_EMAIL,
                LanguageCode = "nl-NL"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateHandler(context);

                // Act
                await handler.Handle(command, It.IsAny<CancellationToken>());

                // Assert
                var user = context.CTAMUser().SingleOrDefault(u => u.Name == NEW_USER_NAME);
                Assert.NotNull(user);
                Assert.Equal(NEW_USER_EMAIL, user.Email);
                Assert.NotNull(context.CTAMUser_Role().Find(user.UID, NEW_USER_ROLES[0]));
                Assert.NotNull(context.CTAMUser_Role().Find(user.UID, NEW_USER_ROLES[1]));
            }
        }

        [Fact]
        public async Task TestCreateNewUserWithExistingEmail()
        {
            // Arrange
            var command = new CheckAndCreateUserCommand()
            {
                Name = NEW_USER_NAME,
                AddRolesIDs = NEW_USER_ROLES,
                Email = EXISTING_USER_EMAIL,
                LanguageCode = "nl-NL"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateHandler(context);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(command, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.users_apiExceptions_duplicateEmail, ex.Message);
            }
        }

        [Fact]
        public async Task TestCreateNewUserWithExistingCardCode()
        {
            // Arrange
            var command = new CheckAndCreateUserCommand()
            {
                Name = NEW_USER_NAME,
                AddRolesIDs = NEW_USER_ROLES,
                Email = NEW_USER_EMAIL,
                CardCode = EXISTING_USER_CARDCODE,
                LanguageCode = "nl-NL"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateHandler(context);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<CustomException>(async () => await handler.Handle(command, It.IsAny<CancellationToken>()));
                Assert.Equal(CloudTranslations.users_apiExceptions_duplicateCardCode, ex.Message);
            }
        }

        [Fact]
        public async Task TestCreateNewUserRunningOutOfLoginCodes()
        {
            // Arrange
            var command = new CheckAndCreateUserCommand()
            {
                Name = NEW_USER_NAME,
                Email = NEW_USER_EMAIL,
                LanguageCode = "nl-NL",
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var handler = CreateHandler(context);
                // Test data has LoginCodes 000001..000009 and 001005..001008. Test user above has EXISTING_USER_LOGINCODE 1009 ("001009")
                // New codes will be released from highest used (so 001010 and up). When this is used up it should use the gap between 000009 and 001005

                //// This could take days
                //for (int i = EXISTING_USER_LOGINCODE + 1; i <= 999999; i++)
                //{
                //    command.Name = NEW_USER_NAME + i.ToString("000000");
                //    command.Email = i.ToString("000000") + NEW_USER_EMAIL;
                //    await handler.Handle(command, It.IsAny<CancellationToken>());
                //}

                var newUsers = new List<CTAMUser>();
                for (int i = EXISTING_USER_LOGINCODE + 1; i <= 999999; i++)
                {
                    newUsers.Add(new CTAMUser {
                        UID = Guid.NewGuid().ToString(),
                        Name = NEW_USER_NAME + i.ToString("000000"), 
                        Email = i.ToString("000000") + NEW_USER_EMAIL, 
                        LoginCode = i.ToString("000000"),
                        LanguageCode = "nl-NL" });
                }
                context.CTAMUser().AddRange(newUsers);
                context.SaveChanges();

                // Act
                await handler.Handle(command, It.IsAny<CancellationToken>());

                // Assert
                var user = context.CTAMUser().SingleOrDefault(u => u.Name == NEW_USER_NAME);
                Assert.NotNull(user);
                Assert.Equal(NEW_USER_EMAIL, user.Email);
            }
        }


        private CheckAndCreateUserHandler CreateHandler(MainDbContext context)
        {
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<UserProfile>();
                c.AddProfile<RoleProfile>();
                c.AddProfile<PermissionProfile>();
            }).CreateMapper();

            var managementLogger = CreateMockedManagementLogger();
            var handler = new CheckAndCreateUserHandler(
                context,
                new Mock<ILogger<CheckAndCreateUserHandler>>().Object,
                mapper,
                managementLogger);
            return handler;
        }
    }
}


