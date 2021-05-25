using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Element<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        public KeyValuePair<TKey, TValue> Pair;
        internal int Next;

        public Element(KeyValuePair<TKey, TValue> pair)
        {
            this.Pair = pair;
            this.Next = -1;
        }
    }
}
