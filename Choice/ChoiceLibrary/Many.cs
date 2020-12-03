using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Many :IPattern
    {
        IPattern pattern;

        public Many(IPattern Pattern)
        {
            this.pattern = Pattern;
        }

        public IMatch Match(string text)
        {
            return string.IsNullOrEmpty(text)
                ? new Match(true, text)
                : GetRedundantText(text);
        }

        private Match GetRedundantText(string text)
        {
            IMatch result = new Match(true, text);
            foreach (char element in text)
            {
                result = this.pattern.Match(result.RemainingText());
                if (!result.Success())
                {
                    return new Match(true, result.RemainingText());
                }
            }

            return new Match(true, string.Empty);
        }
    }
}
