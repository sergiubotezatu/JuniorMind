using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayLibrary
{
    public class SortedList<T>
        where T : IComparable<T>
    {
        private readonly ArrayLibrary.List<T> initList;

        public SortedList(ArrayLibrary.List<T> objects)
        {
            this.initList = objects;
        }

        public int Count { get => this.initList.Count; }

        public T this[int index]
        {
            get => this.initList[index];

            set
            {
                if (IsSmallerOrEqual(GetElement(index - 1, value), value)
                    && IsSmallerOrEqual(value, GetElement(index + 1, value)))
                {
                    this.initList[index] = value;
                }
            }
        }

        public void Add(T element)
        {
            this.initList.Add(element);
            if (Count > 1)
            {
                SortWhileAdding(element);
            }
        }

        public void Insert(int index, T element)
        {
            if (IsInBetween(element, index))
            {
                this.initList.Insert(index, element);
            }
        }

        public bool Contains(T element)
        {
            return !IndexOf(element).Equals(-1);
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this.initList[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Clear()
        {
            this.initList.Clear();
        }

        public void Remove(T element)
        {
            this.initList.Remove(element);
        }

        public void RemoveAt(int index)
        {
            this.initList.RemoveAt(index);
        }

        private void SortWhileAdding(T element)
        {
            int prevIndex = Count - 2;
            int elementIndex = Count - 1;
            while (prevIndex >= 0 && element.CompareTo(this.initList[prevIndex]) < 0)
            {
                Swap(element, elementIndex);
                elementIndex--;
                prevIndex--;
            }
        }

        private void Swap(T element, int index)
        {
            T temp = element;
            this.initList[index] = this.initList[index - 1];
            this.initList[index - 1] = temp;
        }

        private bool IsInBetween(T element, int index)
        {
            if (index != 0)
            {
                return IsSmallerOrEqual(element, this.initList[index])
                    && IsSmallerOrEqual(this.initList[index - 1], element);
            }

            return IsSmallerOrEqual(element, this.initList[index]);
        }

        private T GetElement(int checkedIndex, T defaultValue)
        {
            return checkedIndex != -1 && checkedIndex != this.initList.Length ? this.initList[checkedIndex] : defaultValue;
        }

        private bool IsSmallerOrEqual(T first, T second)
        {
            return first.CompareTo(second) < 0 || first.CompareTo(second) == 0;
        }
    }
}
