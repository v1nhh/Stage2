using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ReservationModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            // Wordt voorlopig niet geimplementeerd. veroorzaakt nu probleem met verwijderen van een user. Zie CAM-367

            //modelBuilder.Entity<Reservation>()
            //   .HasData(
            //        new Reservation() { ID = 1, CTAMUserUID = "lieuwe_123", QRCode = "QR1234567890", CreateDT = DateTime.UtcNow, StartDT = DateTime.UtcNow.AddDays(2), EndDT = DateTime.UtcNow.AddDays(3), IsAdhoc = false, IsDirty = false, ReservationType = ReservationType.Reservation, NoteForUser = "Goed op de spullen passen.", Status = ReservationStatus.Created }
            //    );

            //modelBuilder.Entity<ReservationItem>()
            //   .HasData(
            //        new ReservationItem() { ReservationID = 1, ItemID = 15, CabinetNumber = "210309081254" }

            //    );
        }
    }
}
