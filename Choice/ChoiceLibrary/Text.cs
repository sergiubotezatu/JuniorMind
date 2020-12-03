using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Text :IPattern
    {
        readonly string prefix;

        public Text(string preFix)
        {
            this.prefix = preFix;
        }

        public IMatch Match(string text)
        {
            return new Match(false, text);
        }
    }
}
