using ForMemory.Cache.Interfaces;

namespace ForMemory.Cache
{
    public class RedisCache : ICache
    {
        /// <inheritdoc />
        public string Get(string key)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool Set(string key, string value, int minutes)
        {
            throw new System.NotImplementedException();
        }
    }
}