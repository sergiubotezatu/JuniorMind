using System;

namespace ArrayLibrary
{
    public class IntArray
    {
        private int count;
        private int[] array;

        public IntArray()
        {
            int initialCapacity = 4;
            this.count = 0;
            this.array = new int[initialCapacity];
        }

        public void Add(int element)
        {
            EnsureCapacity();
            this.array[this.count] = element;
            this.count++;
        }

        public int Count()
        {
            return count;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (index <= this.count)
            {
                this.array[index] = element;
            }
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < count; i++)
            {
                if (this.array[i] == element)
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
            this.count = 0;
        }

        public void Remove(int element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            MoveElementsToLeft(index);
            count--;
        }

        private void MoveElementsToRight(int lastElement)
        {
            int farRight = this.count;
            while (farRight > lastElement)
            {
                this.array[farRight] = this.array[farRight - 1];
                farRight--;
            }
        }

        private void MoveElementsToLeft(int index)
        {
            while (index < count - 1)
            {
                this.array[index] = this.array[index + 1];
                index++;
            }
        }

        private void EnsureCapacity()
        {
            if (this.count == this.array.Length)
            {
                Array.Resize(ref this.array, this.array.Length + this.array.Length);
            }
        }
    }
}
