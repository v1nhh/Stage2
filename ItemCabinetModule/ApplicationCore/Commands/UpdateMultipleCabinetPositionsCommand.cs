using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class UpdateMultipleCabinetPositionsCommand : IRequest<List<CabinetPositionDTO>>
    {
        public string CabinetNumber { get; set; }
        public HashSet<int> PositionIds { get; set; }
        public int? DoorId { get; set; }
        public int? PositionType { get; set; }
        public int? MaxNrOfItems { get; set; }
        public int? CabinetCellTypeId { get; set; }
        public CabinetPositionStatus? Status { get; set; }
    }

    public class UpdateMultipleCabinetPositionsHandler : IRequestHandler<UpdateMultipleCabinetPositionsCommand, List<CabinetPositionDTO>>
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;


        public UpdateMultipleCabinetPositionsHandler(MainDbContext context, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<List<CabinetPositionDTO>> Handle(UpdateMultipleCabinetPositionsCommand request, CancellationToken cancellationToken)
        {
            var positions = await _context.CabinetPosition()
                .Where(cabPos => request.PositionIds.Contains(cabPos.ID))
                .ToListAsync();
            foreach (var position in positions)
            {
                var anyItemInPosition = await _context.CabinetPositionContent().AsNoTracking().AnyAsync(cpc => cpc.CabinetPositionID == position.ID);

                if (request.DoorId != null)
                {
                    if (request.DoorId == 0)
                    {
                        position.CabinetDoorID = null;
                    }
                    else
                    {
                        position.CabinetDoorID = position.PositionType == PositionType.Keycop ? request.DoorId : null;
                    }
                }
                if (request.PositionType != null)
                {
                    if (anyItemInPosition)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInMultipleCabinetPosition);
                    }
                    position.PositionType = (PositionType)request.PositionType;
                }
                if (request.MaxNrOfItems != null)
                {
                    position.MaxNrOfItems = (int)request.MaxNrOfItems;
                }
                if (request.CabinetCellTypeId != null)
                {
                    if (anyItemInPosition)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_itemExistsInMultipleCabinetPosition);
                    }
                    position.CabinetCellTypeID = (int)request.CabinetCellTypeId;
                }
                if (request.Status != null)
                {
                    position.Status = (CabinetPositionStatus)request.Status;
                }

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedCabinetPosition),
                    ("positionNumber", position.PositionNumber.ToString()),
                    ("cabinetNumber", request.CabinetNumber));
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<List<CabinetPositionDTO>>(positions);
        }
    }

}
