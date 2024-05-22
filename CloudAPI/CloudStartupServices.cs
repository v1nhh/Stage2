using Azure.Identity;
using CloudAPI.ApplicationCore.Services;
using CloudAPI.Services;
using CloudApiModule.ApplicationCore.DataManagers;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Security;
using CTAM.Core.Utilities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace CloudAPI
{
    public class CloudStartupServices : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }
        private readonly AuthenticationUtilities _authUtils;
        private readonly string _env;

        public CloudStartupServices(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            _authUtils = new AuthenticationUtilities(Configuration);
            _env = EnvironmentUtils.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine(_env);
        }


        public void AddModuleServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<CleanLogs>();
            services.AddTransient<ITenantService, TenantService>();

            services.AddScoped<LiveSyncService>();

            if (Boolean.Parse(EnvironmentUtils.GetEnvironmentVariable("UseAzureAppConfig")))
            {
                services.AddAzureAppConfiguration();
            }
            AddSwagger(services);
            AddDataProtection(services);
            AddDataManagers(services);
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("cloud", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CTAM CloudAPI Module",
                    Description = "API for the Webmanager",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://nautaconnect.com/contact/"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public void AddDataProtection(IServiceCollection services)
        {
            // TODO: change directory for Production on Azure Server
            // https://itinnovatorsbv.atlassian.net/browse/CTAM-539
            // For more info read:
            // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-5.0
            // https://jakeydocs.readthedocs.io/en/latest/security/data-protection/configuration/overview.html
            // https://medium.com/swlh/how-to-distribute-data-protection-keys-with-an-asp-net-core-web-app-8b2b5d52851b
            // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/implementation/key-management?view=aspnetcore-5.0#key-expiration-and-rolling

            // Default key lifetime is 90 days, to change it to e.g. 14 days use: .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
            // The default key lifetime cannot be shorter than 7 days.
            switch (_env)
            {
                case "DevelopmentCloud":
                case "Acceptance":
                case "Production":
                // case "Staging": // use the staging environment variable to populate the databases on creation
                    Console.WriteLine("Adding Data Protection");
                    var blobConnectionString = EnvironmentUtils.GetEnvironmentVariable("BLOB_CONNECTION");
                    var blobContainerName = EnvironmentUtils.GetEnvironmentVariable("BLOB_CONTAINER_NAME");
                    var blobName = EnvironmentUtils.GetEnvironmentVariable("BLOB_NAME");
                    var keyVaultKeyIdentifier = EnvironmentUtils.GetEnvironmentVariable("KEYVAULT_KEYIDENTIFIER");
                    services.AddDataProtection()
                        .SetApplicationName("CTAM_CloudAPI_v1.0")
                        .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
                        .PersistKeysToAzureBlobStorage(blobConnectionString, blobContainerName, blobName)
                        .ProtectKeysWithAzureKeyVault(new Uri(keyVaultKeyIdentifier), new DefaultAzureCredential());
#if DEBUG
                    using (var servicesProvider = services.BuildServiceProvider())
                    {
                        // get a reference to the key manager
                        var keyManager = servicesProvider.GetService<IKeyManager>();

                        // list all keys in the key ring
                        var allKeys = keyManager.GetAllKeys();
                        Console.WriteLine($"The key ring contains {allKeys.Count} key(s).");
                        foreach (var key in allKeys)
                        {
                            Console.WriteLine($"Key {key.KeyId:B}: Created = {key.CreationDate:u}, IsRevoked = {key.IsRevoked}");
                        }
                    }
#endif
                    break;
                default:
                    var securityDbConnectionString = Configuration.GetConnectionString("SecurityDatabase");
                    
                    services.AddDbContext<SecurityDbContext>( options => {
                        options.UseSqlServer(securityDbConnectionString);
                    });

                    services.AddDataProtection()
                        .SetApplicationName("CTAM_CloudAPI_v1.0")
                        .SetDefaultKeyLifetime(TimeSpan.FromDays(7))
                        .PersistKeysToDbContext<SecurityDbContext>();
                    break;
            };
        }

        public CloudStartupServices AddDynamicDbContext<T>(IServiceCollection services) where T : DbContext
        {
            Console.WriteLine($"CloudAPI > StartupService: Adding {typeof(T).FullName}");
            services.AddDbContext<T>(
                (serviceProvider, options) =>
                {
                    Console.WriteLine($"Configuring {typeof(T).FullName}");
                    var sqlConnectionContext = serviceProvider.GetService<SqlConnectionContext>();
                    var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
                    var tenantContext = serviceProvider.GetService<ITenantContext>();
                    var tenantService = serviceProvider.GetService<ITenantService>();
                    if (httpContext != null && httpContext.Request != null)
                    {
                        Console.WriteLine($"HttpContext and HttpContext.Request are not null");
                        Console.WriteLine($"Current ClaimPrincipal: {httpContext.Request.Cookies.Count}");
                        try
                        {
                            Console.WriteLine(httpContext.Request.Cookies.Where(cookie => cookie.Key.Equals("access_token_" + tenantContext.TenantId)).FirstOrDefault().Value);
                        }
                        catch{}
                        var principal = httpContext.User as ClaimsPrincipal;
                        var claims = principal.Identities.Select(id => "[" + id.Name + ": " + string.Join(", ", id.Claims.Select(c => c.Type)) + "]");
                        Console.WriteLine($"Amount of claims from principals: {claims.Count()}");
                        if (ShouldGetTenantIdFromHeader(httpContext))
                        {
                            Console.WriteLine($"Getting Tenant ID from header");
                            var tenantID = _authUtils.ExtractTenantIDFromHeader(httpContext);
                            Console.WriteLine($"Tenant ID: {tenantID}");
                            SetConnectionString(tenantID, options, sqlConnectionContext, tenantService);
                        }
                        // If user is already logged in check for TenantID in claims of JWT token
                        else if (httpContext.User.Claims.Count() > 0)
                        {
                            Console.WriteLine($"Getting Tenant ID from claims");
                            var tenantID = _authUtils.ExtractTenantIDFromClaims(httpContext, tenantContext);
                            Console.WriteLine($"Tenant ID: {tenantID}");
                            SetConnectionString(tenantID, options, sqlConnectionContext, tenantService);
                        }
                    }
                    else if (tenantContext.TenantId != null)
                    {
                        Console.WriteLine($"Tenant ID from TenantContext: {tenantContext.TenantId}");
                        SetConnectionString(tenantContext.TenantId, options, sqlConnectionContext, tenantService);
                    }
                    else if (_env.Equals("Docker") || _env.Equals("Development") || _env.Equals("Staging"))
                    {

                        Console.WriteLine($"Getting default connection string, NOT FOR PRODUCTION");

                        var connectionString = Configuration.GetConnectionString("Database");
                        Console.WriteLine($"Use default Database ConnectionString: {EnvironmentUtils.MaskConnectionStringPassword(connectionString)}");
                        options.UseSqlServer(connectionString, b => b.MigrationsAssembly("CloudAPI"));
                    }
                    else
                    {
                        Console.Error.WriteLine($"Couldn't get Tenant ID or default database connection string");
                    }
                },
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);
            return this;
        }

        public bool ShouldGetTenantIdFromHeader(HttpContext httpContext)
        {
            // If user is logging in or forgot password check for TenantID in the request header
            var controllerRouteValue = httpContext.Request.RouteValues["controller"];
            var actionRouteValue = httpContext.Request.RouteValues["action"];
            return controllerRouteValue != null && (
                controllerRouteValue.Equals("Login") ||
                (controllerRouteValue.Equals("UserSettings") && (actionRouteValue.Equals("UserForgotPassword") || actionRouteValue.Equals("ChangeUserForgottenPassword")))
            );
        }

        public void SetConnectionString(string tenantID, DbContextOptionsBuilder options, SqlConnectionContext sqlConnectionContext, ITenantService tenantService)
        {
            // For development/testing purposes you should add "TENANTS" variable to your launchSettings.json
            // e.g.: "TENANTS": "{'bra':{'connectionString': 'Server=localhost;Database=ctam;User ID=sa;Password=C@ptur3T;Application Name=Politie.CloudAPI', 'license':'lic'}"
            if (tenantID != null)
            {
                var tenantConnection = tenantService.GetConnectionStringForTenant(tenantID);

                if (!string.IsNullOrWhiteSpace(tenantConnection))
                {
                    if (sqlConnectionContext.TenantID == null || sqlConnectionContext.DbConnection == null)
                    {
                        sqlConnectionContext.TenantID = tenantID;
                        var connectionString = tenantConnection.TrimEnd(';') + ";MultipleActiveResultSets=True";
                        sqlConnectionContext.DbConnection = new SqlConnection(connectionString);
                        Console.WriteLine($"New SQL Connection created for Tenant '{tenantID}': {EnvironmentUtils.MaskConnectionStringPassword(connectionString)}");
                    }
                    else
                    {
                        Console.WriteLine($"SQL Connection already exists for Tenant: {tenantID}");
                    }
                    options.UseSqlServer(sqlConnectionContext.DbConnection, b => b.MigrationsAssembly("CloudAPI"));
                }
            }
        }

        public void AddDataManagers(IServiceCollection services)
        {
            services.AddScoped<CloudApiDataManager>();
        }
    }
}
