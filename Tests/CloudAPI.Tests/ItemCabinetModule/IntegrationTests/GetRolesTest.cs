using AutoMapper;
using CTAM.Core;
using CTAM.Core.Constants;
using ItemCabinetModule.ApplicationCore.Profiles;
using ItemCabinetModule.ApplicationCore.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Profiles;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class GetRolesTest : AbstractIntegrationTests
    {
        const int ROLE_ID_ADMIN = 9000;
        const string ROLE_DESC_ADMIN = "AAadminX";

        const int ROLE_ID_FUNC = 9001;
        const string ROLE_DESC_FUNC = "AAfuncX";

        const int ROLE_ID_PAGE2 = 9002;
        const string ROLE_DESC_PAGE2 = "AApage2";

        const int ROLE_ID_IBKZ3 = 9003;
        const string ROLE_DESC_IBKZ3 = "ZZ3ibkz";

        const int ROLE_ID_IBKZ2 = 9004;
        const string ROLE_DESC_IBKZ2 = "ZZ2ibkz";

        const int ROLE_ID_IBKZ1 = 9005;
        const string ROLE_DESC_IBKZ1 = "ZZ1ibkz";

        public GetRolesTest() : base("CTAM_GetRoles")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_ADMIN, Description = ROLE_DESC_ADMIN });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_FUNC, Description = ROLE_DESC_FUNC });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_PAGE2, Description = ROLE_DESC_PAGE2 });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_IBKZ3, Description = ROLE_DESC_IBKZ3 });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_IBKZ2, Description = ROLE_DESC_IBKZ2 });
                context.CTAMRole().Add(new CTAMRole { ID = ROLE_ID_IBKZ1, Description = ROLE_DESC_IBKZ1 });
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_ID_IBKZ3, CTAMPermissionID = 1 }); // Swap
                context.CTAMRole_Permission().Add(new CTAMRole_Permission { CTAMRoleID = ROLE_ID_IBKZ3, CTAMPermissionID = 5 }); // Borrow
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, RoleColumn.Description, false, "", ROLE_ID_ADMIN, 2, true, 0 },
            new object[] { 2, 0, RoleColumn.Description, true, "", ROLE_ID_IBKZ3, 2, true, 2 },
            new object[] { 2, 0, RoleColumn.Description, true, "", ROLE_ID_IBKZ3, 2, false, 1 },
            new object[] { 2, 0, null, true, "1ibkz", ROLE_ID_IBKZ1, 1, true, 0 },
            new object[] { 2, 1, RoleColumn.Description, false, "", ROLE_ID_PAGE2, 2, true, 0 },
            new object[] { 2, 1, RoleColumn.Description, true, "", ROLE_ID_IBKZ1, 2, true, 0 },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, RoleColumn? column, bool desc, string filter, int firstID, int expectedCount, bool hasSwapReplace, int firstCountPermissions)
        {
            // Arrange
            var query = new GetPaginatedItemCabinetRolesQuery( limit, page, column, desc, filter);
            var logger = new Mock<ILogger<GetPaginatedItemCabinetRolesHandler>>().Object;
            var mapper = new MapperConfiguration(c => {
                c.AddProfile<RoleProfile>();
                c.AddProfile<PermissionProfile>();
                c.AddProfile<ItemCabinetRoleProfile>();
            }).CreateMapper();
            var mediator = new Mock<IMediator>().Object;

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var tenantContext = CreateMockedTenantContext();
                var tenantService = CreateMockedTenantService(hasSwapReplace);
                var handler = new GetPaginatedItemCabinetRolesHandler(context, logger, mapper, tenantContext, tenantService);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal(firstCountPermissions, first.Permissions.Count);
                Assert.Equal(firstID, first.ID);
            }
        }

    }
}
