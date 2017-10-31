using Poker.Entities;
using PokerEntities;
using System.Collections.Generic;
using PokerBL;

namespace PokerBL.Classes
{
    public class Initialise
    {
        public static List<Game> ImportGames()
        {
            int GameNumber = 1;
            //Create enumerable to hold games
            List<Game> Games = new List<Game>();
            List<Hand> ThePairOfHandsWeWant = new List<Hand>();

            string filePath = @"E:\gitrepos\Poker\Poker\poker.txt";

            IEnumerable<string> lines = System.IO.File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var game = new Game();
                game.GameNumber = GameNumber;
                //Create two hands
                var hand1Cards = line.Substring(0, 14);
                var hand1 = new Hand(CardFunctions.GetListOfCards(hand1Cards));
                hand1.PlayersName = "Player1";
                game.Hands.Add(hand1);

                var hand2Cards = line.Substring(15);
                var hand2 = new Hand(CardFunctions.GetListOfCards(hand2Cards));
                hand2.PlayersName = "Player2";
                game.Hands.Add(hand2);
                Games.Add(game);
                GameNumber++;
            }
            return Games;
        }
    }
}
