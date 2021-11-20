using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static void AddDifferentOne<T>(this List<T> source, T addedItem)
        {
            if (typeof(T).IsValueType)
            {
                if (!source.Contains(addedItem))
                {
                    source.Add(addedItem);
                }
            }
            else
            {
                bool isEqual = true;
                foreach (var sourceItem in source)
                {

                    Type type = sourceItem.GetType();
                    PropertyInfo[] props = type.GetProperties();

                    bool propEquals = true;
                    foreach (var prop in props)
                    {
                        if (prop.GetValue(sourceItem) is not null && !prop.GetValue(sourceItem).Equals(prop.GetValue(addedItem)))
                            propEquals = false;
                    }
                    isEqual = propEquals;
                    if (propEquals)
                        break;
                }
                if (!isEqual)
                {
                    source.Add(addedItem);
                }
            }
        }

        public static void AddDifferentOnes<T>(this List<T> source, List<T> addedCollection)
        {
            foreach (var item in addedCollection)
            {
                source.AddDifferentOne(item);
            }
        }

        public static List<T> ToListWithoutTheSameOnes<T>(this List<T> source)
        {
            List<T> items = new List<T>();
            if (typeof(T).IsValueType)
            {
                foreach (var sourceItem in source)
                {
                    if (!items.Contains(sourceItem))
                    {
                        items.Add(sourceItem);
                    }
                }
            }
            else
            {
                foreach (var sourceItem in source)
                {
                    bool isEqual = true;
                    if (items.Count > 0)
                        foreach (var item in items)
                        {
                            Type type = item.GetType();
                            PropertyInfo[] props = type.GetProperties();

                            foreach (var prop in props)
                            {
                                if (prop.GetValue(sourceItem).Equals(prop.GetValue(item)))
                                    isEqual = false;
                            }
                        }
                    if (isEqual)
                    {
                        items.Add(sourceItem);
                    }
                }
            }
            return items;
        }
    }
}
