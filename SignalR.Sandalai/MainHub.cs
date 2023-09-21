using Microsoft.AspNet.SignalR;

using System;
using System.Threading.Tasks;

namespace SignalR.Sandalai
{
    public class MainHub : Hub
    {
        private readonly Lobby lobby;
        public MainHub() : this(Lobby.GetInstance()) { }
        public MainHub(Lobby lobby)
        {
            this.lobby = lobby;
        }
        public override Task OnConnected()
        {
            Console.WriteLine("Connected");
            lobby.AddUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("Disconnected");
            lobby.RemoveUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
        public void Send(string message)
        {
            Clients.Others.Send(message);
        }
        public override Task OnReconnected()
        {
            Console.WriteLine("Reconnected");
            lobby.AddUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}
