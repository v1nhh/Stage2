using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Profiles;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Profiles;
using ItemCabinetModule.ApplicationCore.Queries.Cabinets;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.ItemCabinetModule.IntegrationTests
{
    public class GetCabinetPositionTest : AbstractIntegrationTests
    {
        static Cabinet cabA1 = null;
        readonly static string cabA1_CabinetNumber = "210609095541";

        const int CABCELTYPE_ID_A = 9000;
        const string CABCELTYPE_SPECCODE_A = "ASpecCode";

        const int CABCELTYPE_ID_Z = 9001;
        const string CABCELTYPE_SPECCODE_Z = "ZSpecCode";

        const int CABPOS_ID_AA = 8001;
        const string CABPOS_ALIAS_AA = "AA";

        const int CABPOS_ID_AB = 8002;
        const string CABPOS_ALIAS_AB = "AB";

        const int CABPOS_ID_AC = 8003;
        const string CABPOS_ALIAS_AC = "AC";

        const int CABPOS_ID_ZZ = 8004;
        const string CABPOS_ALIAS_ZZ = "ZZ";

        const int CABPOS_ID_ZY = 8005;
        const string CABPOS_ALIAS_ZY = "ZY";

        const int CABPOS_ID_ZX = 8006;
        const string CABPOS_ALIAS_ZX = "ZX";

        public GetCabinetPositionTest() : base("CTAM_GetItems")
        {
            cabA1 = new Cabinet { Name = "A1", CabinetType = CabinetType.Locker, Description = "Zie A1" };
            cabA1.CabinetNumber = cabA1_CabinetNumber;

            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.Cabinet().Add(cabA1);

                context.CabinetCellType().Add(new CabinetCellType { ID = CABCELTYPE_ID_A, SpecCode = CABCELTYPE_SPECCODE_A });
                context.CabinetCellType().Add(new CabinetCellType { ID = CABCELTYPE_ID_Z, SpecCode = CABCELTYPE_SPECCODE_Z });

                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_AA, PositionAlias = CABPOS_ALIAS_AA, CabinetCellTypeID = CABCELTYPE_ID_A });
                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_AB, PositionAlias = CABPOS_ALIAS_AB, CabinetCellTypeID = CABCELTYPE_ID_Z });
                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_AC, PositionAlias = CABPOS_ALIAS_AC, CabinetCellTypeID = CABCELTYPE_ID_A });
                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_ZZ, PositionAlias = CABPOS_ALIAS_ZZ, CabinetCellTypeID = CABCELTYPE_ID_A });
                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_ZY, PositionAlias = CABPOS_ALIAS_ZY, CabinetCellTypeID = CABCELTYPE_ID_A });
                context.CabinetPosition().Add(new CabinetPosition { CabinetNumber = cabA1_CabinetNumber, ID = CABPOS_ID_ZX, PositionAlias = CABPOS_ALIAS_ZX, CabinetCellTypeID = CABCELTYPE_ID_A });
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, CabinetPositionDetailColumn.PositionAlias, false, "", CABPOS_ID_AA, 2 },
            new object[] { 2, 0, CabinetPositionDetailColumn.PositionAlias, true, "", CABPOS_ID_ZZ, 2 },
            new object[] { 2, 0, null, true, "ZX", CABPOS_ID_ZX, 1 },
            new object[] { 2, 1, CabinetPositionDetailColumn.PositionAlias, false, "", CABPOS_ID_AC, 2 },
            new object[] { 2, 1, CabinetPositionDetailColumn.PositionAlias, true, "", CABPOS_ID_ZX, 2 },
            new object[] { 2, 0, CabinetPositionDetailColumn.SpecCode, true, "", CABPOS_ID_AB, 2 },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, CabinetPositionDetailColumn? column, bool desc, string filter, int firstID, int expectedCount)
        {
            // Arrange
            var query = new GetCabinetPositionContentQuery(cabA1_CabinetNumber, limit, page, column, desc, filter);
            var mapper = new MapperConfiguration(c => {
                c.AddProfile<CabinetPositionsDetailsEnvelopeProfile>();
                c.AddProfile<CabinetPositionWithSpecCodeProfile>();
                c.AddProfile<CabinetCellTypeProfile>();
            }).CreateMapper();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetCabinetPositionContentHandler(context, mapper);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault()?.CabinetPosition;
                Assert.NotNull(first);
                Assert.Equal(firstID, first?.ID);
            }
        }
    }
}
