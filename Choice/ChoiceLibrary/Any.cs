using System;
using System.Collections.Generic;
using System.Text;
using InterFace;

namespace ChoiceLibrary
{
    public class Any : IPattern
    {
        string accepted;

        public Any(string Accepted)
        {
            this.accepted = Accepted;
        }

        public IMatch Match(string text)
        {

        }
    }
}
