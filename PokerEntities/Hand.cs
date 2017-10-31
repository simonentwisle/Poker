using PokerEntities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Poker.Entities
{
    public class Hand : IEquatable<Hand>
    {
        public int HandNumber { get; set; }
        public string PlayersName { get; set; }
        public List<Card> Cards { get; set; }
        public int Points { get; set; }
        public  bool HasRoyalFlush { get; set; }
        public  bool HasStraightFlush { get; set; }
        public  bool HasFourOfAKind { get; set; }
        public  bool HasFullHouse { get; set; }
        public  bool HasAFlush { get; set; }
        public  bool HasStraight { get; set; }
        public  bool HasThreeOfAKind { get; set; }
        public  bool HasAPair { get; set; }
        public  bool HasTwoPairs { get; set; }
        public  int HighestCard { get; set; }

        #region Equality

        public bool Equals(Hand other)
        {
            if (other == null) return false;
            return HasRoyalFlush == other.HasRoyalFlush &&
                    HasStraightFlush == other.HasStraightFlush &&
                    HasFourOfAKind == other.HasFourOfAKind &&
                    HasFullHouse == other.HasFullHouse &&
                    HasAFlush == other.HasAFlush &&
                    HasStraight == other.HasStraight &&
                    HasThreeOfAKind == other.HasThreeOfAKind &&
                    HasAPair == other.HasAPair &&
                    HasTwoPairs == other.HasTwoPairs &&
                    HighestCard == other.HighestCard &&
                    Points == other.Points; 
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Hand);
        }
        #endregion

        public Hand(List<Card> theCards)
        {
            Points = 0;
            this.Cards = theCards;
            this.IsSequential();
            this.AreAllCardsOfTheSameSuit();
            this.IsRoyalFlush();
            this.IsStraightFlush();
            this.GetPairsAndXOfAKind();
            this.IsThereAFullHouse();
            this.GetHighestCard();
        }

        public Hand()
        {
            Points = 0;
        }

        internal void IsSequential()
        {
            int[] cardValuesInOrder = Cards.OrderBy(c => c.CardValue).Select(c => c.CardValue).ToArray<int>();
            HasStraight = cardValuesInOrder.Zip(cardValuesInOrder.Skip(1), (a, b) => (a + 1) == b).All(x => x);
            if (HasStraight)
                AssignPoints(4);
        }

        internal void AreAllCardsOfTheSameSuit()
        {
            var val = Cards.First().Suit;
            HasAFlush = Cards.All(card => card.Suit == val) ? true : false;
            if (HasAFlush)
                AssignPoints(6);
        }

        internal void IsRoyalFlush()
        {
            var val = Cards.First().Suit;
            var hasAFlush = Cards.All(card => card.Suit == val) ? true : false;
            if (hasAFlush)
                if (Cards.Where(c => c.CardValue == 10).Count() == 1 && HasStraight)//Hand must start with a 10 if it is a Royal Flush
                {
                    HasRoyalFlush = true;
                    AssignPoints(10);
                }
        }

        internal void IsStraightFlush()
        {
            if (HasAFlush == true && HasStraight == true)
            {
                HasStraightFlush = true;
                AssignPoints(9);
            }
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
                        { 
                            HasTwoPairs = true;
                            AssignPoints(3);
                        }
                        else{ 
                            HasAPair = true;
                            AssignPoints(2);
                        }
                        break;
                    case 3:
                        HasThreeOfAKind = true;
                        AssignPoints(4);
                        break;
                    case 4:
                        HasFourOfAKind = true;
                        AssignPoints(8);
                        break;
                    default:
                        break;
                }
            }

        }

        internal void IsThereAFullHouse()
        {
            if (HasThreeOfAKind == true && HasAPair == true)
            {
                HasFullHouse = true;
                AssignPoints(7);
            }
                
        }
        
        internal void GetHighestCard()
        {
            HighestCard = Cards.Max(c => c.CardValue);
        }

        internal void AssignPoints(int pointsToAssign)
        {
            var currentPoints = this.Points;
            if (pointsToAssign > currentPoints)
                Points = pointsToAssign;
        }


    }
}
