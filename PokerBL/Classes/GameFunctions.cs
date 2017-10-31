using Poker.Entities;
using Poker.Enums;
using System;
using System.Collections.Generic;
using PokerBL.Classes;
using PokerEntities;
using System.Linq;
using System.IO;

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
            int gameNumber = 1;
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

                game.GameNumber = gameNumber;

                WriteResults(game);
                gameNumber++;
            }

            Player1Total = games.Where(w => w.Winner.Equals("Player1")).Count();
            Player2Total = games.Where(w => w.Winner.Equals("Player2")).Count();
            //Calculate player totals and return the winner
            if (Player1Total > Player2Total)
                return "Player1";
            return "Player2";
        }

        internal void WriteResults(Game game)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);

            Console.WriteLine("Game " + game.GameNumber);
            
            foreach (var hand in game.Hands)
            {
                if (hand.PlayersName == "Player1")
                {
                    Console.Write("Player1Cards: ");
                    var player1Cards = hand.Cards;
                    foreach (var card in player1Cards)
                    {
                        Console.Write(card.CardValue + card.Suit + " ");
                    }
                }

                if (hand.PlayersName == "Player2")
                {
                    Console.Write("Player2Cards: ");
                    var player2Cards = hand.Cards;
                    foreach (var card in player2Cards)
                    {
                        Console.Write(card.CardValue + card.Suit + " ");
                    }
                }
                Console.WriteLine("");
            }

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();

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
