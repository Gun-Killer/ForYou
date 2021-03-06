using ForMemory.Network.Sockets;
using System;

namespace ForMemory.Network.SocketsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer();
            server.StartListening();
            Console.WriteLine("Hello World!");
        }
    }
}
