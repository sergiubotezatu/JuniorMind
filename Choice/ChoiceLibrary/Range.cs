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
            if (string.IsNullOrEmpty(text))
            {
                return new Match(false, text);
            }

            return new Match(IsInRange(text[0]), text.Substring(1));
        }

        private bool IsInRange(char firstChar)
        {
            return firstChar >= this.start && firstChar <= this.end;
        }
    }
}

