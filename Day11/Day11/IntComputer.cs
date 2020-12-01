using System;
using System.Net;

namespace Day11
{
    public class IntComputer
    {
        private long[] _intComputer;
        private long _outputValue;
        private long _relativeBase = 0;
        public IResultSink Connector { get; set; }

        public IntComputer(long[] intComputer)
        {
            _intComputer = intComputer;
        }

        [Obsolete]
        public long GetResult()
        {
            return _intComputer[0];
        }

        public long GetNoun()
        {
            return _intComputer[1];
        }

        public void SetNoun(long noun)
        {
            _intComputer[1] = noun;
        }

        public long GetVerb()
        {
            return _intComputer[2];
        }
        
        public void SetVerb(long verb)
        {
            _intComputer[2] = verb;
        }

        public long Run(bool debug = false)
        {
            return Run(new IntComputerInput(0), debug);
        }

        public long Run(long input, bool debug = false)
        {
            return Run(new IntComputerInput(input), debug);
        }

        public long Run(long[] input, bool debug = false)
        {
            return Run(new IntComputerInput(input), debug);
        }

        public long Run(IntComputerInput input, bool debug=false)
        {
            long position = 0;
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
                        SetOperand(modes[2],position++,result, debug);
                        break;
                    case 2:
                        if (debug) Console.WriteLine("instruction is multiplication");
                        result = GetOperand(modes[0], position++) * GetOperand(modes[1], position++);
                        SetOperand(modes[2],position++,result, debug);
                        break;
                    case 3:
                        if (debug) Console.WriteLine("instruction is input");
                        SetOperand(modes[0], position++, input.GetNextInput(), debug);
                        break;
                    case 4:
                        _outputValue = GetOperand(modes[0], position++);
                        if (debug) Console.WriteLine("instruction is output, value now: " + _outputValue);
                        Connector?.AddValue(_outputValue);
                        break;
                    case 5:
                        if (debug) Console.WriteLine("instruction is jump-if-true (non-zero)");
                        if (GetOperand(modes[0], position++) != 0)
                            position = GetOperand(modes[1], position);
                        else position++; //skip the second parameter
                        break;
                    case 6:
                        if (debug) Console.WriteLine("instruction is jump-if-false (zero)");
                        if (GetOperand(modes[0], position++) == 0)
                            position = GetOperand(modes[1], position);
                        else position++; //skip the second parameter
                        break;
                    case 7:
                        if (debug) Console.WriteLine("instruction is less-than");
                        result = GetOperand(modes[0], position++) < GetOperand(modes[1], position++) ? 1 : 0;
                        SetOperand(modes[2],position++,result, debug);
                        break;
                    case 8:
                        if (debug) Console.WriteLine("instruction is equals");
                        result = GetOperand(modes[0], position++) == GetOperand(modes[1], position++) ? 1 : 0;
                        SetOperand(modes[2],position++,result, debug);
                        break;
                    case 9:
                        if (debug) Console.WriteLine("instruction is adjust-relative-base");
                        _relativeBase += GetOperand(modes[0], position++);
                        break;
                    case 99:
                        if (debug) Console.WriteLine("instruction is exit");
                        return _outputValue;
                    default:
                        throw new InvalidOperationException("Unexpected instruction in intComputer at position " +
                                                            // position has already been incremented, so -1 to show original position
                                                            $"{position-1} value {instruction } with modes " +
                                                            string.Join(',', modes));
                }
                if (debug) Console.WriteLine($"Position: {position}, Relative Base {_relativeBase}, Computer: " + string.Join(",", _intComputer));
            }
        }

        private long GetOperand(long mode, long position)
        {
            if (mode < 0 || mode > 2)
                throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should be 0, 1 or 2");
            var location = position;
            switch (mode)
            {
                // mode 0 is position mode - dereference the position
                case 0:
                    location = (location >= _intComputer.Length) ? 0 : _intComputer[location];
                    return (location >= _intComputer.Length) ? 0 : _intComputer[location];
                // mode 1 is immediate mode - use the position
                case 1:
                    return (location >= _intComputer.Length) ? 0 : _intComputer[location];
                // mode 2 is relative mode - use the position offset by relative base
                case 2:
                    location = (location >= _intComputer.Length) ? 0 : _intComputer[location];
                    location += _relativeBase;
                    return (location >= _intComputer.Length) ? 0 : _intComputer[location];
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should be 0, 1 or 2");
            }
        }

        private void SetOperand(long mode, long position, long value, bool debug)
        {
            var location = position;
            if (mode == 1)
                throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should not be 1 when setting a value");
            if (mode < 0 || mode > 2)
                throw new ArgumentOutOfRangeException(nameof(mode), mode, "mode should be 0, 1 or 2");
            
            location = (location >= _intComputer.Length) ? 0 : _intComputer[location];
            if (mode == 2)
            {
                location += _relativeBase;
            }
            if (location >= _intComputer.Length)
            {
                if (debug) Console.WriteLine($"Resizing array to new size {location + 1}");
                Array.Resize(ref _intComputer, (int) (location + 1));
            }
            _intComputer[location] = value;
        }

        private static long[] ValidateModes(long modeLong)
        {
            if (modeLong < 0 || modeLong > 222)
                throw new ArgumentOutOfRangeException(nameof(modeLong), modeLong,
                    "Modes should not be less than 0 or greater than 222");
            var modeArray = new long[3];
            for (var i = 0; i < 3; i++)
            {
                modeArray[i] = modeLong % 10;
                if (modeArray[i] < 0 || modeArray[i] > 2)
                    throw new InvalidOperationException("Modes can only be 0, 1 or 2, found " + modeArray[i]);
                modeLong /= 10;
            }

            return modeArray;
        }

        public long GetOutput()
        {
            return _outputValue;
        }
    }
}