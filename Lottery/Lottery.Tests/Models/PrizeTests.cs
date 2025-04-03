using FluentAssertions;
using Lottery.Core.Models;
using Xunit;

namespace Lottery.Tests.Models
{
    public class PrizeTests
    {
        [Fact]
        public void Prize_ShouldInitializeWithCorrectValues()
        {
            // Arrange
            var expectedAmount = 100m;
            var expectedWinnersCount = 3;

            // Act
            var prize = new Prize(expectedAmount, expectedWinnersCount);

            // Assert
            prize.Amount.Should().Be(expectedAmount);
            prize.WinnersCount.Should().Be(expectedWinnersCount);
        }
    }
}
