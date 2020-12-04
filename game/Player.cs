using System.Collections.Generic;

namespace game
{
    public class Player
    {
        private int _coins;
        public bool Active { get; set; } = true;

        public int Coins
        {
            get => _coins;
            set => _coins = value;
        }
        public int NumberOfMoves { get; set; }
        public int Turns { get; set; } = 0;
        public string Name { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}