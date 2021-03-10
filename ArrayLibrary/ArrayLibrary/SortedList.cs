using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrayLibrary
{
    public class SortedList<T> : IList<T>
        where T : IComparable<T>
    {
        private readonly ArrayLibrary.List<T> initList;

        public SortedList(ArrayLibrary.List<T> objects)
        {
            this.initList = objects;
        }

        public int Count { get => this.initList.Count; }

        public bool IsReadOnly => throw new NotImplementedException();

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

        public void Add(T item)
        {
            this.initList.Add(item);
            if (Count > 1)
            {
                SortWhileAdding(item);
            }
        }

        public void Insert(int index, T item)
        {
            if (IsInBetween(item, index))
            {
                this.initList.Insert(index, item);
            }
        }

        public bool Contains(T item)
        {
            return this.initList.Contains(item);
        }

        public int IndexOf(T item)
        {
            return this.initList.IndexOf(item);
        }

        public void Clear()
        {
            this.initList.Clear();
        }

        public void Delete(T element)
        {
            this.initList.Remove(element);
        }

        public void RemoveAt(int index)
        {
            this.initList.RemoveAt(index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                return;
            }

            int minimumLength = array.Length + this.Count;
            if (array.Length >= minimumLength)
           {
                for (int i = 0; i < this.Count; i++)
                {
                    array[arrayIndex] = this.initList[i];
                    arrayIndex++;
                }
           }
        }

        public bool Remove(T item)
        {
            int itemPos = IndexOf(item);
            int newCount = this.Count - 1;
            if (itemPos == -1)
            {
                return false;
            }

            if (this.Count == 1)
            {
                Remove(item);
                return this.Count == 0;
            }

            Remove(item);
            T replacer = GetReplacer(itemPos);
            return this.Count == newCount && this.initList[itemPos].Equals(replacer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.initList[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.initList[i];
            }
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

        private T GetReplacer(int itemPos)
        {
            if (itemPos + 1 < this.Count)
            {
                return this.initList[itemPos + 1];
            }

            return this.initList[itemPos - 1];
        }
    }
}
