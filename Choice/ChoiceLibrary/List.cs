using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class List : IPattern
    {
        private readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            Sequence listPattern = new Sequence(element, new Many(new Sequence(separator, element)));
            this.pattern = new Optional(listPattern);
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
