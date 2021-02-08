using System;

namespace ClassLibrary1
{
    public class IntArray
    {
        private int[] array;

        public IntArray()
        {
            this.array = new int[] { };
        }

		public void Add(int element)
		{
			Array.Resize(ref this.array, this.array.Length + 1);
			this.array[this.array.Length - 1] = element;
		}

		public int Count()
		{
			int counter = 0;
			foreach(int element in this.array)
            {
				counter++;
            }

			return counter;
		}

		public int Element(int index)
		{
			return array[index];
		}

		public void SetElement(int index, int element)
		{
			array[index] = element;
		}

		public bool Contains(int element)
		{
			foreach(int digit in this.array)
            {
				if(element == digit)
                {
					return true;
                }
            }

			return false;
		}

		public int IndexOf(int element)
		{
			for(int i = 0; i < this.array.Length; i++)
            {
				if(element == this.array[i])
                {
					return i;
                }
            }

			return -1;
		}

		public void Insert(int index, int element)
		{
			Array.Resize(ref this.array, this.array.Length + 1);
			int newPosition = this.array.Length;
			while (newPosition > index)
            {
				array[newPosition] = array[newPosition - 1];
				newPosition--;
            }

			SetElement(index, element);
		}

		public void Clear()
		{
			Array.Resize(ref this.array, 0);
		}

		public void Remove(int element)
		{
			int index = IndexOf(element);
			if (index != -1)
            {
				while (index < this.array.Length - 1)
				{
					this.array[index] = this.array[index + 1];
				}

				Array.Resize(ref this.array, this.array.Length - 1);
			}			
		}

		public void RemoveAt(int index)
		{
			while (index < this.array.Length - 1)
			{
				this.array[index] = this.array[index + 1];
			}

			Array.Resize(ref this.array, this.array.Length - 1);
		}
	}
}
