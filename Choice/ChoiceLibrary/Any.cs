using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Any : IPattern
    {
        readonly string accepted;

        public Any(string Accepted)
        {
            this.accepted = Accepted;
        }

        public IMatch Match(string text)
        {
            return !string.IsNullOrEmpty(text) &&
                this.accepted.Contains(text[0].ToString()) ? new Match(true, text.Substring(1)) :
                new Match(false, text);
        }
    }
}
