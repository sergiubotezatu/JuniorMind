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
        private int freeIndex;

        public Dictionary(int horizontalLength)
        {
            this.buckets = new int[horizontalLength];
            PopulateArr(buckets);
            this.elements = new Element<TKey, TValue>[horizontalLength];
            this.freeIndex = -1;
        }

        public ICollection<TKey> Keys => GetKeys();

        public ICollection<TValue> Values => GetValues();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => GetIndexerValue(key);
            set => SetIndexerValue(key, value);
        }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            int actualPosition;
            if (freeIndex != -1)
            {
                actualPosition = freeIndex;
                freeIndex = this.elements[freeIndex].Next;
            }
            else
            {
                actualPosition = this.Count;
            }

            EnsureCapacity();
            AddOn(actualPosition, item);
            this.Count++;
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

            return value.Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return GetKeyIndex(key, out _) != -1;
        }

        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Parameter can not be less than 0");
            }

            int availableSpace = array.Length - arrayIndex;
            if (this.Count <= availableSpace)
            {
                foreach (var item in this)
                {
                    array[arrayIndex] = item;
                    arrayIndex++;
                }
            }
            else
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity. " +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }
        }

        public bool Remove(TKey key)
        {
            int keyIndex = GetKeyIndex(key, out int previous);
            if (keyIndex == -1)
            {
                return false;
            }

            return RemoveAt(keyIndex, previous);
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            int itemIndex = GetKeyIndex(item.Key, out int previous);
            if (itemIndex == -1 || !this.elements[itemIndex].Pair.Equals(item))
            {
                return false;
            }

            return RemoveAt(itemIndex, previous);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int bucketPos = GetKeyBucket(key);
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

        private int GetKeyBucket(TKey key)
        {
            int output = key.GetHashCode();
            while (output >= this.buckets.Length)
            {
                output %= this.buckets.Length;
            }

            return output;
        }

        private void AddOn(int position, System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            int bucketPos = GetKeyBucket(item.Key);
            this.elements[position] = new Element<TKey, TValue>(item)
            {
                Next = this.buckets[bucketPos]
            };

            this.buckets[bucketPos] = position;
        }

        private ListCollection<TKey> GetKeys()
        {
            ListCollection<TKey> list = new ListCollection<TKey>();
            foreach (var element in this)
            {
                list.Add(element.Key);
            }

            return list;
        }

        private ListCollection<TValue> GetValues()
        {
            ListCollection<TValue> list = new ListCollection<TValue>();
            foreach (var element in this)
            {
                list.Add(element.Value);
            }

            return list;
        }

        private TValue GetIndexerValue(TKey key)
        {
            if (!TryGetValue(key, out TValue value))
            {
                throw new InvalidOperationException("The key used as index does not belong to this dictionary.");
            }

            return value;
        }

        private void SetIndexerValue(TKey key, TValue value)
        {
            int keyIndex = GetKeyIndex(key, out _);
            if (keyIndex == -1)
            {
                throw new InvalidOperationException("The key of which value you are trying to set does not belong to this dictionary.");
            }

            this.elements[keyIndex] = new Element<TKey, TValue>(new KeyValuePair<TKey, TValue>(key, value));
        }

        private int GetKeyIndex(TKey key, out int previous)
        {
            int current = this.buckets[GetKeyBucket(key)];
            previous = -1;
            if (current == -1)
            {
                return current;
            }

            while (!this.elements[current].Pair.Key.Equals(key))
            {
                previous = current;
                current = this.elements[current].Next;
                if (current == -1)
                {
                    return current;
                }
            }

            return current;
        }

        private bool RemoveAt(int index, int prev)
        {
            int bucketPos = GetKeyBucket(this.elements[index].Pair.Key);
            if (prev == -1)
            {
                this.buckets[bucketPos] = this.elements[index].Next;
            }
            else
            {
                this.elements[prev].Next = this.elements[index].Next;
            }

            this.elements[index].Next = freeIndex;
            freeIndex = index;
            this.Count--;
            return true;
        }
    }
}
