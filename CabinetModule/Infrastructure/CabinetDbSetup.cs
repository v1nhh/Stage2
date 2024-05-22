using CabinetModule.ApplicationCore.Entities;
using CabinetModule.Infrastructure.InitialData;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<Cabinet> Cabinet(this MainDbContext mainDbContext) => mainDbContext.Set<Cabinet>();

        public static DbSet<CTAMRole_Cabinet> CTAMRole_Cabinet(this MainDbContext mainDbContext) => mainDbContext.Set<CTAMRole_Cabinet>();

        public static DbSet<CabinetAccessInterval> CabinetAccessIntervals(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetAccessInterval>();

        public static DbSet<CabinetAction> CabinetAction(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetAction>();

        public static DbSet<CabinetCell> CabinetCell(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetCell>();

        public static DbSet<CabinetCellType> CabinetCellType(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetCellType>();

        public static DbSet<CabinetColumn> CabinetColumn(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetColumn>();

        public static DbSet<CabinetDoor> CabinetDoor(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetDoor>();

        public static DbSet<CabinetLog> CabinetLog(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetLog>();

        public static DbSet<CabinetPosition> CabinetPosition(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetPosition>();

        public static DbSet<CabinetUI> CabinetUI(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetUI>();

        public static DbSet<CabinetProperties> CabinetProperties(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetProperties>();
    }
}

namespace CabinetModule.Infrastructure
{
    public class CabinetDbSetup : AbstractDbSetup
    {
        public const string CabinetSchema = "Cabinet";

        public override void OnModelCreating(ModelBuilder modelBuilder, DbContext dbContext)
        {
            AssignTablesToSchema(modelBuilder);
            AddCustomValueConverters(modelBuilder, dbContext);
            DefineManyToManyRelations(modelBuilder);
            DefineOneToManyRelations(modelBuilder);
            DefineOneToOneRelations(modelBuilder);
            ConfigureColumns(modelBuilder);
            InitialInserts(modelBuilder);
        }

        public override void AssignTablesToSchema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>()
                .ToTable("Cabinet", CabinetSchema);

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .ToTable("CTAMRole_Cabinet", CabinetSchema);

            modelBuilder.Entity<CabinetAccessInterval>()
                .ToTable("CabinetAccessIntervals", CabinetSchema);

            modelBuilder.Entity<CabinetAction>()
                .ToTable("CabinetAction", CabinetSchema);

            modelBuilder.Entity<CabinetCell>()
                .ToTable("CabinetCell", CabinetSchema);

            modelBuilder.Entity<CabinetCellType>()
                .ToTable("CabinetCellType", CabinetSchema);

            modelBuilder.Entity<CabinetColumn>()
                .ToTable("CabinetColumn", CabinetSchema);

            modelBuilder.Entity<CabinetDoor>()
                .ToTable("CabinetDoor", CabinetSchema);

            modelBuilder.Entity<CabinetLog>()
                .ToTable("CabinetLog", CabinetSchema);

            modelBuilder.Entity<CabinetPosition>()
                .ToTable("CabinetPosition", CabinetSchema);

            modelBuilder.Entity<CabinetUI>()
                .ToTable("CabinetUI", CabinetSchema);

            modelBuilder.Entity<CabinetProperties>()
                .ToTable("CabinetProperties", CabinetSchema);

        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext) { }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {
            // Cabinet - Role (many-to-many)
            modelBuilder.Entity<CTAMRole_Cabinet>()
                .HasKey(t => new { t.CTAMRoleID, t.CabinetNumber });

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .HasOne(cabinetRole => cabinetRole.Cabinet)
                .WithMany(cabinet => cabinet.CTAMRole_Cabinets)
                .HasForeignKey(cabinetRole => cabinetRole.CabinetNumber);

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .HasOne(cabinetRole => cabinetRole.CTAMRole)
                .WithMany() // (role => role.CTAMRole_Cabinet)
                .HasForeignKey(cabinetRole => cabinetRole.CTAMRoleID);
        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // Role - CabinetAccessInterval (one-to-many)
            modelBuilder.Entity<CabinetAccessInterval>()
                .HasOne(cabinetAccessIntervals => cabinetAccessIntervals.CTAMRole)
                .WithMany()
                .HasForeignKey(cabinetAccessIntervals => cabinetAccessIntervals.CTAMRoleID);

            // Cabinet - CabinetPosition (one-to-many)
            modelBuilder.Entity<CabinetPosition>()
                .HasOne(cabinetPosition => cabinetPosition.Cabinet)
                .WithMany(cabinet => cabinet.CabinetPositions)
                .HasForeignKey(cabinetPosition => cabinetPosition.CabinetNumber);

            // Cabinet - CabinetColumn (one-to-many)
            modelBuilder.Entity<CabinetColumn>()
                .HasOne(cabinetColumn => cabinetColumn.Cabinet)
                .WithMany(cabinet => cabinet.CabinetColumns)
                .HasForeignKey(cabinetColumn => cabinetColumn.CabinetNumber);

            // CabinetDoor - CabinetPosition (one-to-many)
            modelBuilder.Entity<CabinetPosition>()
                .HasOne(cabinetPosition => cabinetPosition.CabinetDoor)
                .WithMany()
                .HasForeignKey(cabinetPosition => cabinetPosition.CabinetDoorID);

            // CabinetCellType - CabinetPosition (one-to-many)
            modelBuilder.Entity<CabinetPosition>()
                .HasOne(cabinetPosition => cabinetPosition.CabinetCellType)
                .WithMany()
                .HasForeignKey(cabinetPosition => cabinetPosition.CabinetCellTypeID);

            // CabinetCellType - CabinetCell (one-to-many)
            modelBuilder.Entity<CabinetCell>()
                .HasOne(cabinetCell => cabinetCell.CabinetCellType)
                .WithMany()
                .HasForeignKey(cabinetCell => cabinetCell.CabinetCellTypeID);

            // CabinetColumn - CabinetCell (one-to-many)
            modelBuilder.Entity<CabinetCell>()
                .HasOne(cabinetCell => cabinetCell.CabinetColumn)
                .WithMany()
                .HasForeignKey(cabinetCell => cabinetCell.CabinetColumnID);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {
            // Cabinet - CabinetProperties (one-to-one)
            modelBuilder.Entity<Cabinet>()
                .HasOne(cabinet => cabinet.CabinetProperties)
                .WithOne(cabinetProperties => cabinetProperties.Cabinet)
                .HasForeignKey<CabinetProperties>(cabinetProperties => cabinetProperties.CabinetNumber);
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>()
                .HasKey(c => c.CabinetNumber);

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.Name).HasMaxLength(250);

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.Description).HasMaxLength(250);

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.LocationDescr).HasMaxLength(250);

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.CabinetConfiguration)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.Email).HasMaxLength(250);

            modelBuilder.Entity<Cabinet>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Cabinet>()
                .Property(c => c.CabinetErrorMessage).HasMaxLength(250);

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .HasIndex(c => new { c.CabinetNumber, c.CTAMRoleID })
                .IsUnique(true);

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CabinetAction>()
               .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CabinetAction>()
               .Property(c => c.CabinetName).HasMaxLength(250);

            modelBuilder.Entity<CabinetAction>()
                .Property(c => c.PositionAlias).HasMaxLength(10);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.CTAMUserUID).HasMaxLength(50);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.CTAMUserName).HasMaxLength(250);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.CTAMUserEmail).HasMaxLength(250);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.PutItemDescription).HasMaxLength(250);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.TakeItemDescription).HasMaxLength(250);

            modelBuilder.Entity<CabinetAction>()
                .Property(ca => ca.ErrorCodeDescription).HasMaxLength(250);

            modelBuilder.Entity<CabinetCell>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.SpecCode).HasMaxLength(30);

            modelBuilder.Entity<CabinetCellType>()
                .HasIndex(c => c.SpecCode)
                .IsUnique(true);

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.ShortDescr).HasMaxLength(50);

            modelBuilder.Entity<CabinetCellType>()
                .HasIndex(c => c.ShortDescr)
                .IsUnique(true);

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.LongDescr)
                .HasColumnType("nvarchar(250)");

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.Picture)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.Material).HasMaxLength(50);

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.Color).HasMaxLength(50);

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.LockType).HasMaxLength(50);

            modelBuilder.Entity<CabinetCellType>()
                .Property(c => c.Reference).HasMaxLength(50);

            modelBuilder.Entity<CabinetCellType>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CabinetColumn>()
               .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CabinetColumn>()
                .Property(c => c.TemplateName).HasMaxLength(50);

            modelBuilder.Entity<CabinetColumn>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CabinetDoor>()
                .Property(c => c.Alias).HasMaxLength(20);


            modelBuilder.Entity<CabinetLog>()
               .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CabinetLog>()
               .Property(c => c.CabinetName).HasMaxLength(250);

            modelBuilder.Entity<CabinetLog>()
                .Property(c => c.LogResourcePath)
                .HasColumnType("nvarchar(max)");


            modelBuilder.Entity<CabinetPosition>()
               .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CabinetPosition>()
                .Property(c => c.PositionNumber).HasMaxLength(10);

            modelBuilder.Entity<CabinetPosition>()
                .HasIndex(c => new { c.CabinetNumber, c.PositionNumber })
                .IsUnique(true);

            modelBuilder.Entity<CabinetPosition>()
                .Property(c => c.PositionAlias).HasMaxLength(20);

            modelBuilder.Entity<CabinetPosition>()
                .HasIndex(c => new { c.CabinetNumber, c.PositionAlias })
                .IsUnique(true);


            modelBuilder.Entity<CabinetPosition>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<CabinetUI>()
                .HasKey(c => c.CabinetNumber);

            modelBuilder.Entity<CabinetUI>()
               .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CabinetUI>()
                .Property(c => c.LogoWhite)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetUI>()
                .Property(c => c.LogoBlack)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetUI>()
                .Property(c => c.ColorTemplate)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetUI>()
                .Property(c => c.MenuTemplate)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetUI>()
                .Property(c => c.Font)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CabinetUI>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CabinetProperties>()
                .Property(c => c.LocalApiVersion).HasMaxLength(25);

            modelBuilder.Entity<CabinetProperties>()
                .Property(c => c.LocalUiVersion).HasMaxLength(25);
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }
}
