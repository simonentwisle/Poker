using PokerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Entities
{
    public class Hand
    {
        public static int HandNumber { get; set; }
        public static string PlayersName { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        
        private bool _isRoyalFlush;
        public bool IsRoyalFlush
        {
            private set
            {
                _isRoyalFlush = value;
            }
            get { return _isRoyalFlush; }
        }


        public static bool IsStraightFlush { get; set; }
        public static bool IsFourOfAKind { get; set; }
        public static bool IsFullHouse { get; set; }
        public static bool IsFlush { get; set; }
        public static bool IsStraight { get; set; }
        public static bool IsThreeOfAKind { get; set; }
        public static IEnumerable<Pair> Pairs { get; set; }
        public static int HighestCard { get; set; }

        public Hand()
        {

        }
    }
}
