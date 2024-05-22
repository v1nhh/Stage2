using System.Collections.Generic;

namespace CTAM.Core.Interfaces
{
    public interface IImportCommand<DTO>
    {
        public List<DTO> InputList { get; set; }
        public string Filename { get; set; }
    }
}
