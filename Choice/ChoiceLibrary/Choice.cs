using System;
using System.Collections.Generic;
using System.Text;

namespace InterFace
{

    public class Choice : IPattern
    {
        IPattern[] patterns;
        public Choice(params IPattern[] patterns)
        {
           this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            bool matchResult = false;
            foreach (IPattern pattern in patterns)
            {
                if (!pattern.Match(text).Success())
                {
                    matchResult = false;
                    text = pattern.Match(text).RemainingText();
                    break;
                }

                matchResult = true;
                text = pattern.Match(text).RemainingText();
            }

            return new Match(matchResult, text);
        }
    }    
}