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

            return this.AreCharsInRange(text);
        }

        private bool AreCharsInRange(string text)
        {
            foreach (char element in text)
            {
                if (element < this.Start || element > this.End)
                {
                    return false;
                }
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
