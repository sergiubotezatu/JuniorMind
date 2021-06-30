using System;
using System.Collections.Generic;
using Linq;

namespace Linq
{
    public static class Class1
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source.Equals(null) || predicate.Equals(null))
            {
                throw new ArgumentNullException($"{0}was null", source.Equals(null)? nameof(source) : nameof(predicate));
            }
            foreach(TSource element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
