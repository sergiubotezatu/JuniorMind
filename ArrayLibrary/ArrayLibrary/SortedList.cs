using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayLibrary
{
    public class SortedList<T> : ArrayLibrary.List<T>
        where T : IComparable<T>
    {
        public SortedList()
            : base()
        {
        }

        public override T this[int index]
        {
            set
            {
                if (IsSmallerOrEqual(GetElement(index - 1, value), value)
                    && IsSmallerOrEqual(value, GetElement(index + 1, value)))
                {
                    array[index] = value;
                }
            }
        }

        public override void Add(T element)
        {
            base.Add(element);
            if (Count > 1)
            {
                SortWhileAdding(element);
            }
        }

        public override void Insert(int index, T element)
        {
            if (IsInBetween(element, index))
            {
                base.Insert(index, element);
            }
        }

        private void SortWhileAdding(T element)
        {
            int prevIndex = Count - 2;
            int elementIndex = Count - 1;
            while (prevIndex >= 0 && element.CompareTo(array[prevIndex]) < 0)
            {
                Swap(element, elementIndex);
                elementIndex--;
                prevIndex--;
            }
        }

        private void Swap(T element, int index)
        {
            T temp = element;
            array[index] = array[index - 1];
            array[index - 1] = temp;
        }

        private bool IsInBetween(T element, int index)
        {
            if (index != 0)
            {
                return IsSmallerOrEqual(element, array[index])
                    && IsSmallerOrEqual(array[index - 1], element);
            }

            return IsSmallerOrEqual(element, array[index]);
        }

        private T GetElement(int checkedIndex, T defaultValue)
        {
            return checkedIndex != -1 && checkedIndex != array.Length ? array[checkedIndex] : defaultValue;
        }

        private bool IsSmallerOrEqual(T first, T second)
        {
            return first.CompareTo(second) < 0 || first.CompareTo(second) == 0;
        }
    }
}
