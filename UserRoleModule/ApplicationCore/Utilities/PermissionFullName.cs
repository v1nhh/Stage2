using System;
using CTAM.Core.Enums;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Web;

namespace UserRoleModule.ApplicationCore.Utilities
{
    public static class PermissionFullName
    {
        public static string GetFullName(this PermissionDTO permission)
        {
            return GenerateFullname(permission.CTAMModule, permission.Description);
        }

        public static string GetFullName(this PermissionWebDTO permission)
        {
            return GenerateFullname(permission.CTAMModule, permission.Description);
        }

        private static string GenerateFullname(CTAMModule ctamModule, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("Description cannot be null or empty");
            }
            return (ctamModule.GetName() + "_" + description).ToUpper();
        }
    }
}
