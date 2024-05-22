using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using ItemModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class ItemLiveSyncEnvelope
    {
        public ItemBaseDTO Item { get; set; }

        public CTAMUserInPossessionBaseDTO UserInPossession { get; set; }

        public CTAMUserPersonalItemBaseDTO UserPersonalItem { get; set; }
    }
}
