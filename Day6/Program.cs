using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Day6
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            const string exampleOrbitPairFile = "/Users/adam/Development/Personal/AdventOfCode2019/Day6/exampleOrbits.txt";
            const string day6OrbitPairFile = "/Users/adam/Development/Personal/AdventOfCode2019/Day6/day6orbits.txt";
            
            var exampleOrbitCom = new Orbit("COM", 0);
            AddOrbits(exampleOrbitCom.Name, ParseOrbitFile(exampleOrbitPairFile), exampleOrbitCom);
            Console.WriteLine("Example Orbits: {0}", exampleOrbitCom.CalculateOrbit());
            
            var day6OrbitCom = new Orbit("COM", 0);
            AddOrbits(day6OrbitCom.Name, ParseOrbitFile(day6OrbitPairFile), day6OrbitCom);
            Console.WriteLine("Day 6 Orbits: {0}", day6OrbitCom.CalculateOrbit());
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