using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using ChoiceLibrary;

namespace ChoiceLibrary
{
    public class Text :IPattern
    {
        readonly string prefix;

        public Text(string preFix)
        {
            this.prefix = preFix;
        }

        public IMatch Match(string text)
        {
            int prefixEnd = this.prefix.Length;
            return !string.IsNullOrEmpty(text) && IsSameAsPrefix(text, prefixEnd) ?
                new Match(true, text.Substring(prefixEnd)) : new Match(false, text);
        }

        private bool IsSameAsPrefix(string text, int prefixEnd)
        {
            if (prefixEnd > text.Length)
            {
                return false;
            }

            string toCheck = text.Substring(0, prefixEnd);
            return this.prefix == toCheck;
        }
    }
}
