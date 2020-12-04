using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day02
{
    internal static class Program
    {
        private const string TestPasswords = "/Users/adam/Development/Personal/AdventOfCode/2020/Day02/test.txt";
        private const string PuzzlePasswords = "/Users/adam/Development/Personal/AdventOfCode/2020/Day02/input.txt";

        private static void Main(string[] args)
        {
            Console.WriteLine("Test Valid1 Passwords: " + CountValidPasswordsPart1(GetAllPasswordsFromFile(TestPasswords)));
            Console.WriteLine("Test Valid2 Passwords: " + CountValidPasswordsPart2(GetAllPasswordsFromFile(TestPasswords)));
            Console.WriteLine("Test Puzzle1 Passwords: " + CountValidPasswordsPart1(GetAllPasswordsFromFile(PuzzlePasswords)));
            Console.WriteLine("Test Puzzle2 Passwords: " + CountValidPasswordsPart2(GetAllPasswordsFromFile(PuzzlePasswords)));
        }

        private static IEnumerable<PasswordSet> GetAllPasswordsFromFile(string filename)
        {
            var passwordCollection = new Collection<PasswordSet>();
            foreach (var passwordString in File.ReadAllLines(filename))
            {
                var hyphenLocation = passwordString.IndexOf("-", StringComparison.Ordinal);
                var spaceLocation = passwordString.IndexOf(" ", StringComparison.Ordinal);
                var colonLocation = passwordString.IndexOf(":", StringComparison.Ordinal);
                var passwordSet = new PasswordSet
                {
                    MinOccurs = int.Parse(passwordString.Substring(0, hyphenLocation)),
                    MaxOccurs = int.Parse(passwordString.Substring(hyphenLocation + 1,
                        spaceLocation - hyphenLocation - 1)),
                    RequiredChar = passwordString[spaceLocation + 1],
                    Password = passwordString.Substring(colonLocation + 2)
                };
                passwordCollection.Add(passwordSet);
            }

            return passwordCollection;
        }

        private static int CountValidPasswordsPart1(IEnumerable<PasswordSet> collection)
        {
            return (
                from passwordSet in collection
                let countChars = passwordSet.Password.Count(x => x == passwordSet.RequiredChar)
                where countChars >= passwordSet.MinOccurs && countChars <= passwordSet.MaxOccurs
                select passwordSet).Count();
        }

        private static int CountValidPasswordsPart2(IEnumerable<PasswordSet> collection)
        {
            return (
                from passwordSet in collection
                where passwordSet.Password[passwordSet.MinOccurs-1] == passwordSet.RequiredChar ^
                      passwordSet.Password[passwordSet.MaxOccurs-1] == passwordSet.RequiredChar
                select passwordSet).Count();
        }
    }

    internal class PasswordSet
    {
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public char RequiredChar { get; set; }
        public string Password { get; set; }
    }
}