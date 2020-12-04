using System;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bankrupt Game!");
            if (args.Length < 1)
            {
                Console.WriteLine("Please send parameters when running the program");
                Environment.Exit(0);
            }

            Console.WriteLine("Enter the number of players");
            var board = new Board(args[0],
                int.Parse(Console.ReadLine() ?? throw new Exception("Number of players can't null")), 
                int.Parse(args[1]));
            board.SetupPlayers();
            while (!board.Finish)
            {
                if (board.ActualPlayer.Active)
                {
                    Console.WriteLine($"It's the {board.ActualPlayer}'s turn");
                    Console.WriteLine($"The die was rolled and the player moved {board.PlayDice()} squares");
                    if (board.Property.HasOwner())
                    {
                        if (board.Property.Owner != board.ActualPlayer)
                            if (board.Property.PayRent(board.ActualPlayer))
                                Console.WriteLine(
                                    $"{board.ActualPlayer} pay {board.Property.Owner} {board.Property.RentValue} coins");
                            else
                                Console.WriteLine(
                                    $"{board.ActualPlayer} left the game, because he/she no have more coins");
                    }
                    else
                    {
                        Console.WriteLine($"{board.ActualPlayer} fell on an unowned property");
                        if (board.ActualPlayer.Coins >= board.Property.Price)
                        {
                            Console.WriteLine(
                                $"Do you wish buy the property? Cost: {board.Property.Price} coins, " +
                                $"Rent Value: {board.Property.RentValue} " +
                                $"You have {board.ActualPlayer.Coins} coins. y/n");
                            var answer = Console.ReadLine() == "y";
                            if (answer)
                            {
                                board.Property.BuyProperty(board.ActualPlayer);
                                Console.WriteLine($"{board.ActualPlayer} buy property");
                            }
                        }
                        else
                            Console.WriteLine($"But {board.ActualPlayer} does no have money to buy");
                    }
                }

                if (board.Turns >= 10000 || board.HasWinner())
                    board.Finish = true;
                board.PlayCount++;
            }

            var winner = board.Winner.Name ?? "nobody";
            Console.Write($"Winner: {winner} Rounds: {board.Turns}");
        }
    }
}
