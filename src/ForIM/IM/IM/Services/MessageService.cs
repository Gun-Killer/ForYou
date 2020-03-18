using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ForYou.ForIM.Services.Infrastructure;
using Newtonsoft.Json;

namespace ForYou.ForIM.Services
{
    /// <inheritdoc />
    public class MessageService : IMessageService
    {
        /// <inheritdoc />
        public async ValueTask<IMessage> ReadMessageAsync(WebSocket socket)
        {
            if (socket.State != WebSocketState.Open)
            {
                return default;
            }

            if (socket.CloseStatus.HasValue)
            {
                return default;
            }

            var buffer = WebSocket.CreateServerBuffer(4 * 1024); ;
            bool next;
            var memory = new MemoryStream();
            WebSocketMessageType messageType;
            do
            {
                var readResult = await socket.ReceiveAsync(buffer, default);
                next = readResult.EndOfMessage == false;
                if (readResult.Count > 0)
                {
                    await memory.WriteAsync(buffer.Array, buffer.Offset, readResult.Count);
                }

                messageType = readResult.MessageType;
            } while (next);

            if (memory.Length > 0)
            {
                switch (messageType)
                {
                    case WebSocketMessageType.Text:
                        var messageStr = Encoding.UTF8.GetString(memory.ToArray());
                        return JsonConvert.DeserializeObject<DefaultSocketMessage>(messageStr);
                    case WebSocketMessageType.Binary:
                        break;
                    case WebSocketMessageType.Close:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            return await Task.FromResult<IMessage>(null);
        }
    }
}