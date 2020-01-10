using System;
using System.Text;

namespace BusyBeaver.BusyBeaverEngine
{
    public class Tape
    {
        private int _offset = 0;

        private bool[] _values;
        
        private int _tapeLength = 0;
        
        public const int TapeMargin = 1024;

        public Tape()
        {
            _values = new bool[TapeMargin];
            _offset = -TapeMargin / 2;
        }

        /// <summary>
        /// Expands the tape in a way that the set value is available
        /// </summary>
        /// <param name="position">Position to which the tape shall be extended</param>
        public void ExpandTape(int position)
        {
            if (IsAvailable(position) && _values != null)
            {
                return;
            }

            var globalLeftBoundary = Math.Min(_offset, position - TapeMargin);
            var globalRightBoundary = Math.Max((_values?.Length ?? 0) + _offset, position + TapeMargin);

            var newValues = new bool[globalRightBoundary - globalLeftBoundary];
            var newOffset = globalLeftBoundary;
            
            // Perform the copy
            if (_values != null)
            {
                var length = _values.Length;
                
                Array.Copy(
                    _values, 
                    0, 
                    newValues, 
                    _offset - newOffset,
                    length);
            }

            _values = newValues;
            _offset = newOffset;
            _tapeLength = _values.Length;
        }

        private bool IsAvailable(int position)
        {
            if (_values == null)
            {
                return false;
            }

            var localPosition = position - _offset;
            return localPosition >= 0 && localPosition < _tapeLength;
        }

        private int ConvertToGlobal(int position) => position - _offset;

        public bool GetValue(int position)
        {
            var globalPosition = ConvertToGlobal(position);
            if (globalPosition < 0 || globalPosition >= _tapeLength)
            {
                ExpandTape(position);
                globalPosition = ConvertToGlobal(position);
            }
            
            return _values![globalPosition];
        }

        public void SetValue(int position, bool value)
        {
            var globalPosition = ConvertToGlobal(position);
            if (globalPosition < 0 || globalPosition >= _tapeLength)
            {
                ExpandTape(position);
                globalPosition = ConvertToGlobal(position);
            }
            
            _values![globalPosition] = value;
        }

        public bool UpdateValue(in int position, State state)
        {
            var globalPosition = ConvertToGlobal(position);
            if (globalPosition < 0 || globalPosition >= _tapeLength)
            {
                ExpandTape(position);
                globalPosition = ConvertToGlobal(position);
            }

            var value = _values[globalPosition];
            _values[globalPosition] = _values[globalPosition] ? state.SetRule.SetValue :state.NotSetRule.SetValue;
            return value;
        }

        public void PrintOnConsole()
        {
            if (_values == null)
            {
                Console.WriteLine("-");
                return;
            }
            
            var builder = new StringBuilder();
            for (var n = 0; n < _values.Length; n++)
            {
                if (_values[n] == TapeValue.Set)
                {
                    builder.Append("1");
                }

                if (_values[n] == TapeValue.NotSet)
                {
                    builder.Append("0");
                }
            }
            
            Console.WriteLine(builder.ToString());
        }

        public void PrintPosition(int position)
        {
            if (_values == null)
            {
                Console.WriteLine("-");
                return;
            }
            
            var builder = new StringBuilder();
            var globalPosition = position - _offset;
            for (var n = 0; n < _values.Length; n++)
            {
                builder.Append(n == globalPosition ? "v" : " ");
            }
            
            Console.WriteLine(builder.ToString());
        }

        public int CountOnes()
        {
            var ones = 0;
            foreach (var t in _values)
            {
                ones += t ? 1 : 0;
            }

            return ones;
        }

        public int FirstOne()
        {
            for (var index = 0; index < _values.Length; index++)
            {
                if (_values[index]) return index + _offset;
            }

            return -1;
        }
        
        public int LastOne()
        {
            var lastOne = 0;
            for (var index = 0; index < _values.Length; index++)
            {
                if (_values[index]) lastOne = index;
            }

            return lastOne + _offset;
        }
    }
}