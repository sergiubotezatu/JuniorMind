using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    class OrderedSequence<T> : IOrderedEnumerable<T>
    {
        private readonly List<T> ordered;
        
        public OrderedSequence(IEnumerable<T> source)
        {
            this.ordered = new List<T>(source);
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            Sort(keySelector, comparer, descending);
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.ordered.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool isDescending)
        {
            T toCompare;
            int start;
            for (int i = 1; i < this.ordered.Count; i++)
            {
                toCompare = this.ordered[i];
                start = i - 1;
                TKey key = keySelector(this.ordered[i]);
                while (start >= 0 && Orientation(key, keySelector(this.ordered[start]), comparer, isDescending))
                {
                    this.ordered[start + 1] = this.ordered[start];
                    start = start - 1;
                }

                this.ordered[start + 1] = toCompare;
            }
        }

        private bool Orientation<TKey>(TKey first, TKey second, IComparer<TKey> comparer, bool isDescending)
        {
            if (isDescending)
            {
                return comparer.Compare(first, second) >= 0;
            }

            return comparer.Compare(first, second) < 0;
        }
    }
}
