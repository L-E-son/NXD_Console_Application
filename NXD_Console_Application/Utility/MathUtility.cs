using System;
using System.Collections.Generic;

namespace NXD_Console_Application.Utility
{
    public static class MathUtility
    {
        //http://www.geeksforgeeks.org/print-all-prime-factors-of-a-given-number/
        public static IEnumerable<uint> GetPrimeFactors(uint num)
        {
            while (num % 2 == 0)
            {
                yield return 2;
                num /= 2;
            }

            for (uint i = 3; i <= Math.Sqrt(num); i += 2)
            {
                while (num % i == 0)
                {
                    yield return i;
                    num /= i;
                }
            }

            if (num > 2)
                yield return num;
        }
    }
}
