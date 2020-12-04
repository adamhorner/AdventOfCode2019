using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {
        const string testPasswords = "/Users/adam/Development/Personal/AdventOfCode/2020/Day02/test.txt";
        const string puzzlePasswords = "/Users/adam/Development/Personal/AdventOfCode/2020/Day02/input.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Test Valid Passwords: " + CountValidPasswords(GetAllPasswordsFromFile(testPasswords)));
            Console.WriteLine("Test Puzzle Passwords: " + CountValidPasswords(GetAllPasswordsFromFile(puzzlePasswords)));
        }

        static Collection<PasswordSet> GetAllPasswordsFromFile(string filename)
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
                    requiredChar = passwordString[spaceLocation + 1],
                    password = passwordString.Substring(colonLocation + 2)
                };
                passwordCollection.Add(passwordSet);
            }

            return passwordCollection;
        }

        static int CountValidPasswords(Collection<PasswordSet> collection)
        {
            return (from passwordSet in collection let countChars = passwordSet.password.Count(x => x == passwordSet.requiredChar) where countChars >= passwordSet.MinOccurs && countChars <= passwordSet.MaxOccurs select passwordSet).Count();
        }
    }

    class PasswordSet
    {
        public int MinOccurs { get; set; }
        public int MaxOccurs { get; set; }
        public char requiredChar { get; set; }
        public string password { get; set; }
    }
}