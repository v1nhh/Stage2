using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CloudAPI;
using CTAM.Core;
using CTAM.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CTAMSeeder
{

    static class CommandExt
    {
        public static Command WithHandler(this Command command, string name)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = typeof(SeederCommands).GetMethod(name, flags);

            var handler = CommandHandler.Create(method!);
            command.Handler = handler;
            return command;
        }
    }

    static class SeederCommands
    {
        internal static async Task<int> HandleSeed(
            SeederEnvironment environment,
            TypeOfData typeOfData,
            string database,
            string blobConnection,
            string blobContainerName,
            string blobName,
            string keyvaultIdentifier)
        {
            Console.WriteLine($"Seeding with environment = {environment} and typeOfData = {typeOfData}");

            if (environment != SeederEnvironment.Development)
            {
                CheckAndSetEnvironmentVariables(environment, database, blobConnection, blobContainerName, blobName, keyvaultIdentifier);
            }

            var mainDbContext = CheckAndFetchDbContext(environment);
            var seeder = new Seeder(environment, mainDbContext);
            ConsoleKey response;
            do
            {
                Console.WriteLine("The database may contain data. Seeding the database will remove all data. Are you sure you want to continue? [y/n]");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            if (response == ConsoleKey.Y)
            {
                await seeder.SeedAsync(typeOfData, new DevelopmentData(), new ProductionData(), new ProductionDataBR());
                Console.WriteLine($"Succesfully seeded database running in {environment} with {typeOfData}");
                return 0;
            }
            else
            {
                Console.WriteLine($"Stopped seeding database running in {environment} with {typeOfData}");
                return 1;
            }

        }



        internal static async Task<int> HandleClean(
            SeederEnvironment environment,
            string database,
            string blobConnection,
            string blobContainerName,
            string blobName,
            string keyvaultIdentifier)
        {
            Console.WriteLine($"Cleaning database with environment = {environment}");
            if (environment != SeederEnvironment.Development)
            {
                CheckAndSetEnvironmentVariables(environment, database, blobConnection, blobContainerName, blobName, keyvaultIdentifier);
            }
            var seeder = FetchSeeder(environment);
            ConsoleKey response;
            do
            {
                Console.WriteLine("The database may contain data. Cleaning the database will remove all data. Are you sure you want to continue? [y/n]");
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            if (response == ConsoleKey.Y)
            {
                await seeder.CleanAsync();
                Console.WriteLine($"Succesfully cleaned database running in {environment}");
                return 0;

            }
            else
            {
                Console.WriteLine($"Stopped cleaning database running in {environment}");
                return 1;

            }
        }

        internal static async Task<int> HandleMigrate(
            SeederEnvironment environment,
            string database,
            string blobConnection,
            string blobContainerName,
            string blobName,
            string keyvaultIdentifier)
        {
            Console.WriteLine($"chosen environment = {environment}");
            if (environment != SeederEnvironment.Development)
            {
                CheckAndSetEnvironmentVariables(environment, database, blobConnection, blobContainerName, blobName, keyvaultIdentifier);
            }

            var mainDbContext = CheckAndFetchDbContext(environment);
            var seeder = new Seeder(environment, mainDbContext);
            await seeder.MigrateAsync();

            return 0;
        }

        internal static int HandleStatus(SeederEnvironment environment, IConsole console)
        {
            Console.WriteLine($"chosen environment = {environment}");
            var seeder = FetchSeeder(environment);
            // TODO: await seeder.StatusAsync();

            return 0;
        }

        private static MainDbContext CheckAndFetchDbContext(SeederEnvironment seederEnvironment)
        {
            // Create services
            // Create simple environment based on chosen environment
            Console.WriteLine($"Environment: {seederEnvironment}");
            var environment = new HostingEnvironment(seederEnvironment.ToString());
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", seederEnvironment.ToString());

            if (Environment.GetEnvironmentVariable("UseAzureAppConfig") == null)
            {
                Environment.SetEnvironmentVariable("UseAzureAppConfig", "false");
            }

            // Create config from appsettings*.json located in CloudAPI
            // For the reference to appsettings files see CTAMSeeder.csproj file
            var builder = new ConfigurationBuilder();

            if (seederEnvironment == SeederEnvironment.Development)
            {
                try
                {
                    builder.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/CloudAPI")
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{seederEnvironment}.json"); ;
                }
                catch (System.Exception)
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory() + "/CloudAPI")
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{seederEnvironment}.json"); ;
                }
            }


            var configuration = builder.AddEnvironmentVariables().Build();

            // Get connection string from the appsettings.*.json file
            var mainDbConnectionString = seederEnvironment == SeederEnvironment.Development
                                            ? configuration.GetConnectionString("Database")
                                            : Environment.GetEnvironmentVariable("Database");
            var securityDbConnectionString = configuration.GetConnectionString("securityDatabase");

            if ((String.IsNullOrEmpty(securityDbConnectionString) && seederEnvironment == SeederEnvironment.Development) || String.IsNullOrEmpty(mainDbConnectionString))
            {
                Console.Error.WriteLine($"database connectionstrings for {seederEnvironment} can not be found in the appsettings.{seederEnvironment}.json. Make sure the appsettings.{seederEnvironment}.json contains both a \'Database\' and in case of development a \'securityDatabase\' connectionstring.");
                throw new DatabaseConnectionStringNotFoundException();
            }

            Console.WriteLine($"Database: '{EnvironmentUtils.MaskConnectionStringPassword(mainDbConnectionString)}'");

            var services = new ServiceCollection();

            // Set up all services including Data Protection
            var servicesSetupFactory = new ServicesSetupFactory(configuration, environment);
            try
            {
                servicesSetupFactory.AddServicesFromAllModules(services);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not add services. Make sure the provided environment variables are correct. The following error has been thrown {e.Message}");
                throw;
            }
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(mainDbConnectionString, sqlServer =>
                {
                    // timeout needed for when clean command is used on cloud database
                    sqlServer.MigrationsAssembly("CloudAPI").CommandTimeout(120).EnableRetryOnFailure(5);
                }));

            if (seederEnvironment == SeederEnvironment.Development)
            {
                services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(securityDbConnectionString));
            }

            var serviceProvider = services.BuildServiceProvider();
            var mainDbContext = serviceProvider.GetService<MainDbContext>();

            // make sure current database is cleaned before seeding.
            if (!mainDbContext.Database.CanConnect())
            {
                throw new Exception("Can not connect to main database, make sure the connectionString is correct and the SQL Server is reachable.");
            }

            if (seederEnvironment == SeederEnvironment.Development)
            {
                var securityDbContext = serviceProvider.GetService<SecurityDbContext>();
                if (!securityDbContext.Database.CanConnect())
                {
                    throw new Exception("Can not connect to security database, make sure the connectionString is correct and the SQL Server is reachable.");
                }
                securityDbContext.Database.Migrate();
            }

            return mainDbContext;
        }

        private static void CheckAndSetEnvironmentVariables(SeederEnvironment seederEnvironment, string database, string blobConnection, string blobContainerName, string blobName, string keyvaultIdentifier)
        {

            Environment.SetEnvironmentVariable("Database", database);
            Environment.SetEnvironmentVariable("BLOB_CONNECTION", blobConnection);
            Environment.SetEnvironmentVariable("BLOB_CONTAINER_NAME", blobContainerName);
            Environment.SetEnvironmentVariable("BLOB_NAME", blobName);
            Environment.SetEnvironmentVariable("KEYVAULT_KEYIDENTIFIER", keyvaultIdentifier);

            var mainDbConnectionString = Environment.GetEnvironmentVariable("Database");
            var blobConnectionString = Environment.GetEnvironmentVariable("BLOB_CONNECTION");
            var blobContainer = Environment.GetEnvironmentVariable("BLOB_CONTAINER_NAME");
            var blob = Environment.GetEnvironmentVariable("BLOB_NAME");
            var keyVaultKeyIdentifier = Environment.GetEnvironmentVariable("KEYVAULT_KEYIDENTIFIER");

            if (String.IsNullOrEmpty(mainDbConnectionString))
            {
                Console.WriteLine($"Provide the connectionstring for the CTAM database running in {seederEnvironment}:");
                mainDbConnectionString = Console.ReadLine();
                Environment.SetEnvironmentVariable("Database", mainDbConnectionString);
            }

            if (String.IsNullOrEmpty(blobConnectionString))
            {
                Console.WriteLine($"Provide the BLOB_CONNECTION for the storage blob containing the protection keys in {seederEnvironment}:");
                blobConnectionString = Console.ReadLine();
                Environment.SetEnvironmentVariable("BLOB_CONNECTION", blobConnectionString);
            }
            if (String.IsNullOrEmpty(blobContainer))
            {
                Console.WriteLine($"Provide the BLOB_CONTAINER_NAME for the storage blob containing the protection keys in {seederEnvironment}:");
                blobContainer = Console.ReadLine();
                Environment.SetEnvironmentVariable("BLOB_CONTAINER_NAME", blobContainer);
            }
            if (String.IsNullOrEmpty(blob))
            {
                Console.WriteLine($"Provide the BLOB_NAME for the storage blob containing the protection keys in {seederEnvironment}:");
                blob = Console.ReadLine();
                Environment.SetEnvironmentVariable("BLOB_NAME", blob);
            }
            if (String.IsNullOrEmpty(keyVaultKeyIdentifier))
            {
                Console.WriteLine($"Provide the KEYVAULT_KEYIDENTIFIER for the keyvault containing the key for encrypting protection keys in {seederEnvironment}:");
                keyVaultKeyIdentifier = Console.ReadLine();
                Environment.SetEnvironmentVariable("KEYVAULT_KEYIDENTIFIER", keyVaultKeyIdentifier);
            }
        }

        internal static Seeder FetchSeeder(SeederEnvironment seederEnvironment)
        {
            try
            {
                var mainDbContext = CheckAndFetchDbContext(seederEnvironment);
                var seeder = new Seeder(seederEnvironment, mainDbContext);
                return seeder;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                throw;
            }

        }
    }

}