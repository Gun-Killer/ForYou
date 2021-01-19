namespace ForMemory.Cache.Interfaces
{
    /// <summary>
    /// 缓存通用接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// /
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, int minutes);
    }
}