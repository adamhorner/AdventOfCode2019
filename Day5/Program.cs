using System;

namespace Day5
{
    class IntComputer
    {
        static void Main(string[] args)
        {
            var intComputers = new long[][]
            {
                new long[] {1, 0, 0, 0, 99},
                new long[] {2, 3, 0, 3, 99},
                new long[] {2, 4, 4, 5, 99, 0},
                new long[] {1, 1, 1, 4, 99, 5, 6, 0, 99},
                new long[] {1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,19,5,23,1,23,9,27,2,27,6,31,1,31,6,35,2,35,9,39,1,6,39,43,2,10,43,47,1,47,9,51,1,51,6,55,1,55,6,59,2,59,10,63,1,6,63,67,2,6,67,71,1,71,5,75,2,13,75,79,1,10,79,83,1,5,83,87,2,87,10,91,1,5,91,95,2,95,6,99,1,99,6,103,2,103,6,107,2,107,9,111,1,111,5,115,1,115,6,119,2,6,119,123,1,5,123,127,1,127,13,131,1,2,131,135,1,135,10,0,99,2,14,0,0},
                new long[] {1002,4,3,4,33},
                new long[] {1002,6,3,6,4,0,33},
                new long[] {1101,100,-1,4,0},
                new long[] {1101,0,0,0,03,0,04,0,99}
            };
            var computer = new IntComputer(intComputers[0]);
            computer.Run();
            Console.WriteLine("Result should be 0: " + computer.GetResult());
            // The original test
            for (long noun = 0; noun <= 99; noun++)
            {
                for (long verb = 0; verb <= 99; verb++)
                {
                    computer = new IntComputer((long[])intComputers[4].Clone());
                    computer.SetNoun(noun);
                    computer.SetVerb(verb);
                    computer.Run();
                    if (computer.GetResult() == 19690720)
                    {
                        Console.WriteLine("Noun: " + noun + " Verb: " + verb + " Computer: MATCH");
                        Console.WriteLine("Result: " + (100 * noun + verb));
                    }
                }
            }
            Console.WriteLine("Result should have been Noun 52, Verb 96");
            computer = new IntComputer(intComputers[5]);
            computer.Run();
            Console.WriteLine("Result should be 1002: " + computer.GetResult());
            computer = new IntComputer(intComputers[6]);
            Console.WriteLine("Result should be 1002: " + computer.Run());
            computer = new IntComputer(intComputers[7]);
            computer.Run();
            Console.WriteLine("Result should be 1101: " + computer.GetResult());
            computer = new IntComputer(intComputers[8]);
            Console.WriteLine("Result should be 700: " + computer.Run(700));
            computer = new IntComputer(new long[] {3,225,1,225,6,6,1100,1,238,225,104,0,1102,46,47,225,2,122,130,224,101,-1998,224,224,4,224,1002,223,8,223,1001,224,6,224,1,224,223,223,1102,61,51,225,102,32,92,224,101,-800,224,224,4,224,1002,223,8,223,1001,224,1,224,1,223,224,223,1101,61,64,225,1001,118,25,224,101,-106,224,224,4,224,1002,223,8,223,101,1,224,224,1,224,223,223,1102,33,25,225,1102,73,67,224,101,-4891,224,224,4,224,1002,223,8,223,1001,224,4,224,1,224,223,223,1101,14,81,225,1102,17,74,225,1102,52,67,225,1101,94,27,225,101,71,39,224,101,-132,224,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,1002,14,38,224,101,-1786,224,224,4,224,102,8,223,223,1001,224,2,224,1,223,224,223,1,65,126,224,1001,224,-128,224,4,224,1002,223,8,223,101,6,224,224,1,224,223,223,1101,81,40,224,1001,224,-121,224,4,224,102,8,223,223,101,4,224,224,1,223,224,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,677,226,224,1002,223,2,223,1005,224,329,1001,223,1,223,107,677,677,224,102,2,223,223,1005,224,344,101,1,223,223,1107,677,677,224,102,2,223,223,1005,224,359,1001,223,1,223,1108,226,226,224,1002,223,2,223,1006,224,374,101,1,223,223,107,226,226,224,1002,223,2,223,1005,224,389,1001,223,1,223,108,226,226,224,1002,223,2,223,1005,224,404,1001,223,1,223,1008,677,677,224,1002,223,2,223,1006,224,419,1001,223,1,223,1107,677,226,224,102,2,223,223,1005,224,434,1001,223,1,223,108,226,677,224,102,2,223,223,1006,224,449,1001,223,1,223,8,677,226,224,102,2,223,223,1006,224,464,1001,223,1,223,1007,677,226,224,1002,223,2,223,1006,224,479,1001,223,1,223,1007,677,677,224,1002,223,2,223,1005,224,494,1001,223,1,223,1107,226,677,224,1002,223,2,223,1006,224,509,101,1,223,223,1108,226,677,224,102,2,223,223,1005,224,524,1001,223,1,223,7,226,226,224,102,2,223,223,1005,224,539,1001,223,1,223,8,677,677,224,1002,223,2,223,1005,224,554,101,1,223,223,107,677,226,224,102,2,223,223,1006,224,569,1001,223,1,223,7,226,677,224,1002,223,2,223,1005,224,584,1001,223,1,223,1008,226,226,224,1002,223,2,223,1006,224,599,101,1,223,223,1108,677,226,224,102,2,223,223,1006,224,614,101,1,223,223,7,677,226,224,102,2,223,223,1005,224,629,1001,223,1,223,8,226,677,224,1002,223,2,223,1006,224,644,101,1,223,223,1007,226,226,224,102,2,223,223,1005,224,659,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,674,1001,223,1,223,4,223,99,226});
            Console.WriteLine("Final result: "+computer.Run(1));
        }

