using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using CTAM.Core;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetItemIDsQuery : IRequest<List<int>>
    {        
        public GetItemIDsQuery()
        {            
        }
    }

    public class GetItemIDsHandler : IRequestHandler<GetItemIDsQuery, List<int>>
    {
        private readonly ILogger<GetItemIDsHandler> _logger;
        private readonly MainDbContext _context;

        public GetItemIDsHandler(MainDbContext context, ILogger<GetItemIDsHandler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<int>> Handle(GetItemIDsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemIDsHandler called");            

            return await _context.Item()
                            .Select(i => i.ID)
                            .ToListAsync();
        }
    }

}
