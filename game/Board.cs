using System;
using System.Collections.Generic;

namespace game
{
   public class Board
    {
        private readonly List<Property> _properties = new List<Property>();
        /// <summary>
        /// Takes the current property based on the current player
        /// </summary>
        public Property Property => _properties[this.ActualPlayer.NumberOfMoves % this._boardSize];
        private int NumberOfPlayers { get; }
        private readonly List<Player> _players = new List<Player>();
        private int _boardSize;
        private readonly Random _rand;
        public int PlayCount { get; set; }
        public bool Finish { get; set; }
        /// <summary>
        /// Takes the current player.
        ///
        /// The rest of the division will always give a number from one to the maximum number of players.
        /// </summary>
        public Player ActualPlayer => _players[this.PlayCount % this.NumberOfPlayers];

        public Board(string configFile, int numberOfPlayers, int seed)
        {
            NumberOfPlayers = numberOfPlayers;
            this._rand = new Random(seed);
            SetupProperties(configFile);
            PlayCount = 0;
            Finish = false;
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
                Console.WriteLine("Type player's name");
                var name = Console.ReadLine();
                this._players.Add(new Player {Coins = 300, Name = name});

            }
            this.Shuffle(this._players);
        }
        public int PlayDice()
        {
            var dice = this._rand.Next(1,6);
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

        public bool HasWinner()
        {
            var count = 0;
            foreach (var player in _players)
            {
                if (player.Active)
                    count++;
            }

            return count == 1;
        }

        public Player Winner => _players.Find(x => x.Active.Equals(true));
        
        /// <summary>
        /// Shuffle a list, by: https://stackoverflow.com/questions/273313/randomize-a-listt
        /// </summary>
        private void Shuffle<T>(IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = _rand.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}