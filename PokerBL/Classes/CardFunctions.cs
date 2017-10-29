using Poker.Entities;
using Poker.Enums;
using System;
using System.Collections.Generic;
using PokerBL.Classes;

namespace PokerBL
{
    public static class CardFunctions
    {
        public static List<Card> GetListOfCards(string hand)
        {
            List<Card> theCardsWeWant = new List<Card>();
            string[] arrayOfCards = hand.Split(new string[] { " " }, StringSplitOptions.None);
            foreach (string card in arrayOfCards)
            {
                var cardValue = GetCardValue(card);
                var cardSuit = GetCardSuit(card);
                var theCardWeWant = new Card()
                {
                    CardValue = cardValue,
                    Suit = cardSuit
                };
                theCardsWeWant.Add(theCardWeWant);
            }
            return theCardsWeWant;
         }

        private static int GetCardValue(string card)
        {
            char cardValueAsChar = Convert.ToChar(card.Trim().Substring(0, 1));
            string cardValueAsString = card.Trim().Substring(0, 1);
            int cardValue = 0;
            if (!Char.IsNumber(cardValueAsChar))
                cardValue = GlobalFuntions.GetIntValueFromEnum<CardValues>(cardValueAsString);
            else
                cardValue = Convert.ToInt32(cardValueAsString);
            return cardValue;
        }

        private static string GetCardSuit(string card)
        {
            return card.Trim().Substring(1, 1);
        }
    }
}
