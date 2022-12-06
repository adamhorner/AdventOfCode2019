using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var allBags = new Collection<Bag>();
            const string path = "/Users/adam/Development/Personal/AdventOfCode/2020/Day07/";
            allBags = ParseRules(path + "test.txt");
            Console.WriteLine("Hello World!");
        }

        static Collection<Bag> ParseRules(string file)
        {
            var allBags = new Dictionary<string,Bag>();
            foreach (var line in File.ReadAllLines(file))
            {
                var halfs = line.Split(" bags contain ");
                if (halfs.Length > 2) throw new DataException("rule not formatted as expected: "+line);
                var bagName = halfs[0];
                var contents = halfs[1];
                var currentBag=allBags[bagName];
                if (currentBag == null)
                {
                    currentBag = new Bag(bagName);
                    allBags.Add(bagName,currentBag);
                }

                foreach (var rule in halfs[1].Split(','))
                {

                }
            }
        }
    }

    class Bag : IComparable
    {
        private string Name { get; }
        private Dictionary<Bag, int> Contents;

        public Bag(string name)
        {
            Name = name;
            Contents = new Dictionary<Bag, int>();
        }

        public void AddContents(Bag bag, int count)
        {
            Contents.Add(bag, count);
        }

        public int CompareTo(object? obj)
        {
            if (obj?.GetType() != GetType())
            {
                throw new ArgumentException($"Comparison between {GetType()} and {obj.GetType()} not possible");
            }
            return string.Compare(Name, ((Bag)obj).Name, StringComparison.Ordinal);
        }
    }
}