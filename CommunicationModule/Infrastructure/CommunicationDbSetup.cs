using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.Infrastructure.InitialData;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CTAM.Core
{
    public static class MainDbContextExt
    {
        public static DbSet<MailQueue> MailQueue(this MainDbContext mainDbContext) => mainDbContext.Set<MailQueue>();

        public static DbSet<MailTemplate> MailTemplate(this MainDbContext mainDbContext) => mainDbContext.Set<MailTemplate>();

        public static DbSet<MailMarkupTemplate> MailMarkupTemplate(this MainDbContext mainDbContext) => mainDbContext.Set<MailMarkupTemplate>();

        public static DbSet<APISetting> APISetting(this MainDbContext mainDbContext) => mainDbContext.Set<APISetting>();

        public static DbSet<Request> Request(this MainDbContext mainDbContext) => mainDbContext.Set<Request>();
    }
}

namespace CommunicationModule.Infrastructure
{
    public class CommunicationDbSetup : AbstractDbSetup
    {
        public const string CommunicationSchema = "Communication";
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
            modelBuilder.Entity<MailQueue>()
                .ToTable("MailQueue", CommunicationSchema);

            modelBuilder.Entity<MailTemplate>()
                .ToTable("MailTemplate", CommunicationSchema);

            modelBuilder.Entity<MailMarkupTemplate>()
                .ToTable("MailMarkupTemplate", CommunicationSchema);

            modelBuilder.Entity<APISetting>()
                .ToTable("APISetting", CommunicationSchema);

            modelBuilder.Entity<Request>()
                .ToTable("Request", CommunicationSchema);
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext)
        {
            modelBuilder.Entity<APISetting>()
                .Property(a => a.API_HEADERS)
                .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));

            modelBuilder.Entity<APISetting>()
                .Property(a => a.API_BODY)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(v, (JsonSerializerOptions)null)
                );
        }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {
        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
            // MailMarkupTemplate - MailQueue (one-to-many)
            modelBuilder.Entity<MailQueue>()
                .HasOne(mailTemplate => mailTemplate.MailMarkupTemplate)
                .WithMany()
                .HasForeignKey(mailTemplate => mailTemplate.MailMarkupTemplateID);

            modelBuilder.Entity<Request>()
                .HasOne(request => request.APISetting)
                .WithMany()
                .HasForeignKey(request => request.APISettingID);

            modelBuilder.Entity<Request>()
                .HasOne(request => request.ReferredRequest)
                .WithMany()
                .HasForeignKey(request => request.ReferredRequestID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailMarkupTemplate>()
                .Property(m => m.Name).HasMaxLength(250);

            modelBuilder.Entity<MailMarkupTemplate>()
                .HasIndex(m => m.Name)
                .IsUnique(true);

            modelBuilder.Entity<MailTemplate>()
                .Property(i => i.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<MailTemplate>()
                .Property(m => m.LanguageCode).HasMaxLength(5);

            modelBuilder.Entity<MailTemplate>()
                .Property(m => m.Name).HasMaxLength(250);

            modelBuilder.Entity<MailTemplate>()
                .HasIndex(m => new { m.LanguageCode, m.Name })
                .IsUnique(true);

            modelBuilder.Entity<MailTemplate>()
                .Property(m => m.Subject).HasMaxLength(250);

            modelBuilder.Entity<MailTemplate>()
                .Property(c => c.Template)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<MailQueue>()
                .Property(i => i.CreateDT)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<MailQueue>()
                .Property(m => m.MailTo).HasMaxLength(250);

            modelBuilder.Entity<MailQueue>()
                .Property(m => m.MailCC).HasMaxLength(250);

            modelBuilder.Entity<MailQueue>()
                .Property(m => m.Subject).HasMaxLength(250);

            modelBuilder.Entity<MailQueue>()
                .Property(c => c.Body)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Request>()
                .Property(i => i.CreateDT)
                .HasDefaultValueSql("getutcdate()");
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
            InsertDataForEnvironment(modelBuilder, new CoreData(), new TestData());
        }
    }
}
