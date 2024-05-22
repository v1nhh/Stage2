using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DataManagers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace ItemCabinetModule.ApplicationCore.Queries.Sync.LiveSync
{
    public class GetItemTypeIDsByItemIDsQuery : IRequest<Dictionary<int, int>>
    {
        public IEnumerable<int> ItemIDs
        {
            get; set;
        }
    }



    public class GetItemTypeIDsByItemIDsQueryHandler : IRequestHandler<GetItemTypeIDsByItemIDsQuery, Dictionary<int, int>>
    {
        private readonly ILogger<GetItemTypeIDsByItemIDsQueryHandler> _logger;
        private readonly MainDbContext _context;
        
        public GetItemTypeIDsByItemIDsQueryHandler(MainDbContext context, ILogger<GetItemTypeIDsByItemIDsQueryHandler> logger, ItemDataManager itemDataManager)
        {
            _logger = logger;
            _context = context;
        }

        private class ItemItemType
        {
            public int ID { get; set; }
            public int ItemTypeID { get; set; }
        }

        public async Task<Dictionary<int, int>> Handle(GetItemTypeIDsByItemIDsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemTypeIDsByItemIDsQueryHandler called");

            var itemItemTypes = new List<ItemItemType>();

            foreach (var chunk in request.ItemIDs.ToList().Partition(15000))
            {
                var itemIDItemTypeIDQuery =
                                    from i in _context.Item()
                                    where chunk.Contains(i.ID)
                                    
                                    select new ItemItemType { ID = i.ID, ItemTypeID = i.ItemTypeID };

                var res = await itemIDItemTypeIDQuery.AsNoTracking().ToListAsync();
                itemItemTypes.AddRange(res);
            }

            var groupedItemIDItemTypeIDs = itemItemTypes
                .Distinct()
                .ToDictionary(i => i.ID, i => i.ItemTypeID);

            return groupedItemIDItemTypeIDs;
        }
    }
}