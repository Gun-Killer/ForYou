using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace ForYou.ForIM.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketReceiver
    {
        private readonly WebSocket _socket;
        private readonly IWebSocketManager _manager;
        private readonly IMessageService _messageService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="manager"></param>
        /// <param name="messageService"></param>
        public WebSocketReceiver(WebSocket socket, IWebSocketManager manager, IMessageService messageService)
        {
            _socket = socket;
            _manager = manager;
            _messageService = messageService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task StartListening()
        {
            while (_socket.State == WebSocketState.Open && _socket.CloseStatus.HasValue == false)
            {
                var message = await _messageService.ReadMessageAsync(_socket);
                if (message != null)
                {
                    
                }
            }
        }
    }
}