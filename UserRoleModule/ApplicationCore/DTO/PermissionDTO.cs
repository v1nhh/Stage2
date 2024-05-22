using CTAM.Core.Enums;
using UserRoleModule.ApplicationCore.Utilities;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class PermissionDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public CTAMModule CTAMModule { get; set; }

        public string FullName
        {
            get
            {
                return this.GetFullName();
            }
        }

    }
}
