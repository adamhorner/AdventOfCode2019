using System;
using System.Text.RegularExpressions;

namespace Day05
{
    class Seat
    {
        public int RowNumber { get; }
        public int SeatNumber { get; }

        public Seat(int rowNumber, int seatNumber)
        {
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
        }

        public Seat(string binaryPartitionText)
        {
            if (binaryPartitionText.Length != 10)
            {
                throw new ArgumentException("Input is not a 10 digit binary partition string");
            }

            if (!Regex.IsMatch(binaryPartitionText, "^[FB]{7}[LR]{3}$"))
            {
                throw new ArgumentException("Binary partition string must have 7 L or B values, three R or L values, got " +binaryPartitionText);
            }

            var rowNumber = binaryPartitionText.Substring(0, 7);
            var seatNumber = binaryPartitionText.Substring(7, 3);
            rowNumber = rowNumber.Replace('F', '0').Replace('B', '1');
            seatNumber = seatNumber.Replace('L', '0').Replace('R', '1');
            RowNumber = Convert.ToInt32(rowNumber, 2);
            SeatNumber = Convert.ToInt32(seatNumber, 2);
        }

        public int SeatIndex()
        {
            return (RowNumber * 8) + SeatNumber;
        }
    }
}