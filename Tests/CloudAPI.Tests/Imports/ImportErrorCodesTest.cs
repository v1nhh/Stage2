using AutoMapper;
using CTAM.Core;
using ItemModule.ApplicationCore.Commands.ErrorCodes;
using ItemModule.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.Imports
{
    public class ImportErrorCodesTest : AbstractIntegrationTests
    {
        const string CODE_INITIAL = "init";
        const string DESCRIPTION_INTITIAL = "INITIAL";

        public ImportErrorCodesTest() : base("CTAM_ImportErrorCodes")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ErrorCode().Add(new ErrorCode { Code = CODE_INITIAL, Description = DESCRIPTION_INTITIAL } );
                context.SaveChanges();
            }
        }

        public static readonly object[][] ErrorCodeChecks =
        {
            new object[] { true, "99999", "Extra ErrorCode", 0, "10001", "0", "0", "0" },
            new object[] { true, "00002", "Overwrite", 1, "10000", "0", "0", "0" }, // Overwrite in generated list
            new object[] { false, "99999", DESCRIPTION_INTITIAL, 1, "10000", "0", "1", "1" }, // Description already exists
            new object[] { false, "99999", "x00001", 1, "10000", "0", "0", "1" }, // Description already exists in import
            new object[] { false, "", "Invalid code", 1, "10000", "0", "0", "1" },
            new object[] { false, null, "Invalid code", 1, "10000", "0", "0", "1" },
            new object[] {false, "99999", null, 1, "10000", "0", "0", "1" },
            new object[] { false, "99999", "", 1, "10000", "0", "0", "1" },
            new object[] { false, "12345678901", "Extra ErrorCode", 1, "10000", "0", "0", "1" }, // Code too long
            new object[] { false, "99999", "Length>250 x012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", 1 , "10000", "0", "0", "1"},
        };

        [Theory, MemberData(nameof(ErrorCodeChecks))]
        public async Task TestImportErrorCodes(bool valid, string code, string description, int cntDoubleOrInvalid, string newErrorCodes, string changedErrorCodes, string unchangedErrorCodes, string invalidErrorCodes)
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var totalStart = context.ErrorCode().Count();
                var range = Enumerable.Range(1, 10000)
                                      .Select(r => new ErrorCodeImportDTO { Code = r.ToString("D5"), Description = "x" + r.ToString("D5") })
                                      .ToList();

                range.Add(new ErrorCodeImportDTO { Code = code, Description = description });

                var importCommand = new ImportErrorCodesCommand()
                {
                    InputList = range
                };
                
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ErrorCodeProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();

                var handler = new ImportErrorCodesHandler(context, 
                                                        new Mock<ILogger<ImportErrorCodesHandler>>().Object, 
                                                        mapper, 
                                                        managementLogger );

                // Act
                var result = await handler.Handle(importCommand, It.IsAny<CancellationToken>() );

                // Assert
                var totalEnd = context.ErrorCode().Count();
                Assert.Equal(range.Count + totalStart - cntDoubleOrInvalid, totalEnd);
                var first = context.ErrorCode().Where(ec => ec.Code.Equals("00001")).FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal("x00001", first.Description);
                if (valid)
                {
                    var newErrorCode = context.ErrorCode().Where(ec => ec.Code.Equals(code)).FirstOrDefault();
                    Assert.NotNull(newErrorCode);
                    Assert.Equal(code, newErrorCode.Code);
                    Assert.Equal(description, newErrorCode.Description);
                }
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("newErrorCodes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("changedErrorCodes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("unchangedErrorCodes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("invalidErrorCodes"));
                Assert.Equal(newErrorCodes, ParametersOfLastManagementLogInfo["newErrorCodes"]);
                Assert.Equal(changedErrorCodes, ParametersOfLastManagementLogInfo["changedErrorCodes"]);
                Assert.Equal(unchangedErrorCodes, ParametersOfLastManagementLogInfo["unchangedErrorCodes"]);
                Assert.Equal(invalidErrorCodes, ParametersOfLastManagementLogInfo["invalidErrorCodes"]);
            }
        }

    }
}

