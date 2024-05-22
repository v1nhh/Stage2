using System;

namespace ItemModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMRole_ItemTypeBaseDTO
    {
        public int CTAMRoleID { get; set; }

        public int ItemTypeID { get; set; }

        public DateTime CreateDT { get; set; }

        public int MaxQtyToPick { get; set; }
    }
}
