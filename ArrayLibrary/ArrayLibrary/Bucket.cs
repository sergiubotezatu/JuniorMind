using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public System.Collections.Generic.KeyValuePair<TKey, TValue> ToCollectionsGeneric()
        {
            System.Collections.Generic.KeyValuePair<TKey, TValue> converted = new System.Collections.Generic.KeyValuePair<TKey, TValue>(this.Key, this.Value);
            return converted;
        }

        public int GetHash(int horizontalLength)
        {
            int hash = this.Key.GetHashCode();
            while (hash >= horizontalLength)
            {
                hash %= horizontalLength;
            }

            return hash;
        }
    }

    class Bucket<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        public KeyValuePair<TKey, TValue> Pair;
        internal int Next;

        public Bucket(KeyValuePair<TKey, TValue> pair)
        {
            this.Pair = pair;
            this.Next = -1;
        }
    }
}
