using System;
using System.Collections.Generic;
using System.Text;

namespace Linq
{
    public class ChainComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> firstCriteria;
        private readonly IComparer<T> secondCriteria;

        public ChainComparer(IComparer<T> FirstCriteria, IComparer<T> SecondCriteria)
        {
            this.firstCriteria = FirstCriteria;
            this.secondCriteria = SecondCriteria;
        }

        public int Compare(T x, T y)
        {
            int firstResult = firstCriteria.Compare(x, y);
            if (firstResult != 0)
            {
                return firstResult;
            }

            return secondCriteria.Compare(x, y);
        }
    }
}
