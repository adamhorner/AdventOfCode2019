using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Day6
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            const string part1ExampleOrbitPairFile = "/Users/adam/Development/Personal/AdventOfCode2019/Day6/exampleOrbits.txt";
            const string part2ExampleOrbitPairFile = "/Users/adam/Development/Personal/AdventOfCode2019/Day6/exampleOrbits2.txt";
            const string day6OrbitPairFile = "/Users/adam/Development/Personal/AdventOfCode2019/Day6/day6orbits.txt";
            
            var exampleOrbitCom = Orbit.CreateComOrbit();
            AddOrbits(exampleOrbitCom.Name, ParseOrbitFile(part1ExampleOrbitPairFile), exampleOrbitCom);
            Console.WriteLine("Example Orbits: {0}", exampleOrbitCom.CalculateOrbit());

            var day6OrbitCom = Orbit.CreateComOrbit();
            AddOrbits(day6OrbitCom.Name, ParseOrbitFile(day6OrbitPairFile), day6OrbitCom);
            Console.WriteLine("Day 6 Orbits: {0}", day6OrbitCom.CalculateOrbit());

            Console.WriteLine("Example 2 shortest path");
            var example2OrbitCom = Orbit.CreateComOrbit();
            AddOrbits(example2OrbitCom.Name, ParseOrbitFile(part2ExampleOrbitPairFile), example2OrbitCom);
            var youOrbitPath = example2OrbitCom.FindOrbit("YOU").ToArray();
            var sanOrbitPath = example2OrbitCom.FindOrbit("SAN").ToArray();
            Console.WriteLine("Shortest Path: {0}", FindShortestPath(youOrbitPath, sanOrbitPath));

            Console.WriteLine("Day 6 shortest path");
            youOrbitPath = day6OrbitCom.FindOrbit("YOU").ToArray();
            sanOrbitPath = day6OrbitCom.FindOrbit("SAN").ToArray();
            Console.WriteLine("Shortest Path: {0}", FindShortestPath(youOrbitPath, sanOrbitPath));
        }

        private static long FindShortestPath(string[] orbit1, string[] orbit2)
        {
//            Console.WriteLine(string.Join("->", orbit1));
//            Console.WriteLine(string.Join("->", orbit2));
            for (var y = 0; y < orbit1.Length; y++)
            {
                for (var s = 0; s < orbit2.Length; s++)
                {
                    // take off 2, one for santa, one for you
                    if (orbit1[y] == orbit2[s]) return y + s - 2;
                }
            }

            return long.MaxValue;
        }

        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
        private static Lookup<string, string> ParseOrbitFile(string filename)
        {
            const string orbitDelimiter = ")";
            return (Lookup<string, string>) File.ReadAllLines(filename).ToLookup(
                p=>p.Substring(0,p.IndexOf(orbitDelimiter)),
                p=>p.Substring(p.IndexOf(orbitDelimiter)+1));
            
        }

        private static void AddOrbits(string name, Lookup<string, string> orbitPairs, Orbit rootOrbit)
        {
            foreach (var orbit in orbitPairs[name])
            {
                rootOrbit.AddOrbit(name, orbit);
                AddOrbits(orbit, orbitPairs, rootOrbit);
            }
        }
    }
}