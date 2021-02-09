using System;

namespace ArrayLibrary
{
    public class IntArray
    {
        private string[] array;

        public IntArray()
        {
            int initialCapacity = 4;
            this.array = new string[initialCapacity];
        }

        public void Add(int element)
        {
            int initialCapacity = this.array.Length;
            int filledPositions = 0;
            for (int i = 0; i < initialCapacity; i++)
            {
                if (!int.TryParse(array[i], out int value))
                {
                    this.array[i] = element.ToString();
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
                this.array[initialCapacity] = element.ToString();
            }
        }

        public int Count()
        {
            int counter = 0;
            foreach (string element in this.array)
            {
                if (int.TryParse(element, out int value))
                {
                    counter++;
                }
            }

            return counter;
        }

        public int Element(int index)
        {
            return Convert.ToInt32(array[index]);
        }

        public void SetElement(int index, int element)
        {
            array[index] = element.ToString();
        }

        public bool Contains(int element)
        {
            string searched = element.ToString();
            foreach (string digit in this.array)
            {
                if (searched == digit)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(int element)
        {
            string searched = element.ToString();
            for (int i = 0; i < this.array.Length; i++)
            {
                if (searched == this.array[i])
                {
                     return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            if (string.IsNullOrEmpty(this.array[index]))
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
            this.array[searched] = "";
        }

        public void RemoveAt(int index)
        {
            this.array[index] = "";
        }

        public int GetLength()
        {
            return this.array.Length;
        }

        private void InsertInFilledPosition(int index, int element)
        {
            Array.Resize(ref this.array, this.array.Length + 1);
            int newPosition = this.array.Length;
            int nextDigit = 2;
            while (newPosition > index)
            {
                array[newPosition - 1] = array[newPosition - nextDigit];
                newPosition--;
            }

            SetElement(index, element);
        }
    }
}
