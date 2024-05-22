
using System;
using System.Linq;
using System.Threading.Tasks;
using CTAM.Core;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace CTAMSeeder
{
    public enum SeederEnvironment
    {
        Development,
        DevelopmentCloud,
        Acceptance
    }

    public enum TypeOfData
    {
        DevelopmentData,
        ProductionData,
        ProductionDataBR,
        DummyData
    }
    public class Seeder
    {
        private readonly SeederEnvironment seederEnvironment;
        private readonly MainDbContext mainDbContext;

        public Seeder(SeederEnvironment environment, MainDbContext mainDbContext)
        {
            this.seederEnvironment = environment;
            this.mainDbContext = mainDbContext;
        }

        public async Task SeedAsync(TypeOfData typeOfData, params ISeedData[] seedDatas)
        {
            switch (typeOfData)
            {
                case TypeOfData.DummyData:
                    await AddDummyData(mainDbContext);
                    break;

                default:
                    // check whether typeOfData exists in provided parameters
                    var seedData = seedDatas.FirstOrDefault(sd => sd.SeederDataType.Equals(typeOfData));

                    if (seedData == null)
                    {
                        Console.Error.WriteLine($"No data found for seederDataType = '{typeOfData}'. Make sure the type of data you are trying to seed exists in the CTAMSeeder.");
                        return;
                    }
                    // make sure current database is cleaned before seeding.
                    var canConnect = mainDbContext.Database.CanConnect();
                    if (canConnect)
                    {
                        await CleanAsync();
                        await MigrateAsync();
                        await SeedEntities(seedData);
                        return;
                    }
                    else
                    {
                        throw new Exception("Can not connect to database(s), make sure the connectionStrings are correct and the SQL Server is reachable.");

                    }
            }


        }

        public async Task MigrateAsync()
        {
            var currentMigrations = (await mainDbContext.Database.GetAppliedMigrationsAsync()).ToList();
            Console.WriteLine("Current migrations are applied to the CTAM database:");
            currentMigrations.ForEach(pm => Console.WriteLine(pm));

            // make sure database is created with the latest migrations.
            var pendingMigrations = (await mainDbContext.Database.GetPendingMigrationsAsync()).ToList();
            Console.WriteLine("Applying the following migration(s) on the CTAM database:");
            pendingMigrations.ForEach(pm => Console.WriteLine(pm));

            var migrator = mainDbContext.Database.GetInfrastructure().GetService<IMigrator>();
            await migrator.MigrateAsync();
        }


        public async Task CleanAsync()
        {
            var tables = mainDbContext.Model.GetEntityTypes()
                 .Select(t => t.GetSchemaQualifiedTableName())
                 .Distinct()
                 .ToList();

            // Drop all foreign key constraints for database so that all tables can be dropped without issues
            await mainDbContext.Database.ExecuteSqlRawAsync(@"declare @sql nvarchar(max) = (
                                                                select 
                                                                    'alter table ' + quotename(schema_name(schema_id)) + '.' +
                                                                    quotename(object_name(parent_object_id)) +
                                                                    ' drop constraint '+quotename(name) + ';'
                                                                from sys.foreign_keys
                                                                for xml path('')
                                                            );
                                                            exec sp_executesql @sql;");
            foreach (var t in tables)
            {
                try
                {
                    await mainDbContext.Database.ExecuteSqlRawAsync($"DROP TABLE {t}");
                }
                catch (Exception)
                {
                    continue;
                }
            }
            try
            {
                await mainDbContext.Database.ExecuteSqlRawAsync($"DROP TABLE dbo.__EFMigrationsHistory");
            }
            catch
            {

            }

        }


        private async Task SeedEntities(ISeedData seedData)
        {
            await SeedMainEntities(seedData);
            await SeedRelationalEntities(seedData);
            await SeedCabinetEntities(seedData);
            await SeedAdditionalEntities(seedData);
        }

        private async Task SeedMainEntities(ISeedData seedData)
        {
            try
            {
                // add main entities
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.Roles);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.Users);
                await mainDbContext.BulkInsertAsync(seedData.ItemTypes);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.ErrorCodes);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.Items);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.Cabinets);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CTAMSetting);
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Something went wrong while trying to seed the database with main entities.\n The following exception was thrown = {e.Message}");
                throw;
            }

        }
        private async Task SeedRelationalEntities(ISeedData seedData)
        {
            try
            {
                // // add relational entities
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.UserRoles);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.RolePermissions);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.RoleCabinets);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.RoleItemTypes);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.ItemTypeErrorCodes);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CTAMUserInPossessions);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CTAMUserPersonalItems);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetStocks);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong while trying to seed the database with relational entities.\n The following exception was thrown = {e.Message}");
                throw;
            }
        }

        private async Task SeedCabinetEntities(ISeedData seedData)
        {
            try
            {
                // // add additional cabinet entities
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetActions);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetColumns);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetCells);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetDoor);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetLogs);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetPositions);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.CabinetPositionContents);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.AllowedCabinetPositions);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong while trying to seed the database with cabinet entities.\n The following exception was thrown = {e.Message}");
                throw;
            }
        }

        private async Task SeedAdditionalEntities(ISeedData seedData)
        {
            try
            {
                // // add additional item entities
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.ItemDetails);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.ItemSets);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.ItemToPicks);

                // // add additional communication entities
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.MailQueue);
                await mainDbContext.BulkInsertOrUpdateAsync(seedData.MailTemplates);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong while trying to seed the database with additional entities.\n The following exception was thrown = {e.Message}");
                throw;
            }
        }


        private async Task AddDummyData(MainDbContext mainDbContext)
        {
            Console.WriteLine("Start adding dummy data...");
            await MigrateAsync();
            var permissions = await mainDbContext.CTAMPermission().AsNoTracking().ToListAsync();
            Console.WriteLine($"Permissions count: {permissions.Count}");
            // UserRoleModule
            var dummyDataSeeder = new BogusDummyDataSeeder(mainDbContext);
            var users = await dummyDataSeeder.AddUsers(60000);
            var roles = await dummyDataSeeder.AddRoles(1000);
            await dummyDataSeeder.AddRolePermissions(roles, permissions);
            await dummyDataSeeder.AddUserRoles(users, roles, 100);

            //CabinetModule
            var cabinets = await dummyDataSeeder.AddCabinets(600);
            var rolecabinets = await dummyDataSeeder.AddRoleCabinets(roles, cabinets, 300);

            // ItemModule
            var itemTypes = await dummyDataSeeder.AddItemTypes(700);
            var roleitemtypes = await dummyDataSeeder.AddRoleItemTypes(roles, itemTypes, 300);
            await dummyDataSeeder.AddItems(80000, itemTypes); // should always be more than amount of users

            // ItemCabinetModule
            await dummyDataSeeder.LinkUserPersonalItems();

            // CabinetLogs
            await dummyDataSeeder.AddCabinetLogs(60000, cabinets);

            // CabinetStocks
            await dummyDataSeeder.AddCabinetStocks(rolecabinets, roleitemtypes);

            Console.WriteLine("Done.");
        }
    }
}