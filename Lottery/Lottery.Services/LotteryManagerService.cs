using Lottery.Core.Contracts;
using Lottery.Core.Models;
using Lottery.Core.Settings;

namespace Lottery
{
    public class LotteryManagerService : ILotteryManagerService
    {
        private readonly Random _random = new();
        private List<Ticket> _lotteryTickets = new();
        private GameSettings _gameSettings;

        public LotteryManagerService(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void RegisterPlayerTickets(List<Player> players)
            => _lotteryTickets = players.SelectMany(x => x.Tickets).ToList();

        public void DrawWinners()
        {
            var lotteryTicketsTotalAmount = _lotteryTickets.Sum(x => x.Price);
            var prizeDistribution = CalculatePrizeDistribution(lotteryTicketsTotalAmount);

            var remainingTickets = new List<Ticket>(_lotteryTickets);
            var winningPlayers = new HashSet<Player>();

            var grandPrizeWinner = HandleGrandPrize(remainingTickets, prizeDistribution.GrandPrize.Amount, winningPlayers);

            var secondTierWinners = HandleTierPrize(
                "Second Tier",
                remainingTickets,
                prizeDistribution.SecondTierPrize.Amount,
                prizeDistribution.SecondTierPrize.WinnersCount,
                winningPlayers);

            var thirdTierWinners = HandleTierPrize(
                "Third Tier",
                remainingTickets,
                prizeDistribution.ThirdTierPrize.Amount,
                prizeDistribution.ThirdTierPrize.WinnersCount,
                winningPlayers);

            // Holds the total distributed amount to all winners
            var distributedAmount = prizeDistribution.GrandPrize.Amount +
                      prizeDistribution.SecondTierPrize.Amount +
                      prizeDistribution.ThirdTierPrize.Amount;

            var houseProfit = lotteryTicketsTotalAmount - distributedAmount;
            HouseWallet.UpdateCasinoRevenue(houseProfit);
            Console.WriteLine($"House Profit: ${houseProfit:F2} and total casino revenue: {HouseWallet.Revenue:F2}");
        }

        private Ticket HandleGrandPrize(List<Ticket> remainingTickets, decimal prizeAmount, HashSet<Player> winners)
        {
            var ticket = PickRandomTicket(remainingTickets);
            ticket.Player.Balance += prizeAmount;
            winners.Add(ticket.Player);

            Console.WriteLine($"Grand Prize: {ticket.Player.Name} (Ticket #{ticket.Id}) wins ${prizeAmount:F2}");
            remainingTickets.RemoveAll(t => t.Player == ticket.Player);

            return ticket;
        }

        private List<Ticket> HandleTierPrize(string tierName, List<Ticket> remainingTickets, decimal totalTierPrize, int numberOfWinners, HashSet<Player> winners)
        {
            var tierWinners = new List<Ticket>();

            var filteredTickets = remainingTickets
                .Where(t => !winners.Contains(t.Player))
                .ToList();

            var selected = PickRandomTickets(filteredTickets, numberOfWinners);
            var prizePerWinner = selected.Any() ? totalTierPrize / selected.Count : 0;

            Console.WriteLine($"{tierName} Winners (${prizePerWinner:F2} each):");
            foreach (var ticket in selected)
            {
                ticket.Player.Balance += prizePerWinner;
                Console.WriteLine($"- {ticket.Player.Name} (Ticket #{ticket.Id})");
                winners.Add(ticket.Player);
                remainingTickets.RemoveAll(t => t.Player == ticket.Player);
                tierWinners.Add(ticket);
            }

            return tierWinners;
        }

        private PrizeInfo CalculatePrizeDistribution(decimal lotteryTicketsTotalAmount)
        {
            var secondTierWinnersCount = (int)Math.Round(_lotteryTickets.Count * (_gameSettings.SecondTierPrizeWinnersPercentage / 100m));
            var thirdTierWinnersCount = (int)Math.Round(_lotteryTickets.Count * (_gameSettings.ThirdTierPrizeWinnersPercentage / 100m));

            var grandPrizeAmount = lotteryTicketsTotalAmount * (_gameSettings.GrandPrizePercentageOfRevenue / 100m);
            var grandPrize = new Prize(grandPrizeAmount, winnersCount: 1);

            var secondPrizeAmount = lotteryTicketsTotalAmount * (_gameSettings.SecondTierPrizePercentageOfRevenue / 100m);
            var secondTierPrize = new Prize(secondPrizeAmount, winnersCount: secondTierWinnersCount);

            var thirdPrizeAmount = lotteryTicketsTotalAmount * (_gameSettings.ThirdTierPrizePercentageOfRevenue / 100m);
            var thirdTierPrize = new Prize(thirdPrizeAmount, winnersCount: thirdTierWinnersCount);

            var prizeInfo = new PrizeInfo();
            prizeInfo.GrandPrize = grandPrize;
            prizeInfo.SecondTierPrize = secondTierPrize;
            prizeInfo.ThirdTierPrize = thirdTierPrize;

            return prizeInfo;
        }

        private Ticket PickRandomTicket(List<Ticket> tickets)
        {
            int index = _random.Next(tickets.Count);
            var ticket = tickets[index];
            tickets.RemoveAt(index);

            return ticket;
        }

        private List<Ticket> PickRandomTickets(List<Ticket> tickets, int count)
        {
            var selectedTickets = new List<Ticket>();
            for (int i = 0; i < count && tickets.Any(); i++)
            {
                selectedTickets.Add(PickRandomTicket(tickets));
            }

            return selectedTickets;
        }
    }
}
