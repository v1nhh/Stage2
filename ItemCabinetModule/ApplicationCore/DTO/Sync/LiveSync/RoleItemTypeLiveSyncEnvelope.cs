using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class RoleItemTypeLiveSyncEnvelope
    {
        public CTAMRole_ItemTypeBaseDTO RoleItemType { get; set; }

        public ItemTypeBaseDTO ItemType { get; set; }

        public CabinetStockBaseDTO CabinetStock { get; set; }

        public List<ItemBaseDTO> Items { get; set; }

        public List<ErrorCodeBaseDTO> ErrorCodes { get; set; }

        public List<ItemType_ErrorCodeBaseDTO> ItemTypeErrorCodes { get; set; }

        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; }

        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; }
    }
}
