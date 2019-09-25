using System;
using System.Collections.Concurrent;
using ForMemory.Cache.Interfaces;

namespace ForMemory.Cache
{
    public class LocalCache : ICache
    {
        private static ConcurrentDictionary<string, CacheEntity> _cache = new ConcurrentDictionary<string, CacheEntity>();
        /// <inheritdoc />
        public string Get(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                if (value.ExpireTime < DateTime.Now)
                {
                    _cache.TryRemove(key, out var _);
                }
                else
                {
                    return value.Value;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public bool Set(string key, string value, int minutes)
        {
            if (minutes < 1)
            {
                throw new ArgumentException("minutes must >=1", nameof(minutes));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(value);
            }

            var addValue = new CacheEntity
            {
                Key = key,
                Value = value,
                ExpireTime = DateTime.Now.AddMinutes(minutes)
            };
            var result = _cache.AddOrUpdate(key, addValue, (t, oldValue) => addValue);

            return result.Equals(addValue);
        }

        private struct CacheEntity : IEquatable<CacheEntity>
        {
            public string Key { get; set; }

            public string Value { get; set; }

            public DateTime ExpireTime { get; set; }



            /// <inheritdoc />
            public bool Equals(CacheEntity other)
            {
                return string.Equals(Key, other.Key) && string.Equals(Value, other.Value) && ExpireTime.Equals(other.ExpireTime);
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                return obj is CacheEntity other && Equals(other);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Key != null ? Key.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ ExpireTime.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}