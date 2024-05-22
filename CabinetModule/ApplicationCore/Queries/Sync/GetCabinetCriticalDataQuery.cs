using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CabinetModule.ApplicationCore.DTO.Sync;
using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.ApplicationCore.DTO.Sync.Base;

namespace CabinetModule.ApplicationCore.Queries.Sync
{
    public class GetCabinetCriticalDataQuery : IRequest<CabinetCriticalDataEnvelope>
    {
        public GetCabinetCriticalDataQuery(string cabinetNumber)
        {
            this.CabinetNumber = cabinetNumber;
        }

        public string CabinetNumber { get; set; }
    }

    public class GetCabinetCriticalDataHandler : IRequestHandler<GetCabinetCriticalDataQuery, CabinetCriticalDataEnvelope>
    {
        private readonly ILogger<GetCabinetCriticalDataHandler> _logger;
        private readonly CabinetDataManager _cabinetManager;
        private readonly IMapper _mapper;        

        public GetCabinetCriticalDataHandler(CabinetDataManager cabinetManager, IMapper mapper, ILogger<GetCabinetCriticalDataHandler> logger)
        {
            _cabinetManager = cabinetManager;
            _logger = logger;            
            _mapper = mapper;
        }

        public async Task<CabinetCriticalDataEnvelope> Handle(GetCabinetCriticalDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetCriticalDataHandler called");
            CabinetCriticalDataEnvelope env = new CabinetCriticalDataEnvelope();

            //Cabinet
            env.CabinetData = _mapper.Map(
                    await _cabinetManager.GetCabinetByCabinetNumber(request.CabinetNumber).FirstOrDefaultAsync(),
                    new CabinetBaseDTO()
                );

            //CabinetUI
            env.CabinetUIData = _mapper.Map(
                    await _cabinetManager.GetCabinetUIByCabinetNumber(request.CabinetNumber).ToListAsync(),
                    new List<CabinetUIBaseDTO>()
                );

            //CabinetColumn   
            var cabinetColumnData = await _cabinetManager.GetCabinetColumnsByCabinetNumber(request.CabinetNumber).ToListAsync();
            env.CabinetColumnData = _mapper.Map(
                    cabinetColumnData,
                    new List<CabinetColumnBaseDTO>()
                );

            //CabinetPosition
            var cabinetPositionData = await _cabinetManager.GetCabinetPositionsByCabinetNumber(request.CabinetNumber).ToListAsync();
            env.CabinetPositionData = _mapper.Map(
                    cabinetPositionData,
                    new List<CabinetPositionBaseDTO>()
                );

            //CTAMRole_Cabinet
            var cTAMRole_CabinetData = await _cabinetManager.GetCTAMRole_CabinetsByCabinetNumber(request.CabinetNumber).ToListAsync();
            env.CTAMRole_CabinetData = _mapper.Map(
                    cTAMRole_CabinetData,
                    new List<CTAMRole_CabinetBaseDTO>()
                );

            //CabinetCell
            var cabinetColumnNumbers = cabinetColumnData.Select(ccd => ccd.ColumnNumber);
            var cabinetCellData = await _cabinetManager.GetCabinetCellsByCabinetColumnIDs(cabinetColumnNumbers).ToListAsync();
            env.CabinetCellData = _mapper.Map(
                    cabinetCellData,
                    new List<CabinetCellBaseDTO>()
                );

            //CabinetCellType
            env.CabinetCellTypeData = _mapper.Map(
                    await _cabinetManager.GetAllCabinetCellTypes().ToListAsync(),
                    new List<CabinetCellTypeBaseDTO>()
                );

            //CabinetDoor
            env.CabinetDoorData = _mapper.Map(
                    await _cabinetManager.GetCabinetDoors(request.CabinetNumber).ToListAsync(),
                    new List<CabinetDoorBaseDTO>()
                );

            //CabinetAccessIntervals
            var cTAMRoleIDs = cTAMRole_CabinetData.Select(cr => cr.CTAMRoleID).Distinct();
            env.CabinetAccessIntervalsData = _mapper.Map(
                    await _cabinetManager.GetCabinetAccessIntervalsByCTAMRoleIDs(cTAMRoleIDs).ToListAsync(),
                    new List<CabinetAccessIntervalsBaseDTO>()
                );

            return env;
        }
    }

}
