using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CTAM.Core
{
    public class ChangedEntry
    {
        public EntityEntry Entry { get; set; }

        public EntityState State { get; set; }

        public object OriginalEntity { get; set; }
    }

    public class MainDbContext : DbContext
    {
        private readonly DbSetupFactory _dbSetupFactory = new DbSetupFactory();
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Func<List<ChangedEntry>, IServiceProvider, Task>> _changeTrackingFunctions;


        /* From DbContext documentation;
         * "DbSet<TEntity> objects are usually obtained from a DbSet<TEntity> property on a derived DbContext or from the Set<TEntity>() method."
         * https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1?view=efcore-3.1
         * 
         * Custom table properties like `public DbSet<MyEntity> { get; set; }` are internally using `DbContext.Set<MyEntity>()`.
         * So equivalent way to define and use tables is a MainDbContext extension method implemented in any module e.g.:
         * 
         * `public static DbSet<MyEntity> MyEntity(this MainDbContext mainDbContext) => mainDbContext.Set<MyEntity>();`
         * 
         * Then use it anywhere in code by (e.g. Any()):
         * 
         * `_mainDbContext.MyEntity().Any()`
         * 
         */
        public MainDbContext(DbContextOptions<MainDbContext> options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _changeTrackingFunctions = _dbSetupFactory.GetAllChangeTrackingFunctions();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var changedEntries = SetUpdateCreateDTAndGetChangedEntries();
            var result =  base.SaveChanges(acceptAllChangesOnSuccess);
            FireChangeTrackingFunctions(changedEntries).Wait();

            return result;
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntries = SetUpdateCreateDTAndGetChangedEntries();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await FireChangeTrackingFunctions(changedEntries);

            return result;
        }

        private async Task FireChangeTrackingFunctions(List<ChangedEntry> changedEntries)
        {
            // Perform actions after database CRUD operations
            // WARNING: this may cause false positives when using transaction scopes!!!
            //          System.Transactions.Transaction.Current.TransactionCompleted event
            //          can be used to do stuff after scope is completed
            //          Check LiveSyncService.cs implementation from CloudAPI for an example on how to use it
            foreach (var changeTrackingFunction in _changeTrackingFunctions)
            {
                await changeTrackingFunction(changedEntries, _serviceProvider);
            }
        }

        private List<ChangedEntry> SetUpdateCreateDTAndGetChangedEntries()
        {
            _dbSetupFactory.AutoInsertCreatedUpdatedFields(ChangeTracker);

            var changedEntries = ChangeTracker.Entries()
                .Where(entity => entity.State != EntityState.Unchanged)
                .Select(changedEntry => new ChangedEntry { Entry = changedEntry, OriginalEntity = GetOriginalEntity(changedEntry), State = changedEntry.State })
                .ToList();
            return changedEntries;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _dbSetupFactory.InitializeAllDbSetups(modelBuilder, this);
        }

        protected object GetOriginalEntity(EntityEntry changedEntry)
        {
            if (changedEntry.State == EntityState.Added)
            {
                // If entity is added there can be no original values
                return null;
            }
            var entityType = changedEntry.Entity.GetType();
            var instance = Activator.CreateInstance(entityType);
            PropertyInfo[] properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                var foundOriginalProperty = changedEntry.OriginalValues.Properties.FirstOrDefault(prop => prop.Name.Equals(property.Name));
                if (foundOriginalProperty != null)
                {
                    property.SetValue(instance, changedEntry.OriginalValues[property.Name], null);
                }
            }
            return instance;
        }
    }
}
