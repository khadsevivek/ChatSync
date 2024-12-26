using System.Net.WebSockets;
using System.Text;
using static ChatSyncPVT.Models.MessageModels;

public class ChatWebSocketMiddleware
{
    private readonly RequestDelegate _next;

    public ChatWebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, MessageRepository messageRepository)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            await _next(context);
            return;
        }

        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await HandleWebSocketConnection(context, webSocket, messageRepository);
    }

    private async Task HandleWebSocketConnection(HttpContext context, WebSocket webSocket, MessageRepository messageRepository)
    {
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                break;
            }

            var messageText = Encoding.UTF8.GetString(buffer, 0, result.Count);
            // Deserialize message
            var message = System.Text.Json.JsonSerializer.Deserialize<Message>(messageText);
            if (message != null)
            {
                await messageRepository.SaveMessageAsync(message);
                var response = Encoding.UTF8.GetBytes($"Message received: {message.Content}");
                await webSocket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
