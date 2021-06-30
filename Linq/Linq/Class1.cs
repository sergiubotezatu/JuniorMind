using System;
using System.Collections.Generic;
using Linq;

namespace Linq
{
    public static class Class1
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIsNull(source);
            ThrowIsNull(predicate);
            foreach(TSource element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }

        static void ThrowIsNull<T>(T toValidate)
        {
            if (toValidate.Equals(null))
            {
                throw new ArgumentNullException($"{nameof(toValidate)}was null");
            }
        }
    }
}
