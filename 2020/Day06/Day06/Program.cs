using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "/Users/adam/Development/Personal/AdventOfCode/2020/Day06";
            var testGroups = ParseCustomsGroups($"{path}/test.txt");
            var inputGroups = ParseCustomsGroups($"{path}/input.txt");
            Console.WriteLine("Part 1: " +
                              testGroups.Select(x => x.CountAnyNonZeroAnswers()).Sum() +
                              " and " +
                              inputGroups.Select(x => x.CountAnyNonZeroAnswers()).Sum());
            Console.WriteLine("Part 2: " +
                              testGroups.Select(x => x.CountAllNonZeroAnswers()).Sum() +
                              " and " +
                              inputGroups.Select(x=>x.CountAllNonZeroAnswers()).Sum());
    }

        static IEnumerable<CustomsGroup> ParseCustomsGroups(string filename){
            var customsGroup = new CustomsGroup();
            var allCustomsGroups = new Collection<CustomsGroup>();
            foreach (var line in File.ReadAllLines(filename) )
            {
                if (line.Length == 0)
                {
                    allCustomsGroups.Add(customsGroup);
                    customsGroup = new CustomsGroup();
                    continue;
                }

                customsGroup.IncrementGroupSize();
                foreach (var c in line.ToCharArray())
                {
                    customsGroup.AddAnswer(c);
                }
            }
            allCustomsGroups.Add(customsGroup);
            return allCustomsGroups;
        }
    }

    internal class CustomsGroup
    {
        private readonly Dictionary<char, int> _answerDictionary;
        private int GroupSize;

        public CustomsGroup()
        {
            var lowerAlphas = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
            };
            _answerDictionary = new Dictionary<char, int>();
            foreach (var character in lowerAlphas)
            {
                _answerDictionary.Add(character,0);
            }
        }

        public void AddAnswer(char character)
        {
            var lowerCharacter = character.ToString().ToLower().ToCharArray()[0];
            _answerDictionary[lowerCharacter] = _answerDictionary[lowerCharacter] + 1;
        }

        public void IncrementGroupSize()
        {
            GroupSize += 1;
        }

        public int CountAnyNonZeroAnswers()
        {
            return _answerDictionary.Values.Select(x => x).Count(x => x > 0);
        }

        public int CountAllNonZeroAnswers()
        {
            return _answerDictionary.Values.Select(x => x).Count(x => x == GroupSize);
        }
    }
}