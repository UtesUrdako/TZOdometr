using System;
using WebSocketSharp;

namespace Connect
{
    public class APIConnect
    {
        private WebSocket webSocket;
        public event Action<Result> Success;
        public void Initialize(string ip, string port)
        {
            webSocket = new WebSocket($"ws://{ip}:{port}/ws");
            webSocket.OnMessage += WsCallback;
            webSocket.Connect();
        }
        public void WsCall(string data)
        {
            webSocket.Send(data);
        }

        public void WsCallback(object sender, MessageEventArgs e)
        {
            Result result = new Result();
            result.Init(e.Data);
            Success(result);
        }
    }
}