using CloudAPI.Services;
using CommunicationModule.Infrastructure.Email;
using CommunicationModule.Infrastructure.Email.Services;
using CommunicationModule.Infrastructure.Email.Strategies;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAMCron;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices(services =>
    {

        services.AddHostedService<SendMailsFunction>();
        services.AddHostedService<CleanLogsFunction>();
        services.AddDataProtection();
        services.AddEntityFrameworkSqlServer();

        services.AddTransient<ITenantService, TenantService>();
        services.AddTransient<SendMails>();
        services.AddTransient<CleanLogs>();
        services.AddTransient<IEmailConfigService, EmailConfigService>();
        services.AddTransient<EmailSenderContext>();

    })
    .Build();

await host.RunAsync();
