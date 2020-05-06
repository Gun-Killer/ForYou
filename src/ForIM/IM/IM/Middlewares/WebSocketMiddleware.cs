using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ForYou.ForIM.Services;
using Microsoft.AspNetCore.Http;

namespace ForYou.ForIM.Middlewares
{


    /// <summary>
    /// /
    /// </summary>
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebSocketManager _webSocketManager;

        /// <summary>
        /// 
        /// </summary> >
        public WebSocketMiddleware(RequestDelegate next, IWebSocketManager webSocketManager)
        {
            _next = next;
            _webSocketManager = webSocketManager;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IMessageService messageService)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();


                    var receiver = new WebSocketReceiver(webSocket, _webSocketManager, messageService);
                    await receiver.StartListening();

                    await SendMessageAsync("新人加入");
                    var id = _webSocketManager.Add(webSocket);
                    while (webSocket.State == WebSocketState.Open && webSocket.CloseStatus.HasValue == false)
                    {
                        var message = await messageService.ReadMessageAsync(webSocket);
                        if (message != null)
                        {
                            await SendMessageAsync($"send people:{webSocket.GetHashCode()}.receive content:{message.Content}.send content:{Guid.NewGuid().ToString()}");
                        }
                    }

                    await _webSocketManager.Remove(id);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
                return;
            }

            await _next.Invoke(context);
        }

        private async ValueTask SendMessageAsync(string msg)
        {
            var mid = Encoding.UTF8.GetBytes(msg);
            foreach (var webSocket in _webSocketManager.GetAll())
            {
                await webSocket.Value.SendAsync(new ArraySegment<byte>(mid), WebSocketMessageType.Text, true, default);
            }
        }
    }
}
