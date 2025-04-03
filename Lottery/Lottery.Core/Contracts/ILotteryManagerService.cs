using Lottery.Core.Models;

namespace Lottery.Core.Contracts
{
    public interface ILotteryManagerService
    {
        void RegisterPlayerTickets(List<Player> players);

        void DrawWinners();
    }
}
