using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class SortedIntArray : IntArray
    {
        public SortedIntArray()
            : base()
        {
        }

        public override void Add(int element)
        {
            base.Add(element);
            SortWhileAdding(element);
        }

        private void SortWhileAdding(int element)
        {
            int prevIndex = IndexOf(element) - 1;
            while (prevIndex >= 0 && element < array[prevIndex])
            {
                Swap(element);
                prevIndex--;
            }
        }

        private void Swap(int element)
        {
            int index = IndexOf(element);
            int temp = element;
            array[index] = array[index - 1];
            array[index - 1] = temp;
        }
    }
}
