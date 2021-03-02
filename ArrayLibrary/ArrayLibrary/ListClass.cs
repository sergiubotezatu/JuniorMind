using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class List<T>
    {
        protected T[] array;

        public List()
        {
            int initialCapacity = 4;
            this.Count = 0;
            this.array = new T[initialCapacity];
        }

        public int Count { get; private set; }

        public virtual T this[int index]
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

        public virtual void Add(T element)
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

        public virtual void Insert(int index, T element)
        {
            EnsureCapacity();
            MoveElementsToRight(index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public void Remove(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            MoveElementsToLeft(index);
            Count--;
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
