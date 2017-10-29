using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBL.Classes
{
    public static class GlobalFuntions
    {
        internal static int GetIntValueFromEnum<T>(string value)
        {
            int cardValueAsInt = 0;
            cardValueAsInt = Convert.ToInt16((T)Enum.Parse(typeof(T), Convert.ToString(value)));
            return cardValueAsInt;
        }
    }
}
