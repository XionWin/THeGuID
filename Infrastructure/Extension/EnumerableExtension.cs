using System;

namespace Extension
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
