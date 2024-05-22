using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ReservationModule.ApplicationCore.DataManagers;
using System;

namespace ReservationModule
{
    public class ReservationServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public ReservationServicesSetup(IConfiguration configuration, IWebHostEnvironment environment)
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
                c.SwaggerDoc("reservation", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM Reservation Module",
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
            services.AddScoped<ReservationDataManager>();
        }
    }
}
