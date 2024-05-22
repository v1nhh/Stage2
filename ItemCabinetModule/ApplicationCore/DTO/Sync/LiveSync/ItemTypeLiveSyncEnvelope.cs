using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;
using System.Collections.Generic;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class ItemTypeLiveSyncEnvelope
    {
        public ItemTypeBaseDTO ItemType { get; set; }

        public CabinetStockBaseDTO CabinetStock { get; set; }

        public List<ItemBaseDTO> Items { get; set; }

        public List<ErrorCodeBaseDTO> ErrorCodes { get; set; }

        public List<ItemType_ErrorCodeBaseDTO> ItemTypeErrorCodes { get; set; }

        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; }

        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; }

        public List<CTAMRole_ItemTypeBaseDTO> RoleItemTypes { get; set; }
    }
}
