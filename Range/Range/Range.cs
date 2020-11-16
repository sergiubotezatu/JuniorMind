using System;

namespace RangeClass
{
    public class Range
    {
        private readonly char start;
        private readonly char end;

        public Range(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public bool Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return this.IsInRange(text[0]);
        }

        private bool IsInRange(char firstChar)
        {
            return firstChar >= this.start && firstChar <= this.end;
        }
    }

    public class Program
    {
        static void Main()
        {
        }
    }
}
