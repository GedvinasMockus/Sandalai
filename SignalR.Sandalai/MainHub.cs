using Microsoft.AspNet.SignalR;

using SignalR.Sandalai.Objects;

using System;
using System.Collections.Generic;

namespace SignalR.Sandalai
{
    public class MainHub : Hub
    {
        private readonly Lobby lobby;
        public static List<Battle> battleList = new List<Battle>();
        private object obj = new object();
        public MainHub() : this(Lobby.Instance) { }
        public MainHub(Lobby lobby)
        {
            this.lobby = lobby;
        }
        public void AddToLobby()
        {
            Console.WriteLine("Connected");
            lobby.AddUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
        }
        public void RemoveFromLobby()
        {
            Console.WriteLine("Disonnected");
            lobby.RemoveUser(Context.ConnectionId);
            Console.WriteLine(Context.ConnectionId);
        }
        public void FindOpponent()
        {
            Player opponent = lobby.FindAnotherUser(Context.ConnectionId);
            if (opponent != null)
            {
                Player player = lobby.GetUser(Context.ConnectionId);
                lobby.RemoveUser(Context.ConnectionId);
                lobby.RemoveUser(opponent.ConnectionId);
                Battle battle = new Battle(opponent, player);
                lock (obj)
                {
                    battleList.Add(battle);
                }
                Clients.Client(battle.Player1.ConnectionId).FoundOpponent(battle.Position1, battle.Position2, (int)battle.Flip1, (int)battle.Flip2);
                Clients.Client(battle.Player2.ConnectionId).FoundOpponent(battle.Position2, battle.Position1, (int)battle.Flip2, (int)battle.Flip1);
            }
        }

        //public override Task OnConnected()
        //{
        //    Console.WriteLine("Connected");
        //    lobby.AddUser(Context.ConnectionId);
        //    Console.WriteLine(Context.ConnectionId);
        //    return base.OnConnected();
        //}
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    Console.WriteLine("Disconnected");
        //    lobby.RemoveUser(Context.ConnectionId);
        //    Console.WriteLine(Context.ConnectionId);
        //    return base.OnDisconnected(stopCalled);
        //}
        //public override Task OnReconnected()
        //{
        //    Console.WriteLine("Reconnected");
        //    lobby.AddUser(Context.ConnectionId);
        //    Console.WriteLine(Context.ConnectionId);
        //    return base.OnReconnected();
        //}
    }
}
