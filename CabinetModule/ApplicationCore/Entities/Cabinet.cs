using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.Entities
{
    public class Cabinet
    {
        public string CabinetNumber { get; set; }

        [Required]
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

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CabinetUIUpdateDT { get; set; }

        public CabinetStatus Status { get; set; } = CabinetStatus.Initial;

        public DateTime? LastSyncTimeStamp { get; set; }


        [JsonIgnore]
        public ICollection<CTAMRole_Cabinet> CTAMRole_Cabinets { get; set; }

        public ICollection<CabinetColumn> CabinetColumns { get; set; }

        public ICollection<CabinetPosition> CabinetPositions { get; set; }

        public CabinetProperties CabinetProperties { get; set; }

    }
}
