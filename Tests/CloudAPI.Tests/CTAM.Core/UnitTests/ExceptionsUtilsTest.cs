using System;
using Xunit;
using CTAM.Core.Utilities;

namespace CloudAPI.Tests.CTAM.Core.UnitTests
{
    public class ExceptionsUtilsTest
    {
        [Fact]
        public void TestGetMostInnerException()
        {
            // Arrange
            Exception inner = new Exception("Most inner exception");
            Exception middle = new Exception("Middle exception", inner);
            Exception outer = new Exception("Most outer exception", middle);

            // Act
            var result = outer.GetMostInnerException();

            // Assert
            Assert.Equal(inner, result);
        }

        [Fact]
        public void TestGetMostInnerExceptionSingleException()
        {
            // Arrange
            Exception inner = new Exception("Most inner exception");

            // Act
            var result = inner.GetMostInnerException();

            // Assert
            Assert.Equal(inner, result);
        }

    }
}
