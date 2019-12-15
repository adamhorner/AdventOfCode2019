using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;

namespace Day9
{
    
    public static class Program
    {
        public static readonly long[] Day7Program =
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

        public static readonly long[] Day9Program =
        {
            1102, 34463338, 34463338, 63, 1007, 63, 34463338, 63, 1005, 63, 53, 1102, 3, 1, 1000, 109, 988, 209, 12, 9,
            1000, 209, 6, 209, 3, 203, 0, 1008, 1000, 1, 63, 1005, 63, 65, 1008, 1000, 2, 63, 1005, 63, 904, 1008, 1000,
            0, 63, 1005, 63, 58, 4, 25, 104, 0, 99, 4, 0, 104, 0, 99, 4, 17, 104, 0, 99, 0, 0, 1102, 1, 0, 1020, 1101,
            0, 23, 1010, 1102, 1, 31, 1009, 1101, 34, 0, 1019, 1102, 38, 1, 1004, 1101, 29, 0, 1017, 1102, 1, 25, 1018,
            1102, 20, 1, 1005, 1102, 1, 24, 1008, 1101, 897, 0, 1024, 1101, 0, 28, 1016, 1101, 1, 0, 1021, 1101, 0, 879,
            1028, 1102, 1, 35, 1012, 1101, 0, 36, 1015, 1101, 311, 0, 1026, 1102, 1, 37, 1011, 1101, 26, 0, 1014, 1101,
            21, 0, 1006, 1102, 1, 32, 1002, 1102, 1, 33, 1003, 1102, 27, 1, 1001, 1102, 1, 667, 1022, 1101, 0, 892,
            1025, 1101, 664, 0, 1023, 1101, 30, 0, 1000, 1101, 304, 0, 1027, 1101, 22, 0, 1013, 1102, 1, 874, 1029,
            1102, 1, 39, 1007, 109, 12, 21108, 40, 41, 1, 1005, 1013, 201, 1001, 64, 1, 64, 1106, 0, 203, 4, 187, 1002,
            64, 2, 64, 109, 5, 1205, 4, 221, 4, 209, 1001, 64, 1, 64, 1106, 0, 221, 1002, 64, 2, 64, 109, 5, 21108, 41,
            41, -5, 1005, 1017, 243, 4, 227, 1001, 64, 1, 64, 1106, 0, 243, 1002, 64, 2, 64, 109, -30, 2101, 0, 8, 63,
            1008, 63, 30, 63, 1005, 63, 269, 4, 249, 1001, 64, 1, 64, 1105, 1, 269, 1002, 64, 2, 64, 109, 15, 2101, 0,
            -5, 63, 1008, 63, 35, 63, 1005, 63, 293, 1001, 64, 1, 64, 1106, 0, 295, 4, 275, 1002, 64, 2, 64, 109, 28,
            2106, 0, -8, 1001, 64, 1, 64, 1105, 1, 313, 4, 301, 1002, 64, 2, 64, 109, -22, 1205, 7, 329, 1001, 64, 1,
            64, 1106, 0, 331, 4, 319, 1002, 64, 2, 64, 109, -12, 1208, 6, 37, 63, 1005, 63, 351, 1001, 64, 1, 64, 1106,
            0, 353, 4, 337, 1002, 64, 2, 64, 109, -3, 2108, 21, 8, 63, 1005, 63, 375, 4, 359, 1001, 64, 1, 64, 1106, 0,
            375, 1002, 64, 2, 64, 109, 14, 1201, -5, 0, 63, 1008, 63, 39, 63, 1005, 63, 401, 4, 381, 1001, 64, 1, 64,
            1105, 1, 401, 1002, 64, 2, 64, 109, 17, 1206, -9, 419, 4, 407, 1001, 64, 1, 64, 1105, 1, 419, 1002, 64, 2,
            64, 109, -10, 21101, 42, 0, -4, 1008, 1015, 42, 63, 1005, 63, 445, 4, 425, 1001, 64, 1, 64, 1105, 1, 445,
            1002, 64, 2, 64, 109, -5, 1206, 7, 457, 1105, 1, 463, 4, 451, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -6,
            2107, 34, -5, 63, 1005, 63, 479, 1105, 1, 485, 4, 469, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -8, 2102, 1,
            5, 63, 1008, 63, 23, 63, 1005, 63, 505, 1106, 0, 511, 4, 491, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5,
            2102, 1, 1, 63, 1008, 63, 21, 63, 1005, 63, 537, 4, 517, 1001, 64, 1, 64, 1105, 1, 537, 1002, 64, 2, 64,
            109, 15, 21107, 43, 44, -6, 1005, 1014, 555, 4, 543, 1106, 0, 559, 1001, 64, 1, 64, 1002, 64, 2, 64, 109,
            -6, 1207, -7, 38, 63, 1005, 63, 579, 1001, 64, 1, 64, 1106, 0, 581, 4, 565, 1002, 64, 2, 64, 109, -17, 1201,
            4, 0, 63, 1008, 63, 28, 63, 1005, 63, 601, 1106, 0, 607, 4, 587, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 14,
            2107, 31, -9, 63, 1005, 63, 625, 4, 613, 1105, 1, 629, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 15, 21102, 44,
            1, -7, 1008, 1019, 44, 63, 1005, 63, 651, 4, 635, 1106, 0, 655, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 3,
            2105, 1, -6, 1106, 0, 673, 4, 661, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -14, 21101, 45, 0, 2, 1008, 1017,
            42, 63, 1005, 63, 693, 1105, 1, 699, 4, 679, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 21107, 46, 45, -8,
            1005, 1012, 719, 1001, 64, 1, 64, 1105, 1, 721, 4, 705, 1002, 64, 2, 64, 109, -19, 2108, 21, 7, 63, 1005,
            63, 737, 1106, 0, 743, 4, 727, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 9, 1207, -2, 25, 63, 1005, 63, 761, 4,
            749, 1106, 0, 765, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -10, 1208, 1, 27, 63, 1005, 63, 783, 4, 771, 1106,
            0, 787, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 1202, 4, 1, 63, 1008, 63, 29, 63, 1005, 63, 807, 1106, 0,
            813, 4, 793, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 8, 21102, 47, 1, 0, 1008, 1013, 50, 63, 1005, 63, 833,
            1106, 0, 839, 4, 819, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -12, 1202, 8, 1, 63, 1008, 63, 31, 63, 1005,
            63, 865, 4, 845, 1001, 64, 1, 64, 1105, 1, 865, 1002, 64, 2, 64, 109, 34, 2106, 0, -7, 4, 871, 1105, 1, 883,
            1001, 64, 1, 64, 1002, 64, 2, 64, 109, -18, 2105, 1, 7, 4, 889, 1105, 1, 901, 1001, 64, 1, 64, 4, 64, 99,
            21101, 0, 27, 1, 21101, 915, 0, 0, 1106, 0, 922, 21201, 1, 13801, 1, 204, 1, 99, 109, 3, 1207, -2, 3, 63,
            1005, 63, 964, 21201, -2, -1, 1, 21102, 942, 1, 0, 1106, 0, 922, 21201, 1, 0, -1, 21201, -2, -3, 1, 21102,
            957, 1, 0, 1105, 1, 922, 22201, 1, -1, -2, 1106, 0, 968, 21202, -2, 1, -2, 109, -3, 2106, 0, 0
        };

