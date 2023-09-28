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
        public void AddToLobby(string className)
        {
            Console.WriteLine("Connected");
            lobby.AddUser(Context.ConnectionId, className);
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
                Clients.Client(battle.Player1.ConnectionId).FoundOpponent(battle.Position1, battle.Position2, (int)battle.Flip1, (int)battle.Flip2, battle.Player1.ClassName, battle.Player2.ClassName);
                Clients.Client(battle.Player2.ConnectionId).FoundOpponent(battle.Position2, battle.Position1, (int)battle.Flip2, (int)battle.Flip1, battle.Player2.ClassName, battle.Player1.ClassName);
            }
        }

        public void AbilityUsed(string name)
        {
            Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
            if (battle.Player1.ConnectionId == Context.ConnectionId)
            {
                Clients.Client(battle.Player2.ConnectionId).AbilityUsed(name);
            }
            else
            {
                Clients.Client(battle.Player1.ConnectionId).AbilityUsed(name);
            }
        }

        //public void LeaveBattle()
        //{
        //    Battle battle = battleList.Find((b) => b.Player1.ConnectionId == Context.ConnectionId || b.Player2.ConnectionId == Context.ConnectionId);
        //    Clients.Caller.BattleLeft();
        //    if (battle.Player1.ConnectionId != Context.ConnectionId)
        //    {
        //        Clients.Client(battle.Player1.ConnectionId).BackToLoading();
        //        lobby.AddUser(battle.Player1.ConnectionId, battle.Player1.ClassName);
        //    }
        //    else
        //    {
        //        Clients.Client(battle.Player2.ConnectionId).BackToLoading();
        //        lobby.AddUser(battle.Player2.ConnectionId, battle.Player2.ClassName);
        //    }
        //}
    }
}
