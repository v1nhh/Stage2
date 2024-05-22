using CTAM.Core.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.DTO.Web
{
    public class PermissionWebDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public CTAMModule CTAMModule { get; set; }

        public bool IsChecked { get; set; }

        public string FullName
        {
            get
            {
                return this.GetFullName();
            }
        }
    }
}
