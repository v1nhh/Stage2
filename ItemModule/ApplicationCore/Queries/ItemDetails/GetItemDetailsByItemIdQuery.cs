using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using CTAM.Core;
using ItemModule.ApplicationCore.DTO;

namespace ItemModule.ApplicationCore.Queries.ItemDetails
{
    public class GetItemDetailsByItemIdQuery : IRequest<List<ItemDetailDTO>>
    {
        public int ItemID { get; set; }

        public GetItemDetailsByItemIdQuery(int itemId)
        {
            ItemID = itemId;
        }
    }

    public class GetItemDetailsByItemIdHandler : IRequestHandler<GetItemDetailsByItemIdQuery, List<ItemDetailDTO>>
    {
        private MainDbContext _context;
        private IMapper _mapper;

        public GetItemDetailsByItemIdHandler(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemDetailDTO>> Handle(GetItemDetailsByItemIdQuery request, CancellationToken cancellationToken)
        {
            var itemdetails = await _context.ItemDetail().AsNoTracking()
                .Where (itemdetail => itemdetail.ItemID == request.ItemID)
                .ToListAsync();
            return _mapper.Map<List<ItemDetailDTO>>(itemdetails.OrderBy(i => i.Description));
        }
    }

}
