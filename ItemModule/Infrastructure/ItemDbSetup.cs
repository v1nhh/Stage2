using CTAM.Core;
using ItemModule.ApplicationCore.Entities;
using ItemModule.Infrastructure.InitialData;
using Microsoft.EntityFrameworkCore;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<CTAMRole_ItemType> CTAMRole_ItemType(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMRole_ItemType>();

        public static DbSet<ItemType_ErrorCode> ItemType_ErrorCode(this MainDbContext mainDbContext) => mainDbContext.Set<ItemType_ErrorCode>();

        public static DbSet<ErrorCode> ErrorCode(this MainDbContext mainDbContext) =>  mainDbContext.Set<ErrorCode>();

        public static DbSet<Item> Item(this MainDbContext mainDbContext) =>  mainDbContext.Set<Item>();

        public static DbSet<ItemDetail> ItemDetail(this MainDbContext mainDbContext) =>  mainDbContext.Set<ItemDetail>();

        public static DbSet<ItemSet> ItemSet(this MainDbContext mainDbContext) =>  mainDbContext.Set<ItemSet>();

        public static DbSet<ItemType> ItemType(this MainDbContext mainDbContext) =>  mainDbContext.Set<ItemType>();
    }
}

namespace ItemModule.Infrastructure
{
    public class ItemDbSetup : AbstractDbSetup
    {
        public const string ItemSchema = "Item";
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
            modelBuilder.Entity<CTAMRole_ItemType>()
                .ToTable("CTAMRole_ItemType", ItemSchema);

            modelBuilder.Entity<ErrorCode>()
                .ToTable("ErrorCode", ItemSchema);

            modelBuilder.Entity<Item>()
                .ToTable("Item", ItemSchema);

            modelBuilder.Entity<ItemDetail>()
                .ToTable("ItemDetail", ItemSchema);

            modelBuilder.Entity<ItemSet>()
                .ToTable("ItemSet", ItemSchema);

            modelBuilder.Entity<ItemType>()
                .ToTable("ItemType", ItemSchema);

            modelBuilder.Entity<ItemType_ErrorCode>()
                .ToTable("ItemType_ErrorCode", ItemSchema);
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext) { }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {
            // Role - ItemType (many-to-many)
            modelBuilder.Entity<CTAMRole_ItemType>()
                .HasKey(t => new { t.CTAMRoleID, t.ItemTypeID });

            modelBuilder.Entity<CTAMRole_ItemType>()
                .HasOne(roleItemType => roleItemType.ItemType)
                .WithMany(itemType => itemType.CTAMRole_ItemType)
                .HasForeignKey(roleItemType => roleItemType.ItemTypeID);

            modelBuilder.Entity<CTAMRole_ItemType>()
                .HasOne(roleItemType => roleItemType.CTAMRole)
                .WithMany() // (role => role.CTAMRole_ItemType)
                .HasForeignKey(roleItemType => roleItemType.CTAMRoleID);

            // ItemType - ErrorCode (many to many)
            modelBuilder.Entity<ItemType_ErrorCode>()
                .HasKey(t => new { t.ItemTypeID, t.ErrorCodeID });

            modelBuilder.Entity<ItemType_ErrorCode>()
                .HasOne(itemTypeErrorCode => itemTypeErrorCode.ErrorCode)
                .WithMany(errorCode => errorCode.ItemType_ErrorCode)
                .HasForeignKey(itemTypeErrorCode => itemTypeErrorCode.ErrorCodeID);

            modelBuilder.Entity<ItemType_ErrorCode>()
                .HasOne(itemType_ErrorCode => itemType_ErrorCode.ItemType)
                .WithMany(itemType => itemType.ItemType_ErrorCode)
                .HasForeignKey(itemType_ErrorCode => itemType_ErrorCode.ItemTypeID);
        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // ItemType - Item (one-to-many)
            modelBuilder.Entity<Item>()
                .HasOne(item => item.ItemType)
                .WithMany(itemType => itemType.Items)
                .HasForeignKey(item => item.ItemTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            // ErrorCode - Item (one-to-many)
            modelBuilder.Entity<Item>()
                .HasOne(item => item.ErrorCode)
                .WithMany(errorCode => errorCode.Items)
                .HasForeignKey(item => item.ErrorCodeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Item - ItemDetail (one-to-many)
            modelBuilder.Entity<ItemDetail>()
                .HasOne(itemDetail => itemDetail.Item)
                .WithMany(item => item.ItemDetails)
                .HasForeignKey(itemDetail => itemDetail.ItemID);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {
            // Item - ItemSet (one-to-one)
            modelBuilder.Entity<Item>()
                .HasOne(item => item.ItemSet)
                .WithOne(itemSet => itemSet.Item)
                .HasForeignKey<ItemSet>(itemSet => itemSet.ItemID);
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTAMRole_ItemType>()
                .HasIndex(c => new { c.CTAMRoleID, c.ItemTypeID })
                .IsUnique(true);

            modelBuilder.Entity<CTAMRole_ItemType>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ErrorCode>()
                .Property(e => e.Code).HasMaxLength(10);

            modelBuilder.Entity<ErrorCode>()
                .HasIndex(e => e.Code)
                .IsUnique(true);

            modelBuilder.Entity<ErrorCode>()
                .Property(e => e.Description).HasMaxLength(250);

            modelBuilder.Entity<ErrorCode>()
                .HasIndex(e => e.Description)
                .IsUnique(true);

            modelBuilder.Entity<ErrorCode>()
                .Property(e => e.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<Item>()
                .Property(item => item.Description).HasMaxLength(250);

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.Description)
                .IsUnique(true);

            modelBuilder.Entity<Item>()
                .Property(item => item.ExternalReferenceID).HasMaxLength(40);

            modelBuilder.Entity<Item>()
                .Property(item => item.Barcode).HasMaxLength(20);

            modelBuilder.Entity<Item>()
                .Property(item => item.Tagnumber).HasMaxLength(40);

            modelBuilder.Entity<Item>()
                .Property(item => item.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<ItemDetail>()
                .HasIndex(itemDetail => new { itemDetail.ID, itemDetail.ItemID })
                .IsUnique(true);

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.Description).HasMaxLength(250);

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.FreeText1)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.FreeText2)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.FreeText3)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.FreeText4)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<ItemDetail>()
                .Property(c => c.FreeText5)
                .HasColumnType("nvarchar(max)");


            modelBuilder.Entity<ItemSet>()
                .HasKey(itemSet => new { itemSet.SetCode, itemSet.ItemID });

            modelBuilder.Entity<ItemSet>()
                .HasIndex(itemSet => new { itemSet.SetCode, itemSet.ItemID })
                .IsUnique(true);

            modelBuilder.Entity<ItemSet>()
                .Property(itemSet => itemSet.SetCode).HasMaxLength(20);


            modelBuilder.Entity<ItemType>()
                .Property(i => i.Description).HasMaxLength(250);

            modelBuilder.Entity<ItemType>()
                .HasIndex(i => i.Description)
                .IsUnique(true);

            modelBuilder.Entity<ItemType>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }
}
