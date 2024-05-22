using System.Collections.Generic;
using CabinetModule.ApplicationCore.DTO.Web;

namespace ItemCabinetModule.ApplicationCore.DTO.Web
{
    public class CabinetPositionsDetailsEnvelope
    {
        public CabinetPositionWithSpecCodeDTO CabinetPosition { get; set; }
        public List<int> Items { get; set; }
    }
}
