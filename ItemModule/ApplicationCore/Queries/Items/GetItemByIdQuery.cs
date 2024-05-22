using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Queries.Items
{
    public class GetItemByIdQuery : IRequest<ItemDTO>
    {
        public int ID { get; set; }

        public GetItemByIdQuery(int id)
        {
            ID = id;
        }
    }

    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, ItemDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemByIdQuery> _logger;
        private readonly IMapper _mapper;

        public GetItemByIdHandler(MainDbContext context, ILogger<GetItemByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ItemDTO> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemByIdHandler called");

            var result = await _context.Item().AsNoTracking()
                .Include(item => item.ItemType)
                .Where(item => item.ID == request.ID)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_notFound,
                                                             new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            return _mapper.Map<ItemDTO>(result);
        }
    }

}
