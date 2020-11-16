using System;

namespace RangeClass
{
    public class Range
    {
        private char Start;
        private char End;

        public Range(char start, char end)
        {
            this.Start = start;
            this.End = end;
        }

        public bool Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return true;
        }
    }

    public class Program
    {
        static void Main()
        {
        }
    }
}
