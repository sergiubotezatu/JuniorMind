using System;

namespace ArrayLibrary
{
    public class IntArray
    {
        protected int[] array;

        public IntArray()
        {
            int initialCapacity = 4;
            this.Count = 0;
            this.array = new int[initialCapacity];
        }

        public int Count { get; private set; }

        public int this[int index]
        {
            get => this.array[index];
            set => this.array[index] = value;
        }

        public virtual void Add(int element)
        {
            EnsureCapacity();
            this.array[this.Count] = element;
            this.Count++;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this.array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void Insert(int index, int element)
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
