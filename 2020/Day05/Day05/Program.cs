using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            if (TestSeat("BFFFBBFRRR", 567) &&
                TestSeat("FFFBBBFRRR", 119) &&
                TestSeat("BBFFBBFRLL", 820))
            {
                Console.WriteLine("All tests pass");
            }
            else
            {
                Console.WriteLine("Some tests fail, exiting");
                return;
            }

            var seatList = File
                .ReadAllLines("/Users/adam/Development/Personal/AdventOfCode/2020/Day05/input.txt")
                .Select(line => new Seat(line)).ToList();

            var maxSeatNumber = seatList.Select(seat => seat.SeatIndex())
                .Prepend(0)
                .Max();
            Console.WriteLine($"Highest seat index is {maxSeatNumber}");

            var seatIndexes = seatList.Select(seat => seat.SeatIndex()).ToList();
            for (var i = 0; i < 1024; i++)
            {
                if (seatIndexes.Contains(i)) continue;
                if (seatIndexes.Contains(i-1) && seatIndexes.Contains(i+1))
                    Console.WriteLine($"Missing seat is {i}");
            }
        }

        private static bool TestSeat(string binaryPartitionText, int seatIndex)
        {
            var testSeat = new Seat(binaryPartitionText);
            if (testSeat.SeatIndex() == seatIndex) return true;

            Console.WriteLine($"Seat {binaryPartitionText} does not have index {seatIndex}, got {testSeat.SeatIndex()}");
            Console.WriteLine($" seat has row {testSeat.RowNumber} and seat {testSeat.SeatNumber}");
            return false;
        }
    }
}