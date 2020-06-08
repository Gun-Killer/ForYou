using System;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <inheritdoc />
    public class MessageEventHandlerRegisterService : IMessageEventHandlerRegisterService
    {
        private readonly IWebSocketManager _socketManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socketManager"></param>
        public MessageEventHandlerRegisterService(IWebSocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        /// <inheritdoc />
        public bool Register(ISocketCacheKey key, EventHandler<MessageReceiveEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var socket = _socketManager.Get(key);
            if (socket == null)
            {
                return false;
            }

            var receiver = WebSocketReceiver.GetReceiver(socket);
            if (receiver == null)
            {
                return false;
            }
            receiver.MessageHandler += handler;
            return true;
        }

        /// <inheritdoc />
        public bool Remove(ISocketCacheKey key, EventHandler<MessageReceiveEventArgs> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var socket = _socketManager.Get(key);
            if (socket == null)
            {
                return false;
            }

            var receiver = WebSocketReceiver.GetReceiver(socket);
            if (receiver == null)
            {
                return false;
            }

            receiver.MessageHandler -= handler;
            return true;
        }
    }
}