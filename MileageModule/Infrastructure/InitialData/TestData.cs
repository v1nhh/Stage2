using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MileageModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData: IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            // Wordt voorlopig niet geimplementeerd. veroorzaakt nu probleem met verwijderen van een user. Zie CAM-367

            //modelBuilder.Entity<Mileage>()
            //   .HasData(
            //        new Mileage() { ID = 1, ItemID = 1 }
            //    );

            //modelBuilder.Entity<MileageRegistration>()
            //   .HasData(
            //        new MileageRegistration() { ID = 1, MileageID = 1, CTAMUserUID = "gijs_123" }

            //    );
        }
    }
}
