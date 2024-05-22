using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CTAM.Core
{
    public class CoreServicesSetup : IServicesSetup
    {
        public IConfiguration Configuration { get; protected set; }
        public IWebHostEnvironment Environment { get; protected set; }


        public CoreServicesSetup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void AddModuleServices(IServiceCollection services)
        {
            AddAuthentication(services);
            AddAuthorization(services);
            AddCors(services);
            ConfigureSignalR(services);
        }

        public void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization();
        }

        public void AddAuthentication(IServiceCollection services)
        {
            //Console.WriteLine("UserRole -> StartupServices: AddAuthentication");
            services.AddSingleton<RsaSecurityKey>(provider =>
            {
                RSA rsa = RSA.Create();
                rsa.ImportRSAPublicKey(
                    source: Convert.FromBase64String(Configuration.GetValue<string>("Jwt:PublicKey")),
                    bytesRead: out _
                );

                return new RsaSecurityKey(rsa);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                SecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
                                options.RequireHttpsMetadata = false;
                                options.SaveToken = true;
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = Configuration["Jwt:Issuer"],
                                    ValidAudience = Configuration["Jwt:Audience"],
                                    IssuerSigningKey = rsa,
                                    ClockSkew = TimeSpan.Zero
                                };
                                options.Events = new JwtBearerEvents
                                {
                                    OnMessageReceived = context =>
                                    {
                                        var tenantFromHeader = context.Request.Headers["X-Tenant-ID"];
                                        var accessToken = context.Request.Query["access_token"];
                                        var hasCookie = context.Request.Cookies.Where(cookie => cookie.Key.Equals("access_token_" + tenantFromHeader)).ToList();

                                        if (hasCookie.Count > 0)
                                        {
                                            context.Token = hasCookie.FirstOrDefault().Value;
                                        }
                                        else if (!string.IsNullOrEmpty(accessToken))
                                        {
                                            context.Token = accessToken;
                                        }
                                        return Task.CompletedTask;
                                    }
                                };
                            });
        }

        public void ConfigureSignalR(IServiceCollection services)
        {
            var replicationMode = Configuration.GetValue<string>("ReplicationMode")?.ToUpper();
            Console.WriteLine($"SignalR Mode: {replicationMode}");
            switch (replicationMode)
            {
                case "OFF":
                    services.AddSignalR();
                    break;
                case "REDIS":
                    var redisConnectionString = Configuration.GetConnectionString("Redis");
                    Console.WriteLine($"Using Redis ConnectionString: {EnvironmentUtils.MaskConnectionStringPassword(redisConnectionString)}");
                    services.AddSignalR()
                        .AddStackExchangeRedis(redisConnectionString,
                        o =>
                        {
                            o.Configuration.AbortOnConnectFail = false;
                            o.Configuration.ResolveDns = true;
                            o.Configuration.ConnectRetry = 3;
                        });
                    break;
                case "AZURE_SIGNALR":
                    services
                        .AddSignalR()
                        .AddAzureSignalR(Configuration.GetValue<string>("AzureSignalRKey"));
                    break;
                default:
                    services.AddSignalR();
                    break;
            }
        }

        public void AddCors(IServiceCollection services)
        {
            // WARNING In production, the only origin that should be allowed is the origin of the webapp(no localhost)

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        switch (Environment.EnvironmentName)
                        {
                            case "DevelopmentCloud":
                                Console.WriteLine("Development CORS configuration");
                                builder.WithOrigins(new string[] { "https://*.ctam-dev.nautaconnect.cloud" })
                                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                                    .AllowCredentials()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                                break;
                            case "Acceptance":
                                Console.WriteLine("Acceptance CORS configuration");
                                builder.WithOrigins(new string[] { "https://*.ctam-uat.nautaconnect.cloud" })
                                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                                break;

                            case "Production":
                                Console.WriteLine("Production CORS configuration");
                                builder.WithOrigins(new string[] { "https://*.ctam.nautaconnect.cloud" })
                                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials();
                                break;

                            default:

                                var additionalAllowedOrigin = Configuration.GetValue<string>("AdditionalAllowedOrigin");
                                var allowedOrigins = new List<string> { "http://localhost:8081", "http://localhost:8080", "http://localhost" };
                                if (!string.IsNullOrEmpty(additionalAllowedOrigin))
                                {
                                    allowedOrigins.Add(additionalAllowedOrigin);
                                }
                                builder.AllowAnyMethod().AllowAnyHeader()
                                        .WithOrigins(allowedOrigins.ToArray())
                                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                                        .AllowCredentials();
                                break;
                        }

                    });
            });

        }
    }
}