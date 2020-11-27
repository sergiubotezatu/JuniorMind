using System;
using System.Collections.Generic;
using System.Text;

namespace InterFace
{
    public class Choice : IPattern
    {
        private readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
           this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (IPattern pattern in this.patterns)
            {
                Match result = (Match)pattern.Match(text);
                if (result.Success())
                {
                    return result;
                }
            }

            return new Match(false, text);
        }
    }
}