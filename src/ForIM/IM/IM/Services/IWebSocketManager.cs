using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <summary>
    /// socket 连接管理
    /// </summary>
    public interface IWebSocketManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        ISocketCacheKey Add(WebSocket webSocket);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        WebSocket Get(ISocketCacheKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        ISocketCacheKey GetKey(WebSocket webSocket);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Remove(ISocketCacheKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<ISocketCacheKey, WebSocket>> GetAll();
    }
}