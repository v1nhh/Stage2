using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.Infrastructure
{
    public class ChangedEntity
    {
        public object Entity { get; set; }

        public EntityState State { get; set; }

        public object OriginalEntity { get; set; }
    }

    public class EntitiesChangedNotification: INotification
    {
        public IEnumerable<ChangedEntity> EntitiesData { get; set; }
    }

    public class CloudDbSetup: AbstractDbSetup
    {
        public CloudDbSetup(): base()
        {
            var entitiesToTrack = new HashSet<Type>
            {
                typeof(Cabinet),
                typeof(CabinetUI),
                typeof(CabinetDoor),
                typeof(CabinetPosition),
                typeof(Item),
                typeof(ItemType),
                typeof(ErrorCode),
                typeof(ItemType_ErrorCode),
                typeof(CTAMUser),
                typeof(CTAMUser_Role),
                typeof(CTAMRole),
                typeof(CTAMRole_Permission),
                typeof(CTAMRole_Cabinet),
                typeof(CTAMRole_ItemType),
                typeof(CabinetAccessInterval),
                typeof(CabinetStock),
                typeof(CTAMSetting),
                typeof(CTAMUserPersonalItem),
                typeof(CTAMUserInPossession),
                typeof(CabinetAction)
            };
            ChangeTrackingFunction = async (changedEntries, serviceProvider) =>
            {
                if (serviceProvider != null && changedEntries != null && changedEntries.Count > 0)
                {
                    var mediator = serviceProvider.GetService(typeof(IMediator)) as IMediator;
                    if (mediator != null)
                    {
                        // generate changes batch for notification
                        var changedEntitiesData = changedEntries
                            .Where(changedEntry => entitiesToTrack.Contains(changedEntry.Entry.Entity.GetType()))
                            .Select(changedEntry => new ChangedEntity { Entity = changedEntry.Entry.Entity, State = changedEntry.State, OriginalEntity = changedEntry.OriginalEntity });
                        var notification = new EntitiesChangedNotification { EntitiesData = changedEntitiesData };
                        // send changes batch notification
                        await mediator.Publish(notification);
                    }
                }
            };
        }

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
        }

        public override void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext)
        {
        }

        public override void DefineManyToManyRelations(ModelBuilder modelBuilder)
        {
        }

        public override void DefineOneToManyRelations(ModelBuilder modelBuilder)
        {
        }

        public override void DefineOneToOneRelations(ModelBuilder modelBuilder)
        {
        }

        public override void ConfigureColumns(ModelBuilder modelBuilder)
        {
        }

        public override void InitialInserts(ModelBuilder modelBuilder)
        {
        }
    }
}
