using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace LinqToFcs.Core.Extensions
{
    public static class LinqExtensions
    {
        public static void DoForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static void DoForEach<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            Contract.Assert(items != null, "item cannot be null");

            items
                .Select((x, index) => new { Item = x, Index = index })
                .DoForEach(x => action(x.Item, x.Index));
        }

        /// <summary>
        /// splits collection of bytes to same size chunks
        /// </summary>
        /// <param name="array"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<byte>> Split(this IEnumerable<byte> array, int chunkSize)
        {
            for (int i = 0; i < array.Count(); i += chunkSize)
            {
                yield return array.Skip(i).Take(chunkSize);
            }
        }
    }
}
