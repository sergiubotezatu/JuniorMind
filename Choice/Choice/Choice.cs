using System;
using System.Collections.Generic;
using System.Text;

namespace ChoiceClass
{
    public class Choice : IPattern
    {
        IPattern[] patterns;
        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public bool Match(string text)
        {
            foreach (var pattern in patterns)
            {
                if (pattern.Match(text))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Program
    {
        static void Main()
        {
        }
    }
}
