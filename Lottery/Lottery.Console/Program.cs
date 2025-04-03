using Lottery;
using Lottery.Core.Contracts;
using Lottery.Core.Helpers;
using Lottery.Core.Models;
using Lottery.Core.Settings;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        var program = new Program();
        program.Run();
    }

    private void Run()
    {
        var settings = LoadGameSettings();

        var player = new Player(playerNumber: 1, settings!);

        bool continuePlaying = true;

        while (continuePlaying)
        {
            ValidatePlayerBalance(player, settings);

            Console.WriteLine("Welcome to the Bede Lottery, Player 1!");
            Console.WriteLine($"Your digital balance is ${settings.PlayerBalance}");
            Console.WriteLine($"Ticket Price: ${settings.TicketPrice} each");
            Console.WriteLine($"How many tickets do you want to buy, Player 1?");

            int ticketsToPurchase = int.Parse(Console.ReadLine()!);

            var tickets = GenerateTicketsForPlayer(player, ticketsToPurchase, settings);
            player.PurchaseTickets(tickets);

            Console.WriteLine($"{player.Name}, Balance: {player.Balance}, Tickets: {player.Tickets.Count}");

            var cpuPlayers = GenerateCpuPlayers(settings);
            Console.WriteLine($"{cpuPlayers.Count} other CPU players also have purchased tickets.");

            List<Player> allPlayers = [player, .. cpuPlayers];

            ILotteryManagerService lotteryManagerService = new LotteryManagerService(settings);
            lotteryManagerService.RegisterPlayerTickets(allPlayers);
            PrintRegisteredPlayerTickets(allPlayers);

            lotteryManagerService.DrawWinners();

            Console.WriteLine("Do you want to play another round? (y/n)");

            var input = Console.ReadLine()?.ToLower();
            continuePlaying = input == "y" || input == "yes";

            player.ClearUsedTickets();
        }

        Console.WriteLine("Thanks for playing the Bede Lottery!");
    }

    private List<Ticket> GenerateTicketsForPlayer(Player player, int count, GameSettings settings)
    {
        var tickets = new List<Ticket>();
        for (int i = 0; i < count; i++)
            tickets.Add(new Ticket(player, settings));

        return tickets;
    }

    private List<Player> GenerateCpuPlayers(GameSettings settings)
    {
        var lotteryHelper = new LotteryHelper();
        var random = new Random();

        return lotteryHelper.GenerateCpuPlayers(random.Next(10, 15), settings);
    }

    private void PrintRegisteredPlayerTickets(List<Player> players)
    {
        Console.WriteLine("Players and their Tickets:");

        foreach (var player in players)
        {
            var ticketIds = player.Tickets.Select(t => t.Id).ToList();
            var ticketList = ticketIds.Any() ? string.Join(", ", ticketIds.Select(x => $"#{x}")) : "No tickets";
            Console.WriteLine($"- {player.Name} (Balance: ${player.Balance:F2}) with Tickets: {ticketList}");
        }

        Console.WriteLine();
    }

    private GameSettings LoadGameSettings()
    {
        var json = File.ReadAllText("gameSettings.json");
        var settings = JsonSerializer.Deserialize<GameSettings>(json);

        if (settings is null)
        {
            Console.WriteLine("Failed to load game settings. Please check gameSettings.json.");
            throw new ArgumentNullException(nameof(settings), "Failed to load game settings.");
        }

        return settings!;
    }

    private void ValidatePlayerBalance(Player player, GameSettings settings)
    {
        if (player.Balance <= settings.TicketPrice)
        {
            Console.WriteLine($"You don't have enough balance to buy a ticket and participate, {player.Name}.");
            throw new InvalidOperationException("Not enough balance to participate in the game.");
        }
    }
}
