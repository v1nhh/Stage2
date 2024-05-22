using System.Data.Common;

namespace CTAM.Core
{
    public class SqlConnectionContext
    {
        public string TenantID { get; set; }

        public DbConnection DbConnection { get; set; }
    }
}
