using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ReservationModule.ApplicationCore.DataManagers
{
    public class ReservationDataManager
  {
    private readonly ILogger<ReservationDataManager> _logger;
    private readonly MainDbContext _context;

    public ReservationDataManager(MainDbContext context, ILogger<ReservationDataManager> logger)
    {
      _logger = logger;
      _context = context;
    }

    public IQueryable<Entities.ReservationItem> GetOpenReservationItemsByCabinetNumber(string cabinetNumber)
    {
       return _context.ReservationItem()
                  .Include(ri => ri.Reservation)
                  .Include(ri => ri.Item)
                  .Where(ri => ri.CabinetNumber.Equals(cabinetNumber) && (ri.Reservation.Status != Enums.ReservationStatus.Picked && ri.Reservation.Status != Enums.ReservationStatus.Cancelled))
                  .AsNoTracking();
    }
  }
}
