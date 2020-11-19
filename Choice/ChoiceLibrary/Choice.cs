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
            int lastChecked = 0;
            for (int i = 0; i < patterns.Length; i++) 
            {
                string toCheck = text.Substring(i);
                if (!patterns[i].Match(toCheck).Success())
                {
                   break;
                }
                    
                lastChecked = i;
            }

            Match result = (Match)patterns[lastChecked].Match(text.Substring(lastChecked));
            return result;
        }
    }    
}