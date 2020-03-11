using System;

namespace ForYou.ForIM.Services.Infrastructure
{
    /// <summary>
    /// 默认缓存标识
    /// </summary>
    public class DefaultSocketCacheKey : IEquatable<DefaultSocketCacheKey>, ISocketCacheKey
    {
        /// <summary>
        /// 
        /// </summary>
        public DefaultSocketCacheKey()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public DefaultSocketCacheKey(string key)
        {
            this.Key = key;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }


        /// <inheritdoc />
        public bool Equals(DefaultSocketCacheKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Key, other.Key);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((DefaultSocketCacheKey)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
    }
}