using Lottery.Core.Settings;

namespace Lottery.Core.Models
{
    public class Ticket
    {
        private static int _latestId = 1;

        public Ticket(Player player, GameSettings settings)
        {
            Id = _latestId++;
            Player = player;
            Price = settings.TicketPrice;
        }

        public int Id { get; }

        public decimal Price { get; set; }

        public Player Player { get; }
    }
}
