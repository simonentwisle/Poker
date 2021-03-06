﻿using PokerEntities;
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
        public int HighestPair { get; set; }
        public int SecondHighestPair { get; set; }
        public int HighestThreeOfAKind { get; set; }
        public int HighestFourOfAKind { get; set; }

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
                    HighestPair == other.HighestPair &&
                    SecondHighestPair == other.SecondHighestPair &&
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
            HighestCard = 0;
            HighestPair = 0;
            HighestThreeOfAKind = 0;
            HighestFourOfAKind = 0;
            SecondHighestPair = 0;
            this.Cards = theCards;
            this.IsSequential();
            this.AreAllCardsOfTheSameSuit();
            this.IsRoyalFlush();
            this.IsStraightFlush();
            this.GetPairsAndXOfAKind();
            this.IsThereAFullHouse();
            this.GetHighestCard();
            this.AssignPoints();
        }

        public Hand()
        {
            Points = 0;
            HighestCard = 0;
            HighestPair = 0;
            SecondHighestPair = 0;
            HighestThreeOfAKind = 0;
            HighestFourOfAKind = 0;
        }

        internal void IsSequential()
        {
            int[] cardValuesInOrder = Cards.OrderBy(c => c.CardValue).Select(c => c.CardValue).ToArray<int>();
            HasStraight = cardValuesInOrder.Zip(cardValuesInOrder.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }

        internal void AreAllCardsOfTheSameSuit()
        {
            var val = Cards.First().Suit;
            HasAFlush = Cards.All(card => card.Suit == val) ? true : false;
        }

        internal void IsRoyalFlush()
        {
            var val = Cards.First().Suit;
            var hasAFlush = Cards.All(card => card.Suit == val) ? true : false;
            if (hasAFlush)
                if (Cards.Where(c => c.CardValue == 10).Count() == 1 && HasStraight)//Hand must start with a 10 if it is a Royal Flush
                {
                    HasRoyalFlush = true;
                }
        }

        internal void IsStraightFlush()
        {
            if (HasAFlush == true && HasStraight == true)
            {
                HasStraightFlush = true;
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
                            SetHighestPair(group.Select(g => g.CardValue).FirstOrDefault());
                        }
                        else{ 
                            HasAPair = true;
                            SetHighestPair(group.Select(g => g.CardValue).FirstOrDefault());
                        }
                        break;
                    case 3:
                        HasThreeOfAKind = true;
                        SetHighestThreeOfAKind(group.Select(g => g.CardValue).FirstOrDefault());
                        break;
                    case 4:
                        HasFourOfAKind = true;
                        SetHighestFourOfAKind(group.Select(g => g.CardValue).FirstOrDefault());
                        break;
                    default:
                        break;
                }
            }

        }

        internal void SetHighestFourOfAKind(int value)
        {
            if (HighestFourOfAKind < value)
            {
                HighestFourOfAKind = value;
            }
        }

        internal void SetHighestPair(int pairValue)
        {
            if (HighestPair < pairValue) {
                SecondHighestPair = HighestPair; 
                HighestPair = pairValue;
            }
            else SecondHighestPair = pairValue;
        }

        internal void SetHighestThreeOfAKind(int value)
        {
            if (HighestThreeOfAKind < value)
            {
                HighestThreeOfAKind = value;
            }
        }

        internal void IsThereAFullHouse()
        {
            if (HasThreeOfAKind == true && HasAPair == true)
            {
                HasFullHouse = true;
            }
                
        }
        
        internal void GetHighestCard()
        {
            HighestCard = Cards.Max(c => c.CardValue);
        }

        internal void AssignPoints()
        {
            if (this.HasAPair == true)
                Points = 2;

            if (this.HasTwoPairs == true)
                Points = 3;

            if (this.HasThreeOfAKind== true)
                Points = 4;

            if (this.HasStraight == true)
                Points = 5;

            if (this.HasAFlush == true)
                Points = 6;

            if (this.HasFullHouse == true)
                Points = 7;

            if (this.HasFourOfAKind == true)
                Points = 8;

            if (this.HasStraightFlush == true)
                Points = 9;

            if (this.HasRoyalFlush == true)
                Points = 10;
        }
        }
        
    }

