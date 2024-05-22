using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Roles;
using CloudAPI.ApplicationCore.DTO.Import;
using CloudAPI.ApplicationCore.Profiles;
using CTAM.Core;
using ItemModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using Xunit;

namespace CloudAPI.Tests.Imports
{
    public class ImportRolesTest : AbstractIntegrationTests
    {
        const string DESCRIPTION_INITIAL = "Initial role";

        const string UID1 = "0000c512-f5c9-1e07-891d-b5bc39613c66";
        const string NAME1 = "User1";
        const string EMAIL1 = "u1@ct.nl";
        const string UID2 = "0000dd26-141b-0c79-9b96-e588dad95960";
        const string NAME2 = "User2";
        const string EMAIL2 = "u2@ct.nl";
        const string UID3 = "0000dd26-141b-0c79-9b96-e588dad95961";
        const string NAME3 = "User3";
        const string EMAIL3 = "u3@ct.nl";

        const string ITEMTYPE1 = "Itemtype 1";
        const string ITEMTYPE2 = "Itemtype 2";
        const string ITEMTYPE3 = "Itemtype 3";

        public ImportRolesTest() : base("CTAM_ImportRoles")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                var u1 = new CTAMUser { UID = UID1, Name = NAME1, Email = EMAIL1, LanguageCode = "nl-NL" };
                var u2 = new CTAMUser { UID = UID2, Name = NAME2, Email = EMAIL2, LanguageCode = "nl-NL" };
                var u3 = new CTAMUser { UID = UID3, Name = NAME3, Email = EMAIL3, LanguageCode = "nl-NL" };
                context.CTAMUser().AddRange(new[] { u1, u2, u3 });

                var it1 = new ItemType { Description = ITEMTYPE1 };
                var it2 = new ItemType { Description = ITEMTYPE2 };
                var it3 = new ItemType { Description = ITEMTYPE3 };
                context.ItemType().AddRange(new[] { it1, it2, it3 });

                var r = new CTAMRole { Description = DESCRIPTION_INITIAL };
                context.CTAMRole().Add(r);

                var ur = new CTAMUser_Role { CTAMRole = r, CTAMUser = u3 };
                context.CTAMUser_Role().Add(ur);
                var rit = new CTAMRole_ItemType { CTAMRole = r, ItemType = it3 };
                context.CTAMRole_ItemType().Add(rit);
                var p1 = context.CTAMPermission().Where(p => p.Description.Equals("Return")).First();
                var pr = new CTAMRole_Permission { CTAMRole = r, CTAMPermissionID = p1.ID };
                context.CTAMRole_Permission().Add(pr);

