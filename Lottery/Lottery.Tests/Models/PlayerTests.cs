using FluentAssertions;
using Lottery.Core.Models;
using Lottery.Core.Settings;
using Xunit;

namespace Lottery.Tests.Models
{
    public class PlayerTests
    {
        [Fact]
        public void PurchaseTickets_ShouldOnlyPurchaseUpToBalanceLimit()
        {
            // Arrange
            var settings = new GameSettings();
            var player = new Player(playerNumber: 1, settings); // Player balance $10 by default
            int requestedTickets = 15; // Each ticket costs $1
            var tickets = GenerateTickets(player, requestedTickets, settings);

            // Act
            player.PurchaseTickets(tickets);

            // Assert
            player.Tickets.Count.Should().Be(10, because: "player should not purchase more tickets than their balance allows.");
            player.Balance.Should().Be(0, because: "player should have used all $10 to buy 10 tickets.");
        }

        private List<Ticket> GenerateTickets(Player owner, int count, GameSettings settings)
        {
            var tickets = new List<Ticket>();
            for (int i = 0; i < count; i++)
                tickets.Add(new Ticket(owner, settings));

            return tickets;
        }
    }
}
