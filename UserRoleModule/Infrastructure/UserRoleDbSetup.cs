using CTAM.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.Infrastructure.InitialData;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<CTAMPermission> CTAMPermission(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMPermission>();

        public static DbSet<CTAMRole> CTAMRole(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMRole>();

        public static DbSet<CTAMRole_Permission> CTAMRole_Permission(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMRole_Permission>();

        public static DbSet<CTAMUser> CTAMUser(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMUser>();

        public static DbSet<CTAMUser_Role> CTAMUser_Role(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMUser_Role>();

        public static DbSet<CTAMSetting> CTAMSetting(this MainDbContext mainDbContext) =>  mainDbContext.Set<CTAMSetting>();

        public static DbSet<ManagementLog> ManagementLog(this MainDbContext mainDbContext) =>  mainDbContext.Set<ManagementLog>();
    }
}

namespace UserRoleModule.Infrastructure
{
    public class UserRoleDbSetup : AbstractDbSetup
    {
        public const string UserRoleSchema = "UserRole";

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
            modelBuilder.Entity<CTAMUser>()
                .ToTable("CTAMUser", UserRoleSchema);

            modelBuilder.Entity<CTAMUser_Role>()
                .ToTable("CTAMUser_Role", UserRoleSchema);

            modelBuilder.Entity<CTAMRole>()
                .ToTable("CTAMRole", UserRoleSchema);

            modelBuilder.Entity<CTAMRole_Permission>()
                .ToTable("CTAMRole_Permission", UserRoleSchema);

            modelBuilder.Entity<CTAMPermission>()
                .ToTable("CTAMPermission", UserRoleSchema);

            modelBuilder.Entity<CTAMSetting>()
                .ToTable("CTAMSetting", UserRoleSchema);

            modelBuilder.Entity<ManagementLog>()
                .ToTable("ManagementLog", UserRoleSchema);
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext)
        {
            ProtectedDataConverter converter = new ProtectedDataConverter(dbContext.GetService<IDataProtectionProvider>());

            Console.WriteLine("Adding encryption to Password and PinCode");
            modelBuilder.Entity<CTAMUser>()
                .Property(u => u.Password)
                .HasConversion(converter);

            modelBuilder.Entity<CTAMUser>()
                .Property(u => u.PinCode)
                .HasConversion(converter);
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CTAMPermission>()
                .Property(c => c.Description).HasMaxLength(250);

            modelBuilder.Entity<CTAMPermission>()
                .HasIndex(c => new { c.Description, c.CTAMModule })
                .IsUnique(true);

            modelBuilder.Entity<CTAMPermission>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<CTAMRole>()
                .Property(c => c.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<CTAMRole>()
                .HasIndex(c => c.Description)
                .IsUnique(true);

            modelBuilder.Entity<CTAMRole>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<CTAMRole_Permission>()
                .HasIndex(c => new { c.CTAMRoleID, c.CTAMPermissionID })
                .IsUnique(true);

            modelBuilder.Entity<CTAMRole_Permission>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<CTAMUser>()
                .HasKey(c => c.UID);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.UID).HasMaxLength(50);

            modelBuilder.Entity<CTAMUser>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.Name).HasMaxLength(250);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.LoginCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            modelBuilder.Entity<CTAMUser>()
                .HasIndex(c => c.LoginCode)
                .IsUnique(true);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.PinCode)
                .HasMaxLength(250) // encrypted value is much longer than 10
                .IsUnicode(false);

            modelBuilder.Entity<CTAMUser>()
                .HasIndex(c => c.PinCode);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.Email).HasMaxLength(250);

            modelBuilder.Entity<CTAMUser>()
                .HasIndex(c => c.Email)
                .IsUnique(true);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.PhoneNumber).HasMaxLength(20);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.Password).HasMaxLength(250);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.CardCode).HasMaxLength(50);

            modelBuilder.Entity<CTAMUser>()
                .HasIndex(c => c.CardCode)
                .IsUnique(true)
                .HasFilter("[CardCode] IS NOT NULL AND [CardCode] <> ''");

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.LanguageCode).HasMaxLength(5);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.RefreshToken);

            modelBuilder.Entity<CTAMUser>()
                .Property(c => c.RefreshTokenExpiryDate);

            modelBuilder.Entity<CTAMUser_Role>()
                .HasIndex(c => new { c.CTAMRoleID, c.CTAMUserUID })
                .IsUnique(true);

            modelBuilder.Entity<CTAMUser_Role>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");


            modelBuilder.Entity<CTAMSetting>()
                .Property(c => c.ParName).HasMaxLength(250);

            modelBuilder.Entity<CTAMSetting>()
                .HasIndex(c => new { c.ParName, c.CTAMModule})
                .IsUnique(true);

            modelBuilder.Entity<CTAMSetting>()
                .Property(c => c.ParValue).HasMaxLength(1000);

            modelBuilder.Entity<CTAMSetting>()
                .Property(b => b.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ManagementLog>()
                .Property(m => m.CTAMUserUID).HasMaxLength(50);

            modelBuilder.Entity<ManagementLog>()
                .Property(m => m.CTAMUserName).HasMaxLength(250);

            modelBuilder.Entity<ManagementLog>()
                .Property(m => m.CTAMUserEmail).HasMaxLength(250);
        }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {
            // User - Role (many-to-many)
            modelBuilder.Entity<CTAMUser_Role>()
                .HasKey(t => new { t.CTAMUserUID, t.CTAMRoleID });

            modelBuilder.Entity<CTAMUser_Role>()
                .HasOne(userRole => userRole.CTAMRole)
                .WithMany(role => role.CTAMUser_Roles)
                .HasForeignKey(userRole => userRole.CTAMRoleID);

            modelBuilder.Entity<CTAMUser_Role>()
                .HasOne(userRole => userRole.CTAMUser)
                .WithMany(user => user.CTAMUser_Roles)
                .HasForeignKey(userRole => userRole.CTAMUserUID);


            // Role - Permission (many-to-many)
            modelBuilder.Entity<CTAMRole_Permission>()
                .HasKey(t => new { t.CTAMRoleID, t.CTAMPermissionID });

            modelBuilder.Entity<CTAMRole_Permission>()
                .HasOne(rolePermission => rolePermission.CTAMPermission)
                .WithMany(permission => permission.CTAMRole_Permission)
                .HasForeignKey(rolePermission => rolePermission.CTAMPermissionID);

            modelBuilder.Entity<CTAMRole_Permission>()
                .HasOne(rolePermission => rolePermission.CTAMRole)
                .WithMany(role => role.CTAMRole_Permission)
                .HasForeignKey(rolePermission => rolePermission.CTAMRoleID);
            
        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder) {}

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder) {}

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }
}
