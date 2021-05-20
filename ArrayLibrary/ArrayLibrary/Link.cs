using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    class Link
    {
        public int Value;
        internal Link Next;

        public Link(int value)
        {
            this.Value = value;
        }
    }
}
