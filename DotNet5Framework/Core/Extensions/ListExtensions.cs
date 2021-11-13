using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    // TODO: Class'larda çalışmıyor incele.
    public static class ListExtensions
    {
        public static void AddDifferentOnes<T>(this List<T> source, List<T> addedCollection)
        {
            foreach (var item in addedCollection)
            {
                if (!source.Contains(item))
                {
                    source.Add(item);
                }
            }
        }
        public static void AddDifferentOne<T>(this List<T> source, T addedItem)
        {
            if (!source.Contains(addedItem))
            {
                source.Add(addedItem);
            }
        }
        public static List<T> ToListWithoutTheSameOnes<T>(this List<T> source)
        {
            List<T> items = new List<T>();
            foreach (var item in source)
            {
                if (!items.Contains(item))
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
