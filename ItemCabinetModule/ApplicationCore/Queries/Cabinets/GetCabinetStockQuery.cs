using AutoMapper;
using ItemCabinetModule.ApplicationCore.DataManagers;
using ItemCabinetModule.ApplicationCore.DTO.CabinetStock;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetStockQuery : IRequest<CabinetStockDTO>
    {
        #region Primary Key
        public string CabinetNumber { get; set; }
        public int ItemTypeID { get; set; }
        #endregion
    }

    public class GetCabinetStockHandler : IRequestHandler<GetCabinetStockQuery, CabinetStockDTO>
    {
        private ILogger<GetCabinetStockHandler> _logger;
        private readonly ItemCabinetDataManager _dataManager;
        private readonly IMapper _mapper;

        public GetCabinetStockHandler(ILogger<GetCabinetStockHandler> logger, ItemCabinetDataManager dataManager, IMapper mapper)
        {
            _logger = logger;
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<CabinetStockDTO> Handle(GetCabinetStockQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetStockHandler called");

            var cabinetStock = _dataManager.GetCabinetStockByCabinetNumberAndItemTypeID(query.CabinetNumber, query.ItemTypeID);
            return _mapper.Map(
                        await cabinetStock.FirstOrDefaultAsync(),
                        new CabinetStockDTO()
                    );
        }

    }
}
