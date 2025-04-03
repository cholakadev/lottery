namespace Lottery.Core.Settings
{
    public class GameSettings
    {
        public decimal TicketPrice { get; set; } = 1m;

        public decimal PlayerBalance { get; set; } = 10m;

        public int MaxTicketsPerPlayer { get; set; } = 10;

        public int MinCpuPlayers { get; set; } = 10;

        public int MaxCpuPlayers { get; set; } = 14;

        public int GrandPrizePercentageOfRevenue { get; set; } = 50;

        public int SecondTierPrizePercentageOfRevenue { get; set; } = 30;

        public int ThirdTierPrizePercentageOfRevenue { get; set; } = 10;

        public int SecondTierPrizeWinnersPercentage { get; set; } = 10;

        public int ThirdTierPrizeWinnersPercentage { get; set; } = 20;
    }
}
