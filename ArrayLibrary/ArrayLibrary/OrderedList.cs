using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArrayLibrary
{
    public class ReadOnlyList<T> : IList<T>
    {
        private readonly IList<T> imutable;

        public ReadOnlyList(IList<T> mutable)
        {
            this.imutable = mutable;
        }

        public int Count { get => this.imutable.Count; }

        public bool IsReadOnly => true;

        public T this[int index]
        {
            get => this.imutable[index];

            set
            {
                throw new NotSupportedException("Setting values to items of this list is not allowed." +
                    "List is readonly");
            }
        }

        public void Insert(int index, T item)
        {
            ThrowNotSupportedException();
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
            ThrowNotSupportedException();
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
            ThrowNotSupportedException();
        }

        public void Clear()
        {
            ThrowNotSupportedException();
        }

        public bool Remove(T item)
        {
            ThrowNotSupportedException();
            return imutable.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)imutable).GetEnumerator();
        }

        private void ThrowNotSupportedException()
        {
            throw new NotSupportedException("Updating items or capacity of this collection is not allowed." +
                    "It is readOnly");
        }
    }
}