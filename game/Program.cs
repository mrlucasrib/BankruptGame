using System;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            
            var board = new Board("/home/lucas/Documentos/gameConfig.txt",
                int.Parse(Console.ReadLine() ?? throw new Exception("Number of players can't null")));
            board.SetupPlayers();
            while (!board.Finish)
            {
                if (board.ActualPlayer.Active)
                {
                    Console.WriteLine($"It's the {board.ActualPlayer}'s turn");
                    Console.WriteLine($"The die was rolled and the player moved {board.playDice()} squares");
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
                            Console.WriteLine($"You wish buy the property? Cost: {board.Property.Price} coins y/n");
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

                if (board.PlayCount >= 10000 || board.hasWinner())
                    board.Finish = true;
                board.PlayCount++;
            }

            Console.WriteLine($"Acabou com {board.PlayCount} jogadas e o ganhador {board.Winner.Name}");
        }
    }
}
