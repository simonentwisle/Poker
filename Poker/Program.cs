using System.Collections.Generic;
using PokerBL.Classes;
using PokerEntities;

namespace Poker
{
    class Program
    {
        private static List<Game> Games = new List<Game>();

        static void Main(string[] args)
        {
            Games = Initialise.ImportGames();
            GameFunctions.AndTheWinnerIs(Games);
        }
    }
}
