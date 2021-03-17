using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class List<T>
    {
        private T[] array;

        public List()
        {
            int initialCapacity = 4;
            this.Count = 0;
            this.array = new T[initialCapacity];
        }

        public int Count { get; private set; }

        public int Length { get => this.array.Length; }

        public T this[int index]
        {
            get => this[index] = this.array[index];

            set
            {
                CheckForInvalidIndex(index);
                this.array[index] = value;
            }
        }

        public IEnumerable Enumerate()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }

        public void Add(T element)
        {
            EnsureCapacity();
            this.array[this.Count] = element;
            this.Count++;
        }

        public bool Contains(T element)
        {
            return !IndexOf(element).Equals(-1);
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this.array[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T element)
        {
            CheckForInvalidIndex(index);
            EnsureCapacity();
            MoveElementsToRight(index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public void RemoveAt(int index)
        {
            CheckForInvalidIndex(index);
            MoveElementsToLeft(index);
            Count--;
        }

        public void CopyTo(T[] array, int index)
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
                    array[index] = this.array[i];
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

        public bool Remove(T element)
        {
            int itemPos = IndexOf(element);
            int newCount = this.Count - 1;
            if (itemPos == -1)
            {
                return false;
            }

            if (itemPos == this.Count - 1)
            {
                RemoveAt(itemPos);
                return itemPos == this.Count;
            }

            T replacer = array[itemPos + 1];
            RemoveAt(IndexOf(element));
            return this.Count == newCount && this.array[itemPos].Equals(replacer);
        }

        public void CheckForInvalidIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                string reason =
                    index < 0 ? "Parameter can not be less than 0" :
                    "Parameter can not be greater than list capacity";
                throw new ArgumentOutOfRangeException(nameof(index), reason);
            }
        }

        private void MoveElementsToRight(int lastElement)
        {
            int farRight = this.Count;
            while (farRight > lastElement)
            {
                this.array[farRight] = this.array[farRight - 1];
                farRight--;
            }

            this.Count++;
        }

        private void MoveElementsToLeft(int index)
        {
            while (index < Count - 1)
            {
                this.array[index] = this.array[index + 1];
                index++;
            }
        }

        private void EnsureCapacity()
        {
            if (this.Count == this.array.Length)
            {
                Array.Resize(ref this.array, this.array.Length + this.array.Length);
            }
        }
    }
}