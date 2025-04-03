namespace Lottery.Core.Models
{
    // Static class so it can preserve the state while application is running
    public static class HouseWallet
    {
        public static decimal Revenue { get; private set; }

        public static void UpdateCasinoRevenue(decimal profit) => Revenue += profit;
    }
}
