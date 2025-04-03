using Lottery.Core.Models;
using Lottery.Core.Settings;

namespace Lottery.Core.Helpers
{
    public class LotteryHelper
    {
        // TODO: Extract the ticket purchasing into a separate method?
        public List<Player> GenerateCpuPlayers(int count, GameSettings settings)
        {
            if (count < 10 || count > 14)
                throw new ArgumentOutOfRangeException(nameof(count), "CPU players count must be between 10 and 14");

            var players = new List<Player>();
            var random = new Random();

            for (int i = 2; i < count + 2; i++)
            {
                var player = new Player(playerNumber: i, settings);

                var ticketsToPurchase = random.Next(1, 11);
                var tickets = Enumerable.Range(0, ticketsToPurchase).Select(_ => new Ticket(player, settings)).ToList();

                player.PurchaseTickets(tickets);
                players.Add(player);
            }

            return players;
        }
    }
}
