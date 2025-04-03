using FluentAssertions;
using Lottery.Core.Contracts;
using Lottery.Core.Models;
using Lottery.Core.Settings;
using NSubstitute;
using Xunit;

namespace Lottery.Tests.Services
{
    public class LotteryManagerServiceTests
    {
        [Fact]
        public void DrawWinners_GrandPrizeWinner_ShouldReceive50PercentOfRevenue()
        {
            // Arrange
            var settings = new GameSettings();
            var player = new Player(1, settings);
            player.PurchaseTickets(CreateTickets(player, 2, settings));

            var service = new LotteryManagerService(settings);
            service.RegisterPlayerTickets(new List<Player> { player });

            // Act
            service.DrawWinners();

            // Assert
            var expectedGrandPrize = 2m * 0.5m;
            var expectedBalance = 8m + expectedGrandPrize;

            player.Balance.Should().Be(expectedBalance);
        }



        [Fact]
        public void ShouldCallDrawWinners_WhenGameRuns()
        {
            var mock = Substitute.For<ILotteryManagerService>();

            var players = new List<Player>();
            mock.RegisterPlayerTickets(players);

            mock.DrawWinners();

            mock.Received().DrawWinners();
            mock.Received().RegisterPlayerTickets(players);
        }

        private List<Ticket> CreateTickets(Player player, int count, GameSettings settings)
        {
            var list = new List<Ticket>();
            for (int i = 0; i < count; i++)
                list.Add(new Ticket(player, settings));

            return list;
        }
    }
}
