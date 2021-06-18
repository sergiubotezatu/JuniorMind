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
            Array.Fill(buckets, -1);
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
            ThrowKeyIsNull(item.Key);
            if (GetKeyIndex(item.Key, out _) != -1)
            {
                throw new InvalidOperationException("The key of your item is already being used in this dictionary");
            }

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
            ThrowKeyIsNull(item.Key);
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
            if (this.Count > availableSpace)
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity. " +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }

            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(TKey key)
        {
            ThrowKeyIsNull(key);
            int keyIndex = GetKeyIndex(key, out int previous);
            if (keyIndex == -1)
            {
                return false;
            }

            RemoveAt(keyIndex, previous);
            return true;
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            ThrowKeyIsNull(item.Key);
            int itemIndex = GetKeyIndex(item.Key, out int previous);
            if (itemIndex == -1 || !this.elements[itemIndex].Pair.Equals(item))
            {
                return false;
            }

            RemoveAt(itemIndex, previous);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            ThrowKeyIsNull(key);
            int bucketPos = GetKeyBucket(key);
            value = default;
            for (int bucketIndex = this.buckets[bucketPos]; bucketIndex != -1; bucketIndex = this.elements[bucketIndex].Next)
            {
                if (this.elements[bucketIndex].Pair.Key.Equals(key))
                {
                    value = this.elements[bucketIndex].Pair.Value;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.buckets.Length; i++)
            {
                for (int index = this.buckets[i]; index != -1; index = this.elements[index].Next)
                {
                    yield return this.elements[index].Pair;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            output %= this.buckets.Length;
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
            ThrowKeyIsNull(key);
            if (!TryGetValue(key, out TValue value))
            {
                throw new KeyNotFoundException("The key used as index does not belong to this dictionary.");
            }

            return value;
        }

        private void SetIndexerValue(TKey key, TValue value)
        {
            ThrowKeyIsNull(key);
            int keyIndex = GetKeyIndex(key, out _);
            if (keyIndex == -1)
            {
                AddOn(keyIndex, new KeyValuePair<TKey, TValue>(key, value));
            }

            int next = this.elements[keyIndex].Next;
            this.elements[keyIndex].Pair = new KeyValuePair<TKey, TValue>(key, value);
            this.elements[keyIndex].Next = next;
        }

        private int GetKeyIndex(TKey key, out int previous)
        {
            previous = -1;
            for (int current = this.buckets[GetKeyBucket(key)]; current != -1; current = this.elements[current].Next)
            {
                if (this.elements[current].Pair.Key.Equals(key))
                {
                    return current;
                }

                previous = current;
            }

            return this.elements[previous].Next;
        }

        private void RemoveAt(int index, int prev)
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
        }

        private void ThrowKeyIsNull(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException($"{nameof(key)} is null");
            }
        }
    }
}
