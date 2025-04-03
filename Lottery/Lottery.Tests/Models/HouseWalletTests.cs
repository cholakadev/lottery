using FluentAssertions;
using Lottery.Core.Models;
using Xunit;

namespace Lottery.Tests.Models
{
    public class HouseWalletTests
    {
        [Fact]
        public void Revenue_ShouldBeZero_AtStartOfTest()
        {
            // Arrange
            ResetHouseWallet();

            // Assert
            HouseWallet.Revenue.Should().Be(0m);
        }

        [Fact]
        public void UpdateCasinoRevenue_ShouldIncreaseRevenue()
        {
            // Arrange
            ResetHouseWallet();
            var profit = 20m;

            // Act
            HouseWallet.UpdateCasinoRevenue(profit);

            // Assert
            HouseWallet.Revenue.Should().Be(profit);
        }

        [Fact]
        public void UpdateCasinoRevenue_ShouldAccumulate()
        {
            // Arrange
            ResetHouseWallet();

            // Act
            HouseWallet.UpdateCasinoRevenue(10m);
            HouseWallet.UpdateCasinoRevenue(5m);
            HouseWallet.UpdateCasinoRevenue(2m);

            // Assert
            HouseWallet.Revenue.Should().Be(17m);
        }

        private void ResetHouseWallet()
        {
            typeof(HouseWallet)
                .GetProperty(nameof(HouseWallet.Revenue))!
                .SetValue(null, 0m);
        }
    }
}
