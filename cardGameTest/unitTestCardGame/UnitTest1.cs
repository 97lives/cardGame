using cardGame;


using System;
using System.IO;
using Xunit;

namespace cardGame.Tests
{
    public class BackEndTests
    {
        [Theory]
        [InlineData("2C,5D,KH", 51)]
        [InlineData("2H,JD,4S", 44)]
        [InlineData("JR", 0)]
        [InlineData("JR,JR", 0)]
        [InlineData("2C,JR", 4)]
        [InlineData("JR,2C,JR", 8)]

        public void CalculateTotalScore_Returns(string input, int expectedScore)
        {
            // Arrange
            var backend = new BackEnd();

            // Act
            int actualScore = backend.CalculateTotalScore(input);

            // Assert
            Assert.Equal(expectedScore, actualScore);
        }

        [Theory]
        [InlineData("3X", typeof(InvalidSuitException))]
        [InlineData("2C,5L,KH", typeof(InvalidSuitException))]
        [InlineData("2C,5D,XH", typeof(InvalidCardRankException))]
        [InlineData("2C,2C", typeof(InvalidDuplicateException))]
        [InlineData("JR,JR,JR,JR", typeof(InvalidTriplicateJokerException))]
        public void CalculateTotalScore_InvalidInput_ThrowsException(string input, Type expectedExceptionType)
        {
            // Arrange
            var backend = new BackEnd();

            // Act & Assert
            Assert.Throws(expectedExceptionType, () => backend.CalculateTotalScore(input));
        }
    }
}
