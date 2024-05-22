using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using ItemModule.ApplicationCore.Queries.Sync;
using CabinetModule.ApplicationCore.Queries.Sync;
using CloudAPI.ApplicationCore.DTO;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Queries.Sync;

namespace CloudAPI.ApplicationCore.Queries
{
    public class GetCriticalDataQuery : IRequest<CriticalDataEnvelope>
    {
        public GetCriticalDataQuery(string cabinetNumber)
        {
            this.CabinetNumber = cabinetNumber;
        }

        public string CabinetNumber { get; set; }
    }

    public class GetCriticalDataHandler : IRequestHandler<GetCriticalDataQuery, CriticalDataEnvelope>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetCriticalDataHandler> _logger;
        private readonly IMapper _mapper;
        private readonly MainDbContext _context;

        public GetCriticalDataHandler(IMediator mediator, MainDbContext context, ILogger<GetCriticalDataHandler> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            if (!_context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                _context.Database.SetCommandTimeout(180);
            }
        }

        public async Task<CriticalDataEnvelope> Handle(GetCriticalDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCriticalDataHandler called");
            var criticalDataEnvelope = new CriticalDataEnvelope();

            //Cabinet
            var cabinetCriticalDataQuery = new GetCabinetCriticalDataQuery(request.CabinetNumber);
            var cabinetCriticalData = await _mediator.Send(cabinetCriticalDataQuery);
            _mapper.Map(cabinetCriticalData, criticalDataEnvelope);

            //UserRole
            var userRoleCriticalDataQuery = new GetUserRoleCriticalDataForCabinetQuery(request.CabinetNumber);
            var userRoleCriticalData = await _mediator.Send(userRoleCriticalDataQuery);
            _mapper.Map(userRoleCriticalData, criticalDataEnvelope);

            //Item
            var roleIDs = userRoleCriticalData.CTAMRoleData.Select(role => role.ID).ToList();

            var itemCriticalDataQuery = new GetItemCriticalDataQuery(roleIDs);
            var itemCriticalData = await _mediator.Send(itemCriticalDataQuery);
            _mapper.Map(itemCriticalData, criticalDataEnvelope);

            //ItemCabinet
            var cabinetPositionIDs = await GetCabinetPositionIDsByCabinetNumber(request.CabinetNumber);
            var userUIDs = userRoleCriticalData.CTAMUserData.Select(user => user.UID).ToList();

            var itemCabinetCriticalDataQuery = new GetItemCabinetCriticalDataQuery(request.CabinetNumber, cabinetPositionIDs, userUIDs, itemCriticalData.ItemTypeData.Select(it => it.ID).ToList());
            var itemCabinetCriticalData = await _mediator.Send(itemCabinetCriticalDataQuery);
            _mapper.Map(itemCabinetCriticalData, criticalDataEnvelope);

            return criticalDataEnvelope;
        }

        private async Task<List<int>> GetCabinetPositionIDsByCabinetNumber(string cabinetNumber)
        {
            return await _context.CabinetPosition()
                .AsNoTracking()
                .Where(cp => cp.CabinetNumber.Equals(cabinetNumber))
                .Select(cp => cp.ID)
                .ToListAsync();
        }
    }

}
