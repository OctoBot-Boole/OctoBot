using System;
using System.Security.Cryptography;

namespace OctoBot.Helper
{
    public class SecureRandom
    {
        private static readonly RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider();

        public static int Random(int min, int max)
        {

            var randomNumber = new byte[8];
            Generator.GetBytes(randomNumber);
            var asc2ConvertBytes = Convert.ToDouble(randomNumber[0]);

            var multy = Math.Max(0, asc2ConvertBytes / 255d);
            var range = max - min + 1;
            var randomInRange = Math.Floor(multy * range);

            if (randomInRange > max)
                randomInRange = max;

            return (int) (min + randomInRange);
        }

        public static int Random(int max)
        {
            var randomNumber = new byte[1];

            Generator.GetBytes(randomNumber);

            var asc2ConvertBytes = Convert.ToDouble(randomNumber[0]);

            var multy = Math.Max(0, asc2ConvertBytes / 255d);
            var range = max - 0 + 1;
            var randomInRange = Math.Floor(multy * range);

            var returnValue = (int) (0 + randomInRange);
            if (returnValue >= max)
                returnValue = max;
            return returnValue;
        }
    }
}