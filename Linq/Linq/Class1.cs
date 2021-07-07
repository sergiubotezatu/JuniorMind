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
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            throw new InvalidOperationException("There is no element in this collection that meets the correct criteria.");
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            ThrowIsNull(source);
            ThrowIsNull(selector);
            foreach (TSource element in source)
            {
                if (!selector(element).Equals(null))
                {
                    yield return selector(element);
                }                
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            ThrowIsNull(source);
            ThrowIsNull(selector);
            foreach (TSource element in source)
            {
                if (selector(element).Equals(null))
                {
                    continue;
                }

                foreach (TResult item in selector(element))
                {
                   if (!item.Equals(null))
                   {
                       yield return item;
                   }                        
                }
                
            }
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
