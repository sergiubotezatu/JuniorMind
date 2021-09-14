using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq
{
    class OrderedSequence<TKey, T> : IOrderedEnumerable<T>
    {
        private readonly List<T> ordered;
        private IComparer<TKey> comparer;
        private Func<T, TKey> keySelector;
        
        public OrderedSequence(IEnumerable<T> source, IComparer<TKey> comparer, Func<T, TKey> keySelector)
        {
            this.ordered = new List<T>(source);
            this.comparer = comparer;
            this.keySelector = keySelector;
            Sort(keySelector, comparer, false);
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            Sort(keySelector, comparer, descending);
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 1; i < this.ordered.Count; i++)
            {
                yield return this.ordered[i];
            }
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
