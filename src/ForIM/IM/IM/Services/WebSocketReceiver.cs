using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;

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
        public async ValueTask StartListening()
        {
            while (_socket.State == WebSocketState.Open && _socket.CloseStatus.HasValue == false)
            {
                var message = await _messageService.ReadMessageAsync(_socket);
                if (message != null && MessageHandler != null)
                {

                    await Task.Run(() =>
                      {
                          MessageHandler(this, new MessageReceiveEventArgs(message));
                      }).ConfigureAwait(false);


                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageReceiveEventArgs> MessageHandler;

        #region static

        private static readonly ConcurrentDictionary<WebSocket, WebSocketReceiver> _receivers = new ConcurrentDictionary<WebSocket, WebSocketReceiver>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="receiver"></param>
        public static void BindSocketReceiver(WebSocket socket, WebSocketReceiver receiver)
        {
            if (socket == null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            if (receiver == null)
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            _receivers.TryAdd(socket, receiver);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static WebSocketReceiver GetReceiver(WebSocket socket)
        {
            if (socket == null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            if (_receivers.TryGetValue(socket, out var receiver))
            {
                return receiver;
            }

            return null;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="socket"></param>
        public static void Unbind(WebSocket socket)
        {
            _receivers.TryRemove(socket, out _);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class MessageReceiveEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MessageReceiveEventArgs(IMessage message)
        {
            Message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMessage Message { get; set; }
    }
}