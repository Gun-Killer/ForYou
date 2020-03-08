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
                    var socketContent = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), default);

                    Console.WriteLine(Encoding.UTF8.GetString(buffer));



                    _socketManager.AddSocket(webSocket);


                    foreach (var socket in _socketManager.GetAll())
                    {
                        if (socket.Value.State == WebSocketState.Open)
                        {
                            var content = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                            await socket.Value.SendAsync(new ArraySegment<byte>(content),
                                System.Net.WebSockets.WebSocketMessageType.Text, true, default);
                        }
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
