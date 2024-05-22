using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync
{
    public class ItemCabinetCriticalDataEnvelope
    {
        //CabinetStock   
        public List<CabinetStockBaseDTO> CabinetStockData { get; set; }

        //AllowedCabinetPosition
        public List<AllowedCabinetPositionBaseDTO> AllowedCabinetPositionData { get; set; }

        //CabinetPositionContent
        public List<CabinetPositionContentBaseDTO> CabinetPositionContentData { get; set; }

        //CTAMUserInPossession  
        public List<CTAMUserInPossessionBaseDTO> CTAMUserInPossessionData { get; set; }

        //CTAMUserPersonalItem  
        public List<CTAMUserPersonalItemBaseDTO> CTAMUserPersonalItemData { get; set; }

        //ItemToPick
        public List<ItemToPickBaseDTO> ItemToPickData { get; set; }
    }
}