                context.SaveChanges();
            }
        }

        public static readonly object[][] RoleChecks =
        {
            new object[] { true, "new description", "2022-05-10T09:13:32.2212678", "2022-05-11T09:13:32.2212678", UID1, "", "", 0, "11", "0", "0", "0" },   // one user
            new object[] { false, "new description", "X2022-05-10T09:13:32.2212678", "2022-05-11T09:13:32.2212678", UID1, "", "", 1, "10", "0", "0", "1" }, // invalid date from
            new object[] { false, "new description", "2022-05-10T09:13:32.2212678", "X2022-05-11T09:13:32.2212678", UID1, "", "", 1, "10", "0", "0", "1" }, // invalid date until
            new object[] { false, "new description", "2022-05-10T09:13:32.2212678", "2022-04-11T09:13:32.2212678", UID1, "", "", 1, "10", "0", "0", "1" },  // invalid date range
            new object[] { true, "new description", "", "", $"{UID1},{UID3}", "", "", 0, "11", "0", "0", "0" },                  // two users
            new object[] { true, "new description", "", "", "", ITEMTYPE1, "", 0, "11", "0", "0", "0" },                         // one itemtype
            new object[] { true, "new description", "", "", "", $"{ITEMTYPE1},{ITEMTYPE3}", "", 0, "11", "0", "0", "0" },        // two itemtypes
            new object[] { true, "new description", "", "", "", "", "Swap", 0, "11", "0", "0", "0" },                            // one permission
            new object[] { true, "new description", "", "", "", "", "Swap, Return", 0, "11", "0", "0", "0" },                    // two permissions
            new object[] { true, "new description", "", "", "", "", "SWAP", 0, "11", "0", "0", "0" },                            // one permission capitals
            new object[] { true, "new description", "", "", "", "", "SWAP,RETURN", 0, "11", "0", "0", "0" },                     // two permissions capitals
            new object[] { true, "new description", "", "", "", "", "Swap,Swap", 0, "11", "0", "0", "0" },                       // double permission
            new object[] { true, "new description", "", "", " ", " ", " ", 0, "11", "0", "0", "0" },                             // spaces
            new object[] { true, "new description", "", "", "", "", " Swap ", 0, "11", "0", "0", "0" },                          // one permission with spaces
            new object[] { true, "R2", "", "", $"{UID2},{UID3}", $"{ITEMTYPE1}", "Swap", 1, "10", "0", "0", "1" },               // overwrite list
            new object[] { true, DESCRIPTION_INITIAL, "2022-05-10T09:13:32.2212678", "2022-05-11T09:13:32.2212678", $"{UID3}", $"{ITEMTYPE3}", "Return", 1, "10", "1", "0", "0" },       // overwrite database, keep linked tables
            new object[] { true, DESCRIPTION_INITIAL, "", "", $"{UID2},{UID3}", $"{ITEMTYPE1},{ITEMTYPE3}", "Swap,Return", 1, "10", "1", "0", "0" },  // overwrite database, extend linked tables
            new object[] { true, DESCRIPTION_INITIAL, "", "", $"{UID1},{UID2}", $"{ITEMTYPE1},{ITEMTYPE2}", "Swap,Pickup", 1, "10", "1", "0", "0" },  // overwrite database, replace linked tables
            new object[] { false, "Length>250 x012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", "", "", UID1, "", "", 1, "10", "0", "0", "1" }, // invalid description
        };

        [Theory, MemberData(nameof(RoleChecks))]
        public async Task TestImportRoles(bool valid, string description, string validFrom, string validUntil, string users, string itemtypes, string permissions,
                                          int cntDoubleOrInvalid, string newRoles, string changedRoles, string unchangedRoles, string invalidRoles)
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var totalStart = context.CTAMRole().Count();
                var range = Enumerable.Range(1, 10)
                                      .Select(r => new RoleImportDTO
                                      {
                                          Description = "R" + r.ToString(),
                                          CTAMUsersString = $"{UID1},{UID2}",
                                          ItemTypeDescriptions = $"{ITEMTYPE2},{ITEMTYPE3}",
                                          PermissionsString = "Borrow,Repair"
                                      })
                                      .ToList();

                var nw = new RoleImportDTO
                {
                    Description = description,
                    ValidFromDTString = validFrom,
                    ValidUntilDTString = validUntil,
                    CTAMUsersString = users,
                    ItemTypeDescriptions = itemtypes,
                    PermissionsString = permissions
                };
                range.Add(nw);

                var importCommand = new ImportRolesCommand()
                {
                    InputList = range
                };

                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<RoleImportProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();
                var handler = new ImportRolesHandler(context, new Mock<ILogger<ImportRolesHandler>>().Object, mapper, managementLogger);

                // Act
                var result = await handler.Handle(importCommand, It.IsAny<CancellationToken>());

                // Assert
                var totalEnd = context.CTAMRole().Count();
                Assert.Equal(range.Count + totalStart - cntDoubleOrInvalid, totalEnd);
                var first = context.CTAMRole().Include(r => r.CTAMUser_Roles)
                                                    .ThenInclude(ur => ur.CTAMUser)
                                              .Include(r => r.CTAMRole_Permission)
                                                    .ThenInclude(rp => rp.CTAMPermission)
                                              .Where(u => u.Description.Equals("R1")).FirstOrDefault();
                Assert.NotNull(first);
                // Users
                var uids = first.CTAMUser_Roles.Select(ur => ur.CTAMUser.UID).ToList();
                Assert.Equal(2, uids.Count());
                Assert.Contains<string>(UID1, uids);
                Assert.Contains<string>(UID2, uids);
                // ItemTypes
                var itDescDb = context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == first.ID).Select(rit => rit.ItemType.Description).ToList();
                Assert.Equal(2, itDescDb.Count());
                Assert.Contains<string>(ITEMTYPE2, itDescDb);
                Assert.Contains<string>(ITEMTYPE3, itDescDb);
                // Permissions
                var pDescDb = first.CTAMRole_Permission.Select(rp => rp.CTAMPermission.Description.ToUpper()).ToList();
                Assert.Equal(2, pDescDb.Count());
                Assert.Contains<string>("BORROW", pDescDb);
                Assert.Contains<string>("REPAIR", pDescDb);

                if (valid)
                {
                    var newRole = context.CTAMRole().Include(r => r.CTAMUser_Roles)
                                                            .ThenInclude(ur => ur.CTAMUser)
                                                    .Include(r => r.CTAMRole_Permission)
                                                          .ThenInclude(rp => rp.CTAMPermission)
                                                    .Where(r => r.Description.Equals(description)).FirstOrDefault();
                    Assert.NotNull(newRole);
                    if (!string.IsNullOrWhiteSpace(validFrom))
                    {
                        Assert.True(DateTime.TryParse(validFrom, out DateTime dtFrom));
                        Assert.Equal(dtFrom, newRole.ValidFromDT);
                    }
                    if (!string.IsNullOrWhiteSpace(validUntil))
                    {
                        Assert.True(DateTime.TryParse(validUntil, out DateTime dtUntil));
                        Assert.Equal(dtUntil, newRole.ValidUntilDT);
                    }
                    var udb = newRole.CTAMUser_Roles.Select(ur => ur.CTAMUser.UID).ToList();
                    var uimp = users.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(uid => uid.Trim()).ToList();
                    Assert.Equal(udb.Count, uimp.Count);
                    Assert.Empty(udb.Except(uimp));
                    Assert.Empty(uimp.Except(udb));

                    var itdb = context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == newRole.ID).Select(rit => rit.ItemType.Description).ToList();
                    var itimp = itemtypes.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(desc => desc.Trim()).ToList();
                    Assert.Equal(itdb.Count, itimp.Count);
                    Assert.Empty(itdb.Except(itimp));
                    Assert.Empty(itimp.Except(itdb));

                    var pdb = newRole.CTAMRole_Permission.Select(rp => rp.CTAMPermission.Description.ToLower()).ToList();
                    var pimp = permissions.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(desc => desc.Trim().ToLower()).Distinct().ToList();
                    Assert.Equal(pdb.Count, pimp.Count);
                    Assert.Empty(pdb.Except(pimp));
                    Assert.Empty(pimp.Except(pdb));
                }

                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("newRoles"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("changedRoles"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("unchangedRoles"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("invalidRoles"));
                Assert.Equal(newRoles, ParametersOfLastManagementLogInfo["newRoles"]);
                Assert.Equal(changedRoles, ParametersOfLastManagementLogInfo["changedRoles"]);
                Assert.Equal(unchangedRoles, ParametersOfLastManagementLogInfo["unchangedRoles"]);
                Assert.Equal(invalidRoles, ParametersOfLastManagementLogInfo["invalidRoles"]);
            }
        }
    }
}


