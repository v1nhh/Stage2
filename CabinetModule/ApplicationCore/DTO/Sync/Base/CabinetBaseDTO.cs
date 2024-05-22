using System;
using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetBaseDTO
    {
        public string CabinetNumber { get; set; }

        public string Name { get; set; }

        public CabinetType CabinetType { get; set; }

        public string Description { get; set; }

        public LoginMethod LoginMethod { get; set; }

        public DateTime CreateDT { get; set; }        

        public DateTime? UpdateDT { get; set; }

        public string LocationDescr { get; set; }

        public string CabinetConfiguration { get; set; }

        public string CabinetErrorMessage { get; set; }

        public bool HasSwipeCardAssign { get; set; }

        public string CabinetLanguage { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public DateTime? CabinetUIUpdateDT { get; set; }

        public CabinetStatus Status { get; set; }

    }
}
