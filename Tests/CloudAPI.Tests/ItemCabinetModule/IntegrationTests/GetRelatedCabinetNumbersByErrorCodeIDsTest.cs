using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class GetRelatedCabinetNumbersByErrorCodeIDsTest : AbstractIntegrationTests
    {
        private const int ITEM_TYPE_ID = int.MaxValue;
        private const string ITEM_TYPE_DESCRIPTION = "Portofoon";

        private const int ITEM_TYPE_2_ID = int.MaxValue - 1;
        private const string ITEM_TYPE_2_DESCRIPTION = "Laptop";

        private const int ERROR_CODE_ID = int.MaxValue;
        private const string ERROR_CODE_CODE = "01";
        private const string ERROR_CODE_DESCRIPTION = "Antenne";

        private const int ERROR_2_CODE_ID = int.MaxValue - 1;
        private const string ERROR_2_CODE_CODE = "02";
        private const string ERROR_2_CODE_DESCRIPTION = "Description2";

        private const int ERROR_3_CODE_ID = int.MaxValue - 2;
        private const string ERROR_3_CODE_CODE = "03";
        private const string ERROR_3_CODE_DESCRIPTION = "Description3";

        private const int ROLE_1_ID = int.MaxValue;
        private const string ROLE_1_DESCRIPION = "RolWithCabinet";

        private const int ROLE_2_ID = int.MaxValue - 1;
        private const string ROLE_2_DESCRIPION = "RolWithoutCabinet";

        private const string CABINET_NAME = "IBK TEST 1";
        private const string CABINET_NUMBER = "01";

        public GetRelatedCabinetNumbersByErrorCodeIDsTest() : base("CTAM_GetRelatedCabinetNumbersByErrorCodeIDs")
        {
            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.ItemType().Add(new ItemType() { ID = ITEM_TYPE_ID, Description = ITEM_TYPE_DESCRIPTION });
                context.ItemType().Add(new ItemType() { ID = ITEM_TYPE_2_ID, Description = ITEM_TYPE_2_DESCRIPTION });

                context.ErrorCode().Add(new ErrorCode() { ID = ERROR_CODE_ID, Code = ERROR_CODE_CODE, Description = ERROR_CODE_DESCRIPTION });
                context.ErrorCode().Add(new ErrorCode() { ID = ERROR_2_CODE_ID, Code = ERROR_2_CODE_CODE, Description = ERROR_2_CODE_DESCRIPTION });
                context.ErrorCode().Add(new ErrorCode() { ID = ERROR_3_CODE_ID, Code = ERROR_3_CODE_CODE, Description = ERROR_3_CODE_DESCRIPTION });


                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode() { ItemTypeID = ITEM_TYPE_ID, ErrorCodeID = ERROR_CODE_ID });
                context.ItemType_ErrorCode().Add(new ItemType_ErrorCode() { ItemTypeID = ITEM_TYPE_2_ID, ErrorCodeID = ERROR_3_CODE_ID });


                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_1_ID, Description = ROLE_1_DESCRIPION, CreateDT = DateTime.UtcNow });
                context.CTAMRole().Add(new CTAMRole() { ID = ROLE_2_ID, Description = ROLE_2_DESCRIPION, CreateDT = DateTime.UtcNow });

                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_1_ID, ItemTypeID = ITEM_TYPE_ID });
                context.CTAMRole_ItemType().Add(new CTAMRole_ItemType() { CTAMRoleID = ROLE_2_ID, ItemTypeID = ITEM_TYPE_2_ID });


                context.Cabinet().Add(new Cabinet() { CabinetNumber = CABINET_NUMBER, Name = CABINET_NAME });

                context.CTAMRole_Cabinet().Add(new CTAMRole_Cabinet() { CabinetNumber = CABINET_NUMBER, CTAMRoleID = ROLE_1_ID});
                context.SaveChanges();
            }
        }

        public static readonly object[][] ErrorCodeCabinetsCheck =
        {
            new object[]{ 
                new List<int> { ERROR_CODE_ID }, new Dictionary<int, List<string>>() { { ERROR_CODE_ID, new List<string>() { CABINET_NUMBER } } }, 
            },
            // Niet verbonden aan een itemtype
            new object[]{
                new List<int> { ERROR_2_CODE_ID }, new Dictionary<int, List<string>>() {  },
            },
            // wel verbonden aan itemtype en rol, maar niet aan een role_cabinet
            new object[]{
                new List<int> { ERROR_3_CODE_ID }, new Dictionary<int, List<string>>() {  },
            },
        };

        [Theory, MemberData(nameof(ErrorCodeCabinetsCheck))]
        public async Task TestErrorCodeCabinetsCheck(List<int> errorCodeIDs, Dictionary<int, List<string>> groupedCabinetNumbers)
        {
            //  Arrange
            var checkQuery = new GetRelatedCabinetNumbersByErrorCodeIDsQuery(errorCodeIDs);

            using (var context = new MainDbContext(ContextOptions, null))
            {
                // Arrange handler
                var logger = new Mock<ILogger<GetRelatedCabinetNumbersByErrorCodeIDsHandler>>().Object;
                var handler = new GetRelatedCabinetNumbersByErrorCodeIDsHandler(context, logger);

                // Act
                var result = await handler.Handle(checkQuery, It.IsAny<CancellationToken>());

                // Assert
                Assert.Equal(groupedCabinetNumbers, result);
            }
        }
    }
}
