using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.Infrastructure.InitialData;
using Microsoft.EntityFrameworkCore;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {

        public static DbSet<AllowedCabinetPosition> AllowedCabinetPosition(this MainDbContext mainDbContext) => mainDbContext.Set<AllowedCabinetPosition>();

        public static DbSet<CabinetPositionContent> CabinetPositionContent(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetPositionContent>();

        public static DbSet<CabinetStock> CabinetStock(this MainDbContext mainDbContext) => mainDbContext.Set<CabinetStock>();

        public static DbSet<CTAMUserInPossession> CTAMUserInPossession(this MainDbContext mainDbContext) => mainDbContext.Set<CTAMUserInPossession>();

        public static DbSet<CTAMUserPersonalItem> CTAMUserPersonalItem(this MainDbContext mainDbContext) => mainDbContext.Set<CTAMUserPersonalItem>();

        public static DbSet<ItemToPick> ItemToPick(this MainDbContext mainDbContext) => mainDbContext.Set<ItemToPick>();
    }
}

namespace ItemCabinetModule.Infrastructure
{
    public class ItemCabinetDbSetup : AbstractDbSetup
    {
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
            string itemCabinetSchema = "ItemCabinet";


            modelBuilder.Entity<AllowedCabinetPosition>()
                .ToTable("AllowedCabinetPosition", itemCabinetSchema);

            modelBuilder.Entity<CabinetPositionContent>()
                .ToTable("CabinetPositionContent", itemCabinetSchema);

            modelBuilder.Entity<CabinetStock>()
                .ToTable("CabinetStock", itemCabinetSchema);

            modelBuilder.Entity<CTAMUserInPossession>()
                .ToTable("CTAMUserInPossession", itemCabinetSchema);

            modelBuilder.Entity<CTAMUserPersonalItem>()
                .ToTable("CTAMUserPersonalItem", itemCabinetSchema);

            modelBuilder.Entity<ItemToPick>()
                .ToTable("ItemToPick", itemCabinetSchema);
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext) { }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder) { }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // Item - UserInPossession (one-to-many)
            modelBuilder.Entity<CTAMUserInPossession>()
                .HasOne(userInPossession => userInPossession.Item)
                .WithMany()
                .HasForeignKey(userInPossession => userInPossession.ItemID);

            // CabinetPosition - CabinetPositionContent (one-to-many)
            modelBuilder.Entity<CabinetPositionContent>()
                .HasOne(cabinetPositionContent => cabinetPositionContent.CabinetPosition)
                .WithMany()
                .HasForeignKey(cabinetPositionContent => cabinetPositionContent.CabinetPositionID);

            // Item - CabinetPositionContent (one-to-many)
            modelBuilder.Entity<CabinetPositionContent>()
                .HasOne(cabinetPositionContent => cabinetPositionContent.Item)
                .WithMany()
                .HasForeignKey(cabinetPositionContent => cabinetPositionContent.ItemID);

            // Cabinet - CabinetStock (one-to-many)
            modelBuilder.Entity<CabinetStock>()
                .HasOne(cabinetStock => cabinetStock.Cabinet)
                .WithMany()
                .HasForeignKey(cabinetStock => cabinetStock.CabinetNumber);

            // ItemType - CabinetStock (one-to-many)
            modelBuilder.Entity<CabinetStock>()
                .HasOne(cabinetStock => cabinetStock.ItemType)
                .WithMany()
                .HasForeignKey(cabinetStock => cabinetStock.ItemTypeID);

            // CTAMUser - ItemToPick (one-to-many)
            modelBuilder.Entity<ItemToPick>()
                .HasOne(itemToPick => itemToPick.CTAMUser)
                .WithMany()
                .HasForeignKey(itemToPick => itemToPick.CTAMUserUID);

            // Item - ItemToPick (one-to-many)
            modelBuilder.Entity<ItemToPick>()
                .HasOne(itemToPick => itemToPick.Item)
                .WithMany()
                .HasForeignKey(itemToPick => itemToPick.ItemID);

            // CabinetPosition - ItemToPick (one-to-many)
            modelBuilder.Entity<ItemToPick>()
                .HasOne(itemToPick => itemToPick.CabinetPosition)
                .WithMany()
                .HasForeignKey(itemToPick => itemToPick.CabinetPositionID);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {
            // TODO: double check if code bellow is correct
            // https://itinnovatorsbv.atlassian.net/browse/CTAM-356
            // https://itinnovatorsbv.atlassian.net/browse/CTAM-226
            // Personal Item - Item (one-to-one)
            //modelBuilder.Entity<CTAMUserPersonalItem>()
            //    .HasOne(personalItem => personalItem.Item)
            //    .WithOne()
            //    .HasForeignKey<CTAMUserPersonalItem>(personalItem => personalItem.ItemID)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Personal Item - Item(Replacement) (one-to-one)
            //modelBuilder.Entity<CTAMUserPersonalItem>()
            //    .HasOne(personalItem => personalItem.ReplacementItem)
            //    .WithOne()
            //    .HasForeignKey<CTAMUserPersonalItem>(personalItem => personalItem.ReplacementItemID)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllowedCabinetPosition>()
                .HasKey(t => new { t.CabinetPositionID, t.ItemID });

            modelBuilder.Entity<CabinetPositionContent>()
                .HasKey(t => new { t.CabinetPositionID, t.ItemID });

            modelBuilder.Entity<CabinetStock>()
                .HasKey(t => new { t.CabinetNumber, t.ItemTypeID });

            modelBuilder.Entity<CabinetStock>()
                .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserUIDOut).HasMaxLength(50);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserNameOut).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserEmailOut).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserUIDIn).HasMaxLength(50);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserNameIn).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CTAMUserEmailIn).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CabinetNumberOut).HasMaxLength(20);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CabinetNameOut).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CabinetNumberIn).HasMaxLength(20);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(c => c.CabinetNameIn).HasMaxLength(250);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(b => b.OutDT);

            modelBuilder.Entity<CTAMUserInPossession>()
                .Property(b => b.CreatedDT);

            modelBuilder.Entity<CTAMUserPersonalItem>()
                .Property(c => c.CTAMUserUID).HasMaxLength(50);

            modelBuilder.Entity<CTAMUserPersonalItem>()
                .Property(c => c.CabinetNumber).HasMaxLength(20);

            modelBuilder.Entity<CTAMUserPersonalItem>()
                .Property(c => c.CabinetName).HasMaxLength(250);


            modelBuilder.Entity<CTAMUserPersonalItem>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ItemToPick>()
                .Property(c => c.CTAMUserUID).HasMaxLength(50);
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }
}
