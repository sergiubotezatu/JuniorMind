using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly Buckets<TKey, TValue>[] listOFBuckets;

        public Dictionary(int horisontalLength)
        {
            this.listOFBuckets = new Buckets<TKey, TValue>[horisontalLength];
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(key, value);
            Add(newItem);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int arrayPos = HashIndex(item);
            if (this.listOFBuckets[arrayPos].FreePositions[0] == -1)
            {
                this.listOFBuckets[arrayPos].Add(item);
            }
            else
            {
                this.listOFBuckets[arrayPos][this.listOFBuckets[arrayPos].FreePositions[0]] = item;
                this.listOFBuckets[arrayPos].FreePositions.Remove(this.listOFBuckets[arrayPos].FreePositions[0]);
            }

            this.Count++;
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            Add(newItem);
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = item.GetHash(this.listOFBuckets.Length);
            return this.listOFBuckets[index].Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            int index = GetKeyPosition(key);
            return listOFBuckets[index].Count > 0;
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this.listOFBuckets[GetKeyPosition(key)] = new Buckets<TKey, TValue>();
            return true;
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (!ContainsKey(key))
            {
                return false;
            }

            value = listOFBuckets[GetKeyPosition(key)][0].Value;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                foreach (KeyValuePair<TKey, TValue> element in this.listOFBuckets[i])
                {
                    yield return element;
                }
            }
        }

        private int HashIndex(KeyValuePair<TKey, TValue> bucket)
        {
            return bucket.GetHash(this.listOFBuckets.Length);
        }

        private int GetKeyPosition(TKey key)
        {
            int index = key.GetHashCode();
            while (index >= this.listOFBuckets.Length)
            {
                index %= this.listOFBuckets.Length;
            }

            return index;
        }
    }
}
