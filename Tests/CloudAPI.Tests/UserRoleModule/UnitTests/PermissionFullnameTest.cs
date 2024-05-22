using CTAM.Core.Enums;
using System;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Web;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.UnitTests
{
    public class PermissionFullnameTest
    {
        private const string PERMISSION_DESCRIPTION = (("testdescription"));

        [Fact]
        public void TestGetPermissionFullNamePermissionDTO()
        {
            // Arrange
            var permission = new PermissionDTO() { CTAMModule = CTAMModule.Cabinet, Description = PERMISSION_DESCRIPTION };

            // Act
            var fullname = permission.FullName;

            // Assert
            Assert.NotNull(fullname);
            Assert.Equal((CTAMModule.Cabinet.GetName() + "_" + PERMISSION_DESCRIPTION).ToUpper(), fullname);
        }

        [Fact]
        public void TestGetPermissionFullNamePermissionDTODescriptionNull()
        {
            // Arrange
            var permission = new PermissionDTO() { CTAMModule = CTAMModule.Cabinet, Description = null };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => permission.FullName);
        }

        [Fact]
        public void TestGetPermissionFullNamePermissionWebDTO()
        {
            // Arrange
            var permission = new PermissionWebDTO() { CTAMModule = CTAMModule.Management, Description = PERMISSION_DESCRIPTION };

            // Act
            var fullname = permission.FullName;

            // Assert
            Assert.NotNull(fullname);
            Assert.Equal((CTAMModule.Management.GetName() + "_" + PERMISSION_DESCRIPTION).ToUpper(), fullname);
        }
    }
}
