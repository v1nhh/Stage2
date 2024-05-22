using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemCabinetModule.ApplicationCore.DTO.Web;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetPositionDetailsQuery : IRequest<PositionDetailsEnvelope>
    {
        public string CabinetNumber { get; set; }
        public int PositionID { get; set; }
    }

    public class GetPositionDetailsHandler : IRequestHandler<GetPositionDetailsQuery, PositionDetailsEnvelope>
    {
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPositionDetailsHandler> _logger;

        public GetPositionDetailsHandler(MainDbContext context, IMediator mediator, IMapper mapper, ILogger<GetPositionDetailsHandler> logger)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PositionDetailsEnvelope> Handle(GetPositionDetailsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPositionDetailsHandler called");
            var cabinetPosition = await _context.CabinetPosition()
                .FirstOrDefaultAsync(cabPos =>
                    cabPos.CabinetNumber.Equals(request.CabinetNumber) &&
                    cabPos.ID == request.PositionID);
            if (cabinetPosition == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_cabinetPositionNotFound,
                                          new Dictionary<string, string> { { "positionID", request.PositionID.ToString() },
                                                                           { "cabinetNumber", request.CabinetNumber } });
            }
            var cabinetPositionContents = await _context.CabinetPositionContent()
                .AsNoTracking()
                .Include(cabPosContent => cabPosContent.Item)
                .ThenInclude(item => item.ItemType)
                .Where(cabPosContent => cabPosContent.CabinetPositionID == request.PositionID)
                .ToListAsync();
            var items = cabinetPositionContents.Select(cabPosContent => cabPosContent.Item);
            var result = new PositionDetailsEnvelope
            {
                Items = _mapper.Map<List<ItemWithItemTypeDTO>>(items)
            };
            return result;
        }
    }

}
