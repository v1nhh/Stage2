using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItemCabinetModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class CoreData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Core;

        public void InsertData(ModelBuilder modelBuilder)
        {
            
        }
    }
}
