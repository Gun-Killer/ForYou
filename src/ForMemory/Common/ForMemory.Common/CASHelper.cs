using System;
using System.Threading;

namespace ForMemory.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CASHelper
    {
        public static void CAS<T>(ref T input, Func<T, T> func) where T : class
        {
            T temp, replace;
            do
            {
                temp = input;
                replace = func(temp);
            } while (Interlocked.CompareExchange(ref input, replace, temp) != temp);
        }
    }
}
