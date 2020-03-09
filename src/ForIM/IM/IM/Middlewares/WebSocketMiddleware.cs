using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ForYou.ForIM.SocketHandle;
using Microsoft.AspNetCore.Http;

namespace ForYou.ForIM.Middlewares
{


    /// <summary>
    /// /
    /// </summary>
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private SocketManager _socketManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public WebSocketMiddleware(RequestDelegate next, SocketManager socketManager)
        {
            _next = next;
            _socketManager = socketManager;
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
                    _socketManager.AddSocket(webSocket);

                    while (webSocket.State == WebSocketState.Open && webSocket.CloseStatus.HasValue == false)
                    {
                        var socketContent = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), default);
                        await webSocket.SendAsync(
                            new ArraySegment<byte>(Encoding.UTF8.GetBytes($"接收内容:{Encoding.UTF8.GetString(buffer, 0, socketContent.Count)}:{Guid.NewGuid()}")),
                            WebSocketMessageType.Text, true, default);
                    }

                    var id = _socketManager.GetId(webSocket);
                    if (!string.IsNullOrEmpty(id))
                    {
                        await _socketManager.RemoveSocket(id);
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
                return;
            }

            await _next.Invoke(context);
        }
    }
}
