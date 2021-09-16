using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class SourceComparer<TSource, TKey> : IComparer<TSource>
    {
        private readonly IComparer<TKey> baseComparer;
        private readonly Func<TSource, TKey> baseKeySelector;

        public SourceComparer(IComparer<TKey> comparer, Func<TSource, TKey> keySelector)
        {
            this.baseComparer = comparer;
            this.baseKeySelector = keySelector;
        }

        public int Compare(TSource x, TSource y)
        {
            return baseComparer.Compare(baseKeySelector(x), baseKeySelector(y));
        }
    }
}
