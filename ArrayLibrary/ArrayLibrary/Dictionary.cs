using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private Buckets<TKey, TValue>[] buckets;

        public Dictionary(int horisontalLength)
        {
            this.buckets = new Buckets<TKey, TValue>[horisontalLength];
        }

        public ICollection<TKey> Keys => GetAllKeys();

        public ICollection<TValue> Values => GetAllValues();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> newPair = new KeyValuePair<TKey, TValue>(key, value);
            Add(newPair);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int arrayPos = HashIndex(item);
            if (this.buckets[arrayPos].FreePositions[0] == -1)
            {
                this.buckets[arrayPos].KeysValues.Add(item);
            }
            else
            {
                AddOnFreePositions(item, arrayPos);
            }

            this.Count++;
        }

        public void Clear()
        {
            this.buckets = new Buckets<TKey, TValue>[this.buckets.Length];
            this.Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = item.GetHash(this.buckets.Length);
            return this.buckets[index].Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            int index = GetKeyPosition(key);
            return buckets[index].Count > 0;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
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
                foreach (KeyValuePair<TKey, TValue> pair in this.buckets[i])
                {
                    array[arrayPositions] = pair;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                foreach (KeyValuePair<TKey, TValue> pair in this.buckets[i])
                {
                    yield return pair;
                }
            }
        }

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            this.buckets[GetKeyPosition(key)] = new Buckets<TKey, TValue>(key, default);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Contains(item))
            {
                return false;
            }

            int index = item.GetHash(this.buckets.Length);
            if (buckets[index].Count > 1)
            {
                AddNewEmptyPos(buckets[index], item);
            }

            this.buckets[index].Remove(item);
            this.Count--;
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = GetKeyPosition(key);
            if (this.buckets[index].Count > 1)
            {
                throw new InvalidOperationException("There Are multiple values using the specified key");
            }

            value = this.buckets[index].GetValue(key);
            return this.buckets[index].Count == 1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<TKey, TValue> newItem = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            Add(newItem);
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<TKey, TValue> searchedItem = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            return Contains(searchedItem);
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
                foreach (KeyValuePair<TKey, TValue> pair in this.buckets[i])
                {
                    array[arrayPositions] = new System.Collections.Generic.KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
                }
            }
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<TKey, TValue> searchedItem = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            return Remove(searchedItem);
        }

        IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>>)GetEnumerator();
        }

        private int HashIndex(KeyValuePair<TKey, TValue> bucket)
        {
            return bucket.GetHash(this.buckets.Length);
        }

        private void AddOnFreePositions(KeyValuePair<TKey, TValue> item, int position)
        {
            int foundFreePos = 0;
            foreach (KeyValuePair<TKey, TValue> element in this.buckets[position])
            {
                if (foundFreePos == this.buckets[position].FreePositions[0])
                {
                    buckets[position].AddBefore(element, item);
                }

                foundFreePos++;
            }

            this.buckets[position].FreePositions.Remove(foundFreePos);
            this.Count++;
        }

        private ICollection<TKey> GetAllKeys()
        {
            LinkedCollection<TKey> keys = new LinkedCollection<TKey>();
            foreach (Buckets<TKey, TValue> bucket in buckets)
            {
                keys.Add(bucket.GetBucketKey());
            }

            return keys;
        }

        private ICollection<TValue> GetAllValues()
        {
            LinkedCollection<TValue> values = new LinkedCollection<TValue>();
            foreach (Buckets<TKey, TValue> bucket in buckets)
            {
                foreach (var value in bucket.Values)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        private void AddNewEmptyPos(Buckets<TKey, TValue> bucket, KeyValuePair<TKey, TValue> item)
        {
                int newEmptyPos = 0;
                foreach (var element in bucket)
                {
                    if (element.Equals(item))
                    {
                        bucket.FreePositions.Insert(bucket.FreePositions.Length - 1, newEmptyPos);
                        break;
                    }

                    newEmptyPos++;
                }
        }

        private int GetKeyPosition(TKey key)
        {
            int index = key.GetHashCode();
            while (index >= this.buckets.Length)
            {
                index %= this.buckets.Length;
            }

            return index;
        }
    }
}
