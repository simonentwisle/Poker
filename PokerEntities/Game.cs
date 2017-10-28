using Poker.Entities;
using System.Collections.Generic;

namespace PokerEntities
{
    public class Game
    {
        private static int GameNumber { get; set; }
        private static IEnumerable<Player> Players { get; set; }
    }
}
