using System;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Services;
using UserRoleModule.Web.Security;

namespace UserRoleModule
{
    public class UserRoleServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }

        public UserRoleServicesSetup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void AddModuleServices(IServiceCollection services)
        {
            AddControllers(services);
            AddSwagger(services);
            AddScopedAuthenticationService(services);
            AddmanagementLogger(services);
            AddDataManagers(services);
        }

        public void AddScopedAuthenticationService(IServiceCollection services)
        {
            services.AddScoped<UserAuthenticationService>();
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("user-role", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM UserRole Module",
                    Description = "API for the Webmanager",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://nautaconnect.com/contact/"),
                    }
                });
            });
        }

        public void AddControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void AddmanagementLogger(IServiceCollection services)
        {
            services.AddTransient<IManagementLogger, ManagementLogger>();
        }

        public void AddDataManagers(IServiceCollection services)
        {
            services.AddScoped<UserRoleDataManager>();
        }
    }
}