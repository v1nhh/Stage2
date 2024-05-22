using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Profiles;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CTAM.Core;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudAPI.Tests.CabinetModule.IntegrationTests
{
    public class GetCabinetsTest : AbstractIntegrationTests
    {
        static Cabinet cabA1 = null;
        readonly static string cabA1_CabinetNumber = "210609095541";
        static Cabinet cabA2 = null;
        readonly static string cabA2_CabinetNumber = "210609095542";
        static Cabinet cabA3 = null;
        readonly static string cabA3_CabinetNumber = "210609095543";
        static Cabinet cabZ1 = null;
        readonly static string cabZ1_CabinetNumber = "210609095544";
        static Cabinet cabZ2 = null;
        readonly static string cabZ2_CabinetNumber = "210609095545";
        static Cabinet cabZ3 = null;
        readonly static string cabZ3_CabinetNumber = "210609095546";

        public GetCabinetsTest() : base("CTAM_GetCabinets")
        {
            cabA1 = new Cabinet { Name = "A1", CabinetType = CabinetType.Locker, Description = "Zie A1" };
            cabA1.CabinetNumber = cabA1_CabinetNumber;

            cabA2 = new Cabinet { Name = "A2", CabinetType = CabinetType.Locker, Description = "Desc A2" };
            cabA2.CabinetNumber = cabA2_CabinetNumber;

            cabA3 = new Cabinet { Name = "A3", CabinetType = CabinetType.Locker, Description = "Desc A3" };
            cabA3.CabinetNumber = cabA3_CabinetNumber;

            cabZ1 = new Cabinet { Name = "Z1", CabinetType = CabinetType.Locker, Description = "Desc Z1" };
            cabZ1.CabinetNumber = cabZ1_CabinetNumber;

            cabZ2 = new Cabinet { Name = "Z2", CabinetType = CabinetType.Locker, Description = "Desc Z2" };
            cabZ2.CabinetNumber = cabZ2_CabinetNumber;

            cabZ3 = new Cabinet { Name = "Z3", CabinetType = CabinetType.Locker, Description = "Desc Z3" };
            cabZ3.CabinetNumber = cabZ3_CabinetNumber;

            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.Cabinet().Add(cabA1);
                context.Cabinet().Add(cabA2);
                context.Cabinet().Add(cabA3);
                context.Cabinet().Add(cabZ1);
                context.Cabinet().Add(cabZ2);
                context.Cabinet().Add(cabZ3);
                context.SaveChanges();
            }
        }

        public static readonly object[][] LimitSortedDescFiltered =
        {
            new object[] { 2, 0, CabinetDisplayColumn.Name, false, "", cabA1_CabinetNumber, 2 },
            new object[] { 2, 0, CabinetDisplayColumn.Description, false, "", cabA2_CabinetNumber, 2 },
            new object[] { 2, 0, CabinetDisplayColumn.Description, true, "", cabA1_CabinetNumber, 2 },
            new object[] { 2, 1, CabinetDisplayColumn.Name, false, "", cabA3_CabinetNumber, 2 },
            new object[] { 2, 1, CabinetDisplayColumn.Name, true, "", cabZ1_CabinetNumber, 2 },
            new object[] { 2, 0, CabinetDisplayColumn.Name, false, "Z2", cabZ2_CabinetNumber, 1 },
        };

        [Theory, MemberData(nameof(LimitSortedDescFiltered))]
        public async Task TestSortFiltered(int limit, int page, CabinetDisplayColumn? column, bool desc, string filter, string firstID, int expectedCount)
        {
            // Arrange
            var query = new GetAllCabinetsQuery(limit, page, column, desc, filter);
            var logger = new Mock<ILogger<GetAllCabinetsHandler>>().Object;
            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<CabinetProfile>();
            }).CreateMapper();

            using (var context = new MainDbContext(ContextOptions, null))
            {
                var handler = new GetAllCabinetsHandler(context, logger, mapper);
                // Act
                var pagres = await handler.Handle(query, It.IsAny<CancellationToken>());

                // Assert
                Assert.NotNull(pagres);
                Assert.Equal(expectedCount, pagres.Limit);
                Assert.NotNull(pagres.Objects);
                Assert.Equal(expectedCount, pagres.Objects.Count);
                var first = pagres.Objects.FirstOrDefault();
                Assert.NotNull(first);
                Assert.Equal(firstID, first.CabinetNumber);
            }
        }

    }
}
