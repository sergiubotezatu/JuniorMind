using System;

namespace ArrayLibrary
{
    public class IntArray
    {
        private IntNumber[] array;

        public IntArray()
        {
            int initialCapacity = 4;
            this.array = new IntNumber[initialCapacity];
        }

        public void Add(int element)
        {
            int initialCapacity = this.array.Length;
            int filledPositions = 0;
            for (int i = 0; i < initialCapacity; i++)
            {
                if (array[i] == null)
                {
                    this.array[i] = new IntNumber(element);
                    break;
                }
                else
                {
                    filledPositions++;
                }
            }

            if (filledPositions == initialCapacity)
            {
                Array.Resize(ref this.array, this.array.Length + initialCapacity);
                this.array[initialCapacity] = new IntNumber(element);
            }
        }

        public int Count()
        {
            int counter = 0;
            foreach (IntNumber element in this.array)
            {
                if (element != null)
                {
                    counter++;
                }
            }

            return counter;
        }

        public int Element(int index)
        {
            return array[index].GetValue();
        }

        public void SetElement(int index, int element)
        {
            array[index] = new IntNumber(element);
        }

        public bool Contains(int element)
        {
            IntNumber searched = new IntNumber(element);
            foreach (IntNumber digit in this.array)
            {
                if (searched.IsEqualTo(digit))
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
                if (searched.IsEqualTo(this.array[i]))
                {
                     return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            if (this.array[index] == null)
            {
                SetElement(index, element);
            }
            else
            {
                InsertInFilledPosition(index, element);
            }
        }

        public void Clear()
        {
            int capacity = this.array.Length;
            Array.Resize(ref this.array, 0);
            Array.Resize(ref this.array, capacity);
        }

        public void Remove(int element)
        {
            int searched = IndexOf(element);
            this.array[searched] = null;
        }

        public void RemoveAt(int index)
        {
            this.array[index] = null;
        }

        public int GetLength()
        {
            return this.array.Length;
        }

        private void InsertInFilledPosition(int index, int element)
        {
            int posNotUsed = IndexOfFirstNull(index);
            if (posNotUsed != -1)
            {
                MoveElementsToRight(posNotUsed, index);
            }
            else
            {
                int initialCapacity = this.array.Length;
                Array.Resize(ref this.array, this.array.Length + initialCapacity);
                MoveElementsToRight(initialCapacity, index);
            }

            this.array[index] = new IntNumber(element);
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

        private void MoveElementsToRight(int farRight, int lastElement)
        {
            while (farRight > lastElement)
            {
                this.array[farRight] = this.array[farRight - 1];
                farRight--;
            }
        }
    }
}
