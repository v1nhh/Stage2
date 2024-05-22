using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using ReservationModule.ApplicationCore.Entities;
using ReservationModule.Infrastructure.InitialData;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<Reservation> Reservation(this MainDbContext mainDbContext) => mainDbContext.Set<Reservation>();

        public static DbSet<ReservationItem> ReservationItem(this MainDbContext mainDbContext) => mainDbContext.Set<ReservationItem>();

        public static DbSet<ReservationCabinetPosition> ReservationCabinetPosition(this MainDbContext mainDbContext) => mainDbContext.Set<ReservationCabinetPosition>();

        public static DbSet<ReservationRecurrencySchedule> ReservationRecurrencySchedule(this MainDbContext mainDbContext) => mainDbContext.Set<ReservationRecurrencySchedule>();
    }
}

namespace ReservationModule.Infrastructure
{
    public class ReservationDbSetup : AbstractDbSetup
    {
        public const string ReservationSchema = "Reservation";
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

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext) { }
        
        public override void AssignTablesToSchema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .ToTable("Reservation", ReservationSchema);

            modelBuilder.Entity<ReservationItem>()
                .ToTable("ReservationItem", ReservationSchema);

            modelBuilder.Entity<ReservationCabinetPosition>()
                .ToTable("ReservationCabinetPosition", ReservationSchema);

            modelBuilder.Entity<ReservationRecurrencySchedule>()
                .ToTable("ReservationRecurrencySchedule", ReservationSchema);

        }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {

        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // ReservationRecurrencySchedule - Reservation (one-to-many)
            modelBuilder.Entity<Reservation>()
                .HasOne(reservation => reservation.ReservationRecurrencySchedule)
                .WithMany(reservationRecurrencySchedule => reservationRecurrencySchedule.Reservations)
                .HasForeignKey(reservation => reservation.ReservationRecurrencyScheduleID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {

        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            // Reservation
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.ID)
                .IsUnique(true);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CTAMUserUID).HasMaxLength(50);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CTAMUserName).HasMaxLength(250);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CTAMUserEmail).HasMaxLength(250);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.CTAMUserUID);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.CTAMUserName);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.QRCode).HasMaxLength(255);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Reservation>()
                .Property(r => r.NoteForUser).HasMaxLength(500);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ExternalReservationNumber).HasMaxLength(50);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ExternalReservationSourceType).HasMaxLength(50);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ExternalReservationCallBackInfo).HasMaxLength(255);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CancelledByCTAMUserUID).HasMaxLength(50);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CancelledByCTAMUserName).HasMaxLength(250);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CancelledByCTAMUserEmail).HasMaxLength(250);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.CancelledByCTAMUserUID);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.CancelledByCTAMUserName);


            // ReservationItem
            modelBuilder.Entity<ReservationItem>()
                .HasKey(ri => new { ri.ReservationID, ri.ItemID, ri.CabinetNumber });

            modelBuilder.Entity<ReservationItem>()
                .Property(ri => ri.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<ReservationItem>()
                .HasIndex(ri => ri.CabinetNumber);

            modelBuilder.Entity<ReservationItem>()
                .Property(ri => ri.CabinetName).HasMaxLength(250);

            modelBuilder.Entity<ReservationItem>()
                .HasIndex(ri => ri.CabinetName);

            // ReservationCabinetPosition
            modelBuilder.Entity<ReservationCabinetPosition>()
                .HasKey(rcp => new { rcp.ReservationID, rcp.CabinetPositionID });

            // ReservationRecurrencySchedule
            modelBuilder.Entity<ReservationRecurrencySchedule>()
                .HasKey(rrs => rrs.ID);

        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder,  new CoreData(), new TestData());
        }
    }
}
