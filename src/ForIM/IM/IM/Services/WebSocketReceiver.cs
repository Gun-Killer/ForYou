using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
                if (message != null && OnMessageReceived != null)
                {
                    //OnMessageReceived.BeginInvoke(_socket, new MessageReceiveEventArgs(message), (result) =>
                    //{
                    //    var handler = result.AsyncState as EventHandler<MessageReceiveEventArgs>;
                    //    handler?.EndInvoke(result);
                    //}, OnMessageReceived);

                    await OnMessageReceived.InvokeAsync(this, new MessageReceiveEventArgs(message));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MessageEventHandler<MessageReceiveEventArgs> OnMessageReceived;

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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageEventHandler<T> where T : EventArgs
    {
        private readonly List<Func<object, T, ValueTask>> _funcs;
        private readonly object _obj;
        private MessageEventHandler()
        {
            _funcs = new List<Func<object, T, ValueTask>>();
            _obj = new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MessageEventHandler<T> operator +(
            MessageEventHandler<T> e, Func<object, T, ValueTask> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (e == null)
            {
                e = new MessageEventHandler<T>();
            }

            lock (e._obj)
            {
                e._funcs.Add(callback);
            }

            return e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MessageEventHandler<T> operator -(
            MessageEventHandler<T> e, Func<object, T, ValueTask> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (e == null)
            {
                return null;
            }

            lock (e._obj)
            {
                e._funcs.Remove(callback);
            }

            return e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public async ValueTask InvokeAsync(object sender, T eventArgs)
        {
            List<Func<object, T, ValueTask>> funcs;
            lock (_obj)
            {
                funcs = new List<Func<object, T, ValueTask>>(_funcs);
            }

            foreach (var callback in funcs)
            {
                await callback(sender, eventArgs);
            }
        }
    }
}