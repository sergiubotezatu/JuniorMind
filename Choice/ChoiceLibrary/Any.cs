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
                this.IsOneOfAny(text) ? new Match(true, text.Substring(1)) :
                new Match(false, text);
        }

        private bool IsOneOfAny(string text)
        {
            foreach (char element in this.accepted)
            {
                if (element == text[0])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
