using Azure.Identity;
using CabinetModule.Services;
using CloudAPI.ApplicationCore.Interfaces;
using CloudAPI.Infrastructure;
using CloudAPI.Services;
using CloudAPI.Web.Events;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace CloudAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                IHost host = CreateHostBuilder(args).Build();

                // Create a new scope to retrieve scoped services
                using (var scope = host.Services.CreateScope())
                {
                    var initService = scope.ServiceProvider.GetRequiredService<InitService>();
                    initService.Migrate();
                }

                // Register the ApplicationStopping event
                host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping.Register(() =>
                {
                    using var scope = host.Services.CreateScope();
                    var connectedClients = scope.ServiceProvider.GetRequiredService<IConnectedClients>();
                    var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();

                    try
                    {
                        var availableTenantConnections = tenantService.GetTenantConnections();
                        foreach (var tenant in connectedClients.GetAllConnectedTenants())
                        {
                            if (availableTenantConnections.TryGetValue(tenant, out string connection))
                            {
                                var dbContext = tenantService.GetDbContext(connection);
                                var connectedCabinetNumbers = connectedClients.GetConnectedCabinetNumbersForTenant(tenant);
                                var cabinetNumbers = string.Join(",", connectedCabinetNumbers);
                                // Status 2 = Offline, Status 1 = Online, Status 3 = OnlineInUse, Status 5 = Syncing, Status 6 = Error
                                dbContext.Database.ExecuteSqlRaw($"UPDATE [Cabinet].[Cabinet] SET [Status] = 2 WHERE [Status] IN (1, 3, 5, 6) AND [CabinetNumber] IN ({cabinetNumbers})");
                                logger.Info("Connected cabinets are set to Offline, with cabinet numbers: " + cabinetNumbers);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        logger.Error(ex, "An error occurred during application stopping");
                    }
                });

                // Run the WebHost, and start accepting requests
                // There's an async overload, so we may as well use it
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Boolean.Parse(EnvironmentUtils.GetEnvironmentVariable("UseAzureAppConfig")))
                    {
                        webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var settings = config.Build();

                            config.AddAzureAppConfiguration(options =>
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
                        })
                        .UseStartup<Startup>();
                    } else
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                })
                .ConfigureServices(services =>
                {
                    services.AddTransient<InitService>();
                    services.AddSingleton<IConnectedClients, ConnectedClients>();
                    services.AddSingleton<IFullSyncQueue, FullSyncQueue>();
                    services.AddHostedService<BackgroundFullSyncQueueService>();
                    services.AddHostedService<RequestBackgroundService>();
                    services.AddSingleton<IBackgroundRequestQueue, BackgroundRequestQueue>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders()
                        .SetMinimumLevel(LogLevel.Trace)
                        .AddAzureWebAppDiagnostics();
                })
                .UseNLog();
    }
}