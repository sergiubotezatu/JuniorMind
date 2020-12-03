using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Optional : IPattern
    {
        readonly IPattern pattern;

        public Optional(IPattern Pattern)
        {
            this.pattern = Pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(true, this.pattern.Match(text).RemainingText());
        }
    }
}
