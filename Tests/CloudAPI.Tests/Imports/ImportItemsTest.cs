using AutoMapper;
using CloudAPI.ApplicationCore.Commands.Items;
using CloudAPI.ApplicationCore.DTO.Import;
using CloudAPI.ApplicationCore.Profiles;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
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
    public class ImportItemsTest : AbstractIntegrationTests
    {
        const string UID1 = "0000c512-f5c9-1e07-891d-b5bc39613c66";
        const string NAME1 = "User1";
        const string EMAIL1 = "u1@ct.nl";
        const string UID2 = "0000dd26-141b-0c79-9b96-e588dad95960";
        const string NAME2 = "User2";
        const string EMAIL2 = "u2@ct.nl";
        const string UID3 = "0000dd26-141b-0c79-9b96-e588dad95961";
        const string NAME3 = "User3";
        const string EMAIL3 = "u3@ct.nl";

        const string ITEMTYPE_DESCRIPTION_1 = "Itemtype 1";
        const int ITEMTYPE_ID_1 = int.MaxValue - 1000;
        const string ITEMTYPE_DESCRIPTION_2 = "Itemtype 2";
        const int ITEMTYPE_ID_2 = int.MaxValue - 1001;
        const string ITEMTYPE_DESCRIPTION_3 = "Itemtype 3";
        const int ITEMTYPE_ID_3 = int.MaxValue - 1002;

        const string ITEM_DESCRIPTION_1 = "Item 1";
        const int ITEM_ID_1 = int.MaxValue - 1000;
        const string ITEM_DESCRIPTION_2 = "Item 2";
        const int ITEM_ID_2 = int.MaxValue - 1001;
        const string ITEM_DESCRIPTION_3 = "Item 3";
        const int ITEM_ID_3 = int.MaxValue - 1002;

        public ImportItemsTest() : base("CTAM_ImportItems")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                var u1 = new CTAMUser { UID = UID1, Name = NAME1, Email = EMAIL1, LanguageCode = "nl-NL" };
                var u2 = new CTAMUser { UID = UID2, Name = NAME2, Email = EMAIL2, LanguageCode = "nl-NL" };
                var u3 = new CTAMUser { UID = UID3, Name = NAME3, Email = EMAIL3, LanguageCode = "nl-NL" };
                context.CTAMUser().AddRange(new[] { u1, u2, u3 });

                var it1 = new ItemType { ID = ITEMTYPE_ID_1, Description = ITEMTYPE_DESCRIPTION_1 };
                var it2 = new ItemType { ID = ITEMTYPE_ID_2, Description = ITEMTYPE_DESCRIPTION_2 };
                var it3 = new ItemType { ID = ITEMTYPE_ID_3, Description = ITEMTYPE_DESCRIPTION_3 };
                context.ItemType().AddRange(new[] { it1, it2, it3 });

                var i1 = new Item { ID = ITEM_ID_1, Description = ITEM_DESCRIPTION_1, ItemType = it1, Status = ItemStatus.ACTIVE, CreateDT = DateTime.UtcNow };
                var i2 = new Item { ID = ITEM_ID_2, Description = ITEM_DESCRIPTION_2, ItemType = it2, Status = ItemStatus.ACTIVE, CreateDT = DateTime.UtcNow };
                var i3 = new Item { ID = ITEM_ID_3, Description = ITEM_DESCRIPTION_3, ItemType = it3, Status = ItemStatus.ACTIVE, CreateDT = DateTime.UtcNow , ExternalReferenceID = "123456"};
                context.Item().AddRange(new[] { i1, i2, i3 });

                var pers1 = new CTAMUserPersonalItem { ItemID = ITEM_ID_2, CTAMUserUID = UID3, Status = UserPersonalItemStatus.OK, CreateDT = DateTime.UtcNow };
                var pers2 = new CTAMUserPersonalItem { ItemID = ITEM_ID_3, CTAMUserUID = UID3, Status = UserPersonalItemStatus.OK, CreateDT = DateTime.UtcNow };
                context.CTAMUserPersonalItem().AddRange(new[] {pers1, pers2 });

                var poss1 = new CTAMUserInPossession { ItemID = ITEM_ID_2, CTAMUserUIDOut = UID3, Status = UserInPossessionStatus.Picked, CreatedDT = DateTime.UtcNow, OutDT = DateTime.UtcNow };
                var poss2 = new CTAMUserInPossession { ItemID = ITEM_ID_3, CTAMUserUIDOut = UID3, Status = UserInPossessionStatus.Picked, CreatedDT = DateTime.UtcNow, OutDT = DateTime.UtcNow };
                context.CTAMUserInPossession().AddRange(new[] { poss1, poss2 });

                context.SaveChanges();
            }
        }

        public static readonly object[][] ItemChecks =
        {
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, UID1, true, 0, "101", "0", "0", "0" },   // new
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" },      // new, invalid tagnumber
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, " ", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" },     // new, invalid tagnumber
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "Length>40 x012345678901234567890123456789", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" },   // new, invalid tagnumber
            new object[] { true, ITEM_DESCRIPTION_2, ITEMTYPE_DESCRIPTION_2, "1234", "", UID3, true, UID3, true, 1, "100", "1", "0", "0" },      // exist in db, same personal and possession, tagnumber changed
            new object[] { false, ITEM_DESCRIPTION_2, ITEMTYPE_DESCRIPTION_3, "1234", "", UID3, true, UID3, true, 1 , "100", "1", "0", "0"},      // exist in db, same personal and possession, changed itemtype
            new object[] { true, ITEM_DESCRIPTION_2, ITEMTYPE_DESCRIPTION_2, "1234", "", UID2, true, UID3, true, 1, "100", "1", "0", "0" },      // exist in db, changed personal and same possession
            new object[] { true, ITEM_DESCRIPTION_2, ITEMTYPE_DESCRIPTION_2, "1234", "", UID3, true, UID2, true, 1, "100", "1", "0", "0" },      // exist in db, same personal and changed possession
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, UID2, true, 0, "101", "0", "0", "0" },   // new, personal and possession differ
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", "X", false, UID2, true, 0, "101", "0", "0", "1" },   // new, invalid personal but still imported
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", " ", false, UID2, true, 0, "101", "0", "0", "0" },   // new, invalid personal but whitespace is ignored
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, "X", false, 0, "101", "0", "0", "1" },   // new, invalid possession but still imported
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, " ", false, 0, "101", "0", "0", "0" },   // new, invalid possession but whitespace is ignored
            new object[] { true, "I2", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" },                                              // overwrite list so double in import
            new object[] { true, ITEM_DESCRIPTION_1, ITEMTYPE_DESCRIPTION_1, "1234", "", UID1, true, UID1, true, 1, "100", "1", "0", "0" },                                // overwrite database
            new object[] { false, "", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, UID1, true, 1 , "100", "0", "0", "1"},                                               // invalid description
            new object[] { false, "Length>250 x012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789", ITEMTYPE_DESCRIPTION_3, "1234", "", UID1, true, UID1, true, 1 , "100", "0", "0", "1"}, // invalid description
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", "", "1234", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" }, // invalid itemType
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", "XXX", "1234", "", UID1, true, UID1, true, 1, "100", "0", "0", "1" }, // invalid itemType
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "" ,UID1, true, UID1, true, 0, "101", "0", "0", "0" },   // new, valid externalID
            new object[] { true, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "123456" , UID1, true, UID1, true, 0, "101", "0", "0", "0" },   // new, valid externalID
            new object[] { false, "Portofoon - noord-holland - verkeer - PLX9999", ITEMTYPE_DESCRIPTION_3, "1234", "Length>40 x012345678901234567890123456789", UID1, true, UID1, true, 1, "100", "0", "0", "1" },   // new, invalid externalID
            new object[] { true, ITEM_DESCRIPTION_3, ITEMTYPE_DESCRIPTION_3, "1234", "234567", UID3, true, UID3, true, 1, "100", "1", "0", "0" },      // exist in db, new externalID
        };

        [Theory, MemberData(nameof(ItemChecks))]
        public async Task TestImportItems(bool valid, string description, string itemType, string tagnumber, string externalReferenceID, string personalUID, bool personalValid, string possessionUID, bool possessionValid,
                                          int cntDoubleOrInvalid, string newItems, string changedItems, string unchangedItems, string invalidItems)
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var totalStart = context.Item().Count();
                var userIDs = new string[] { UID1, UID2, UID3 };
                var range = Enumerable.Range(1, 100)
                                      .Select(r => new ItemImportDTO
                                      {
                                          Description = "I" + r.ToString(),
                                          ItemTypeDescription = ITEMTYPE_DESCRIPTION_1,
                                          Tagnumber = "Tag" + r.ToString(),
                                          CTAMUserInPossessionString = userIDs[r % 3],
                                          CTAMUserPersonalItemString = userIDs[r % 3],
                                          ExternalReferenceID = "Ext" + r.ToString()
                                      })
                                      .ToList();

                var nw = new ItemImportDTO
                {
                    Description = description,
                    ItemTypeDescription = itemType,
                    Tagnumber = tagnumber,
                    CTAMUserPersonalItemString = personalUID,
                    CTAMUserInPossessionString = possessionUID,
                    ExternalReferenceID = externalReferenceID
                };
                range.Add(nw);

                var importCommand = new ImportItemsCommand()
                {
                    InputList = range
                };

                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemImportProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();
                var handler = new ImportItemsHandler(context, new Mock<ILogger<ImportItemsHandler>>().Object, mapper, managementLogger);

                // Act
                var result = await handler.Handle(importCommand, It.IsAny<CancellationToken>());

                // Assert
                var totalEnd = await context.Item().CountAsync();
                Assert.Equal(range.Count + totalStart - cntDoubleOrInvalid, totalEnd);
                var first = await context.Item().Where(i => i.Description.Equals("I1")).FirstOrDefaultAsync();
                var pers = await context.CTAMUserPersonalItem().Include(upi => upi.Item).Where(upi => upi.CTAMUserUID.Equals(UID2) && upi.ItemID == first.ID).FirstOrDefaultAsync();
                var poss = await context.CTAMUserInPossession().Include(uip => uip.Item).Where(uip => uip.CTAMUserUIDOut.Equals(UID2) && uip.ItemID == first.ID).FirstOrDefaultAsync();
                Assert.NotNull(first);
                Assert.Equal("Tag1", first.Tagnumber);
                Assert.NotNull(pers);
                Assert.NotNull(poss);
                Assert.Equal("I1", pers.Item.Description);
                Assert.Equal(UserPersonalItemStatus.OK, pers.Status);
                Assert.Equal("I1", poss.Item.Description);
                Assert.Equal(UserInPossessionStatus.Picked, poss.Status);
                Assert.Equal(ItemStatus.ACTIVE, first.Status);

                if (valid)
                {
                    var newItem = context.Item().Include(i => i.ItemType).Where(i => i.Description.Equals(description)).FirstOrDefault();
                    Assert.NotNull(newItem);
                    Assert.Equal(tagnumber, newItem.Tagnumber);
                    Assert.Equal(itemType, newItem.ItemType.Description);
                    Assert.Equal(externalReferenceID, newItem.ExternalReferenceID);

                    var newPers = await context.CTAMUserPersonalItem().Include(upi => upi.Item).Where(upi => upi.CTAMUserUID.Equals(personalUID) && upi.ItemID == newItem.ID).FirstOrDefaultAsync();
                    if (personalValid)
                    {
                        Assert.Equal(description, newPers.Item.Description);
                        Assert.Equal(UserPersonalItemStatus.OK, newPers.Status);
                    }
                    else
                    {
                        Assert.Null(newPers);
                    }

                    var newPoss = await context.CTAMUserInPossession().Include(uip => uip.Item).Where(uip => uip.Status == UserInPossessionStatus.Picked && uip.ItemID == newItem.ID).FirstOrDefaultAsync();
                    if (possessionValid)
                    {
                        Assert.Equal(description, newPoss.Item.Description);
                        Assert.Equal(possessionUID, newPoss.CTAMUserUIDOut);
                        Assert.Equal(UserInPossessionStatus.Picked, newPoss.Status);
                    }
                    else
                    {
                        Assert.Null(newPoss);
                    }

                    Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("newItems"));
                    Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("changedItems"));
                    Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("unchangedItems"));
                    Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("invalidItems"));
                    Assert.Equal(newItems, ParametersOfLastManagementLogInfo["newItems"]);
                    Assert.Equal(changedItems, ParametersOfLastManagementLogInfo["changedItems"]);
                    Assert.Equal(unchangedItems, ParametersOfLastManagementLogInfo["unchangedItems"]);
                    Assert.Equal(invalidItems, ParametersOfLastManagementLogInfo["invalidItems"]);
                }
            }
        }
    }
}


