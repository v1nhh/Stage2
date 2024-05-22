using System;
using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync
{
    [Obsolete]
    public class ItemCabinetLiveSyncEnvelope
    {
        //CabinetStock
        public List<CabinetStockBaseDTO> CabinetStockData { get; set; }

        //CTAMUserPersonalItem
        public List<CTAMUserPersonalItemBaseDTO> CTAMUserPersonalItemData { get; set; }

    }
}
