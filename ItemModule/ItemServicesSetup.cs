using System;
using Microsoft.AspNetCore.Hosting;
using ItemModule.ApplicationCore.DataManagers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CTAM.Core.Interfaces;

namespace ItemModule
{
    public class ItemServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public ItemServicesSetup (IConfiguration configuration, IWebHostEnvironment environment)
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
                c.SwaggerDoc("item", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM Item Module",
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
            services.AddScoped<ItemDataManager>();
        }
    }
}
