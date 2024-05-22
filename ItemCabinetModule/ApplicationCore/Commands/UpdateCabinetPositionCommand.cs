using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class UpdateCabinetPositionCommand : IRequest<CabinetPositionDTO>
    {
        public string CabinetNumber { get; set; }
        public int CabinetPositionId { get; set; }
        public int? CabinetDoorId { get; set; }
        public int? MaxNrOfItems { get; set; }
        public int? PositionType { get; set; }
        public string PositionAlias { get; set; }
        public int? CabinetCellTypeId { get; set; }
        public CabinetPositionStatus? Status { get; set; }
        public int? BladeNo { get; set; }
        public int? BladePosNo { get; set; }
    }

    public class UpdateCabinetPositionHandler : IRequestHandler<UpdateCabinetPositionCommand, CabinetPositionDTO>
    {
        private readonly MainDbContext _context;
        private ILogger<UpdateCabinetPositionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public UpdateCabinetPositionHandler(MainDbContext context, ILogger<UpdateCabinetPositionHandler> logger, IMapper mapper, 
                                            IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetPositionDTO> Handle(UpdateCabinetPositionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateCabinetPositionHandler called");
            var cabinetPosition = await _context.CabinetPosition()
                .Where(cabPos => cabPos.CabinetNumber.Equals(request.CabinetNumber) && cabPos.ID == request.CabinetPositionId)
                .FirstOrDefaultAsync();

            var anyItemInCabinetPosition = await _context.CabinetPositionContent().AsNoTracking().AnyAsync(cpc => cpc.CabinetPositionID == cabinetPosition.ID);

            if (cabinetPosition == null)
            {
                var m = $"Could not find cabinet positions with id {request.CabinetPositionId} for cabinet {request.CabinetNumber}";
                _logger.LogError(m);
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_cabinetPositionNotFound,
                                                                 new Dictionary<string, string> { { "positionID", request.CabinetPositionId.ToString() },
                                                                                                  { "cabinetNumber", request.CabinetNumber } });
            }

            if (request.CabinetDoorId != null)
            {
                if (request.CabinetDoorId == 0)
                {
                    cabinetPosition.CabinetDoorID = null;
                }
                else
                {
                    cabinetPosition.CabinetDoorID = request.CabinetDoorId;
                }
            }
            if (request.MaxNrOfItems != null)
            {
                cabinetPosition.MaxNrOfItems = (int)request.MaxNrOfItems;
            }
            if (request.PositionType != null)
            {
                if ((PositionType)request.PositionType != cabinetPosition.PositionType && anyItemInCabinetPosition)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInSingleCabinetPosition);
                }
                cabinetPosition.PositionType = (PositionType)request.PositionType;
            }
            if (request.PositionAlias != null)
            {
                if(await _context.CabinetPosition().AnyAsync(cp => cp.CabinetNumber.Equals(request.CabinetNumber) && cp.ID != cabinetPosition.ID && cp.PositionAlias.Equals(request.PositionAlias)))
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_duplicatePositionAlias,
                        new Dictionary<string, string> { { "positionAlias", request.PositionAlias } });
                }
                cabinetPosition.PositionAlias = request.PositionAlias;
            }
            if (request.CabinetCellTypeId != null)
            {
                if(request.CabinetCellTypeId != cabinetPosition.CabinetCellTypeID && anyItemInCabinetPosition)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInSingleCabinetPosition);
                }
                cabinetPosition.CabinetCellTypeID = (int)request.CabinetCellTypeId;
            }
            if (request.Status != null)
            {
                cabinetPosition.Status = (CabinetPositionStatus)request.Status;
            }
            if (request.BladeNo != null && request.BladePosNo != null)
            {
                if (await CombinationExistsAsync(request.CabinetNumber, (int)request.BladeNo, (int)request.BladePosNo, cabinetPosition.ID))
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_duplicateBladeNoAndPosNo,
                                              new Dictionary<string, string> { { "bladeNo", request.BladeNo.ToString() },
                                                                               { "bladePosNo", request.BladePosNo.ToString() } });
                }
            }
            if (request.BladeNo != null)
            {
                if (request.BladeNo != cabinetPosition.BladeNo && anyItemInCabinetPosition)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInSingleCabinetPosition);
                }
                cabinetPosition.BladeNo = (int)request.BladeNo;
            }
            if (request.BladePosNo != null)
            {
                if (request.BladePosNo != cabinetPosition.BladePosNo && anyItemInCabinetPosition)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInSingleCabinetPosition);
                }
                cabinetPosition.BladePosNo = (int)request.BladePosNo;
            }
            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedCabinetPosition), 
                    ("positionNumber", cabinetPosition.PositionNumber.ToString()),
                    ("cabinetNumber", request.CabinetNumber));

            return _mapper.Map<CabinetPositionDTO>(cabinetPosition);
        }
        public async Task<bool> CombinationExistsAsync(string cabinetNumber, int bladeNo, int bladePosNo, int cabinetPositionID)
        {
            var existingCombination = await _context.CabinetPosition()
                .AnyAsync(cp => cp.CabinetNumber.Equals(cabinetNumber) && cp.ID != cabinetPositionID &&
                                cp.BladeNo.Equals(bladeNo) && 
                                cp.BladePosNo.Equals(bladePosNo));

            return existingCombination;
        }
    }
}
