using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly int[] indexes;
        private Bucket<TKey, TValue>[] buckets;

        public Dictionary(int horizontalLength)
        {
            int initLength = 5;
            this.indexes = new int[horizontalLength];
            PopulateArr(indexes);
            this.buckets = new Bucket<TKey, TValue>[initLength];
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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int bucketPos = item.GetHash(this.indexes.Length);
            int empty = FreePositions.FirstEmpty;
            this.buckets[this.Count] = new Bucket<TKey, TValue>(item)
            {
                Next = empty != -1 ? empty : this.indexes[bucketPos]
            };
            this.indexes[bucketPos] = this.Count;
            this.Count++;
            EnsureCapacity();
            FreePositions.Remove();
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            Add(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (!TryGetValue(item.Key, out TValue value))
            {
                return false;
            }

            return item.Value.Equals(value);
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            return Contains(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
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

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int bucketPos = item.GetHash(this.indexes.Length);
            if (indexes[bucketPos] == -1)
            {
                return false;
            }

            if (this.buckets[bucketPos].Next != -1)
            {
                return RemoveFromHashBucket(item, bucketPos);
            }

            if (!this.buckets[bucketPos].Pair.Equals(item))
            {
                return false;
            }

            this.indexes[bucketPos] = -1;
            this.Count--;
            return true;
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int bucketPos = GetKeyBucket(key);
            value = default;
            if (this.indexes[bucketPos] == -1)
            {
                return false;
            }

            int bucketIndex = this.indexes[bucketPos];
            Bucket<TKey, TValue> toCheck = this.buckets[bucketIndex];
            while (toCheck.Next != -1)
            {
                if (toCheck.Pair.Key.Equals(key))
                {
                    value = toCheck.Pair.Value;
                    return true;
                }

                toCheck = this.buckets[toCheck.Next];
            }

            return toCheck.Pair.Key.Equals(key);
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.indexes.Length; i++)
            {
                Bucket<TKey, TValue> toEnum = this.buckets[indexes[i]];
                while (toEnum.Next != -1)
                {
                    yield return toEnum.Pair.ToCollectionsGeneric();
                    toEnum = this.buckets[toEnum.Next];
                }

                yield return toEnum.Pair.ToCollectionsGeneric();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.indexes.Length; i++)
            {
                Bucket<TKey, TValue> toEnum = this.buckets[indexes[i]];
                while (toEnum.Next != -1)
                {
                    yield return toEnum.Pair;
                    toEnum = this.buckets[toEnum.Next];
                }

                yield return toEnum.Pair;
            }
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
            if (this.Count >= this.buckets.Length)
            {
                Array.Resize(ref this.buckets, this.buckets.Length + this.buckets.Length);
            }
        }

        private int GetKeyBucket(TKey key)
        {
            int output = key.GetHashCode();
            while (output > this.indexes.Length)
            {
                output %= this.indexes.Length;
            }

            return output;
        }

        private bool RemoveFromHashBucket(KeyValuePair<TKey, TValue> item, int bucketPos)
        {
            int bucketIndex = this.indexes[bucketPos];
            int next = this.buckets[bucketIndex].Next;
            int current = bucketIndex;
            if (this.buckets[bucketIndex].Pair.Equals(item))
            {
                AddFreePosition(bucketIndex);
                this.indexes[bucketPos] = this.buckets[bucketIndex].Next;
            }
            else
            {
                if (!SetCurrentAndNext(ref current, ref next, item))
                {
                    return false;
                }

                AddFreePosition(next);
                this.buckets[current].Next = this.buckets[next].Next;
            }

            this.Count--;
            return true;
        }

        private bool SetCurrentAndNext(ref int current, ref int next, KeyValuePair<TKey, TValue> item)
        {
            while (!this.buckets[next].Equals(item))
            {
                Bucket<TKey, TValue> toCheck = this.buckets[next];
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
            if (this.buckets[index].Next != -1)
            {
                FreePositions.Add(index);
            }
        }
    }
}
