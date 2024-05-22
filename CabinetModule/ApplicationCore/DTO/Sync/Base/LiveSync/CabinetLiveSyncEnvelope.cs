using System;
using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Sync.Base;

namespace CabinetModule.ApplicationCore.DTO.Sync
{
    [Obsolete]
    public class CabinetLiveSyncEnvelope
    {

        //CabinetAccessIntervals  
        public List<CabinetAccessIntervalsBaseDTO> CabinetAccessIntervalsData { get; set; }

        //CTAMRole_Cabinet
        public List<CTAMRole_CabinetBaseDTO> CTAMRole_CabinetData { get; set; }

    }
}
