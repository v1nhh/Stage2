using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using MileageModule.ApplicationCore.Entities;
using MileageModule.Infrastructure.InitialData;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<Mileage> Mileage(this MainDbContext mainDbContext) => mainDbContext.Set<Mileage>();

        public static DbSet<MileageRegistration> MileageRegistration(this MainDbContext mainDbContext) => mainDbContext.Set<MileageRegistration>();
    }
}

namespace MileageModule.Infrastructure
{
    public class MileageDbSetup : AbstractDbSetup
    {
        public const string MileageSchema = "Mileage";
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
            modelBuilder.Entity<Mileage>()
                .ToTable("Mileage", MileageSchema);

            modelBuilder.Entity<MileageRegistration>()
                .ToTable("MileageRegistration", MileageSchema);
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext) { }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {

        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // Mileage - MileageRegistration (one-to-many)
            modelBuilder.Entity<MileageRegistration>()
                .HasOne(mileageRegistration => mileageRegistration.Mileage)
                .WithMany(mileage => mileage.MileageRegistrations)
                .HasForeignKey(mileageRegistration => mileageRegistration.MileageID);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder) 
        {
        
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mileage>()
                .Property(m=> m.LicensePlate).HasMaxLength(20);

            modelBuilder.Entity<Mileage>()
                .Property(m => m.UoM).HasMaxLength(10);

            modelBuilder.Entity<Mileage>()
                .Property(m => m.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.CTAMUserUID)
                .HasMaxLength(50);
            modelBuilder.Entity<MileageRegistration>()
                .HasIndex(mr => mr.CTAMUserUID);
            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.ValidatedByCTAMUserUID)
                .HasMaxLength(50);
            modelBuilder.Entity<MileageRegistration>()
                .HasIndex(mr => mr.ValidatedByCTAMUserUID);

            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.CTAMUserName)
                .HasMaxLength(250);
            modelBuilder.Entity<MileageRegistration>()
                .HasIndex(mr => mr.CTAMUserName);
            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.ValidatedByCTAMUserName)
                .HasMaxLength(250);
            modelBuilder.Entity<MileageRegistration>()
                .HasIndex(mr => mr.ValidatedByCTAMUserName);

            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.CTAMUserEmail)
                .HasMaxLength(250);
            modelBuilder.Entity<MileageRegistration>()
                .Property(mr => mr.ValidatedByCTAMUserEmail)
                .HasMaxLength(250);
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }

}
