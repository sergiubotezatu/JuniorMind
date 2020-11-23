using System;
using System.Collections.Generic;
using System.Text;

namespace InterFace
{
    public class Character : IPattern
    {
        readonly char pattern;

        public Character(char pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Match(false, text);
            }

            string remainder = text[0] == pattern ? text.Substring(1) : text;
            return new Match(text[0] == pattern, remainder);
        }
    }
}
