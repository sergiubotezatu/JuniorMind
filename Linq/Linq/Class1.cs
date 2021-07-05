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

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIsNull(source);
            ThrowIsNull(predicate);
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIsNull(source);
            ThrowIsNull(predicate);
            if (source.Count() == 0)
            {
                throw new InvalidOperationException("The source sequence is empty.");
            }
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            throw new InvalidOperationException("There is no element in this collection that meets the correct criteria.");
        }

        static void ThrowIsNull<T>(T toValidate)
        {
            if (toValidate.Equals(null))
            {
                throw new ArgumentNullException($"{nameof(toValidate)}was null");
            }
        }

        static int Count<T>(this IEnumerable<T> elements)
        {
            int count = 0;

            foreach (var element in elements)
                count += 1;

            return count;
        }
    }
}
