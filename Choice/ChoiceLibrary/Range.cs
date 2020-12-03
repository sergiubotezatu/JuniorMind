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

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) && this.IsInRange(text[0])
                ? new Match(true, text.Substring(1))
                : new Match(false, text);
        }

        private bool IsInRange(char firstChar)
        {
            return firstChar >= this.start && firstChar <= this.end;
        }
    }
}

