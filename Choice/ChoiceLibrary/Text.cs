using System;
using System.Collections.Generic;
using System.Text;
using InterFace;
using ChoiceLibrary;

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
            int prefixEnd = this.prefix.Length;
            return !string.IsNullOrEmpty(text) && text.StartsWith(this.prefix, StringComparison.CurrentCulture)
                ? new Match(true, text.Substring(prefixEnd))
                : new Match(false, text);
        }
    }
}
