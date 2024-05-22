using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace CTAM.Core.Utilities
{
    public static class BogusUtils
    {
        public static IEnumerable<V> MakeRelation<T, U, V>(
              this Faker f,
              List<T> parent,
              List<U> children,
              Func<T, U, V> creator,
              int maxChildrenCount = -1)
        {
            return parent.SelectMany(a =>
            {
                if (maxChildrenCount == -1)
                {
                    maxChildrenCount = children.Count;
                }
                return f.Random.ListItems(children, f.Random.Int(0, maxChildrenCount))
                        .Select(c => creator(a, c));
            });
        }
    }
}
