﻿using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using ForYou.ForIM.Services.Infrastructure;

namespace ForYou.ForIM.Services
{
    /// <inheritdoc />
    public class WebSocketManager : IWebSocketManager
    {
        private static readonly ConcurrentDictionary<ISocketCacheKey, WebSocket> _keySocket = new ConcurrentDictionary<ISocketCacheKey, WebSocket>();
        private static readonly ConcurrentDictionary<WebSocket, ISocketCacheKey> _socketKey = new ConcurrentDictionary<WebSocket, ISocketCacheKey>();
        /// <inheritdoc />
        public ISocketCacheKey Add(WebSocket webSocket)
        {
            var key = new DefaultSocketCacheKey(Guid.NewGuid().ToString("N"));
            _keySocket.TryAdd(key, webSocket);
            _socketKey.TryAdd(webSocket, key);
            return key;
        }

        /// <inheritdoc />
        public WebSocket Get(ISocketCacheKey key)
        {
            _keySocket.TryGetValue(key, out var socket);
            return socket;
        }

        /// <inheritdoc />
        public ISocketCacheKey GetKey(WebSocket webSocket)
        {
            _socketKey.TryGetValue(webSocket, out var key);
            return key;
        }

        /// <inheritdoc />
        public bool Remove(ISocketCacheKey key)
        {
            var result = _keySocket.TryRemove(key, out var socket);
            if (result)
            {
                _socketKey.TryGetValue(socket, out _);
            } 
            return result;
        }
    }
}