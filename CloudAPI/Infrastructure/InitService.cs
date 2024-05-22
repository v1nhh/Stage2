using System;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudAPI.Infrastructure
{
    public class InitService
    {
        private readonly MainDbContext _context;
        private readonly IServiceProvider _services;
        private readonly ILogger<InitService> _logger;

        public InitService(MainDbContext context, IServiceProvider services, ILogger<InitService> logger)
        {
            _context = context;
            _services = services;
            _logger = logger;
        }

        public void Migrate()
        {
            try
            {
                _context.Database.Migrate();
                var securityDbContext = _services.GetService(typeof(SecurityDbContext)) as SecurityDbContext;
                if (securityDbContext != null)
                {
                    securityDbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var msg = $"{ex.Message}\n{ex.InnerException}\n{ex.StackTrace}";
                _logger.LogError(msg);
            }
        }
    }
}
