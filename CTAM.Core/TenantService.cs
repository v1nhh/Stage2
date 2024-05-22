using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Standard.Licensing;
using Standard.Licensing.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTAM.Core
{
    public class TenantService: ITenantService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TenantService> _logger;

        private enum TenantProperty
        {
            connectionString = 0,
            license = 1
        }


        public TenantService(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<TenantService> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public Dictionary<string, string> GetTenantConnections()
        {
            var tenantConnections = new Dictionary<string, string>();
            if (Boolean.Parse(Environment.GetEnvironmentVariable("UseAzureAppConfig", EnvironmentVariableTarget.Process) ?? "false"))
            {
                var tenants = _configuration["Settings:Tenants"];
                tenants.Split(";").ToList().ForEach(tenant =>
                {
                    var secretValue = JsonConvert.DeserializeObject<Dictionary<string, string>>(_configuration.GetValue<string>("Tenant:" + tenant));
                    var connection = secretValue[TenantProperty.connectionString.ToString()];
                    tenantConnections.Add(tenant, connection);
                });
            }
            else
            {
                var jsonString = _configuration["TENANTS"]; // Environment.GetEnvironmentVariable("TENANT_CONNECTION", EnvironmentVariableTarget.Process);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var tenants = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
                    foreach (var tenant in tenants)
                    {
                        tenantConnections.Add(tenant.Key, tenant.Value[TenantProperty.connectionString.ToString()]);
                    }
                }
                else
                {
                    // This block is for backward compatibility only after april 2024
                    // TODO: remove this block if all configurations are moved to { "connectionString": "x", "license": "y" } format
                    var jsonStringConnectionstringsOnly = _configuration["TENANT_CONNECTION"]; // Environment.GetEnvironmentVariable("TENANT_CONNECTION", EnvironmentVariableTarget.Process);
                    _logger.LogWarning($"Using oldstyle TENANT_CONNECTION: {jsonStringConnectionstringsOnly}");
                    tenantConnections = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStringConnectionstringsOnly);
                }
            }

            return tenantConnections;
        }

        private string GetPropertyForTenant(string tenant, TenantProperty property)
        {
            string secretProperty = "";

            if (Boolean.Parse(Environment.GetEnvironmentVariable("UseAzureAppConfig", EnvironmentVariableTarget.Process) ?? "false"))
            {
                var secretString = _configuration.GetValue<string>("Tenant:" + tenant);
                
                if (!string.IsNullOrWhiteSpace(secretString))
                {
                    var secretDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(secretString);
                    secretDict.TryGetValue(property.ToString(), out secretProperty);
                }
                else
                {
                    _logger.LogError($"Configuration value 'Tenant:{tenant}' not found");
                }
            }
            else
            {
                var jsonString = Environment.GetEnvironmentVariable("TENANTS", EnvironmentVariableTarget.Process);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var tenants = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
                    if (tenants.ContainsKey(tenant))
                    {
                        tenants[tenant].TryGetValue(property.ToString(), out secretProperty);
                    }
                }
                else
                {
                    // This block is for backward compatibility only after april 2024
                    // TODO: remove this block if all configurations are moved to { "connectionString": "x", "license": "y" } format
                    var jsonStringConnectionstringsOnly = Environment.GetEnvironmentVariable("TENANT_CONNECTION", EnvironmentVariableTarget.Process);
                    _logger.LogWarning($"Using oldstyle TENANT_CONNECTION: {jsonStringConnectionstringsOnly}");
                    var tenantConnections = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStringConnectionstringsOnly);
                    tenantConnections.TryGetValue(tenant, out secretProperty);
                }
            }

            return secretProperty;
        }

        public string GetLicenseKeyForTenant(string tenant)
        {
            return GetPropertyForTenant(tenant, TenantProperty.license);
        }

        public string GetConnectionStringForTenant(string tenant)
        {
            return GetPropertyForTenant(tenant, TenantProperty.connectionString);
        }

        public LicenseFields GetLicenseFieldsForTenant(string tenant)
        {
            var licenseKey = GetLicenseKeyForTenant(tenant);
            var licenseFields = new LicenseFields();
            var publicKey = Environment.GetEnvironmentVariable("LicensePublicKey", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(licenseKey))
            {
                throw new ArgumentException("License key is empty");
            }

            if (string.IsNullOrEmpty(publicKey))
            {
                throw new ArgumentException("License public key is empty");
            }

            var license = License.Load(Base64UrlEncoder.Decode(licenseKey));

            var validationFailures =
                license.Validate()
                       .ExpirationDate()
                       .And()
                       .Signature(publicKey)
                       .AssertValidLicense()
                       .ToList();

            if (validationFailures.Any())
            {
                throw new UnauthorizedAccessException(validationFailures.First().Message);
            }

            licenseFields.Tenant = license.AdditionalAttributes.Get(nameof(licenseFields.Tenant));
            licenseFields.CustomerName = license.AdditionalAttributes.Get(nameof(licenseFields.CustomerName));
            licenseFields.MaxUsers = int.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.MaxUsers)));
            licenseFields.MaxItems = int.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.MaxItems)));
            licenseFields.MaxIBKs = int.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.MaxIBKs)));
            licenseFields.ModuleBorrowReturn = bool.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.ModuleBorrowReturn)));
            licenseFields.ModuleDatadumpImport = bool.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.ModuleDatadumpImport)));
            licenseFields.ModuleSwapSwapbackReplace = bool.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.ModuleSwapSwapbackReplace)));
            licenseFields.StartLicense = DateTime.ParseExact(license.AdditionalAttributes.Get(nameof(licenseFields.StartLicense)), "dd-MM-yyyy", null);
            licenseFields.EndLicense = DateTime.ParseExact(license.AdditionalAttributes.Get(nameof(licenseFields.EndLicense)), "dd-MM-yyyy", null);
            licenseFields.Keyconductor = bool.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.Keyconductor)));
            licenseFields.Locker = bool.Parse(license.AdditionalAttributes.Get(nameof(licenseFields.Locker)));

            return licenseFields;
        }

        public List<string> GetLicensePermissionsForTenant(string tenant)
        {
            var licensePermissions = new List<string> { "Remove", "Add", "Admin", "Repair", "Read", "Write", "Delete" };
            var licenseFields = GetLicenseFieldsForTenant(tenant);

            if (licenseFields.ModuleBorrowReturn)
            {
                licensePermissions.Add("Borrow");
                licensePermissions.Add("Return");
            }

            if (licenseFields.ModuleSwapSwapbackReplace)
            {
                licensePermissions.Add("Swap");
                licensePermissions.Add("Replace");
            }

            return licensePermissions;
        }

        public MainDbContext GetDbContext(string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentException("Connection string is empty");
            }
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

            optionsBuilder
                .UseSqlServer(connection, sqlServer =>
                {
                    sqlServer.CommandTimeout(120).EnableRetryOnFailure(5);
                })
                .UseApplicationServiceProvider(_serviceProvider);

            return new MainDbContext(optionsBuilder.Options, null);
        }
    }
}