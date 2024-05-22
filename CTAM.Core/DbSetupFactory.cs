using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CTAM.Core
{
    public class DbSetupFactory
    {
        public DbSetupFactory()
        {
        }

        public List<Func<List<ChangedEntry>, IServiceProvider, Task>> GetAllChangeTrackingFunctions() => GetAllDbSetups()
            .Select(dbSetup => dbSetup.ChangeTrackingFunction)
            .Where(action => action != null)
            .ToList();

        public void InitializeAllDbSetups(ModelBuilder modelBuilder, DbContext dbContext)
        {
            var dbSetups = GetAllDbSetups();

            foreach (var dbSetup in dbSetups)
            {
                dbSetup.AssignTablesToSchema(modelBuilder);
                dbSetup.AddCustomValueConverters(modelBuilder, dbContext);
                dbSetup.DefineManyToManyRelations(modelBuilder);
                dbSetup.DefineOneToManyRelations(modelBuilder);
                dbSetup.DefineOneToOneRelations(modelBuilder);
                dbSetup.ConfigureColumns(modelBuilder);
                dbSetup.InitialInserts(modelBuilder);
            }
        }

        private List<AbstractDbSetup> GetAllDbSetups()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(AbstractDbSetup).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => (AbstractDbSetup)Activator.CreateInstance(type))
                .ToList();
        }

        public void AutoInsertCreatedUpdatedFields(ChangeTracker changeTracker, string createDTField = "CreateDT", string updateDTField = "UpdateDT")
        {
            var currentDatetime = DateTime.UtcNow;
            changeTracker.Entries()
                .Where(E => E.State == EntityState.Added)
                .ToList()
                .ForEach(E =>
                {
                    if (E.Metadata.FindProperty(createDTField) != null)
                    {
                        E.Property(createDTField).CurrentValue = currentDatetime;
                    }
                    if (E.Metadata.FindProperty(updateDTField) != null)
                    {
                        E.Property(updateDTField).CurrentValue = currentDatetime;
                    }
                });

            changeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList()
                .ForEach(E =>
                {
                    if (E.Metadata.FindProperty(updateDTField) != null)
                    {
                        if (!E.Property(updateDTField).IsModified)
                        {
                            E.Property(updateDTField).CurrentValue = currentDatetime;
                        }
                    }
                });
        }
    }
}
