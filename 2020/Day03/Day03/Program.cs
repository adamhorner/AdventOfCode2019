using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Day03
{
    internal static class Program
    {
        private const int HorizontalOffSetRate = 3;
        private const string Testmap = "/Users/adam/Development/Personal/AdventOfCode/2020/Day03/testmap.txt";
        private const string Map = "/Users/adam/Development/Personal/AdventOfCode/2020/Day03/map.txt";

        private static void Main(string[] args)
        {
            var testMap= File.ReadAllLines(Testmap);
            Console.WriteLine("Trees hit in testMap:" + CountTreesHit(testMap, HorizontalOffSetRate));
            var map = File.ReadAllLines(Map);
            Console.WriteLine("Trees hit in map:" + CountTreesHit(map, HorizontalOffSetRate));
            Console.WriteLine("Part 2");
            // calculate the variants
            Console.WriteLine("Slope Tree Factor - testMap: "+CalculateSlopeTreeFactor(testMap));
            Console.WriteLine("Slope Tree Factor - map: "+CalculateSlopeTreeFactor(map));
        }

        private static long CalculateSlopeTreeFactor(IEnumerable<string> map)
        {
            var fastDrop = CountTreesHit(map, 1, true);
            Console.WriteLine($"Fast Drop Value: {fastDrop}");
            return CountTreesHit(map, 1) * CountTreesHit(map, 3) * CountTreesHit(map, 5) *
                   CountTreesHit(map, 7) * fastDrop;
        }

        private static long CountTreesHit(IEnumerable<string> map, int offsetRate, bool fastDrop=false)
        {
            // assume all lines in map are the same length
            var offset = 0;
            var treesHit = 0;
            bool oddLine = false;
            foreach (var line in map)
            {
                oddLine = !oddLine;
                if (fastDrop && !oddLine) continue;
                if (line[offset % line.Length]=='#') treesHit++;
                offset += offsetRate;
            }

            return treesHit;
        }
    }
}