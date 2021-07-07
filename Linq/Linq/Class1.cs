using System;
using System.Collections.Generic;
using Linq;

namespace Linq
{
    public static class Class1
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(predicate, nameof(predicate));
            foreach (TSource element in source)
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
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(predicate, nameof(predicate));
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
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(predicate, nameof(predicate));
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
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(selector, nameof(selector));
            foreach (TSource element in source)
            {
                yield return selector(element);                              
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(selector, nameof(selector));
            foreach (TSource element in source)
            {
                foreach (TResult item in selector(element))
                {                         
                      yield return item;                                       
                }
                
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(predicate, nameof(predicate));
            foreach (TSource element in source)
            {
                if (predicate(element))
                {
                    yield return element;
                }
            }
        }

        static void ThrowIsNull<T>(T toValidate, string name)
        {
            if (toValidate.Equals(null))
            {
                throw new ArgumentNullException(name);
            }
        }        
    }
}
