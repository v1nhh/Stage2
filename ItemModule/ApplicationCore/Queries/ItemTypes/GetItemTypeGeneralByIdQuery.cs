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

namespace ItemModule.ApplicationCore.Queries.ItemTypes
{
    public class GetItemTypeGeneralByIdQuery : IRequest<ItemTypeDTO>
    {
        public int ItemTypeID { get; set; }

        public GetItemTypeGeneralByIdQuery(int itemType_id)
        {
            ItemTypeID = itemType_id;
        }
    }

    public class GetItemTypeGeneralByIdHandler : IRequestHandler<GetItemTypeGeneralByIdQuery, ItemTypeDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemTypeGeneralByIdQuery> _logger;
        private readonly IMapper _mapper;

        public GetItemTypeGeneralByIdHandler(MainDbContext context, ILogger<GetItemTypeGeneralByIdQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ItemTypeDTO> Handle(GetItemTypeGeneralByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemTypeGeneralByIdHandler called");
            var result = await _context.ItemType().AsNoTracking()
                .Where(itemtype => itemtype.ID == request.ItemTypeID)
                .SingleOrDefaultAsync();

            if (result == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.itemTypes_apiExceptions_notFound,
                                                             new Dictionary<string, string> { { "id", request.ItemTypeID.ToString() } });
            }
            return _mapper.Map<ItemTypeDTO>(result);
        }
    }

}
