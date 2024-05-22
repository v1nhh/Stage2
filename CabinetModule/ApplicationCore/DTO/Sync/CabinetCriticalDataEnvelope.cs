using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Sync.Base;

namespace CabinetModule.ApplicationCore.DTO.Sync
{
    public class CabinetCriticalDataEnvelope
    {
        //Cabinet
        public CabinetBaseDTO CabinetData { get; set; }

        //CabinetCell				
        public List<CabinetCellBaseDTO> CabinetCellData { get; set; }

        //CabinetCellType
        public List<CabinetCellTypeBaseDTO> CabinetCellTypeData { get; set; }

        //CabinetDoor				
        public List<CabinetDoorBaseDTO> CabinetDoorData { get; set; }

        //CabinetUI
        public List<CabinetUIBaseDTO> CabinetUIData { get; set; }

        //CabinetColumn           
        public List<CabinetColumnBaseDTO> CabinetColumnData { get; set; }

        //CabinetPosition         
        public List<CabinetPositionBaseDTO> CabinetPositionData { get; set; }

        //CabinetAccessIntervals  
        public List<CabinetAccessIntervalsBaseDTO> CabinetAccessIntervalsData { get; set; }

        //CTAMRole_Cabinet
        public List<CTAMRole_CabinetBaseDTO> CTAMRole_CabinetData { get; set; }

        //These are not part of critical data:
        //CabinetLog
        //CabinetAction           
    }
}
