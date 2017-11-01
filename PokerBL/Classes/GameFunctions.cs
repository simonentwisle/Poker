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
            //Empty Results File
            File.WriteAllText(@"../../results.txt", string.Empty);
            foreach (Game game in games)
            {
                game.GameNumber = gameNumber;

                List<Hand> theHands = game.Hands;
                
                foreach (var hand in theHands)
                {
                    if (hand.PlayersName == "Player1") { player1sHand = hand; }
                    if (hand.PlayersName == "Player2") { player2sHand = hand; }
                }

                //Check for pairs
                //if (player1sHand.HasAPair == true || player2sHand.HasAPair == true)
                //{
                //    game.Winner = WhoHasTheHighestPairs(player1sHand, player2sHand);
                //    WriteResults(game);
                //    continue;
                //}
                if (player1sHand.Points.Equals(player2sHand.Points))
                {
                    //Full House check for four of a kind
                    if ((player1sHand.Points == 7) && (player2sHand.Points == 7))
                    {
                        if (player1sHand.HighestThreeOfAKind > player2sHand.HighestThreeOfAKind)
                            game.Winner = "Player1";
                        if (player1sHand.HighestThreeOfAKind < player2sHand.HighestThreeOfAKind)
                            game.Winner = "Player2";
                        WriteResults(game);
                        gameNumber++;
                        continue;
                    }

                    //Check for four of a kind
                    if ((player1sHand.Points == 8) && (player2sHand.Points == 8))
                    {
                        if (player1sHand.HighestFourOfAKind > player2sHand.HighestFourOfAKind)
                            game.Winner = "Player1";
                        if (player1sHand.HighestFourOfAKind < player2sHand.HighestFourOfAKind)
                            game.Winner = "Player2";
                        WriteResults(game);
                        gameNumber++;
                        continue;
                    }

                    //Check for three of a kind
                    if ((player1sHand.Points == 4) && (player2sHand.Points == 4 ))
                    {
                        if (player1sHand.HighestThreeOfAKind > player2sHand.HighestThreeOfAKind)
                            game.Winner = "Player1";
                        if (player1sHand.HighestThreeOfAKind < player2sHand.HighestThreeOfAKind)
                            game.Winner = "Player2";
                        WriteResults(game);
                        gameNumber++;
                        continue;
                    }

                    //Check for pairs
                    if ((player1sHand.Points <= 3 && player1sHand.Points > 1)|| (player2sHand.Points <= 3 && player2sHand.Points > 1))
                    {
                        game.Winner = WhoHasTheHighestPairs(player1sHand, player2sHand);
                        WriteResults(game);
                        gameNumber++;
                        continue;
                    }
                       
                    //game.Winner = AddNextHighestCard();
                }

                if (player1sHand.Points > player2sHand.Points)
                {
                    game.Winner = "Player1";
                    if (player1sHand.Points <= 3)
                        game.Winner = WhoHasTheHighestPairs(player1sHand, player2sHand);
                    WriteResults(game);
                    gameNumber++;
                    continue;
                }
                    
                if (player2sHand.Points > player1sHand.Points)
                {
                    game.Winner = "Player2";
                    if (player2sHand.Points <= 3)
                        game.Winner = WhoHasTheHighestPairs(player1sHand, player2sHand);
                    WriteResults(game);
                    gameNumber++;
                    continue;
                }

                game.Winner = AddNextHighestCard();
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

        internal string WhoHasTheHighestPairs(Hand player1sHand, Hand player2sHand)
        {
            int player1PairsTotal = player1sHand.HighestPair + player1sHand.SecondHighestPair;
            int player2PairsTotal = player2sHand.HighestPair + player2sHand.SecondHighestPair;
            if (player1PairsTotal > player2PairsTotal)
                return "Player1";
            if (player2PairsTotal > player1PairsTotal)
                return "Player2";
            return AddNextHighestCard(); 
        }

        internal void WriteResults(Game game)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("../../results.txt", FileMode.Append, FileAccess.Write);
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
                    Console.Write("----");
                    if (hand.HasAFlush == true)
                        Console.Write("Flush  ");
                    if (hand.HasAPair == true)
                        Console.Write("Pair  ");
                    if (hand.HasFourOfAKind == true)
                        Console.Write("Four Of A Kind  ");
                    if (hand.HasFullHouse == true)
                        Console.Write("Full House  ");
                    if (hand.HasRoyalFlush == true)
                        Console.Write("Royal Flush  ");
                    if (hand.HasStraight == true)
                        Console.Write("Straight  ");
                    if (hand.HasStraightFlush == true)
                        Console.Write("StraightFlush  ");
                    if (hand.HasThreeOfAKind == true)
                        Console.Write("Three Of A Kind  ");
                    if (hand.HasTwoPairs == true)
                        Console.Write("Two Pairs  ");
                        Console.Write(" Highest Card = " + hand.HighestCard.ToString());
                        Console.Write(" Highest Pair = " + hand.HighestPair.ToString());
                        Console.Write(" Second Highest Pair = " + hand.SecondHighestPair.ToString());
                        Console.Write(" Points = " + hand.Points);
                    if (game.Winner == "Player1")
                        Console.Write(" **WINNER**");
                }
                
                if (hand.PlayersName == "Player2")
                {
                    Console.Write("Player2Cards: ");
                    var player2Cards = hand.Cards;
                    foreach (var card in player2Cards)
                    {
                        Console.Write(card.CardValue + card.Suit + " ");
                    }
                    Console.Write("----");
                    if (hand.HasAFlush == true)
                        Console.Write("Flush  ");
                    if (hand.HasAPair == true)
                        Console.Write("Pair  ");
                    if (hand.HasFourOfAKind == true)
                        Console.Write("Four Of A Kind  ");
                    if (hand.HasFullHouse == true)
                        Console.Write("Full House  ");
                    if (hand.HasRoyalFlush == true)
                        Console.Write("Royal Flush  ");
                    if (hand.HasStraight == true)
                        Console.Write("Straight  ");
                    if (hand.HasStraightFlush == true)
                        Console.Write("StraightFlush  ");
                    if (hand.HasThreeOfAKind == true)
                        Console.Write("Three Of A Kind  ");
                    if (hand.HasTwoPairs == true)
                        Console.Write("Two Pairs  ");
                    Console.Write("Highest Card = " + hand.HighestCard.ToString());
                    Console.Write(" Highest Pair = " + hand.HighestPair.ToString());
                    Console.Write(" Second Highest Pair = " + hand.SecondHighestPair.ToString());
                    Console.Write(" Points = " + hand.Points);
                    if (game.Winner == "Player2")
                        Console.Write(" **WINNER**");
                }
                Console.WriteLine("");
            }

            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();

        }

        internal string AddNextHighestCard(){

            string winner = "";
            for (int i = 0; i < 5; i++)
			{
                player1sHand.Points = player1sHand.Points + player1sHand.Cards.OrderByDescending(c => c.CardValue).ElementAt(i).CardValue;
                player2sHand.Points = player2sHand.Points + player2sHand.Cards.OrderByDescending(c => c.CardValue).ElementAt(i).CardValue;
                if (player1sHand.Points > player2sHand.Points)
                {
                    winner = "Player1";
                    break;
                }
                if (player2sHand.Points > player1sHand.Points)
                { 
                    winner = "Player2";
                    break;
                }
            }
            return winner;
        }
    }
}
