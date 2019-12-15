using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Day9;
using NUnit.Framework;

namespace Day9_test
{
    public class Day9Tests
    {
        [Test]
        public void CopyCloneTest()
        {
            var original = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            var computer = new IntComputer(original);
            var outputs = new TestOutput();
            computer.Connector = outputs;
            computer.Run(true);
            Assert.AreEqual(original.Length, outputs.Results.Count);
            for (var i = 0; i < original.Length; i++)
            {
                Assert.AreEqual(original[i], outputs.Results[i]);
            }
        }

        [Test]
        public void SixteenDigitTest()
        {
            var computer = new IntComputer(new long[] {1102,34915192,34915192,7,4,7,99});
            computer.Run();
            Assert.AreEqual(16, computer.GetOutput().ToString().Length);
        }

        [Test]
        public void LargeOutputTest()
        {
            const long bigNumber = 1125899906842624;
            var computer = new IntComputer(new long[] {104,bigNumber,99});
            computer.Run();
            Assert.AreEqual(bigNumber,computer.GetOutput());
        }
    }

    public class TestOutput : IResultSink
    {
        public List<long> Results = new List<long>();

        public void AddValue(long output)
        {
            Results.Add(output);
        }
    }
}