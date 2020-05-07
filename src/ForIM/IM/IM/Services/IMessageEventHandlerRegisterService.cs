using System;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <summary>
    /// 注册处理消息事件
    /// </summary>
    public interface IMessageEventHandlerRegisterService
    {
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        bool Register(ISocketCacheKey key, Func<object, MessageReceiveEventArgs, ValueTask> handler);

        /// <summary>
        /// 移除处理事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        bool Remove(ISocketCacheKey key, Func<object, MessageReceiveEventArgs, ValueTask> handler);
    }
}