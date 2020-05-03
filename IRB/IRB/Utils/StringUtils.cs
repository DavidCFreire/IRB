using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRB.Utils
{
    static class StringUtils
    {
        public static string Before(this string value, string a, int numberCharacter)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            string parcial = value.Substring(0, posA);
            if (numberCharacter > parcial.Length)
                numberCharacter = parcial.Length;
            string x = parcial.Substring(parcial.Length - numberCharacter);
            return x;
        }

        public static string After(this string value, string a, int numberCharacter)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            //Math.Min(str.Length, maxLength)
            string x = value.Substring(adjustedPosA);
            return x.Substring(0, Math.Min(x.Length, numberCharacter));
        }

    }
}
