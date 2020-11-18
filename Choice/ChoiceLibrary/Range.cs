using System;
using System.Collections.Generic;
using System.Text;

namespace InterFace
{
    public class Range : IPattern
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
}
