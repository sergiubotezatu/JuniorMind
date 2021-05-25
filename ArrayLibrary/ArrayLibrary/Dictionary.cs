using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly int[] buckets;
        private Element<TKey, TValue>[] elements;

        public Dictionary(int horizontalLength)
        {
            this.buckets = new int[horizontalLength];
            PopulateArr(buckets);
            this.elements = new Element<TKey, TValue>[horizontalLength];
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count { get; private set; }

        public bool IsReadOnly => throw new NotImplementedException();

        private SLinkedList FreePositions { get; set; } = new SLinkedList();

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            int bucketPos = GetKeyElement(item.Key);
            int empty = FreePositions.FirstEmpty;
            EnsureCapacity();
            this.elements[this.Count] = new Element<TKey, TValue>(item)
            {
                Next = empty != -1 ? empty : this.buckets[bucketPos]
            };
            this.buckets[bucketPos] = this.Count;
            this.Count++;
            FreePositions.Remove();
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            if (!TryGetValue(item.Key, out TValue value))
            {
                return false;
            }

            return item.Value.Equals(value);
        }

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out _);
        }

        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            int bucketPos = GetKeyElement(item.Key);
            if (buckets[bucketPos] == -1)
            {
                return false;
            }

            int elementIndex = this.buckets[bucketPos];
            if (this.elements[elementIndex].Next != -1)
            {
                return RemoveFromHashElement(item, bucketPos);
            }

            if (!this.elements[elementIndex].Pair.Equals(item))
            {
                return false;
            }

            FreePositions.Add(buckets[bucketPos]);
            this.buckets[bucketPos] = -1;
            this.Count--;
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int bucketPos = GetKeyElement(key);
            value = default;
            if (this.buckets[bucketPos] == -1)
            {
                return false;
            }

            int bucketIndex = this.buckets[bucketPos];
            Element<TKey, TValue> toCheck = this.elements[bucketIndex];
            while (toCheck.Next != -1)
            {
                if (toCheck.Pair.Key.Equals(key))
                {
                    value = toCheck.Pair.Value;
                    return true;
                }

                toCheck = this.elements[toCheck.Next];
            }

            return toCheck.Pair.Key.Equals(key);
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.buckets.Length; i++)
            {
                Element<TKey, TValue> toEnum = this.elements[buckets[i]];
                while (toEnum.Next != -1)
                {
                    yield return toEnum.Pair;
                    toEnum = this.elements[toEnum.Next];
                }

                yield return toEnum.Pair;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void PopulateArr(int[] buckets)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }
        }

        private void EnsureCapacity()
        {
            if (this.Count >= this.elements.Length)
            {
                Array.Resize(ref this.elements, this.elements.Length + this.elements.Length);
            }
        }

        private int GetKeyElement(TKey key)
        {
            int output = key.GetHashCode();
            while (output > this.buckets.Length)
            {
                output %= this.buckets.Length;
            }

            return output;
        }

        private bool RemoveFromHashElement(KeyValuePair<TKey, TValue> item, int bucketPos)
        {
            int bucketIndex = this.buckets[bucketPos];
            int next = this.elements[bucketIndex].Next;
            int current = bucketIndex;
            if (this.elements[bucketIndex].Pair.Equals(item))
            {
                AddFreePosition(bucketIndex);
                this.buckets[bucketPos] = this.elements[bucketIndex].Next;
            }
            else
            {
                if (!SetCurrentAndNext(ref current, ref next, item))
                {
                    return false;
                }

                AddFreePosition(next);
                this.elements[current].Next = this.elements[next].Next;
            }

            this.Count--;
            return true;
        }

        private bool SetCurrentAndNext(ref int current, ref int next, KeyValuePair<TKey, TValue> item)
        {
            while (!this.elements[next].Equals(item))
            {
                Element<TKey, TValue> toCheck = this.elements[next];
                current = next;
                next = toCheck.Next;
                if (next == -1)
                {
                    return false;
                }
            }

            return true;
        }

        private void AddFreePosition(int index)
        {
            if (this.elements[index].Next != -1)
            {
                FreePositions.Add(index);
            }
        }
    }
}
