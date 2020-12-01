using System;
using System.Collections.Generic;
using System.Threading;

namespace Day11
{
    public class IntComputerInput : IResultSink
    {
        private List<long> _input;
        private int _pointer = 0;
        long _blockedCount = 0;

        #region Constructors
        public IntComputerInput(long[] input)
        {
            _input = new List<long>(input);
        }

        public IntComputerInput() : this(0)
        {
        }

        public IntComputerInput(long value)
        {
            _input = new List<long>();
            _input.Add(value);
        }
        #endregion

        #region Methods
        public long GetNextInput()
        {
            if (_pointer > _input.Count)
            {
                throw new InvalidOperationException("pointer greater than input array size");
            }

            while (_pointer == _input.Count)
            {
                if (++_blockedCount % 1000 == 0) Console.WriteLine("  Thread blocked waiting ...");
                if (_blockedCount == 100000)//long.MaxValue)
                {
                    Console.WriteLine("Blocked waiting for input.");
                    throw new Exception("Blocked waiting for input for too many cycles");
                }
                Thread.Sleep(50);
            }
            return _input[_pointer++];
        }

        public void AddValue(long input)
        {
            _input.Add(input);
            _blockedCount = 0;
        }
        #endregion
    }
}