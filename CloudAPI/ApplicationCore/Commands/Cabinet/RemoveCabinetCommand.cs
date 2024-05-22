using CabinetModule.ApplicationCore.DataManagers;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservationModule.ApplicationCore.DataManagers;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.Cabinet
{
    public class RemoveCabinetCommand: IRequest
    {
        public string CabinetNumber { get; set; }

        public RemoveCabinetCommand(string cabinetNumber)
        {
            CabinetNumber = cabinetNumber;
        }
    }

    public class RemoveCabinetHandler : IRequestHandler<RemoveCabinetCommand>
    {
        private readonly ILogger<RemoveCabinetHandler> _logger;
        private readonly IManagementLogger _managementLogger;
        private readonly MainDbContext _context;
        private readonly CabinetDataManager _cabinetDataManager;
        private readonly ReservationDataManager _reservationDataManager;

        public RemoveCabinetHandler(ILogger<RemoveCabinetHandler> logger, MainDbContext context, CabinetDataManager cabinetDataManager, ReservationDataManager reservationDataManager, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _cabinetDataManager = cabinetDataManager;
            _reservationDataManager = reservationDataManager;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(RemoveCabinetCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("RemoveCabinetHandler is called");
            var cabinet = await _context.Cabinet()
                .Include(c => c.CTAMRole_Cabinets)
                .Include(c => c.CabinetColumns)
                .Include(c => c.CabinetProperties)
                .Include(c => c.CabinetPositions)
                .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                .FirstOrDefaultAsync();
            if (cabinet == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                                                new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } });
            }

            if (cabinet.IsActive || (cabinet.Status != CabinetStatus.Offline && cabinet.Status != CabinetStatus.Initial && cabinet.Status != CabinetStatus.Online))
            { 
                await _managementLogger.LogError(nameof(CloudTranslations.cabinets_apiExceptions_CabinetActiveOrNotOfflineAndNotInitialAndNotOnline), ("cabinetNumber", request.CabinetNumber));
                throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_CabinetActiveOrNotOfflineAndNotInitialAndNotOnline,
                                                                 new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } });
            }

            var reservationItem = await _reservationDataManager.GetOpenReservationItemsByCabinetNumber(request.CabinetNumber).FirstOrDefaultAsync();
            if (reservationItem != null)
            {
                await _managementLogger.LogError(nameof(CloudTranslations.cabinets_apiExceptions_removeWithReservationsOpen), ("reservationItemDescription", reservationItem.Item.Description), ("reservationUserName", reservationItem.Reservation.CTAMUserName));
                throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_removeWithReservationsOpen,
                                                                 new Dictionary<string, string> { { "reservationItemDescription", reservationItem.Item.Description },
                                                                                                  { "reservationUserName", reservationItem.Reservation.CTAMUserName } });
            }

            _context.Cabinet().Remove(cabinet);

            var doors = await _context.CabinetDoor()
                .Where(cd => cd.CabinetNumber == request.CabinetNumber)
                .ToListAsync();
            if (doors.Any())
            {
                _context.CabinetDoor().RemoveRange(doors);
            }

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedCabinet), ("name", cabinet.Name));
            return new Unit();
        }
    }
}
