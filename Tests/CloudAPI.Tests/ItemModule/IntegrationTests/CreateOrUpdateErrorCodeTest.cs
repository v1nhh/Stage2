using AutoMapper;
using CloudAPI.ApplicationCore.Commands.ErrorCodes;
using CTAM.Core;
using CTAM.Core.Commands.ErrorCodes;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemModule.IntegrationTests
{
    public class CreateOrUpdateErrorCodeTest : AbstractIntegrationTests
    {
        private const int ERRORCODE_ID = 999;
        private const string ERRORCODE_DESCRIPTION = "Description1";
        private const string ERRORCODE_CODE = "Code1";

        public CreateOrUpdateErrorCodeTest(): base("CTAM_CreateOrUpdateErrorCode")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ErrorCode().Add(new ErrorCode() { ID = ERRORCODE_ID, Description = ERRORCODE_DESCRIPTION, Code = ERRORCODE_CODE});
                context.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task TestCreateNewErrorCode()
        {
            var createErrorCodeCommand = new CheckAndCreateErrorCodeCommand()
            {
                Description = "Description2",
                Code = "Code2"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ErrorCodeProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndCreateErrorCodeHandler(context, new Mock<ILogger<CheckAndCreateErrorCodeHandler>>().Object, mapper, managementLogger);

                var createdErrorCode = await handler.Handle(createErrorCodeCommand, It.IsAny<CancellationToken>());

                Assert.Equal(createErrorCodeCommand.Description, createdErrorCode.Description);
                Assert.Equal(createErrorCodeCommand.Code, createdErrorCode.Code);
            }
        }

        [Fact]
        public async Task TestCreateNewErrorCodeDuplicateDescription()
        {
            var createErrorCodeCommand = new CheckAndCreateErrorCodeCommand()
            {
                Description = ERRORCODE_DESCRIPTION,
                Code = "Code3"
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ErrorCodeProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndCreateErrorCodeHandler(context, new Mock<ILogger<CheckAndCreateErrorCodeHandler>>().Object, mapper, managementLogger);

                await Assert.ThrowsAsync<CustomException> (async () => await handler.Handle(createErrorCodeCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestCreateNewErrorCodeDuplicateCode()
        {
            var createErrorCodeCommand = new CheckAndCreateErrorCodeCommand()
            {
                Description = "Description3",
                Code = ERRORCODE_CODE
            };

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ErrorCodeProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new CheckAndCreateErrorCodeHandler(context, new Mock<ILogger<CheckAndCreateErrorCodeHandler>>().Object, mapper, managementLogger);

                await Assert.ThrowsAsync<CustomException> (async () => await handler.Handle(createErrorCodeCommand, It.IsAny<CancellationToken>()));
            }
        }

        [Fact]
        public async Task TestUpdateErrorCode()
        {
            var updateErrorCodeCommand = new CheckAndModifyErrorCodeCommand()
            {
                ID = ERRORCODE_ID,
                Description = "Description4",
                Code = ERRORCODE_CODE
            };

            var mapper = new MapperConfiguration(c => {
                c.AddProfile<ErrorCodeProfile>();
            }).CreateMapper();

            var managementLogger = CreateMockedManagementLogger();

            var context = new MainDbContext(ContextOptions, null);
            var handler = new CheckAndModifyErrorCodeHandler(context, new Mock<ILogger<CheckAndModifyErrorCodeHandler>>().Object, mapper, managementLogger);
            var updatedErrorCode = await handler.Handle(updateErrorCodeCommand, It.IsAny<CancellationToken>());
            Assert.Equal("Description4", updatedErrorCode.Description);
        }

        [Fact]
        public async Task TestUpdateErrorCodeWithExistingCode()
        {

            var context = new MainDbContext(ContextOptions, null);
            context.ErrorCode().Add(new ErrorCode() { ID = 1001, Description = "Description5", Code = "Code5"});
            await context.SaveChangesAsync();

            var updateErrorCodeCommand = new CheckAndCreateErrorCodeCommand()
            {
                Description = ERRORCODE_DESCRIPTION,
                Code = "Code5"
            };

            var mapper = new MapperConfiguration(c => {
                c.AddProfile<ErrorCodeProfile>();
            }).CreateMapper();

            var managementLogger = CreateMockedManagementLogger();

            var handler = new CheckAndCreateErrorCodeHandler(context, new Mock<ILogger<CheckAndCreateErrorCodeHandler>>().Object, mapper, managementLogger);
            await Assert.ThrowsAsync<CustomException> (async () => await handler.Handle(updateErrorCodeCommand, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task TestUpdateErrorCodeWithExistingDescription()
        {
            var context = new MainDbContext(ContextOptions, null);
            context.ErrorCode().Add(new ErrorCode() { ID = 1001, Description = "Description5", Code = "Code5" });
            await context.SaveChangesAsync();

            var updateErrorCodeCommand = new CheckAndCreateErrorCodeCommand()
            {
                Description = "Description5",
                Code = "Code5"
            };

            var mapper = new MapperConfiguration(c => {
                c.AddProfile<ErrorCodeProfile>();
            }).CreateMapper();

            var managementLogger = CreateMockedManagementLogger();

            var handler = new CheckAndCreateErrorCodeHandler(context, new Mock<ILogger<CheckAndCreateErrorCodeHandler>>().Object, mapper, managementLogger);
            await Assert.ThrowsAsync<CustomException> (async () => await handler.Handle(updateErrorCodeCommand, It.IsAny<CancellationToken>()));
        }
    }
}
