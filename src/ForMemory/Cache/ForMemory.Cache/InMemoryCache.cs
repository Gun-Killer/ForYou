using System;
using ForMemory.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace ForMemory.Cache
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <inheritdoc />
        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.Get<string>(key);
        }

        /// <inheritdoc />
        public bool Set(string key, string value, int minutes)
        {
            if (minutes < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "minutes must >= 1");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _cache.Set(key, value, TimeSpan.FromMinutes(minutes));
            return true;
        }
    }
}