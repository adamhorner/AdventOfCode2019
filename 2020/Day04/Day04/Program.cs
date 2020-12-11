using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    internal class Program
    {

        private const string FilePath = "/Users/adam/Development/Personal/AdventOfCode/2020/Day04/";

        private static void Main(string[] args)
        {
            var testPassports = ParseBatch($"{FilePath}testbatch.txt");
            var validTestPassports = ParseBatch($"{FilePath}testvalidbatch.txt");
            var invalidTestPassports = ParseBatch($"{FilePath}testinvalidbatch.txt");
            var allPassports = ParseBatch($"{FilePath}batch.txt");
            var validTestBatchCount = (from passport in testPassports where passport.IsMinimallyValid() select passport).Count();
            var validMainBatchCount = (from passport in allPassports where passport.IsMinimallyValid() select passport).Count();
            Console.WriteLine("Part 1:");
            Console.WriteLine($"Test batch has {testPassports.Count()} passports and {validTestBatchCount} valid passports.");
            Console.WriteLine($"Main batch has {allPassports.Count()} passports and {validMainBatchCount} valid passports.");
            // part 2
            Console.WriteLine("Part 2:");
            validTestBatchCount = (from passport in testPassports where passport.IsValid() select passport).Count();
            validMainBatchCount = (from passport in allPassports where passport.IsValid() select passport).Count();
            var validTestValidBatchCount = (from passport in validTestPassports where passport.IsValid() select passport).Count();
            var validTestInvalidBatchCount = (from passport in invalidTestPassports where passport.IsValid() select passport).Count();
            Console.WriteLine($"Test batch has {testPassports.Count()} passports and {validTestBatchCount} valid passports.");
            Console.WriteLine($"Test valid batch correctly has 4 passports: {validTestValidBatchCount==4}");
            Console.WriteLine($"Test invalid batch correctly has 0 passports: {validTestInvalidBatchCount==0}");
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
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public bool IsMinimallyValid()
        {
            return BirthYear != null &&
                   IssueYear != null &&
                   ExpirationYear != null &&
                   Height != null &&
                   HairColor != null &&
                   EyeColor != null &&
                   PassportId != null;
        }

        private bool IsHeightValid()
        {
            if (Height.EndsWith("cm"))
            {
                var height = int.Parse(Height.Substring(0, Height.Length - 2));
                return height >= 150 && height <= 193;
            }

            if (Height.EndsWith("in"))
            {
                var height = int.Parse(Height.Substring(0, Height.Length - 2));
                return height >= 59 && height <= 76;
            }

            return false;
        }

        private bool IsHairColorValid()
        {
            if (HairColor == null || HairColor.Length != 7) return false;
            return Regex.IsMatch(HairColor,"^#[0-9a-f]{6}$");
        }

        private bool IsEyeColorValid()
        {
            string[] acceptableEyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            return EyeColor != null && new HashSet<string>(acceptableEyeColors).Contains(EyeColor);
        }

        private bool IsPassportIdValid()
        {
            return PassportId != null && Regex.IsMatch(PassportId, "^[0-9]{9}$");
        }

        public bool IsValid()
        {
            return IsMinimallyValid() &&
                   int.Parse(BirthYear) >= 1920 && int.Parse(BirthYear) <= 2002 &&
                   int.Parse(IssueYear) >= 2010 && int.Parse(IssueYear) <= 2020 &&
                   int.Parse(ExpirationYear) >= 2020 && int.Parse(ExpirationYear) <= 2030 &&
                   IsHeightValid() &&
                   IsHairColorValid() &&
                   IsEyeColorValid() &&
                   IsPassportIdValid();
        }
    }
}