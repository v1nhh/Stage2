using Azure.Identity;
using CloudAPI.Services;
using CommunicationModule.Infrastructure.Email;
using CommunicationModule.Infrastructure.Email.Services;
using CommunicationModule.Infrastructure.Email.Strategies;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(CTAMFunctions.Startup))]
namespace CTAMFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            if (Boolean.Parse(EnvironmentUtils.GetEnvironmentVariable("UseAzureAppConfig")))
            {
                var settings = builder.ConfigurationBuilder.Build();
                builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(settings.GetConnectionString("AppConfig"))
                        .ConfigureRefresh(refresh =>
                            {
                                refresh.Register("Sentinel", refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 15, 0));
                            })
                            .ConfigureKeyVault(kv =>
                            {
                                kv.SetCredential(new DefaultAzureCredential());
                            });
                });
            }
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDataProtection();
            builder.Services.AddEntityFrameworkSqlServer();

            // Add custom services
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<SendMails>();
            builder.Services.AddScoped<CleanLogs>();
            builder.Services.AddScoped<IEmailConfigService, EmailConfigService>();
            builder.Services.AddScoped<EmailSenderContext>();
        }
    }
}