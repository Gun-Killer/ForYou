using System;

namespace ForYou.ForIM.Services.Infrastructure
{
    /// <summary>
    /// 默认缓存标识
    /// </summary>
    public struct DefaultSocketCacheKey : IEquatable<DefaultSocketCacheKey>, ISocketCacheKey
    {
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
        public string Key { get; }


        /// <inheritdoc />
        public bool Equals(DefaultSocketCacheKey other)
        {
            return string.Equals(other.Key, Key, StringComparison.InvariantCulture);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (Key == null)
            {
                return 0;
            }

            return Key.GetHashCode();//每次重启可能会不一样
            //using var md5 = new MD5CryptoServiceProvider();
            //var buffer = Encoding.UTF8.GetBytes(Key);
            //var hash = md5.ComputeHash(buffer);
            //return BitConverter.ToInt32(hash);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Key;
        }
    }
}