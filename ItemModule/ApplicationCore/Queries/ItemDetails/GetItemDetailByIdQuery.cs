using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using CTAM.Core;
using ItemModule.ApplicationCore.DTO;

namespace ItemModule.ApplicationCore.Queries.ItemDetails
{
    public class GetItemDetailByIdQuery : IRequest<ItemDetailDTO>
    {
        public int ID { get; set; }

        public GetItemDetailByIdQuery(int id)
        {
            ID = id;
        }
    }

    public class GetItemDetailByIdHandler : IRequestHandler<GetItemDetailByIdQuery, ItemDetailDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemDetailByIdQuery> _logger;
        private readonly IMapper _mapper;

        public GetItemDetailByIdHandler(MainDbContext context, ILogger<GetItemDetailByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ItemDetailDTO> Handle(GetItemDetailByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemDetailByIdHandler called");
            var result = await _context.ItemDetail().AsNoTracking()
                .Where(itemdetail => itemdetail.ID == request.ID)
                .FirstOrDefaultAsync();

            return _mapper.Map<ItemDetailDTO>(result);
        }
    }

}
