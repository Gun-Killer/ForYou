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
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var buffer = new byte[4 * 1024];

                    await SendMessageAsync("新人加入");
                    var id = _webSocketManager.Add(webSocket);
                    while (webSocket.State == WebSocketState.Open && webSocket.CloseStatus.HasValue == false)
                    {
                        var socketContent = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), default);
                        await SendMessageAsync($"send people:{webSocket.GetHashCode()}.receive content:{Encoding.UTF8.GetString(buffer, 0, socketContent.Count)}.send content:{Guid.NewGuid().ToString()}");
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

        private async Task SendMessageAsync(string msg)
        {
            var mid = Encoding.UTF8.GetBytes(msg);
            foreach (var webSocket in _webSocketManager.GetAll())
            {
                await webSocket.Value.SendAsync(new ArraySegment<byte>(mid), WebSocketMessageType.Text, true, default);
            }
        }
    }
}
