using System;
using CTAM.Core.Interfaces;
using ItemCabinetModule.ApplicationCore.DataManagers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ItemCabinetModule
{
    public class ItemCabinetServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public ItemCabinetServicesSetup (IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void AddModuleServices(IServiceCollection services)
        {
            
            AddSwagger(services);
            AddDataManagers(services);
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("item-cabinet", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM ItemCabinet Module",
                    Description = "API for the Webmanager",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://nautaconnect.com/contact/"),
                    }
                });
            });
        }

        public void AddDataManagers(IServiceCollection services)
        {
            services.AddScoped<ItemCabinetDataManager>();
            services.AddScoped<LiveSyncDataManager>();
        }
    }
}
