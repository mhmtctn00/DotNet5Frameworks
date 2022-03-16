using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Search<T>(this List<T> source, string propertyName, string text, int count = 10, bool ignoreCaseSensivity = false) where T : IEquatable<T>
        {
            text.Trim();
            text = Regex.Replace(text, @"\s+", " ");
            var result = new List<T>();

            if (text.Contains(" "))
            {
                //TODO: Eğer iki veya daha fazla kelimeden oluşan bir sorgu gelirse, verileri parçalamadan bütün olarak değerlendirir.
                // İki kelime gelirse verileri ikili guruplara ayırarak arama yapabiliriz.

                result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Equals(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().StartsWith(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Contains(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().EqualsWithLevenshteinDistance(text, 1, ignoreCaseSensivity)).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().StartsWithLevenshteinDistance(text, 1, ignoreCaseSensivity)).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().ContainsWithLevenshteinDistance(text, 1, ignoreCaseSensivity)).Take(count - result.Count).ToList());
            }
            else
            {
                result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.Equals(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.StartsWith(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.Contains(text, ignoreCaseSensivity ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.EqualsWithLevenshteinDistance(text, 1, ignoreCaseSensivity))).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.StartsWithLevenshteinDistance(text, 1, ignoreCaseSensivity))).Take(count - result.Count).ToList());

                if (result.Count < count)
                    result.AddRange(source.Where(t => !result.Any(x => x.Equals(t)) && t.GetType().GetProperty(propertyName).GetValue(t, null).ToString().Split(" ").Any(y => y.ContainsWithLevenshteinDistance(text, 1, ignoreCaseSensivity))).Take(count - result.Count).ToList());
            }

            return result;
        }
        public static void AddDifferentOne<T>(this List<T> source, T addedItem) where T : IEquatable<T>
        {
            if (!source.Any(s => s.Equals(addedItem)))
                source.Add(addedItem);
        }
        public static void AddDifferentOnes<T>(this List<T> source, List<T> addedCollection) where T : IEquatable<T>
        {
            var col = addedCollection.Where(a => !source.Any(s => s.Equals(a)));
            if (col.Count() > 0)
                source.AddRange(col);
        }

        public static string ToString(this List<string> source, string separator)
        {
            var str = "";

            for (int i = 0; i < source.Count; i++)
            {
                str += source[i];
                if (i != source.Count - 1)
                {
                    str += separator;
                }
            }

            return str;
        }

        public static IEnumerable<T> Map<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null) return new List<TSource>();
            if (keySelector == null) return source;

            return Distinct();

            IEnumerable<TSource> Distinct()
            {
                var knownKeys = new HashSet<TKey>();

                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }
    }
}
