using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Users;
using CTAM.Core;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Import;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.Imports
{
    public class ImportUsersTest : AbstractIntegrationTests
    {
        const string UID_INITIAL = "0000a5e8-f68a-484d-7f83-322561bf2686";
        const string NAME_INTITIAL = "Isaac Nitieel";
        const string EMAIL_INTITIAL = "Isaac@ct.nl";
        const string CARDCODE_INTITIAL = "123456H";

        const string UID_FIRST_RANGE = "0000a5e8-f68a-484d-7f83-322561bf2687";

        const string UID_EMPTY_CARDCODE = "0000a5e8-f68a-484d-7f83-322561bf2688";
        const string NAME_EMPTY_CARDCODE = "E. Cardcode";
        const string EMAIL_EMPTY_CARDCODE = "e.cardcode@ct.nl";

        const string UID_NULL_CARDCODE = "0000a5e8-f68a-484d-7f83-322561bf2689";
        const string NAME_NULL_CARDCODE = "Null Cardcode";
        const string EMAIL_NULL_CARDCODE = "null.cardcode@ct.nl";

        public ImportUsersTest() : base("CTAM_ImportUsers")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { UID = UID_INITIAL, Name = NAME_INTITIAL, Email = EMAIL_INTITIAL, CardCode = CARDCODE_INTITIAL, LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = UID_EMPTY_CARDCODE, Name = NAME_EMPTY_CARDCODE, Email = EMAIL_EMPTY_CARDCODE, CardCode = "", LanguageCode = "nl-NL" });
                context.CTAMUser().Add(new CTAMUser { UID = UID_NULL_CARDCODE, Name = NAME_NULL_CARDCODE, Email = EMAIL_NULL_CARDCODE, LanguageCode = "nl-NL" });
                context.SaveChanges();
            }
        }

        public static readonly object[][] UserChecks =
        {
            new object[] { true, "99999", "N.E. Wobble", "new@ct.nl", "999991234", "nl-NL", 0, "10001", "0", "0", "0" },         // new
            new object[] { false, "99999", "N.E. Wobble", "new@ct.nl", " ", "nl-NL", 1, "10000", "0", "0", "1" },                // invalid cardcode
            new object[] { false, "99999", "N.E. Wobble", "new@ct.nl", "", "nl-NL", 0, "10001", "0", "0", "0" },                 // empty cardcode
            new object[] { false, "99999", "N.E. Wobble", "new@ct.nl", null, "nl-NL", 0, "10001", "0", "0", "0" },               // null cardcode
            new object[] { false, "", "N.E. Wobble", "new@ct.nl", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },             // invalid mandatory uid
            new object[] { false, "99999", "", "new@ct.nl", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },                   // invalid mandatory name
            new object[] { false, "99999", "N.E. Wobble", "", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },                 // invalid mandatory email
            new object[] { false, "Length>50 x0123456789012345678901234567890123456789", "N.E. Wobble", "new@ct.nl", "999991234", "nl-NL", 1 , "10000", "0", "0", "1"},       // invalid UID
            new object[] { false, "99999", "Length>250 x012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "new@ct.nl", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },         // invalid Name
            new object[] { false, "99999", "N.E. Wobble", "Length>250 x012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },         // invalid Email
            new object[] { false, "99999", "N.E. Wobble", "new@@ct.nl", "999991234", "nl-NL", 1, "10000", "0", "0", "1" },                                                    // invalid email
            new object[] { false, "99999", "N.E. Wobble", EMAIL_INTITIAL, "999991234", "nl-NL", 1, "10000", "0", "0", "1" },                                                  // existing email
            new object[] { false, "99999", "N.E. Wobble", "new@ct.nl", "Length>50 x0123456789012345678901234567890123456789", "nl-NL", 1, "10000", "0", "0", "1" },           // invalid CardCode
            new object[] { false, "99999", "N.E. Wobble", "new@ct.nl", CARDCODE_INTITIAL, "nl-NL", 1, "10000", "0", "0", "1" },                                               // existing CardCode
            new object[] { true, UID_FIRST_RANGE, "N.E. Wobble", "new@ct.nl", "999991234", "nl-NL", 1, "10000", "0", "0", "1" }, // overwrite in import
            new object[] { true, UID_INITIAL, "N.E. Wobble", "new@ct.nl", "999991234", "nl-NL", 1, "10000", "1", "0", "0" },     // overwrite in database
        };

        [Theory, MemberData(nameof(UserChecks))]
        public async Task TestImportUsers(bool valid, string uid, string name, string email, string cardCode, string languageCode,
                                          int cntDoubleOrInvalid, string newUsers, string changedUsers, string unchangedUsers, string invalidUsers)
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var totalStart = context.CTAMUser().Count();
                var range = Enumerable.Range(1, 10000)
                                      .Select(r => new UserImportDTO { UID = Guid.NewGuid().ToString(), 
                                                                       Name = "Name" + r.ToString("D5"),
                                                                       Email = r.ToString("D5") + "@ct.nl",
                                                                       CardCode = r.ToString("D50"),
                                                                       LanguageCode = "nl-NL"
                                      })
                                      .ToList();
                range[0].UID = UID_FIRST_RANGE;

                range.Add(new UserImportDTO { UID = uid, Name = name, Email = email, CardCode = cardCode , LanguageCode = languageCode });

                var importCommand = new ImportUsersCommand()
                {
                    InputList = range
                };
                
                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<UserProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();
                var handler = new ImportUsersHandler(context, 
                                                     new Mock<ILogger<ImportUsersHandler>>().Object,
                                                     mapper, managementLogger );

                // Act
                var result = await handler.Handle(importCommand, It.IsAny<CancellationToken>() );

                // Assert
                var totalEnd = context.CTAMUser().Count();
                Assert.Equal(range.Count + totalStart - cntDoubleOrInvalid, totalEnd);
                var second = context.CTAMUser().Where(u => u.Email.Equals("00002@ct.nl")).FirstOrDefault();
                Assert.NotNull(second);
                Assert.Equal("Name00002", second.Name);
                if (valid)
                {
                    var newUser = context.CTAMUser().Where(u => u.UID.Equals(uid)).FirstOrDefault();
                    Assert.NotNull(newUser);
                    Assert.Equal(email, newUser.Email);
                }

                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("newUsers"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("changedUsers"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("unchangedUsers"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("invalidUsers"));
                Assert.Equal(newUsers, ParametersOfLastManagementLogInfo["newUsers"]);
                Assert.Equal(changedUsers, ParametersOfLastManagementLogInfo["changedUsers"]);
                Assert.Equal(unchangedUsers, ParametersOfLastManagementLogInfo["unchangedUsers"]);
                Assert.Equal(invalidUsers, ParametersOfLastManagementLogInfo["invalidUsers"]);
            }
        }
    }
}

