using System;
using System.Collections.Generic;
using System.Linq;
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

        public static IEnumerable<TResult> SelectMany<TSource, TResult>
            (this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TResult>> selector)
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

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(keySelector, nameof(keySelector));
            ThrowIsNull(elementSelector, nameof(elementSelector));
            Dictionary<TKey, TElement> result = new Dictionary<TKey, TElement>();
            foreach (TSource element in source)
            {
                string keyIsNull = $"key generated for one of the elements";
                ThrowIsNull(keySelector(element), keyIsNull);
                result.Add(keySelector(element), elementSelector(element));
            }

            return result;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
        this IEnumerable<TFirst> first,
        IEnumerable<TSecond> second,
        Func<TFirst, TSecond, TResult> resultSelector)
        {
            ThrowIsNull(first, nameof(first));
            ThrowIsNull(second, nameof(second));
            ThrowIsNull(resultSelector, nameof(resultSelector));
            IEnumerator<TFirst> firstEnum = first.GetEnumerator();
            IEnumerator<TSecond> secondEnum = second.GetEnumerator();
            while(firstEnum.MoveNext() && secondEnum.MoveNext())
            {
                yield return resultSelector(firstEnum.Current, secondEnum.Current);
            }
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
        {
            ThrowIsNull(outer, nameof(outer));
            ThrowIsNull(inner, nameof(inner));
            ThrowIsNull(outerKeySelector, nameof(outerKeySelector));
            ThrowIsNull(innerKeySelector, nameof(innerKeySelector));
            ThrowIsNull(resultSelector, nameof(resultSelector));
            foreach (TOuter outerItem in outer)
            {
                foreach (TInner innerItem in inner)
                {
                    if (outerKeySelector(outerItem).Equals(innerKeySelector(innerItem)))
                    {
                        yield return resultSelector(outerItem, innerItem);
                        break;
                    }
                }
            }            
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
        this IEnumerable<TSource> source,
        TAccumulate seed,
        Func<TAccumulate, TSource, TAccumulate> func)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(func, nameof(func));
            ThrowIsNull(seed, nameof(seed));
            TAccumulate accumulator = seed;
            foreach (TSource item in source)
            {
                accumulator = func(seed, item);
            }
            return accumulator;
        }

        public static IEnumerable<TSource> Distinct<TSource>(
        this IEnumerable<TSource> source,
        IEqualityComparer<TSource> comparer)
        {
            ThrowIsNull(source, nameof(source));
            ThrowIsNull(comparer, nameof(comparer));
            return new HashSet<TSource>(source, comparer);
        }

        public static IEnumerable<TSource> Union<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
        {
            ThrowIsNull(first, nameof(first));
            ThrowIsNull(second, nameof(second));
            ThrowIsNull(comparer, nameof(comparer));
            HashSet<TSource> result = new HashSet<TSource>(first, comparer);
            result.UnionWith(second);
            return result;
        }

        public static IEnumerable<TSource> Intersect<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
        {
            ThrowIsNull(first, nameof(first));
            ThrowIsNull(second, nameof(second));
            ThrowIsNull(comparer, nameof(comparer));
            HashSet<TSource> result = new HashSet<TSource>(first, comparer);
            result.IntersectWith(second);
            return result;
        }

        public static IEnumerable<TSource> Except<TSource>(
        this IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        IEqualityComparer<TSource> comparer)
        {
            ThrowIsNull(first, nameof(first));
            ThrowIsNull(second, nameof(second));
            ThrowIsNull(comparer, nameof(comparer));
            HashSet<TSource> result = new HashSet<TSource>(first, comparer);
            result.ExceptWith(second);
            return result;
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector,
        Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
        IEqualityComparer<TKey> comparer)
        {
            Dictionary<TKey, List<TElement>> result = new Dictionary<TKey, List<TElement>>(comparer);
            foreach (TSource item in source)
            {
                TKey key = keySelector(item);
                TElement element = elementSelector(item);
                if (!result.ContainsKey(key))
                {
                    result.Add(key, new List<TElement>());
                }

                result[key].Add(element);
            }

            foreach (var item in result)
            {
                yield return resultSelector(item.Key, item.Value); 
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey> comparer)
        {
            IComparer<TSource> baseComparer = new SourceComparer<TSource, TKey>(comparer, keySelector);
            IOrderedEnumerable<TSource> result = new OrderedSequence<TSource>(source, baseComparer);
            return result;
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
        this IOrderedEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
            {
                action(item);
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
