using ItemModule.ApplicationCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MileageModule.ApplicationCore.Entities
{
    public class Mileage
    {
        [Key]
        public int ID { get; set; }

        public int ItemID { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }

        public DateTime CreateDT { get; set; }

        public string LicensePlate { get; set; }

        public int CurrentMileage { get; set; }

        public int MaxDeltaMileage { get; set; }

        public int ServiceMileage { get; set; }

        public string UoM { get; set; }

        [JsonIgnore]
        public ICollection<MileageRegistration> MileageRegistrations { get; set; }
    }
}
