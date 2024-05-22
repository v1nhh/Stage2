using UserRoleModule.ApplicationCore.Utilities;
using Xunit;

namespace CloudAPI.Tests.UserRoleModule.UnitTests
{
    public class ValidateEmailTest
    {

        [Theory]
        [InlineData("ed@nautaconnect.com")]
        [InlineData("ed.@nautaconnect.com")]
        public void emailAdressIsValid(string email)
        {
            Assert.True(ValidateEmail.IsValidEmail(email));
        }

        [Theory]
        [InlineData(@"@.@nautaconnect.com")]
        [InlineData(@"@nautaconnect.")]
        [InlineData(@"ed.@nauta connect.")]
        [InlineData(@"ed.nautaconnect.com")]
        public void emailAdressIsNotValid(string email)
        {
            Assert.False(ValidateEmail.IsValidEmail(email));
        }

    }
}
