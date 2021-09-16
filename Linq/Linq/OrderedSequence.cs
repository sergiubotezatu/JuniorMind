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
        private readonly IComparer<T> comparer;
                
        public OrderedSequence(IEnumerable<T> source, IComparer<T> comparer)
        {
            this.ordered = new List<T>(source);
            this.comparer = comparer;
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            IComparer<T> newComparer = new SourceComparer<T, TKey>(comparer, keySelector);
            IComparer<T> chained = new ChainComparer<T>(this.comparer, newComparer);
            return new OrderedSequence<T>(this.ordered, chained);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Sort(this.comparer, false);
            for (int i = 1; i < this.ordered.Count; i++)
            {
                yield return this.ordered[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Sort(IComparer<T> comparer, bool isDescending)
        {
            T toCompare;
            int start;
            for (int i = 0; i < this.ordered.Count; i++)
            {
                toCompare = this.ordered[i];
                start = i - 1;
                while (start >= 0 && Orientation(this.ordered[i], this.ordered[start], comparer, isDescending))
                {
                    this.ordered[start + 1] = this.ordered[start];
                    start -= 1;
                }

                this.ordered[start + 1] = toCompare;
            }
        }

        private bool Orientation(T first, T second, IComparer<T> comparer, bool isDescending)
        {
            if (isDescending)
            {
                return this.comparer.Compare(first, second) >= 0;
            }

            return comparer.Compare(first, second) < 0;
        }
    }
}