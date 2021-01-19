namespace ForMemory.Cache.Interfaces
{
    /// <summary>
    /// 缓存通用接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取key 对应的缓存数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存标识</param>
        /// <param name="value">缓存数据</param>
        /// <param name="minutes">缓存时间 单位分钟</param>
        /// <returns></returns>
        bool Set<T>(string key, T value, int minutes);
    }
}