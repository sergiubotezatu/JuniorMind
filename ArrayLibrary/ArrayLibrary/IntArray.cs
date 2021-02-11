using System;

namespace ArrayLibrary
{
    public class IntArray
    {
        private int occupiedPos;
        private int[] array;

        public IntArray()
        {
            int initialCapacity = 4;
            this.occupiedPos = 0;
            this.array = new int[initialCapacity];
        }

        public void Add(int element)
        {
            if (occupiedPos == this.array.Length)
            {
                Array.Resize(ref this.array, this.array.Length + this.array.Length);
            }

            this.array[this.occupiedPos] = element;
            this.occupiedPos++;
        }

        public int Count()
        {
            return occupiedPos;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (index <= this.occupiedPos)
            {
                this.array[index] = element;
            }
        }

        public bool Contains(int element)
        {
            foreach (int digit in this.array)
            {
                if (digit == element)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(int element)
        {
            IntNumber searched = new IntNumber(element);
            for (int i = 0; i < this.array.Length; i++)
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
            if (index == this.occupiedPos)
            {
                Add(element);
            }

            if (index < this.occupiedPos)
            {
                InsertInFilledPosition(index, element);
            }
        }

        public void Clear()
        {
            this.occupiedPos = 0;
        }

        public void Remove(int element)
        {
            int toRemove = IndexOf(element);
            for (int i = toRemove; i < occupiedPos - 1; i++)
            {
                this.array[i] = this.array[i + 1];
            }

            occupiedPos--;
        }

        public void RemoveAt(int index)
        {
            while (index < occupiedPos - 1)
            {
                this.array[index] = this.array[index + 1];
                index++;
            }

            occupiedPos--;
        }

        public int GetLength()
        {
            return this.array.Length;
        }

        private void InsertInFilledPosition(int index, int element)
        {
            if (this.occupiedPos == this.array.Length)
            {
                Array.Resize(ref this.array, this.array.Length + this.array.Length);
            }

            MoveElementsToRight(index);
            this.array[index] = element;
        }

        private int IndexOfFirstNull(int index)
        {
            while (index < this.array.Length)
            {
                if (array[index] == null)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private void MoveElementsToRight(int lastElement)
        {
            int farRight = this.occupiedPos;
            while (farRight > lastElement)
            {
                this.array[farRight] = this.array[farRight - 1];
                farRight--;
            }
        }
    }
}
