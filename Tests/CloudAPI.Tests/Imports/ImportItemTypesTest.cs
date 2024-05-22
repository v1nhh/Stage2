using AutoMapper;
using CTAM.Core;
using ItemModule.ApplicationCore.Commands.ItemTypes;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.Imports
{
    public class ImportItemTypesTest : AbstractIntegrationTests
    {
        const string DESCRIPTION_INITIAL = "0000a5e8-f68a-484d-7f83-322561bf2686";
        const TagType TAGTYPE_INTITIAL = TagType.LF;
        const double DEPTH_INTITIAL = 1;
        const double WIDTH_INTITIAL = 2;
        const double HEIGHT_INTITIAL = 3;

        const string CODE1 = "X1";
        const string DESCRIPTION1 = "DescriptionX1";
        const string CODE2 = "X2";
        const string DESCRIPTION2 = "DescriptionX2";
        const string CODE3 = "X3";
        const string DESCRIPTION3 = "DescriptionX3";

        public ImportItemTypesTest() : base("CTAM_ImportItemTypes")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                var ec1 = new ErrorCode { Code = CODE1, Description = DESCRIPTION1 };
                var ec2 = new ErrorCode { Code = CODE2, Description = DESCRIPTION2 };
                var ec3 = new ErrorCode { Code = CODE3, Description = DESCRIPTION3 };
                context.ErrorCode().Add(ec1);
                context.ErrorCode().Add(ec2);
                context.ErrorCode().Add(ec3);

                var it = new ItemType { Description = DESCRIPTION_INITIAL, TagType = TAGTYPE_INTITIAL, Depth = DEPTH_INTITIAL, Width = WIDTH_INTITIAL, Height = HEIGHT_INTITIAL };
                context.ItemType().Add(it);

                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode { ItemType = it, ErrorCode = ec1 });
                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode { ItemType = it, ErrorCode = ec2 });

                context.SaveChanges();
            }
        }

        public static readonly object[][] ItemTypeChecks =
        {
            new object[] { true, "new description", "UHF", "2.1", "2.2", "2.3", "", 0, "11", "0", "0", "0" },                   // new tagtype string
            new object[] { false, "new description", "XUHF", "2.1", "2.2", "2.3", "", 1, "10", "0", "0", "1" },                 // invalid tagtype string
            new object[] { true, "new description", "2", "2.1", "2.2", "2.3", "", 0, "11", "0", "0", "0" },                     // new tagtype int
            new object[] { false, "new description", "99", "2.1", "2.2", "2.3", "", 1 , "10", "0", "0", "1"},                   // invalid tagtype int
            new object[] { true, "new description", "UHF", "2.1", "2.2", "2.3", CODE1, 0, "11", "0", "0", "0" },                // one errorcode
            new object[] { true, "new description", "UHF", "2.1", "2.2", "2.3", $"{CODE1},{CODE3}", 0 , "11", "0", "0", "0"},   // two errorcodes
            new object[] { true, "IT9", "UHF", "2.1", "2.2", "2.3", "", 1, "10", "0", "0", "1" },                               // overwrite, remove errorcodes
            new object[] { true, "IT9", "UHF", "2.1", "2.2", "2.3", CODE1, 1, "10", "0", "0", "1" },                            // overwrite, one errorcode
            new object[] { true, "IT9", "UHF", "2.1", "2.2", "2.3", $"{CODE1},{CODE3}", 1, "10", "0", "0", "1" },               // overwrite, two errorcodes
            new object[] { true, DESCRIPTION_INITIAL, "UHF", "2.1", "2.2", "2.3", "", 1, "10", "1", "0", "0" },                 // overwrite db, remove errorcodes
            new object[] { true, DESCRIPTION_INITIAL, "UHF", "2.1", "2.2", "2.3", CODE1, 1, "10", "1", "0", "0" },              // overwrite db, one errorcode
            new object[] { true, DESCRIPTION_INITIAL, "UHF", "2.1", "2.2", "2.3", $"{CODE1},{CODE3}", 1 , "10", "1", "0", "0"}, // overwrite db, two errorcodes
        };

        [Theory, MemberData(nameof(ItemTypeChecks))]
        public async Task TestImportItemTypes(bool valid, string description, string tagtype, string depth, string width, string height, string errorCodes,
                                              int cntDoubleOrInvalid, string newItemTypes, string changedItemTypes, string unchangedItemTypes, string invalidItemTypes)
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange
                var totalStart = context.ItemType().Count();
                var range = Enumerable.Range(1, 10)
                                      .Select(r => new ItemTypeImportDTO
                                      {
                                          Description = "IT" + r.ToString(),
                                          TagTypeString = "1",
                                          DepthString = "12",
                                          WidthString = "13",
                                          HeightString = "14",
                                          ErrorCodesString = $"{CODE1},{CODE2}"
                                      })
                                      .ToList();

                var nw = new ItemTypeImportDTO
                {
                    Description = description,
                    TagTypeString = tagtype,
                    DepthString = depth,
                    WidthString = width,
                    HeightString = height,
                    ErrorCodesString = errorCodes
                };
                range.Add(nw);

                var importCommand = new ImportItemTypesCommand()
                {
                    InputList = range
                };

                var mapper = new MapperConfiguration(c => {
                    c.AddProfile<ItemTypeProfile>();
                }).CreateMapper();

                var managementLogger = CreateMockedManagementLogger();
                var handler = new ImportItemTypesHandler(context, new Mock<ILogger<ImportItemTypesHandler>>().Object, mapper, managementLogger);

                // Act
                var result = await handler.Handle(importCommand, It.IsAny<CancellationToken>());

                // Assert
                var totalEnd = context.ItemType().Count();
                Assert.Equal(range.Count + totalStart - cntDoubleOrInvalid, totalEnd);
                var first = context.ItemType().Include(it => it.ItemType_ErrorCode)
                                                    .ThenInclude(itec => itec.ErrorCode)
                                              .Where(u => u.Description.Equals("IT1")).FirstOrDefault();
                Assert.NotNull(first);
                var codes = first.ItemType_ErrorCode.Select(itec => itec.ErrorCode.Code).ToList();
                Assert.Equal(2, codes.Count());
                Assert.Contains<string>(CODE1, codes);
                Assert.Contains<string>(CODE2, codes);
                if (valid)
                {
                    var newItemType = context.ItemType().Include(it => it.ItemType_ErrorCode)
                                                            .ThenInclude(itec => itec.ErrorCode)
                                                        .Where(it => it.Description.Equals(description)).FirstOrDefault();
                    Assert.NotNull(newItemType);
                    Assert.True(tagtype.Equals(newItemType.TagType.ToString()) || tagtype.Equals(((int)newItemType.TagType).ToString()));
                    Assert.Equal(depth, newItemType.Depth.ToString(CultureInfo.InvariantCulture));
                    Assert.Equal(width, newItemType.Width.ToString(CultureInfo.InvariantCulture));
                    Assert.Equal(height, newItemType.Height.ToString(CultureInfo.InvariantCulture));
                    var ecsdb = newItemType.ItemType_ErrorCode.Select(itec => itec.ErrorCode.Code).ToList();
                    var ecsimp = errorCodes.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ec => ec.Trim()).ToList();
                    Assert.Empty(ecsdb.Except(ecsimp));
                    Assert.Empty(ecsimp.Except(ecsdb));
                }

                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("newItemTypes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("changedItemTypes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("unchangedItemTypes"));
                Assert.True(ParametersOfLastManagementLogInfo.ContainsKey("invalidItemTypes"));
                Assert.Equal(newItemTypes, ParametersOfLastManagementLogInfo["newItemTypes"]);
                Assert.Equal(changedItemTypes, ParametersOfLastManagementLogInfo["changedItemTypes"]);
                Assert.Equal(unchangedItemTypes, ParametersOfLastManagementLogInfo["unchangedItemTypes"]);
                Assert.Equal(invalidItemTypes, ParametersOfLastManagementLogInfo["invalidItemTypes"]);
            }
        }

    }
}


