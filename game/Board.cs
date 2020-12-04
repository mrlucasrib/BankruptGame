using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace game
{
    public class Board
    {
        private List<Property> _properties = new List<Property>();
        public Property Property => _properties[this.ActualPlayer.NumberOfMoves % this._boardSize];
        private int NumberOfPlayers { get; }
        private List<Player> _players = new List<Player>();
        private int _boardSize;
        private static int seed = 251415474;
        private Random rand = new Random(seed);
        public int PlayCount { get; set; } = 0;
        public bool Finish { get; set; } = false;
        public Player ActualPlayer => _players[this.PlayCount % this.NumberOfPlayers];

        public Board(string configFile, int numberOfPlayers)
        {
            NumberOfPlayers = numberOfPlayers;
            SetupProperties(configFile);
        }

        private void SetupProperties(string fileName)
        {
            try
            {
                
                var lines = System.IO.File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    var data = line.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var price = int.Parse(data[0]);
                    var rentValue = int.Parse(data[1]);
                    _properties.Add(new Property(price, rentValue));
                }

                this._boardSize = lines.Length;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Environment.Exit(1);
            }
        }

        public void SetupPlayers()
        {
            for (int i = 0; i < this.NumberOfPlayers; ++i)
            {
                Console.WriteLine("Type name");
                var name = Console.ReadLine();
                this._players.Add(new Player {Coins = 300, Name = name});

            }
        }
        public int playDice()
        {
            int dice = this.rand.Next(1,6);
            ActualPlayer.NumberOfMoves += dice;
            // !(Dice > boardSize)
            if (ActualPlayer.NumberOfMoves / this._boardSize != ActualPlayer.Turns)
            {
                ActualPlayer.Turns++;
                Console.WriteLine($"{ActualPlayer} won 100 coins");
                ActualPlayer.Coins += 100;
            }
            return dice;

        }

        public bool hasWinner()
        {
            int count = 0;
            foreach (var player in _players)
            {
                if (player.Active)
                    count++;
            }

            return count == 1;
        }

        public Player Winner => _players.Find(x => x.Active.Equals(true));
    }
}