        private readonly long[] _intComputer;
        private long _outputValue;

        public IntComputer(long[] intComputer)
        {
            this._intComputer = intComputer;
        }

        private long GetResult()
        {
            return _intComputer[0];
        }

        private long GetNoun()
        {
            return _intComputer[1];
        }

        private void SetNoun(long noun)
        {
            _intComputer[1] = noun;
        }

        private long GetVerb()
        {
            return _intComputer[2];
        }
        
        private void SetVerb(long verb)
        {
            _intComputer[2] = verb;
        }

        private long Run(long input=0, bool debug=false)
        {
            var position = 0;
            if (debug) Console.WriteLine("Position: " + position + " Computer: " + string.Join(",", _intComputer));
            while (true)
            {
                var instruction = _intComputer[position++];
                // opcode is the right two digits of the instruction
                // modes are leftmost digits once the instruction code is taken away
                var modes = ValidateModes(instruction / 100);
                instruction %= 100;
                long result;
                switch (instruction)
                {
                    case 1:
                        if (debug) Console.WriteLine("instruction is addition");
                        result = GetOperand(modes[0], position++) + GetOperand(modes[1], position++);
                        SetOperand(modes[2],position++,result);
                        break;
                    case 2:
                        if (debug) Console.WriteLine("instruction is multiplication");
                        result = GetOperand(modes[0], position++) * GetOperand(modes[1], position++);
                        SetOperand(modes[2],position++,result);
                        break;
                    case 3:
                        if (debug) Console.WriteLine("instruction is input");
                        SetOperand(modes[0], position++, input);
                        break;
                    case 4:
                        _outputValue = GetOperand(modes[0], position++);
                        if (debug) Console.WriteLine("instruction is output, value now: " + _outputValue);
                        else Console.WriteLine(" Output generated: " + _outputValue);
                        break;
                    case 99:
                        if (debug) Console.WriteLine("instruction is exit");
                        return _outputValue;
                    default:
                        throw new InvalidOperationException("Unexpected instruction in intComputer at position " +
                                                            position + " value " +
                                                            instruction + " with opcodes " + modes);
                }
                if (debug) Console.WriteLine("Position: " + position + " Computer: " + string.Join(",", _intComputer));
            }
        }

        private long GetOperand(long mode, long position)
        {
            switch (mode)
            {
                // mode 0 is position mode - dereference the position
                case 0:
                    return _intComputer[_intComputer[position]];
                // mode 1 is immediate mode - use the position
                case 1:
                    return _intComputer[position];
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should be 0 or 1");
            }
        }

        private void SetOperand(long mode, long position, long value)
        {
            switch (mode)
            {
                // mode 0 is position mode - dereference the position
                case 0:
                    _intComputer[_intComputer[position]] = value;
                    break;
                // mode 1 is immediate mode - illegal
                case 1:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should not be 1 when setting a value");
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should be 0 or 1");
            }
        }

        private static long[] ValidateModes(long modeLong)
        {
            if (modeLong < 0 || modeLong > 111)
                throw new ArgumentOutOfRangeException(nameof(modeLong), modeLong,
                    "Modes should not be less than 0 or greater than 111");
            var modeArray = new long[3];
            for (var i = 0; i < 3; i++)
            {
                modeArray[i] = modeLong % 10;
                if (modeArray[i] < 0 || modeArray[i] > 1)
                    throw new InvalidOperationException("Opcodes can only be 0 or 1, found " + modeArray[i]);
                modeLong /= 10;
            }

            return modeArray;
        }
    }
}