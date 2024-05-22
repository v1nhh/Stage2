using AutoMapper;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Profiles;
using UserRoleModule.ApplicationCore.Queries.Users;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.IntegrationTests
{
    public class GetUsersTest : AbstractIntegrationTests
    {
        const string USER_UID_ADAM = "UID_ADAM";
        const string NAME_ADAM = "Adam";
        const string EMAIL_ADAM = "curry@countdown.org";

        const string USER_UID_AAF = "UID_AAF";
        const string NAME_AAF = "Aaf";
        const string EMAIL_AAF = "aaf@brandtcortius.org";

        const string USER_UID_ZORBA = "UID_ZORBA";
        const string EMAIL_ZORBA = "degriek@griekenland.gr";
        const string NAME_ZORBA = "Zorba de Griek";

        public GetUsersTest() : base("CTAM_GetUsers")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMUser().Add(new CTAMUser { UID = USER_UID_ADAM, Name = NAME_ADAM, Email = EMAIL_ADAM, LanguageCode = "nl-NL"});
                context.CTAMUser().Add(new CTAMUser { UID = USER_UID_AAF, Name = NAME_AAF, Email = EMAIL_AAF, LanguageCode = "nl-NL"});
                context.CTAMUser().Add(new CTAMUser { UID = USER_UID_ZORBA, Name = NAME_ZORBA, Email = EMAIL_ZORBA, LanguageCode = "nl-NL" });
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, UserColumn.Name, false, "", USER_UID_AAF, 2 },
            new object[] { 2, 0, UserColumn.Name, true, "", USER_UID_ZORBA, 2 },
            new object[] { 2, 0, UserColumn.Email, false, "", USER_UID_AAF, 2 },
            new object[] { 2, 0, null, true, "griek", USER_UID_ZORBA, 1 },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, UserColumn? column, bool desc, string filter, string firstUID, int expectedCount)
        {
            // Arrange
            var query = new GetUsersPaginatedQuery( limit, page, column, desc, filter);
            var logger = new Mock<ILogger<GetUsersPaginatedHandler>>().Object;
            var mapper = new MapperConfiguration(c => {
                c.AddProfile<UserProfile>();
                c.AddProfile<RoleProfile>();
            }).CreateMapper();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var result = await context.CTAMUser()
                    .AsNoTracking()
                    .ToListAsync();

                var handler = new GetUsersPaginatedHandler(context, logger, mapper);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal(firstUID, first.UID);
            }
        }

    }
}
