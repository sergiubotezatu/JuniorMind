using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Sequence : IPattern
    {
        IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            
            foreach (IPattern pattern in patterns)
            {
                if (!pattern.Match(text).Success())
                {
                    return pattern.Match(text);
                }

                text = text.Substring(1);
            }

            return new Match(true, text);
        }
    }
}
