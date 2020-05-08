using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessageSendService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        ValueTask<bool> Send(ISocketCacheKey key, IMessage message);
    }
}