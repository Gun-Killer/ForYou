using System;
using System.Text;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;
using Newtonsoft.Json;

namespace ForYou.ForIM.Services
{
    /// <inheritdoc />
    public class MessageSendService : IMessageSendService
    {
        private readonly IWebSocketManager _socketManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socketManager"></param>
        public MessageSendService(IWebSocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        /// <inheritdoc />
        public async ValueTask<bool> Send(ISocketCacheKey key, IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var webSocket = _socketManager.Get(key);
            if (webSocket == null)
            {
                return false;
            }

            if (webSocket.State != System.Net.WebSockets.WebSocketState.Open || webSocket.CloseStatus.HasValue)
            {
                return false;
            }

            var str = JsonConvert.SerializeObject(message);
            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(str)),
                System.Net.WebSockets.WebSocketMessageType.Text, true, default);

            return true;
        }
    }
}