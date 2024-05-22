using CommunicationModule.ApplicationCore.DataManagers;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CommunicationModule
{
    public class CommunicationServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public CommunicationServicesSetup(IConfiguration configuration, IWebHostEnvironment environment)
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
                c.SwaggerDoc("communication", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM Communication Module",
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
            services.AddScoped<CommunicationDataManager>();
        }
    }
}
