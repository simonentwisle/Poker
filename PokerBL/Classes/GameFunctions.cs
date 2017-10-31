using Poker.Entities;
using Poker.Enums;
using System;
using System.Collections.Generic;
using PokerBL.Classes;
using PokerEntities;
using System.Linq;

namespace PokerBL.Classes
{
    public class GameFunctions
    {
        private Hand player1sHand = new Hand();
        private Hand player2sHand = new Hand();
        private int Player1Total = 0;
        private int Player2Total = 0;

        public string AndTheWinnerIs(List<Game> games)
        {   
            foreach (Game game in games)
            {
                List<Hand> theHands = game.Hands;
                
                foreach (var hand in theHands)
                {
                    if (hand.PlayersName == "Player1") { player1sHand = hand; }
                    if (hand.PlayersName == "Player2") { player2sHand = hand; }
                }

                if (player1sHand.Points > player2sHand.Points)
                    game.Winner = "Player1";
                if (player2sHand.Points > player1sHand.Points)
                    game.Winner = "Player2";

                if (player1sHand.Points.Equals(player2sHand.Points))
                {
                    game.Winner = AddNextHighestCard();
                }
            }

            Player1Total = games.Where(w => w.Winner.Equals("Player1")).Count();
            Player2Total = games.Where(w => w.Winner.Equals("Player2")).Count();
            //Calculate player totals and return the winner
            if (Player1Total > Player2Total)
                return "Player1";
            return "Player2";
        }

        internal string AddNextHighestCard(){

            for (int i = 0; i < 5; i++)
			{
                player1sHand.Points = player1sHand.Points + player1sHand.Cards.OrderByDescending(c => c.CardValue).ElementAt(i).CardValue;
                player2sHand.Points = player2sHand.Points + player2sHand.Cards.OrderByDescending(c => c.CardValue).ElementAt(i).CardValue;
                if (player1sHand.Points > player2sHand.Points)
	                return "Player1";
			}
            return "Player2";
        }
    }
}
