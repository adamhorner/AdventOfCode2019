using System;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            IsCombinationValid(100000, true);
            IsCombinationValid(111111, true);
            IsCombinationValid(120111, true);
            IsCombinationValid(223450, true);
            IsCombinationValid(123789, true);
            IsCombinationValid(177888, true);
            IsCombinationValid(133556, true);
            IsCombinationValid(333556, true);
            IsCombinationValid(333366, true);
            IsCombinationValid(223333, true);
            var validCounter = 0;
            for (var i = 136818; i <= 685979; i++)
                validCounter += IsCombinationValid(i) ? 1 : 0;
            Console.WriteLine(validCounter + " valid combinations in the given range");
        }

        private static bool IsCombinationValid(int combination, bool debug=false)
        {
            if (debug) Console.WriteLine("Checking "+combination);
            if (combination < 111111 || combination > 999999)
            {
                if (debug) Console.WriteLine(" not in valid range");
                return false;
            }
            //set this to true if we get 2 consecutive digits
            var pair = false;
            var consecutiveCount = 1;
            var previousDigit = int.Parse(combination.ToString().Substring(0, 1));
            var digits = combination.ToString().Substring(1).ToCharArray();
            foreach (var digit in digits)
            {
//                if (debug) Console.WriteLine(" checking " + digit);
                int intDigit = int.Parse(digit.ToString());
                if (intDigit < previousDigit)
                {
                    return false;
                }
                if (intDigit == previousDigit)
                {
                    consecutiveCount++;
                }
                else
                {
                    if (consecutiveCount == 2)
                        pair = true;
                    consecutiveCount = 1;
                }
                previousDigit = intDigit;
            }
            if (consecutiveCount == 2)
                pair = true;

            if (debug)
                Console.WriteLine("End of sequence, pair is " + pair + " and consecutive count is " + consecutiveCount);

            if (debug && pair) Console.WriteLine(combination+" is a valid combination");
            return pair;
        }
    }
}