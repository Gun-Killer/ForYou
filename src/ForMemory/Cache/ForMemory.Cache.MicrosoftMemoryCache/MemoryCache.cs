using System;
using Microsoft.Extensions.Caching.Memory;

namespace ForMemory.Cache.MicrosoftMemoryCache
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryCache : IMemoryCache
    {
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _memoryCache;

        public MemoryCache(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        /// <inheritdoc />
        public bool Set<T>(string key, T value, int minutes)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(minutes));
            return true;
        }
    }
}