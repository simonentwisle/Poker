using Poker.Entities;
using System.Collections.Generic;

namespace PokerEntities
{
    public class Game
    {
        private static int GameNumber { get; set; }
        public static string Winner { get; set; }
        private static IEnumerable<Hand> Hands { get; set; }
    }
}
