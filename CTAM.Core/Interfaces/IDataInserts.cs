using Microsoft.EntityFrameworkCore;

namespace CTAM.Core.Interfaces
{
    public interface IDataInserts
    {
        public string Environment { get; }

        public void InsertData(ModelBuilder modelBuilder);
    }
}
