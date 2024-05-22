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
    public class GetItemTypesByRoleIdsQuery: IRequest<Dictionary<int, List<ItemTypeWebDTO>>>
    {
        public IEnumerable<int> RoleIDs { get; set; }

        public GetItemTypesByRoleIdsQuery(IEnumerable<int> roleIDs)
        {
            RoleIDs = roleIDs;
        }
    }

    public class GetItemTypesByRoleIdsHandler: IRequestHandler<GetItemTypesByRoleIdsQuery, Dictionary<int, List<ItemTypeWebDTO>>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemTypesByRoleIdsHandler> _logger;
        private readonly IMapper _mapper;

        public GetItemTypesByRoleIdsHandler(MainDbContext context, ILogger<GetItemTypesByRoleIdsHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, List<ItemTypeWebDTO>>> Handle(GetItemTypesByRoleIdsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemTypesByRoleIdsHandler called");
            var itemTypesWithRoles = await _context.CTAMRole_ItemType().AsNoTracking()
                .Where(ri => request.RoleIDs.Contains(ri.CTAMRoleID))
                .Include(ri => ri.ItemType)
                .Select(ri => new { ri.CTAMRoleID, ri.ItemType })
                .ToListAsync();
            var itemtypesPerRoleId = itemTypesWithRoles
                .GroupBy(kv => kv.CTAMRoleID, kv => kv.ItemType)
                .ToDictionary(gr => gr.Key, gr => gr.ToList());
            return _mapper.Map<Dictionary<int, List<ItemTypeWebDTO>>>(itemtypesPerRoleId);
        }
    }

}
