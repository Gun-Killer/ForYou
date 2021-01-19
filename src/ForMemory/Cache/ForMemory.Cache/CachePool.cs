using System.Collections.Generic;

namespace ForMemory.Cache
{
    public class CachePool<T>
    {
        private readonly List<T> _pools;

        public CachePool()
        {
            _pools = new List<T>(5);
        }
    }
}