using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ZooKeeper.ClientSample
{
    public static class OrderByZhCn
    {
        public static IEnumerable<T> OrderByZnCh<T>(this IEnumerable<T> source, Func<T, string> keySelect)
        {
            CultureInfo culture = new CultureInfo("zn-CN");
            return source.OrderBy(keySelect, StringComparer.Create(culture, false));
        }
    }
}