        public static void Main(string[] args)
        {
            Console.WriteLine("Running Main");
            
            /*
            var test1FbAmplifierInput = new long[] {3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5};
            Console.WriteLine("Feedback Test 1 Result {0} from {2} - Expected {1}",
                RunFeedbackAmplifier(test1FbAmplifierInput, out var maxOutputCombinationFbTest1), 139629729, string.Join(",", maxOutputCombinationFbTest1));

            var test2FbAmplifierInput = new long[] {3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10};
            Console.WriteLine("Feedback Test 2 Result {0} from {2} - Expected {1}",
                RunFeedbackAmplifier(test2FbAmplifierInput, out var maxOutputCombinationFbTest2), 18216, string.Join(",", maxOutputCombinationFbTest2));

            var part2Output = RunFeedbackAmplifier((long[])Day7Input.Clone(), out var part2OutputCombination);
            Console.WriteLine("Maximum feedback amplifier output achieved at {0} using combination {1}",
                part2Output, string.Join(",",part2OutputCombination.ToArray()));
            */
            
            //Day 9
            var computer = new IntComputer(Day9Program);
            var input = new IntComputerInput(1);
            computer.Connector = new OutputPrinter();
            computer.Run(input);
            Console.WriteLine($"Day 9 part 1 final output: {computer.GetOutput()}");
        }

        class OutputPrinter : IResultSink
        {
            public void AddValue(long input)
            {
                Console.WriteLine($" Output from IntComputer: {input}");
            }
        }

        public static long RunAmplifier(long[] computerInput, out List<long> maxOutputCombination)
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