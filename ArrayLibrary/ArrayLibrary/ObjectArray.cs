using System;
using System.Collections;

namespace ArrayLibrary
{
    public class ObjectCollection : IEnumerable
    {
        private object[] array;

        public ObjectCollection()
        {
            int initialCapacity = 4;
            this.Count = 0;
            this.array = new object[initialCapacity];
        }

        public int Count { get; private set; }

        public object this[int index]
        {
            get => this.array[index];
            set => this.array[index] = value;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Add(object element)
        {
            EnsureCapacity();
            this.array[this.Count] = element;
            this.Count++;
        }

        public bool Contains(int element)
        {
            return !IndexOf(element).Equals(-1);
        }

        public int IndexOf(object element)
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

        public void Insert(int index, int element)
        {
            EnsureCapacity();
            MoveElementsToRight(index);
            this.array[index] = element;
        }

        public void Clear()
        {
            this.Count = 0;
        }

        public void Remove(int element)
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