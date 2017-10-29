using PokerEntities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Poker.Entities
{
    public class Hand
    {
        public static int HandNumber { get; set; }
        public static string PlayersName { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        
        public static bool HasRoyalFlush { get; set; }
        public static bool HasStraightFlush { get; set; }
        public static bool HasFourOfAKind { get; set; }
        public static bool HasFullHouse { get; set; }
        public static bool HasAFlush { get; set; }
        public static bool HasStraight { get; set; }
        public static bool HasThreeOfAKind { get; set; }
        public static bool HasAPair { get; set; }
        public static bool HasTwoPairs { get; set; }
        public static int HighestCard { get; set; }
        
        public Hand(List<Card> theCards)
        {
            //this.Cards = theCards;

            List<Card> TestCards = new List<Card>();
           
            Card card1 = new Card()
            {
                CardValue = 10,
                Suit = "D"
            };
            TestCards.Add(card1);

            Card card2 = new Card()
            {
                CardValue = 10,
                Suit = "S"
            };
            TestCards.Add(card2);

            Card card3 = new Card()
            {
                CardValue = 10,
                Suit = "C"
            };
            TestCards.Add(card3);

            Card card4 = new Card()
            {
                CardValue = 10,
                Suit = "H"
            };
            TestCards.Add(card4);

            Card card5 = new Card()
            {
                CardValue = 14,
                Suit = "D"
            };
            TestCards.Add(card5);
            this.Cards = TestCards;

            this.IsSequential();
            this.AreAllCardsOfTheSameSuit();
            this.IsRoyalFlush();
            this.IsStraightFlush();
            this.GetPairsAndXOfAKind();
            this.GetHighestCard();
        }

        internal void AreAllCardsOfTheSameSuit()
        {
            var val = Cards.First().Suit;
            HasAFlush = Cards.All(card => card.Suit == val) ? true : false;
        }

        internal void GetPairsAndXOfAKind()
        {
            var groups = Cards.GroupBy(c => c.CardValue);

            HasAPair = false;
            foreach (var group in groups)
            {
                switch (group.Count())
                {
                    case 2:
                        if (HasAPair == true)
                            HasTwoPairs = true;
                        else
                            HasAPair = true;  
                        break;
                    case 3:
                        HasThreeOfAKind = true;
                        break;
                    case 4:
                        HasFourOfAKind = true;
                        break;
                    default:
                        break;
                }
            }
           
        }

        internal void IsRoyalFlush()
        {
            var val = Cards.First().Suit;
            var hasAFlush = Cards.All(card => card.Suit == val) ? true : false;
            if (hasAFlush)
                if (Cards.Where(c => c.CardValue == 10).Count() == 1 && HasStraight) //Hand must start with a 10 if it is a Royal Flush
                    HasRoyalFlush = true;
        }

        internal void IsStraightFlush()
        {
            if (HasAFlush == true && HasStraight == true)
                HasStraightFlush = true;
        }

        internal void IsSequential()
        {
            int[] cardValuesInOrder = Cards.OrderBy(c => c.CardValue).Select(c => c.CardValue).ToArray<int>();
            HasStraight = cardValuesInOrder.Zip(cardValuesInOrder.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        internal void GetHighestCard()
        {
            HighestCard = Cards.Max(c => c.CardValue);
        }
    }
}
