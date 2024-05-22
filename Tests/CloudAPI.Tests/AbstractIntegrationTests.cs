using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace CloudAPI.Tests
{
    public abstract class AbstractIntegrationTests
    {
        protected DbContextOptions<MainDbContext> ContextOptions { get; }
        public string LogResourcePathOfLastManagementLogInfo;
        public Dictionary<string, string> ParametersOfLastManagementLogInfo;

        public AbstractIntegrationTests(string databaseName = "CTAM")
        {
            // TODO: replace In-Memory database by Azure Development or Staging database
            // For more info: https://docs.microsoft.com/en-us/ef/core/testing/
            // https://itinnovatorsbv.atlassian.net/browse/CTAM-537
            //  Arrange
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            var fakeServiceProvider = new Mock<IServiceProvider>();
            string keyPath = Path.Combine(Directory.GetCurrentDirectory(), "ProtectionKeys");
            var dpProvider = DataProtectionProvider.Create(new DirectoryInfo(keyPath));
            //.SetApplicationName("CTAM_CloudAPI_v1.0")
            fakeServiceProvider.Setup(sp => sp.GetService(typeof(IDataProtectionProvider))).Returns(dpProvider);
            ContextOptions = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseApplicationServiceProvider(fakeServiceProvider.Object)
                .Options;

            using (var context = new MainDbContext(ContextOptions, null))
            {
                context.Database.EnsureDeleted();
                // Creates tables and adds rows from InitialInserts!
                context.Database.EnsureCreated();

                context.SaveChanges();
            }
        }

        public IManagementLogger CreateMockedManagementLogger()
        {
            var mockedManagementLogger = new Mock<IManagementLogger>();
            // All methods will return a completed Task, only LogInfo will be intercepted
            mockedManagementLogger.SetReturnsDefault(Task.CompletedTask);
            mockedManagementLogger.Setup(m => m.LogInfo(It.IsAny<string>(), It.IsAny<(string key, string value)[]>()))
                .Returns((string logResourcePath, (string key, string value)[] parameters) => SaveLogInfo(logResourcePath, parameters));
            return mockedManagementLogger.Object;
        }

        private Task SaveLogInfo(string logResourcePath, (string key, string value)[] parameters)
        { 
            this.LogResourcePathOfLastManagementLogInfo = logResourcePath;
            this.ParametersOfLastManagementLogInfo = parameters.ToDictionary(pair => pair.key, pair => pair.value);

            return Task.CompletedTask;
        }

        public IConfiguration CreateConfigurationStub(Dictionary<string, string> configuration = null)
        {
            var myConfiguration = configuration ?? new Dictionary<string, string>
            {
                {"Jwt:CabinetAPIKey", "CABINET_API_KEY"},
                // FOR TEST PURPOSES ONLY Generated keys (2048) from https://travistidwell.com/jsencrypt/demo/
                {"Jwt:PrivateKey", "MIIEoQIBAAKCAQBq6ygkRHJMvxpG9iTMpfoRMcHompfTC75XIYOAlM+uKO5Yi7l7g8xPj2T+bkUF+rjR/BO+TtczGuUqYGylWoH2dIRKQHJ4pFeQbvyBUZk6V4EqrTFfSYfghaOAjSMNHUlXttX/D1NY2k6HpK1qhMKfnvUJuJYeGKotksN13olnCoaOSzHL5XPcJTZ+6RaX4u9f0EbRX3NPpjYzsznfZzB2046baN0v2Co8VZ/1XFQoHHkRM676Eb7ZMJLRisxnpWq4Ft+j8nrfZK1PaBX7u1IgFa0PL+OhctPQ3PsLaxZunbhXkMv6L8rmmr6eoztHarOShQUBmGxJQhj/cDogXthFAgMBAAECggEAM+sdAv95wWrLB8pe9EFkvRS8ZIctUn5RX14WzUl1z8xwcO7okuHdRIUNNb+2JMErHkyaCb+fIGHfBkTsfR8GHPdXhfbln0+udaRvWRyWm44CWwbfArZiFNTQIIxoiOW/x6gxIjn+7xaTK6r7ys3M9FXsB1xrCRJREPBDmekGL9J7t1HHzKBidlXSnINP84sSYiFKja1bkCZwLwnPFiHqtdKU/dmAwc/cNw1Th3Z19bB5O9HiBNq/ieLuZg9FGQ1WKDcClPNwrFNH4w3C2Ko1dnqR5ZtWqJdFEzGJSrLmlb/7UUua0djWB3sJTJKXyRos4I1n0CKQCvbO3tpYx8BAAQKBgQCu8ClN0OOk5uW6NfCSNObG9XgnbkMJfLaettUNy+2mbKmlRgUBCJdIw9yPYqCuG/H6Beygs6aoRK+nCrjKrKcF3bValEx168Tz2/jYjXxtTjNxRyiOM9wczhjXsOrqUeIr9U2w+qmTzP3kJZLJeNUNycTxpLtg11mf84deiYtjRQKBgQCcdkDJm7KIHVaUuINK7YPEsHXhfKkESyIK5Qp2eD62gqf1TMdcxSxNqIhssINLmMq/CqxdwymKYUILfsM+itZfRiHZ6YCmGIgCHrMFhQz4YL/NF1W/McGX6HTh3xm144iSS0vji6hjX34WAOJVAtL+P7jt7bSAEv7J9U/DPxpxAQKBgCiXk4355YizByVRNNRIMDCEbu//VtDRvnJPjFpaMnfGiQrPl4Oo4anBwOEIiq47z8crLsAPT6GF5phFFaH5s/vZeSZeeOt33MiVB0YAdpnKZodncpKkl/ObIwqRg8EBZRC8J93kHAsuqs8QpOHZBt7fLbLAfbY34hUKZiPamJxdAoGAUogmQLK6iAZBx41U5E9JdJVIeTsSxMsFGA4daMShABEHm+N2Pj9b0VMXM44gk93zBLcSmRr7bJ1FA19IilMeJX+PF62OSQ8jn0qmUEEQyGvwgLTQbFxIrGLYVNUkcMenINRLIapnlpOmpOCugv1MYwUl/Yun7O8YK1wVSP6eagECgYAiJAdu7iKbDjCt+JPStsPcJuvoPdlQzazCxurJi2Ex7kpTl3z/0wo3dpzM2naqXd45rS3hbe1PUGLukyvs5bpS69cYLzJTK6IHDu/kIOrB2293k/j61+wqcxJvCJ4Qyf6MfKhV5v0SMRAGa3frMDIFXttaONJQ7kL6+92+3SnF2Q=="},
                {"Jwt:PublicKey", "MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQBq6ygkRHJMvxpG9iTMpfoRMcHompfTC75XIYOAlM+uKO5Yi7l7g8xPj2T+bkUF+rjR/BO+TtczGuUqYGylWoH2dIRKQHJ4pFeQbvyBUZk6V4EqrTFfSYfghaOAjSMNHUlXttX/D1NY2k6HpK1qhMKfnvUJuJYeGKotksN13olnCoaOSzHL5XPcJTZ+6RaX4u9f0EbRX3NPpjYzsznfZzB2046baN0v2Co8VZ/1XFQoHHkRM676Eb7ZMJLRisxnpWq4Ft+j8nrfZK1PaBX7u1IgFa0PL+OhctPQ3PsLaxZunbhXkMv6L8rmmr6eoztHarOShQUBmGxJQhj/cDogXthFAgMBAAE="},
                {"Jwt:Issuer", "https://localhost:5001/"},
                {"Jwt:Audience", "https://localhost:5001/"}
            };
            return new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
        
        public ITenantContext CreateMockedTenantContext()
        {
            var httpContextAccessor = CreateMockedHttpContextAccessor((CTAMClaimNames.TenandID, "ct"), (CTAMClaimNames.ClientType, "Web"));
            var tenantContext = new TenantContext(httpContextAccessor, new Mock<ILogger<TenantContext>>().Object);

            return tenantContext;
        }

        public ITenantService CreateMockedTenantService(bool hasSwapReplace)
        {
            List<string> list = new List<string>{ "Remove", "Add", "Admin", "Repair", "Read", "Write", "Delete", "Borrow", "Return" };
            if (hasSwapReplace)
            {
                list.Add("Swap");
                list.Add("Replace");
            }
            var tenantService = new Mock<ITenantService>();
            tenantService.Setup(ts => ts.GetLicensePermissionsForTenant("ct"))
                                    .Returns(list);

            return tenantService.Object;
        }


        /// <summary>
        /// Create mocked IHttpContextAccessor object, mostly needed to identify client
        /// </summary>
        /// <param name="claimKeyValuePairs">
        ///  For example KEY is JwtRegisteredClaimNames.Sub and VALUE is a CabinetNumber
        /// </param> 
        /// <returns></returns>
        public IHttpContextAccessor CreateMockedHttpContextAccessor(params (string, string)[] claimKeyValuePairs)
        {
            var claims = new List<Claim>();
            foreach (var (key, value) in claimKeyValuePairs)
            {
                var claim = new Claim(key, value);
                claim.Properties.Add(key, key);
                claims.Add(claim);
            }
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.SetupGet(hca => hca.HttpContext.User.Claims).Returns(claims);

            return httpContextAccessor.Object;
        }
    }
}
