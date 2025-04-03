namespace Lottery.Core.Models
{
    public class Prize
    {
        public Prize(decimal amount, int winnersCount)
        {
            Amount = amount;
            WinnersCount = winnersCount;
        }

        public decimal Amount { get; private set; }

        public int WinnersCount { get; private set; }
    }
}
