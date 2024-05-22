using System;
using System.Collections.Generic;

namespace CTAM.Core.Utilities
{
    public static class ListUtils
    {
        /// <summary>
        /// Partition list in chunks of size chunkSize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<List<T>> Partition<T>(this List<T> values, int chunkSize)
        {
            for (int i = 0; i < values.Count; i += chunkSize)
            {
                yield return values.GetRange(i, Math.Min(chunkSize, values.Count - i));
            }
        }
    }
}
