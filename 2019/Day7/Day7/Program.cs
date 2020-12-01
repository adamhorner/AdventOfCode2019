using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

namespace Day7
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Running Main");
            var test1AmplifierInput = new long[] {3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0};
            Console.WriteLine("Test 1 Result {0} - Expected {1}", RunAmplifier(test1AmplifierInput, out var maxOutputCombinationTest1), 43210);
            var test2AmplifierInput = new long[]
                {3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0};
            Console.WriteLine("Test 2 Result {0} - Expected {1}", RunAmplifier(test2AmplifierInput, out var maxOutputCombinationTest2), 54321);
            var test3AmplifierInput = new long[]
            {
                3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33,
                1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0
            };
            Console.WriteLine("Test 3 Result {0} - Expected {1}", RunAmplifier(test3AmplifierInput, out var maxOutputCombinationTest3), 65210);
            var computerInput = new long[]
            {
                3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 38, 47, 64, 89, 110, 191, 272, 353, 434, 99999, 3, 9, 101, 4, 9,
                9, 102, 3, 9, 9, 101, 5, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 5, 9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 102, 5, 9,
                9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 1001, 9, 5, 9, 102, 4, 9, 9, 1001, 9, 5, 9, 1002, 9, 2, 9, 1001, 9, 3,
                9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 101, 4, 9, 9, 1002, 9, 4, 9, 1001, 9, 4, 9, 4, 9, 99, 3, 9, 101, 1, 9,
                9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9,
                9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9,
                1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9,
                102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9,
                1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99,
                3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4,
                9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9,
                4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9,
                1, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001,
                9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9,
                1002, 9, 2, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9,
                3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4,
                9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99
            };
            var part1Output = RunAmplifier((long[])computerInput.Clone(), out var maxOutputCombination);
            Console.WriteLine("Maximum amplifier output achieved at {0} using combination {1}",
                part1Output, string.Join(",",maxOutputCombination.ToArray()));
            Console.WriteLine(38500==part1Output ? "Part 1 worked: {0}" : "Part 1 failed {0}", part1Output);
            
            var test1FbAmplifierInput = new long[] {3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5};
            Console.WriteLine("Feedback Test 1 Result {0} from {2} - Expected {1}",
                RunFeedbackAmplifier(test1FbAmplifierInput, out var maxOutputCombinationFbTest1), 139629729, string.Join(",", maxOutputCombinationFbTest1));

            var test2FbAmplifierInput = new long[] {3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10};
            Console.WriteLine("Feedback Test 2 Result {0} from {2} - Expected {1}",
                RunFeedbackAmplifier(test2FbAmplifierInput, out var maxOutputCombinationFbTest2), 18216, string.Join(",", maxOutputCombinationFbTest2));

            var part2Output = RunFeedbackAmplifier((long[])computerInput.Clone(), out var part2OutputCombination);
            Console.WriteLine("Maximum feedback amplifier output achieved at {0} using combination {1}",
                part2Output, string.Join(",",part2OutputCombination.ToArray()));
        }

        private static long RunAmplifier(long[] computerInput, out List<long> maxOutputCombination)
        {
            var phaseCombinations = GenerateLists(new List<long>(), new List<long>() {0, 1, 2, 3, 4});
            var maxOutput = long.MinValue;
            maxOutputCombination = new List<long>();
            foreach (var phaseCombination in phaseCombinations)
            {
                long lastOutput = 0;
                foreach (var phase in phaseCombination)
                {
                    var amplifier = new IntComputer((long[]) computerInput.Clone());
                    lastOutput = amplifier.Run(new[] {phase, lastOutput});
                }

                if (lastOutput > maxOutput)
                {
                    maxOutput = lastOutput;
                    maxOutputCombination = phaseCombination;
                }
            }

            return maxOutput;
        }
        
        private static long RunFeedbackAmplifier(long[] computerInput, out List<long> maxOutputCombination)
        {
            var phaseCombinations = GenerateLists(new List<long>(), new List<long>() {5, 6, 7, 8, 9});
            var maxOutput = long.MinValue;
            maxOutputCombination = new List<long>();
            var combinationCounter = 0;
            foreach (var phaseCombination in phaseCombinations)
            {
                Console.WriteLine("Trying combination {0}", combinationCounter++);
                var lastAmplifier=new IntComputer(new long[]{99});
                var firstInput=new IntComputerInput();
                var amplifierThreads = new List<Thread>();
                foreach (var phase in phaseCombination)
                {
                    var amplifier = new IntComputer((long[]) computerInput.Clone());
                    var input = new IntComputerInput(phase);
                    if (amplifierThreads.Count == 0)
                    {
                        input.AddValue(0);
                        firstInput = input;
                    }
                    else if (amplifierThreads.Count == 4)
                    {
                        lastAmplifier.Connector = input;
                        amplifier.Connector = firstInput;
                    }
                    else
                        lastAmplifier.Connector = input;
                    var newRunThread = new Thread(() => amplifier.Run(input));
                    amplifierThreads.Add(newRunThread);
                    newRunThread.Start();
                    lastAmplifier = amplifier;
                }
                

                foreach (var thread in amplifierThreads)
                {
                    thread.Join();
                }

                if (lastAmplifier.GetOutput() > maxOutput)
                {
                    maxOutput = lastAmplifier.GetOutput();
                    maxOutputCombination = phaseCombination;
                }
            }

            return maxOutput;
        }

        private static List<List<long>> GenerateLists(List<long> baseArray, List<long> digits)
        {
            var result = new List<List<long>>();
            if (digits.Count == 0)
            {
                result.Add(baseArray);
                return result;
            }
            foreach (var digit in digits)
            {
                var newBaseArray = new List<long>(baseArray.ToArray());
                newBaseArray.Add(digit);
                var newDigits = new List<long>(digits.ToArray());
                newDigits.Remove(digit);
                result.AddRange(GenerateLists(newBaseArray, newDigits));
            }

            return result;
        }
    
    }
}