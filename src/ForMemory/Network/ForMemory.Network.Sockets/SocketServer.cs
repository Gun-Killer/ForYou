using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ForMemory.Network.Sockets
{
    public class SocketServer
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public void StartListening()
        {
            IPEndPoint ped = new IPEndPoint(IPAddress.Any, 5000);

            Socket socket = new Socket(ped.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ped);
            socket.Listen(1000);
            while (true)
            {
                allDone.Reset();
                var result = socket.BeginAccept(new AsyncCallback(SocketAcceptHandler), socket);
                allDone.WaitOne();
            }
        }

        private void SocketAcceptHandler(IAsyncResult ar)
        {
            allDone.Set();
            var socket = ar.AsyncState as Socket;
            var listener = socket.EndAccept(ar);
            SocketContext context = new SocketContext()
            {
                workSocket = listener
            };

            listener.BeginReceive(context.buffer, 0, SocketContext.BufferSize, 0, new AsyncCallback(SocketReceiveandler), context);
        }

        private void SocketReceiveandler(IAsyncResult ar)
        {
            var context = ar.AsyncState as SocketContext;
            var readBytes = context.workSocket.EndReceive(ar);
            if (readBytes < 1)
            {
                return;
            }

            context.sb.Append(Encoding.UTF8.GetString(context.buffer, 0, readBytes));
            var content = context.sb.ToString();
            if (content.IndexOf("<EOF>") > -1)
            {
                // Echo the data back to the client.  
                Send(context, content);
            }
            else
            {
                // Not all data received. Get more.  
                context.workSocket.BeginReceive(context.buffer, 0, SocketContext.BufferSize, 0,
                new AsyncCallback(SocketReceiveandler), ar.AsyncState);
            }
        }
        private void Send(SocketContext handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.workSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                SocketContext handler = ar.AsyncState as SocketContext;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.workSocket.EndSend(ar);
                handler.sb.Clear();
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
                handler.workSocket.BeginReceive(handler.buffer, 0, SocketContext.BufferSize, 0,
                new AsyncCallback(SocketReceiveandler), ar.AsyncState);
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}