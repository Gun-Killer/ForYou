namespace ForMemory.Cache
{
    public class RedisOption
    {
        public RedisInfo[] ConnectInfos { get; set; }
    }

    public class RedisInfo
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 验证
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 端口 默认6379
        /// </summary>
        public int Port { get; set; } = 6379;
        /// <summary>
        /// 链接超时时间 ms
        /// </summary>
        public int ConnectTimeout { get; set; } = 2000;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int Retry { get; set; } = 3;
    }
}