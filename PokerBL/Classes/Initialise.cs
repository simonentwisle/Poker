﻿using Poker.Entities;
using PokerEntities;
using System.Collections.Generic;
using PokerBL;

namespace PokerBL.Classes
{
    public class Initialise
    {
        public static void ImportCardsToList()
        {
            int GameNumber = 0;
            //Create enumerable to hold games
            List<Game> Games = new List<Game>();
            string filePath = @"D:\VisualStudio2017\Poker\Poker\Poker\poker.txt";

            IEnumerable<string> lines = System.IO.File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                //Create two hands
                var hand1Cards = line.Substring(0, 14);
                var hand1 = new Hand(CardFunctions.GetListOfCards(hand1Cards));
                
                var hand2Cards = line.Substring(15);
                var hand2 = new Hand(CardFunctions.GetListOfCards(hand2Cards));
                
                GameNumber++;
            }
        }
    }
}
