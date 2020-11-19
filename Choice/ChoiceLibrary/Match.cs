using System;
using System.Collections.Generic;
using System.Text;

namespace InterFace
{
    public class Match : IMatch
    {
        bool match;
        string text;

        public Match(bool match, string text)
        {
            this.match = match;
            this.text = text;
        }
        public bool Success()
        {
            return this.match;
        }

        public string RemainingText()
        {
            return this.text;
        }
    }
}
