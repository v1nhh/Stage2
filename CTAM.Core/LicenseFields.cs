using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTAM.Core
{
    public class LicenseFields
    {
        public string Tenant { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public int MaxUsers { get; set; }
        public int MaxItems { get; set; }
        public int MaxIBKs { get; set; }
        public bool ModuleBorrowReturn { get; set; }
        public bool ModuleDatadumpImport { get; set; }
        public bool ModuleSwapSwapbackReplace { get; set; }
        public DateTime StartLicense { get; set; }
        public DateTime EndLicense { get; set; }
        public bool Keyconductor { get; set; }
        public bool Locker { get; set; }
    }
}
