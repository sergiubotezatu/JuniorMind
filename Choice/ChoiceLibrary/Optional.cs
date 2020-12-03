using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Optional : IPattern
    {
        IPattern pattern;

        public Optional(IPattern Pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(true, text);
        }
    }
}
