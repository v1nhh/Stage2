using AutoMapper;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetStockPaginatedQuery : IRequest<PaginatedResult<CabinetStock, CabinetStockDTO>>
    {
        public string CabinetNumber { get; private set; }
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.CabinetStockColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }
        public int? ItemTypeID { get; private set; }
        public int? CabinetStockStatus { get; private set; }

        public GetCabinetStockPaginatedQuery(string cabinetNumber, int pageLimit, int page, Enums.CabinetStockColumn? sortedBy, 
                                             bool sortDescending, string filterQuery, int? itemTypeID, int? cabinetStockStatus)
        {
            CabinetNumber = cabinetNumber;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
            ItemTypeID = itemTypeID;
            CabinetStockStatus = cabinetStockStatus;
        }
    }

    public class GetCabinetStockPaginatedHandler : IRequestHandler<GetCabinetStockPaginatedQuery, PaginatedResult<CabinetStock, CabinetStockDTO>>
    {
        private readonly ItemCabinetDataManager _dataManager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCabinetStockPaginatedHandler> _logger;

        public GetCabinetStockPaginatedHandler(IMapper mapper, ItemCabinetDataManager dataManager, ILogger<GetCabinetStockPaginatedHandler> logger)
        {
            _mapper = mapper;
            _dataManager = dataManager;
            _logger = logger;
        }

        public async Task<PaginatedResult<CabinetStock, CabinetStockDTO>> Handle(GetCabinetStockPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetStockPaginatedHandler called");

            var cabinetStock = _dataManager.GetCabinetStockByCabinetNumber(request.CabinetNumber, request.SortedBy, request.SortDescending, 
                                                                           request.FilterQuery, request.ItemTypeID, request.CabinetStockStatus);

            return await cabinetStock.Paginate<CabinetStockDTO>(request.PageLimit, request.Page, _mapper);
        }
    }
}
