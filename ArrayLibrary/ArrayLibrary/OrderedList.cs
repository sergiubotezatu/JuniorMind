using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class OrderedList<T>
        where T : IComparable<T>
    {
        private readonly SortedList<T> imutable;

        public OrderedList(SortedList<T> mutable)
        {
            this.imutable = mutable;
        }

        public int Count { get => this.imutable.Count; }

        public T this[int index]
        {
            get => this.imutable[index];

            set
            {
                IfReadOnlyThrowException();
                this.imutable[index] = value;
            }
        }

        public void Insert(int index, T item)
        {
            IfReadOnlyThrowException();
            this.imutable.Insert(index, item);
        }

        public bool Contains(T item)
        {
            return this.imutable.Contains(item);
        }

        public int IndexOf(T item)
        {
            return this.imutable.IndexOf(item);
        }

        public void RemoveAt(int index)
        {
            IfReadOnlyThrowException();
            this.imutable.RemoveAt(index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.imutable.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.imutable.GetEnumerator();
        }

        public void Add(T item)
        {
            IfReadOnlyThrowException();
            this.imutable.Add(item);
        }

        public void Clear()
        {
            IfReadOnlyThrowException();
            this.imutable.Clear();
        }

        public void Remove(T item)
        {
            IfReadOnlyThrowException();
            this.imutable.Remove(item);
        }

        private void IfReadOnlyThrowException()
        {
            if (imutable.IsReadOnly)
            {
                throw new NotSupportedException("Updating items or capacity of this collection is not allowed." +
                    "It is readOnly");
            }
        }
    }
}