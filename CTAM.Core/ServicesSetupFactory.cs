using System;
using System.Collections.Generic;
using System.Linq;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CTAM.Core
{
    public class ServicesSetupFactory
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public ServicesSetupFactory(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void AddServicesFromAllModules(IServiceCollection services)
        {
            var servicesSetups = GetAllServicesSetups();

            foreach (var servicesSetup in servicesSetups)
            {
                servicesSetup.AddModuleServices(services);
            }
        }

        private List<IServicesSetup> GetAllServicesSetups()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IServicesSetup).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                .Select(type => (IServicesSetup)Activator.CreateInstance(type, Configuration, Environment))
                .ToList();
        }
    }
}
