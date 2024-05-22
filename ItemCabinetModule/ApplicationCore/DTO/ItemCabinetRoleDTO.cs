using System;
using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.DTO.Web;
using ItemModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.DTO.Web;

namespace ItemCabinetModule.ApplicationCore.DTO
{
    public class ItemCabinetRoleDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime? ValidFromDT { get; set; }

        public DateTime? ValidUntilDT { get; set; }

        public ICollection<PermissionWebDTO> Permissions { get; set; }

        public ICollection<ItemTypeWebDTO> ItemTypes { get; set; }

        public ICollection<CabinetWebDTO> Cabinets { get; set; }

        public ICollection<CabinetAccessIntervalDTO> CabinetAccessIntervals { get; set; }
    }
}
