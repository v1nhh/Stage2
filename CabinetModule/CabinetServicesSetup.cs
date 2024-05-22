using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.Web.Security;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CabinetModule
{
    public class CabinetServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public CabinetServicesSetup (IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void AddModuleServices(IServiceCollection services)
        {
            AddSwagger(services);
            AddScopedAuthenticationService(services);
            AddDataManagers(services);
        }

        public void AddScopedAuthenticationService(IServiceCollection services)
        {
            services.AddScoped<CabinetAuthenticationService>();
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("cabinet", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM Cabinet Module",
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
            services.AddScoped<CabinetDataManager>();
        }
    }
}