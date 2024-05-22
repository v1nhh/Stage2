using System;

namespace ItemModule.ApplicationCore.DTO.Sync.Base
{
    public class ErrorCodeBaseDTO
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
