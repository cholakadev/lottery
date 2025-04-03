using Lottery.Core.Models;
using Lottery.Core.Settings;

namespace Lottery.Core.Helpers
{
    public class LotteryHelper
    {
        public List<Player> GenerateCpuPlayers(int count, GameSettings settings)
        {
            if (count < settings.MinCpuPlayers || count > settings.MaxCpuPlayers)
                throw new ArgumentOutOfRangeException(nameof(count), "CPU players count must be between 10 and 14");

            var players = new List<Player>();
            var random = new Random();

            for (int i = 2; i < count + 2; i++)
            {
                var player = new Player(playerNumber: i, settings);

                var ticketsToPurchase = random.Next(1, settings.MaxTicketsPerPlayer);
                var tickets = Enumerable.Range(0, ticketsToPurchase).Select(_ => new Ticket(player, settings)).ToList();

                player.PurchaseTickets(tickets);
                players.Add(player);
            }

            return players;
        }
    }
}
