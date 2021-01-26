using System;
using System.Collections.Generic;
using System.Text;
using ChoiceLibrary;

namespace InterFace
{
    public class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
           this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (IPattern pattern in this.patterns)
            {
                var result = pattern.Match(text);
                if (result.Success())
                {
                    return result;
                }
            }

            return new Match(false, text);
        }

        public void Add(IPattern patternToAdd)
        {
            Array.Resize(ref this.patterns, this.patterns.Length + 1);
            this.patterns[this.patterns.Length - 1] = patternToAdd;
        }
    }
}