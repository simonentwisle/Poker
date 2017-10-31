using Poker.Entities;
using System.Collections.Generic;

namespace PokerEntities
{
    public class Game
    {
        public int GameNumber { get; set; }
        public string Winner { get; set; }
        public List<Hand> Hands { get; set; }

        public Game()
        {
            Winner = "";
            Hands = new List<Hand>();
        }
    }
}
