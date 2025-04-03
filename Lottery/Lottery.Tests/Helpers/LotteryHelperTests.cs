using FluentAssertions;
using Lottery.Core.Helpers;
using Lottery.Core.Settings;
using Xunit;

namespace Lottery.Tests.Helpers
{
    public class LotteryHelperTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(12)]
        [InlineData(14)]
        public void GenerateCpuPlayers_ShouldReturnCorrectNumberOfPlayers(int count)
        {
            // Arrange
            var helper = new LotteryHelper();
            var settings = new GameSettings();

            // Act
            var players = helper.GenerateCpuPlayers(count, settings);

            // Assert
            players.Should().NotBeNull();
            players.Count.Should().Be(count);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(16)]
        [InlineData(0)]
        public void GenerateCpuPlayers_ShouldThrow_WhenOutsideValidRange(int invalidCount)
        {
            // Arrange
            var helper = new LotteryHelper();
            var settings = new GameSettings();

            // Act
            Action act = () => helper.GenerateCpuPlayers(invalidCount, settings);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
