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
            get => this.array[index];
            set => this.array[index] = value;
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
            EnsureCapacity();
            MoveElementsToRight(index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public void Delete(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            MoveElementsToLeft(index);
            Count--;
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
            {
                return;
            }

            int minimumLength = array.Length + this.Count;
            if (array.Length >= minimumLength)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    array[index] = this.array[i];
                    index++;
                }
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

            if (this.Count == 1)
            {
                Delete(element);
                return this.Count == 0;
            }

            Delete(element);
            T replacer = GetReplacer(itemPos);
            return this.Count == newCount && this.array[itemPos].Equals(replacer);
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

        private T GetReplacer(int itemPos)
        {
            if (itemPos + 1 < this.Count)
            {
                return this.array[itemPos + 1];
            }

            return this.array[itemPos - 1];
        }
    }
}