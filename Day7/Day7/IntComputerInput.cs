namespace Day7
{
    internal class IntComputerInput
    {
        private long[] _input;
        private long _pointer = 0;

        public IntComputerInput(long[] input)
        {
            _input = input;
        }

        public IntComputerInput()
        {
            _input = new long[] {0};
        }

        public IntComputerInput(long value)
        {
            _input = new long[] {value};
        }

        public long GetNextInput()
        {
            return _pointer == (_input.Length-1) ? _input[_pointer] : _input[_pointer++];
        }
    }
}