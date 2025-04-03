namespace Lottery.Core.Models
{
    public class PrizeInfo
    {
        public Prize GrandPrize { get; set; }

        public Prize SecondTierPrize { get; set; }

        public Prize ThirdTierPrize { get; set; }
    }
}
