using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CTAM.Core
{
    public class ProtectedDataConverter : ValueConverter<string, string>
    {
        private static readonly string[] purpose = new string[] { "UserRoleModule.Infrastructure", "CTAM.v1" };

        public ProtectedDataConverter(IDataProtectionProvider protectionProvider)
            : base(s => protectionProvider
                        .CreateProtector(purpose)
                        .Protect(s),
                    s => protectionProvider
                        .CreateProtector(purpose)
                        .Unprotect(s),
                    default)
        { }
    }

    public abstract class AbstractDbSetup
    {
        private Func<List<ChangedEntry>, IServiceProvider, Task> _changeTrackingFunction;
        /// <summary>
        /// Provided action is fired after every SaveChangesAsync call on each Entity wich State != EntityState.Unchanged
        /// </summary>
        public Func<List<ChangedEntry>, IServiceProvider, Task> ChangeTrackingFunction {
            get => _changeTrackingFunction;
            set
            {
                _changeTrackingFunction = value;
                var sf = new StackTrace().GetFrame(0);
                Console.WriteLine($"{sf.GetMethod()}: ChangeTrackingAction is set");
            }
        }

        abstract public void OnModelCreating(ModelBuilder modelBuilder, DbContext dbContext);

        abstract public void AssignTablesToSchema(ModelBuilder modelBuilder);
        abstract public void AddCustomValueConverters(ModelBuilder modelBuilder, DbContext dbContext);
        abstract public void DefineManyToManyRelations(ModelBuilder modelBuilder);
        abstract public void DefineOneToManyRelations(ModelBuilder modelBuilder);
        abstract public void DefineOneToOneRelations(ModelBuilder modelBuilder);
        abstract public void ConfigureColumns(ModelBuilder modelBuilder);
        abstract public void InitialInserts(ModelBuilder modelBuilder);

        public void InsertDataForEnvironment(ModelBuilder modelBuilder, params IDataInserts[] dataInserts)
        {
            InsertDataForEnvironment(modelBuilder, "ASPNETCORE_ENVIRONMENT", dataInserts);
        }

        public void InsertDataForEnvironment(ModelBuilder modelBuilder, string environmentName, params IDataInserts[] dataInserts)
        {
            var environment = EnvironmentUtils.GetEnvironmentVariable(environmentName);
            var foundDataInsert = dataInserts.Where(di => di.Environment.Equals(environment)).FirstOrDefault();
            if (foundDataInsert == null)
            {
                Console.WriteLine($"ERROR: No initial data inserts found for env '{environment}'");
            }
            else
            {
                foundDataInsert.InsertData(modelBuilder);
            }
        }
    }
}
