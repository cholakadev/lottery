using Lottery.Core.Settings;

namespace Lottery.Core.Models
{
    public class Player
    {
        private readonly string _namePrefix = "Player";
        private readonly int _maxAllowedTickets = 10;

        public Player() { }

        public Player(int playerNumber, GameSettings settings)
        {
            Name = $"{_namePrefix} {playerNumber}";
            Balance = settings.PlayerBalance;
            _maxAllowedTickets = settings.MaxTicketsPerPlayer;
        }

        public string Name { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public List<Ticket> Tickets { get; } = new();

        public void PurchaseTickets(List<Ticket> tickets)
        {
            // Iterates through tickets and modifies the user balance and tickets count after every purchase.
            // Breaks the loop if the user tickets count reaches the max allowed tickets per user or if the user does not have balance to purchase more tickets.
            foreach (var ticket in tickets)
            {
                var hasEnoughBalance = Balance - ticket.Price >= 0;
                if (Tickets.Count == _maxAllowedTickets || !hasEnoughBalance)
                    break;

                Balance -= ticket.Price;
                Tickets.Add(ticket);
            }
        }

        public void ClearUsedTickets() => Tickets.Clear();
    }
}
