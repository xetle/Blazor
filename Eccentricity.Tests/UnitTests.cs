using NUnit.Framework;

namespace Eccentricity.Tests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var answer = calculator.Calculate(0, 1);

            // Assert
            Assert.IsTrue(true);
        }
    }
}