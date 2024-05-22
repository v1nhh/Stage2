using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using ItemModule.ApplicationCore.DTO.Web;
using CTAM.Core;

namespace ItemModule.ApplicationCore.Queries.ItemTypes
{
    public class GetItemTypesMaxQtyByRoleIdsQuery : IRequest<Dictionary<int, List<RoleItemTypeMaxQtyDTO>>>
    {
        public IEnumerable<int> RoleIDs { get; set; }

        public GetItemTypesMaxQtyByRoleIdsQuery(IEnumerable<int> roleIDs)
        {
            RoleIDs = roleIDs;
        }
    }

    public class GetItemTypesMaxQtyByRoleIdsHandler : IRequestHandler<GetItemTypesMaxQtyByRoleIdsQuery, Dictionary<int, List<RoleItemTypeMaxQtyDTO>>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemTypesMaxQtyByRoleIdsHandler> _logger;
        private readonly IMapper _mapper;

        public GetItemTypesMaxQtyByRoleIdsHandler(MainDbContext context, ILogger<GetItemTypesMaxQtyByRoleIdsHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, List<RoleItemTypeMaxQtyDTO>>> Handle(GetItemTypesMaxQtyByRoleIdsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemTypesMaxQtyByRoleIdsHandler called");
            var roleItemTypes = await _context.CTAMRole_ItemType().AsNoTracking()
                .Where(ri => request.RoleIDs.Contains(ri.CTAMRoleID))
                .Include(ri => ri.CTAMRole)
                .Include(ri => ri.ItemType)
                .ToListAsync();
            var roleItemTypesPerRoleId = roleItemTypes
                .GroupBy(kv => kv.CTAMRoleID, kv => kv)
                .ToDictionary(gr => gr.Key, gr => gr.ToList());
            return _mapper.Map<Dictionary<int, List<RoleItemTypeMaxQtyDTO>>>(roleItemTypesPerRoleId);
        }
    }

}
