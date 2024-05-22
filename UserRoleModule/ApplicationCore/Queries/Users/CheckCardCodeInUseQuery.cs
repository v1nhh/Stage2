using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class CheckCardCodeInUseQuery : IRequest<bool>
    {
        public string CardCode { get; set; }
    }

    public class CheckCardCodeInUseHandler : IRequestHandler<CheckCardCodeInUseQuery, bool>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CheckCardCodeInUseQuery> _logger;


        public CheckCardCodeInUseHandler(MainDbContext context, ILogger<CheckCardCodeInUseQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckCardCodeInUseQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckCardCodeInUseHandler called");
            var result = await _context.CTAMUser().AsNoTracking()
                .Where(user => user.CardCode.Equals(request.CardCode))
                .AnyAsync();

            return result;
        }
    }

}
