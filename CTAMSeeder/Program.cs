using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.CommandLine;

namespace CTAMSeeder
{

    class Program
    {


        public static async Task<int> Main(string[] args)
        {

            LoadNecessaryAssemblies();


            var cmd = new RootCommand
            {
                new Option<string>(new []{"-db", "--database"}, description: "The connection string of the database."),
                new Option<string>(new []{"-bc", "--blob-connection"}, description: "The blob connection string of the storage account containing the protection keys."),
                new Option<string>(new []{"-bcn", "--blob-container-name"}, description: "The blob container name of the storage account containing the protection keys."),
                new Option<string>(new []{"-bn", "--blob-name"}, description: "The blob name of the storage account containing the protection keys."),
                new Option<string>(new []{"-kv", "--keyvault-identifier"}, description: "The identifier of the keyvault containing the key that encrypts the protection keys."),
                new Argument<SeederEnvironment>("environment", description: "The environment to perform commands on.",
                            getDefaultValue: () => SeederEnvironment.Development),

                new Command("seed", "Seed a CTAM database with data.")
                {
                    new Argument<TypeOfData>("typeOfData", description: "The type of data to seed the database with.",
                        getDefaultValue: () => TypeOfData.DevelopmentData)
                }.WithHandler(nameof(SeederCommands.HandleSeed)),

                new Command("clean", "Clean a CTAM database.")
                {
                }.WithHandler(nameof(SeederCommands.HandleClean)),

                new Command("migrate", "Migrates a CTAM database to its latest migration."){
                }.WithHandler(nameof(SeederCommands.HandleMigrate)),

                new Command("status", "Shows the status of the CTAM database."){
                }.WithHandler(nameof(SeederCommands.HandleStatus)),

            };



            return await cmd.InvokeAsync(args);


        }

        private static void LoadNecessaryAssemblies()
        {
            // Load all important assemblies from CloudAPI
            Assembly.Load("CTAM.Core");
            Assembly.Load("UserRoleModule");
            Assembly.Load("CabinetModule");
            Assembly.Load("ItemModule");
            Assembly.Load("ItemCabinetModule");
            Assembly.Load("CloudAPI");
            Assembly.Load("CommunicationModule");
            Assembly.Load("MileageModule");
            Assembly.Load("ReservationModule");
        }


    }

    internal class HostingEnvironment : IWebHostEnvironment
    {
        public HostingEnvironment(string environmentName)
        {
            EnvironmentName = environmentName;
        }

        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
