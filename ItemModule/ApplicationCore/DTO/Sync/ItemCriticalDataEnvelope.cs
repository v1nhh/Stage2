using System.Collections.Generic;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemModule.ApplicationCore.DTO.Sync
{
    public class ItemCriticalDataEnvelope
    {
        //Item
        public List<ItemBaseDTO> ItemData { get; set; }

        //ItemSet        	
        public List<ItemSetBaseDTO> ItemSetData { get; set; }

        //ItemType
        public List<ItemTypeBaseDTO> ItemTypeData { get; set; }

        //ErrorCode
        public List<ErrorCodeBaseDTO> ErrorCodeData { get; set; }

        //CTAMRole_ItemType
        public List<CTAMRole_ItemTypeBaseDTO> CTAMRole_ItemTypeData { get; set; }

        //ItemType_ErrorCode
        public List<ItemType_ErrorCodeBaseDTO> ItemType_ErrorCodeData { get; set; }
    }
}
