using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class ListExtensions
{
    public static IEnumerable<List<T>> Split<T>(this List<T> locations, int nSize = 8)
    {
        for (int i = 0; i < locations.Count; i += nSize)
        {
            yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
        }
    }
}