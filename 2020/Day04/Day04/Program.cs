using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Day04
{
    internal class Program
    {

        private const string FilePath = "/Users/adam/Development/Personal/AdventOfCode/2020/Day04/";

        private static void Main(string[] args)
        {
            var testPassports = ParseBatch($"{FilePath}testbatch.txt");
            var allPassports = ParseBatch($"{FilePath}batch.txt");
            var validTestBatchCount = (from passport in testPassports where passport.IsValid() select passport).Count();
            var validMainBatchCount = (from passport in allPassports where passport.IsValid() select passport).Count();
            Console.WriteLine($"Test batch has {testPassports.Count()} passports and {validTestBatchCount} valid passports.");
            Console.WriteLine($"Main batch has {allPassports.Count()} passports and {validMainBatchCount} valid passports.");
        }

        private static IEnumerable<Passport> ParseBatch(string batchFile)
        {
            var passports = new Collection<Passport>();
            var passport = new Passport();
            foreach (var fileLine in File.ReadAllLines(batchFile))
            {
                if (fileLine.Length == 0)
                {
                    passports.Add(passport);
                    passport = new Passport();
                    continue;
                }

                var fieldsets = fileLine.Split(' ');
                foreach (var fieldset in fieldsets)
                {
                    var namevalue = fieldset.Split(':');
                    switch (namevalue[0])
                    {
                        case "byr": passport.BirthYear = namevalue[1];
                            break;
                        case "iyr": passport.IssueYear = namevalue[1];
                            break;
                        case "eyr": passport.ExpirationYear = namevalue[1];
                            break;
                        case "hgt": passport.Height = namevalue[1];
                            break;
                        case "hcl": passport.HairColor = namevalue[1];
                            break;
                        case "ecl": passport.EyeColor = namevalue[1];
                            break;
                        case "pid": passport.PassportId = namevalue[1];
                            break;
                        case "cid": passport.CountryId = namevalue[1];
                            break;
                    }
                }
            }
            passports.Add(passport);
            return passports;
        }
    }

    internal class Passport
    {
        private const bool CountryIdNotRequired=true;
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        private bool IsMinimallyValid()
        {
            return BirthYear != null && IssueYear != null && ExpirationYear != null &&
                   Height != null && HairColor != null && EyeColor != null && PassportId != null;
        }

        public bool IsValid()
        {
            return IsMinimallyValid() && (CountryIdNotRequired || CountryId != null);
        }
    }
}