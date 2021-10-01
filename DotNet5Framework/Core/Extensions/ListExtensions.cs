using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static void AddDifferentOnes<T>(this List<T> source, List<T> addedCollection)
        {
            var list = addedCollection.Where(ac => !source.Contains(ac)).ToList();
            source.AddRange(list);
        }
        public static void AddDifferentOne<T>(this List<T> source, T addedItem)
        {
            if (!source.Contains(addedItem))
            {
                source.Add(addedItem);
            }
        }
    }
}
