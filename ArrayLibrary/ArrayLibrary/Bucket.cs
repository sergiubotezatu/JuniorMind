﻿using System;
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
        public KeyValuePair<TKey, TValue>[] Bucket;
        internal ListCollection<int> FreePositions;

        public Buckets()
        {
            int initiCapacity = 1;
            this.FreePositions = new ListCollection<int>() { -1 };
            this.Bucket = new KeyValuePair<TKey, TValue>[initiCapacity];
        }

        public int Count { get; private set; }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        internal KeyValuePair<TKey, TValue> this[int index]
        {
            get => this.Bucket[index];
            set => this.Bucket[index] = value;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            foreach (KeyValuePair<TKey, TValue> element in this)
            {
                if (item.Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Parameter can not be less than 0");
            }

            int availableSpace = array.Length - index;
            if (availableSpace >= this.Count)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    Bucket[index] = this.Bucket[i];
                    index++;
                }
            }
            else
            {
                throw new ArgumentException(
                    "Available space in destination array starting from index is smaller than the source list capacity" +
                    $"You need minimum {this.Count - 1} more positions after your index");
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return Bucket[i];
            }
        }

        internal void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Bucket[this.Count] = item;
            this.Count++;
        }

        internal void Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = -1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this.Bucket[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            RemoveAt(index);
        }

        internal void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                return;
            }

            AddNewEmptyPos(index);
            while (index < this.Count - 1)
            {
                this.Bucket[index] = this.Bucket[index + 1];
                index++;
            }

            this.Count--;
        }

        internal void Clear()
        {
            this.Count = 0;
            this.FreePositions = new ListCollection<int> { -1 };
        }

        private void AddNewEmptyPos(int newEmpty)
        {
            FreePositions[FreePositions.Count - 1] = newEmpty;
            FreePositions.Add(-1);
        }
    }
}
