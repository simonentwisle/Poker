using Poker.Entities;
using Poker.Enums;
using System;
using System.Collections.Generic;
using PokerBL.Classes;
using PokerEntities;
using System.Linq;

namespace PokerBL.Classes
{
    public static class GameFunctions
    {
        public static string AndTheWinnerIs(List<Game> games)
        {
            
            foreach (Game game in games)
            {
                List<Hand> theHands = game.Hands;
                Hand player1sHand = new Hand();
                Hand player2sHand = new Hand();
                foreach (var hand in theHands)
                {
                    if (hand.PlayersName == "Player1") { player1sHand = hand; }
                    if (hand.PlayersName == "Player2") { player2sHand = hand; }
                }

                if (player1sHand.Points > player2sHand.Points)
                    game.Winner = "Player1";
                else
                    game.Winner = "Player2";

                if (player1sHand.Points.Equals(player2sHand.Points))
                {
                    while (player1sHand.Points.Equals(player2sHand.Points))
                    {
                        //AddNextHighestCard
                    }
                }
            }
            return "";
        }

        private static string DoesEitherPlayerHaveARoyalFlush(Hand player1, Hand player2)
        {
            if (player1.HasRoyalFlush)
                return "Player1";
            if (player2.HasRoyalFlush)
                return "Player2";
            return "";
        }

        private static string DoesEitherPlayerHaveAStraightFlush(Hand player1, Hand player2)
        {
            if (player1.HasStraightFlush)
                return "Player1";
            if (player2.HasStraightFlush)
                return "Player2";
            return "";
        }
    }
}
