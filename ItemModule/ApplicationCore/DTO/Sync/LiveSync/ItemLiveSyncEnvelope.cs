using System;
using System.Collections.Generic;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemModule.ApplicationCore.DTO.Sync
{
    [Obsolete]
    public class ItemLiveSyncEnvelope
    {
        
        //CTAMRole_ItemType
        public List<CTAMRole_ItemTypeBaseDTO> CTAMRole_ItemTypeData { get; set; }

        //ItemType
        public List<ItemTypeBaseDTO> ItemTypeData { get; set; }

    }
}
