using System.Linq;
using Day9;
using NUnit.Framework;

namespace Day9_test
{
    public class AmplifierTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AmplifierTest1()
        {
            var test1AmplifierInput = new long[] {3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0};
            Assert.AreEqual(Program.RunAmplifier(test1AmplifierInput, out var maxOutputCombinationTest1), 43210);
        }

        [Test]
        public void AmplifierTest2()
        {
            var test2AmplifierInput = new long[]
                {3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0};
            Assert.AreEqual(Program.RunAmplifier(test2AmplifierInput, out var maxOutputCombinationTest2), 54321);
        }

        [Test]
        public void AmplifierTest3()
        {
            var test3AmplifierInput = new long[]
            {
                3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33,
                1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0
            };
            Assert.AreEqual(Program.RunAmplifier(test3AmplifierInput, out var maxOutputCombinationTest3), 65210);
        }

        [Test]
        public void Day7Part1Test()
        {
            var part1Output = Program.RunAmplifier((long[]) Program.Day7Program.Clone(), out var maxOutputCombination);
            // Console.WriteLine("Maximum amplifier output achieved at {0} using combination {1}", part1Output, string.Join(",",maxOutputCombination.ToArray()));
            Assert.AreEqual(part1Output, 38500);
            Assert.AreEqual("03241",
                maxOutputCombination.ConvertAll(x => x.ToString()).Aggregate("", (left, right) => left + right));
        }
    }
}