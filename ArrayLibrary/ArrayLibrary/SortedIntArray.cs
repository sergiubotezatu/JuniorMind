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

        public override int this[int index]
        {
            set => array[index] = NewlySetFits(value, index) ? value : array[index];
        }

        public override void Add(int element)
        {
            base.Add(element);
            if (Count > 1)
            {
                SortWhileAdding(element);
            }
        }

        public override void Insert(int index, int element)
        {
            if (IsInBetween(element, index))
            {
                base.Insert(index, element);
            }
        }

        private void SortWhileAdding(int element)
        {
            int prevIndex = Count - 2;
            int elementIndex = Count - 1;
            while (prevIndex >= 0 && element < array[prevIndex])
            {
                Swap(element, elementIndex);
                elementIndex--;
                prevIndex--;
            }
        }

        private void Swap(int element, int index)
        {
            int temp = element;
            array[index] = array[index - 1];
            array[index - 1] = temp;
        }

        private bool IsInBetween(int element, int index)
        {
            if (index != 0)
            {
                return element <= array[index] && element >= array[index - 1];
            }

            return element <= array[index];
        }

        private bool NewlySetFits(int element, int index)
        {
            int previous = GetComparer(index - 1, element);
            int next = GetComparer(index + 1, element);
            return element >= previous && element <= next;
        }

        private int GetComparer(int checkedIndex, int element)
        {
            return checkedIndex != -1 && checkedIndex != array.Length ? array[checkedIndex] : element;
        }
    }
}
