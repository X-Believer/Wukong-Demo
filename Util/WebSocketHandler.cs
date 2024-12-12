using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

namespace WukongDemo.Util
{
    public class WebSocketHandler
    {
        // 使用字典来存储每个用户的 WebSocket 连接
        private readonly ConcurrentDictionary<int, WebSocket> _sockets = new ConcurrentDictionary<int, WebSocket>();

        // 处理 WebSocket 连接
        public async Task HandleAsync(WebSocket webSocket, int userId)
        {
            _sockets[userId] = webSocket;

            var buffer = new byte[1024 * 4];

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);                       
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // 移除断开的连接
                _sockets.TryRemove(userId, out _);
                webSocket.Dispose();
            }
        }

        // 广播消息给所有连接的 WebSocket
        public async Task BroadcastMessageAsync(string message, int senderId)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            // 发送消息到所有连接的 WebSocket，跳过发送消息的用户
            foreach (var socket in _sockets)
            {
                if (socket.Value != null && socket.Value.State == WebSocketState.Open && socket.Key != senderId)
                {
                    await socket.Value.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }



}
