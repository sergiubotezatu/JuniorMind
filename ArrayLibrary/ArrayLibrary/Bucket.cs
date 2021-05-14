using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public struct KeyValuePair<TKey, TValue>
    {
        public readonly TKey Key;
        public readonly TValue Value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
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

    class Buckets<TKey, TValue> : ICollection
        where TKey : IEquatable<TKey>
    {
        public LinkedCollection<KeyValuePair<TKey, TValue>> KeysValues;
        internal ListCollection<int> FreePositions;

        public Buckets(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> bucket = new KeyValuePair<TKey, TValue>(key, value);
            KeysValues = new LinkedCollection<KeyValuePair<TKey, TValue>>() { bucket };
            FreePositions = new ListCollection<int>() { -1 };
        }

        public int Count => this.KeysValues.Count;

        public ICollection<TValue> Values => GetAllValues();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public void CopyTo(Array array, int index)
        {
            KeysValues.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.KeysValues.GetEnumerator();
        }

        internal void Add(KeyValuePair<TKey, TValue> newPair)
        {
            KeysValues.Add(new Node<KeyValuePair<TKey, TValue>>(newPair));
        }

        internal void AddBefore(KeyValuePair<TKey, TValue> after, KeyValuePair<TKey, TValue> newPair)
        {
            KeysValues.AddBefore(KeysValues.Find(after), new Node<KeyValuePair<TKey, TValue>>(newPair));
        }

        internal void AddAfter(KeyValuePair<TKey, TValue> before, KeyValuePair<TKey, TValue> newPair)
        {
            KeysValues.AddBefore(KeysValues.Find(before), new Node<KeyValuePair<TKey, TValue>>(newPair));
        }

        internal void Remove(KeyValuePair<TKey, TValue> toRemove)
        {
            int emptyPos = 0;
            foreach (KeyValuePair<TKey, TValue> bucket in KeysValues)
            {
                if (bucket.Value.Equals(toRemove))
                {
                    KeysValues.Remove(toRemove);
                    break;
                }

                emptyPos++;
            }

            FreePositions.Add(emptyPos);
        }

        internal bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return KeysValues.Contains(item);
        }

        internal TValue GetValue(TKey key)
        {
            TValue value = default;
            foreach (KeyValuePair<TKey, TValue> bucket in this)
            {
                if (bucket.Key.Equals(key))
                {
                    value = bucket.Value;
                    break;
                }
            }

            return value;
        }

        internal TKey GetBucketKey()
        {
            return this.KeysValues.GetFirstElement().Value.Key;
        }

        private ICollection<TValue> GetAllValues()
        {
            LinkedCollection<TValue> values = new LinkedCollection<TValue>();
            foreach (KeyValuePair<TKey, TValue> bucket in this)
            {
                values.Add(bucket.Value);
            }

            return values;
        }

        private void Testare()
        {
            var test = new Dictionary<TKey, TValue>(5);
        }
    }
}
