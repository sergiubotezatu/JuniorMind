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
            bool correctMAtch = true;
            foreach (IPattern pattern in patterns)
            {
                if (!pattern.Match(text).Success())
                {
                    correctMAtch = false;
                    text = pattern.Match(text).RemainingText();
                    return new Match(correctMAtch, pattern.Match(text).RemainingText());
                }

                text = pattern.Match(text).RemainingText();
            }

            return new Match(correctMAtch, text);
        }
    }    
}