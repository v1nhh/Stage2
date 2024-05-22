using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.DTO;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetItemsInPossessionByCTAMUserQuery : IRequest<List<UserInPossessionDTO>>
    {
        public string UID { get; set; }
        public bool IncludeHistory { get; set; } = false;

        public GetItemsInPossessionByCTAMUserQuery(string uid, bool includeHistory)
        {
            this.UID = uid;
            this.IncludeHistory = includeHistory;
        }
    }

    public class GetItemsInPossessionByCTAMUserHandler : IRequestHandler<GetItemsInPossessionByCTAMUserQuery, List<UserInPossessionDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetItemsInPossessionByCTAMUserHandler> _logger; 
        private IMapper _mapper;

        public GetItemsInPossessionByCTAMUserHandler(MainDbContext context, ILogger<GetItemsInPossessionByCTAMUserHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserInPossessionDTO>> Handle(GetItemsInPossessionByCTAMUserQuery request, CancellationToken cancellationToken)
        {
            var itemsInPossessionAdded = await _context.CTAMUserInPossession().AsNoTracking()
                .Include(uip => uip.Item)
                .Where(uip => uip.CTAMUserUIDIn == request.UID && uip.Status == UserInPossessionStatus.Added)
                .Select(uip => new { ItemID = uip.ItemID, CreateDT = uip.CreatedDT})
                .ToListAsync();

            var itemsInPossession = await _context.CTAMUserInPossession().AsNoTracking()
                    .Include(itemsInPossession => itemsInPossession.Item).ThenInclude(item => item.ItemType)
                    .Where(uip => uip.CTAMUserUIDOut == request.UID && (uip.Status == UserInPossessionStatus.Picked
                                || uip.Status == UserInPossessionStatus.Overdue
                                || uip.Status == UserInPossessionStatus.Removed
                                || uip.Status == UserInPossessionStatus.Unjustified
                                || request.IncludeHistory))
                    .ToListAsync();

            // Remove the item in possession records that are added again after a remove
            itemsInPossession.RemoveAll(x => x.Status == UserInPossessionStatus.Removed && (itemsInPossessionAdded.Exists(i => i.ItemID.Equals(x.ItemID) && i.CreateDT.CompareTo(x.CreatedDT) > 0)));

            return _mapper.Map<List<UserInPossessionDTO>>(itemsInPossession.OrderByDescending(i => i.OutDT).ToList());
        }
    }

}
