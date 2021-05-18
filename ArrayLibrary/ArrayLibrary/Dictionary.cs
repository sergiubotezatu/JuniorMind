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

        public ICollection<TKey> Keys => GetKeysCollection();

        public ICollection<TValue> Values => GetValuesCollection();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => GetValue(key);
            set
            {
                EditSpecifiedValue(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(key, value);
            Add(newItem);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int arrayPos = HashIndex(item);
            if (ContainsKey(item.Key))
            {
                throw new InvalidOperationException("This key is already used in this collection");
            }

            if (this.listOFBuckets[arrayPos].FreePositions[0] == -1)
            {
                Buckets<TKey, TValue> toAdd = new Buckets<TKey, TValue>();
                toAdd.Add(item);
                this.listOFBuckets[arrayPos] = toAdd;
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
            foreach (Buckets<TKey, TValue> bucket in this.listOFBuckets)
            {
                bucket.FreePositions = new ListCollection<int> { -1 };
            }

            this.Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = item.GetHash(this.listOFBuckets.Length);
            return this.listOFBuckets[index].Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return TryFindKey(key, out _);
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            return Contains(new KeyValuePair<TKey, TValue>(item.Key, item.Value));
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
            if (availableSpace < this.Count)
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity" +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }

            int arrayPositions = 0;
            for (int i = 0; i < this.Count; i++)
            {
                foreach (KeyValuePair<TKey, TValue> pair in this.listOFBuckets[i])
                {
                    array[arrayPositions] = new System.Collections.Generic.KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
                }
            }
        }

        IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>>)GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                foreach (KeyValuePair<TKey, TValue> pair in this.listOFBuckets[i])
                {
                    yield return pair;
                }
            }
        }

        public bool Remove(TKey key)
        {
            if (!TryFindKey(key, out int index))
            {
                return false;
            }

            this.listOFBuckets[GetKeyPosition(key)].RemoveAt(index);
            this.Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Contains(item))
            {
                return false;
            }

            return Remove(item.Key);
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            Buckets<TKey, TValue> searchedBucket = listOFBuckets[GetKeyPosition(key)];
            if (!TryFindKey(key, out int index))
            {
                return false;
            }

            value = searchedBucket[index].Value;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        private bool TryFindKey(TKey key, out int keyPos)
        {
            Buckets<TKey, TValue> container = listOFBuckets[GetKeyPosition(key)];
            keyPos = -1;
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].Key.Equals(key))
                {
                    keyPos = i;
                    return true;
                }
            }

            return false;
        }

        private TValue GetValue(TKey key)
        {
            if (!TryGetValue(key, out TValue value))
            {
                throw new InvalidOperationException("The searched key is not used in this collection");
            }

            return value;
        }

        private void EditSpecifiedValue(TKey key, TValue value)
        {
            int position = GetKeyPosition(key);
            if (TryFindKey(key, out int index))
            {
                this.listOFBuckets[position][index] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                Add(new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        private ICollection<TKey> GetKeysCollection()
        {
            ListCollection<TKey> keys = new ListCollection<TKey>();
            foreach (var item in this)
            {
                keys.Add(item.Key);
            }

            return keys;
        }

        private ICollection<TValue> GetValuesCollection()
        {
            ListCollection<TValue> values = new ListCollection<TValue>();
            foreach (var item in this)
            {
                values.Add(item.Value);
            }

            return values;
        }
    }
}
