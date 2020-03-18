using System.Net.WebSockets;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <summary>
    /// 处理消息
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        ValueTask<IMessage> ReadMessageAsync(WebSocket socket);
    }
}