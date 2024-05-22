using CloudAPI.ApplicationCore.Services;
using CloudAPI.Web.Events;
using CTAM.Core;
using CTAM.Core.Converters;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using UserRoleModule;

namespace CloudAPI
{
    public class Startup
    {
        private readonly ServicesSetupFactory _servicesSetupFactory;
        private readonly CloudStartupServices _cloudStartupServices;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            _cloudStartupServices = new CloudStartupServices(Configuration, Environment);
            _servicesSetupFactory = new ServicesSetupFactory(Configuration, Environment);
        }

        public IConfiguration Configuration
        {
            get;
        }
        public IWebHostEnvironment Environment
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Boolean.Parse(EnvironmentUtils.GetEnvironmentVariable("UseAzureAppConfig")))
            {
                var settings = Configuration.GetSection("Settings");
                services.Configure<Settings>(settings);
                services.AddAzureAppConfiguration();
            }

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Path = "/";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new CultureInfo[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("nl-NL"),
                    new CultureInfo("ar-AE"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("sv-SE"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
            services.AddHealthChecks();
            services.AddScoped<ITenantContext, TenantContext>();
            services.AddScoped<SqlConnectionContext>();

            _cloudStartupServices.AddDynamicDbContext<MainDbContext>(services);

            _servicesSetupFactory.AddServicesFromAllModules(services);

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHostedService<ManageRequestsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsDevelopment())
            {
                app.UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = SameSiteMode.Strict,
                    HttpOnly = HttpOnlyPolicy.Always,
#if !DEBUG
                    Secure = CookieSecurePolicy.Always
#endif
                });
            };

            if (Boolean.Parse(EnvironmentUtils.GetEnvironmentVariable("UseAzureAppConfig")))
            {
                app.UseAzureAppConfiguration();
            }

            app.UseRequestLocalization();

            app.UseMiddleware<NLogMiddleware>();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                await next();
            });

            var replicationMode = Configuration.GetValue<string>("ReplicationMode")?.ToUpper();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = (_) => false
                });
                endpoints.MapHealthChecks("/health/liveness", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = (_) => false
                });
                endpoints.MapControllers();
                if (replicationMode.Equals("OFF") || replicationMode.Equals("REDIS"))
                {
                    Console.WriteLine("Configuring Standard SignalR service");
                    endpoints.MapHub<EventHub>("/events");
                }
            });

            if (replicationMode.Equals("AZURE_SIGNALR"))
            {
                Console.WriteLine("Configuring Azure SignalR service");
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<EventHub>("/events");
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/cloud/swagger.json", "CloudAPI V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}