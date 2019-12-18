using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    static class Program
    {
        private static void Main(string[] args)
        {
            CalculateAsteroidMapFromFile("exampleMap1");
            CalculateAsteroidMapFromFile("bigExampleMap1");
            CalculateAsteroidMapFromFile("bigExampleMap2");
            CalculateAsteroidMapFromFile("bigExampleMap3");
            CalculateAsteroidMapFromFile("biggestExampleMap");
            CalculateAsteroidMapFromFile("day10Map");
        }

        private static void CalculateAsteroidMapFromFile(string fileNameRoot)
        {
            const string directory = "/Users/adam/Development/Personal/AdventOfCode2019/Day10";
            var asteroidMap = ReadAsteroidFieldFromFile($"{directory}/{fileNameRoot}.txt");
            var maxAst = new Asteroid(0, 0);
            foreach (var asteroid in asteroidMap)
            {
                foreach (var other in asteroidMap.Where(other => !asteroid.Equals(other)))
                {
                    asteroid.AssessAsteroid(other);
                }

                if (asteroid.GetVisibleAsteroidCount() > maxAst.GetVisibleAsteroidCount())
                    maxAst = asteroid;
                //Console.WriteLine(asteroid);
            }

            Console.WriteLine($"{fileNameRoot}: Maximum visible asteroids is {maxAst.GetVisibleAsteroidCount()} from {maxAst.X},{maxAst.Y}");

            var (angle, explodedAsteroid) = maxAst.FireLaser();
            var count = 0;
            while (angle >= 0)
            {
               Console.WriteLine($"Vaporised asteroid number {++count} at position {explodedAsteroid.X},{explodedAsteroid.Y} at angle {angle}");
               (angle, explodedAsteroid) = maxAst.FireLaser();
            }
            Console.WriteLine($"Asteroid cleared from {maxAst.X},{maxAst.Y} for map {fileNameRoot}\n");
        }

        private static List<Asteroid> ReadAsteroidFieldFromFile(string filename)
        {
            var asteroidField = new List<Asteroid>();
            var lines = File.ReadLines(filename);
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var place in line.ToCharArray())
                {
                    if (place == '#')
                    {
                        asteroidField.Add(new Asteroid(x,y));
                    }
                    x++;
                }
                y++;
            }
            return asteroidField;
        }
    }
}