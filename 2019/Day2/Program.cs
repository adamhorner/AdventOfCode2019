using System;

namespace Day2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var intComputers = new long[][]
            {
                new long[] {1, 0, 0, 0, 99},
                new long[] {2, 3, 0, 3, 99},
                new long[] {2, 4, 4, 5, 99, 0},
                new long[] {1, 1, 1, 4, 99, 5, 6, 0, 99},
                new long[] {1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,19,5,23,1,23,9,27,2,27,6,31,1,31,6,35,2,35,9,39,1,6,39,43,2,10,43,47,1,47,9,51,1,51,6,55,1,55,6,59,2,59,10,63,1,6,63,67,2,6,67,71,1,71,5,75,2,13,75,79,1,10,79,83,1,5,83,87,2,87,10,91,1,5,91,95,2,95,6,99,1,99,6,103,2,103,6,107,2,107,9,111,1,111,5,115,1,115,6,119,2,6,119,123,1,5,123,127,1,127,13,131,1,2,131,135,1,135,10,0,99,2,14,0,0}
            };
            for (long noun = 0; noun <= 99; noun++)
            {
                for (long verb = 0; verb <= 99; verb++)
                {
                    long[] newComputer = (long[])intComputers[4].Clone();
                    newComputer[1] = noun;
                    newComputer[2] = verb;
                    long result = RunIntComputer(newComputer)[0];
                    if (result == 19690720)
                    {
                        Console.WriteLine("Noun: " + noun + " Verb:" + verb + " Computer: MATCH");
                        Console.WriteLine("Result: " + (100 * noun + verb));
                    }
                }
            }
/*
            foreach (var intComputer in intComputers)
            {
                Console.WriteLine("Runing for " + String.Join(",", intComputer));
                Console.WriteLine(" Result is "+String.Join(",",RunIntComputer(intComputer)));
            }
*/
        }

        private static long[] RunIntComputer(long[] intComputer)
        {
            int position = 0;
            while (true)
            {
                var instruction = intComputer[position];
                if (instruction == 99)
                {
                    //Console.WriteLine("instruction is exit");
                    break;
                }
                else if (instruction == 1)
                {
                    //Console.WriteLine("instruction is addition");
                    intComputer[intComputer[position+3]] = intComputer[intComputer[position + 1]] + intComputer[intComputer[position + 2]];
                    position += 4;
                }
                else if (instruction == 2)
                {
                    //Console.WriteLine("instruction is multiplication");
                    intComputer[intComputer[position+3]] = intComputer[intComputer[position + 1]] * intComputer[intComputer[position + 2]];
                    position += 4;
                }
                else
                {
                    Console.WriteLine("Unexpected instruction in intComputer at position "+position+" value "+instruction);
                    break;
                }
                //Console.WriteLine("Computer: "+string.Join(",",intComputer));
            }
            return intComputer;
        }

    }
}